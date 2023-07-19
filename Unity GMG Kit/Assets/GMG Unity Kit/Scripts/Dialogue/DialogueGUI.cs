using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class DialogueGUI : MonoBehaviour
{
    public GameObject DialogueCanvas;                            // Reference to canvas used to display/hide the dialogue screen.    
    public GameObject dialogueOptionPrefab;                      // Prefab of an options button used when displaying options to player.      
    public Text characterName;                                   // Text object for storing name of npc you're speaking too.
    public Text textBox;                                         // Reference to text box for main dialogue.
    public Text optionDialogue;                                  // A smaller text box for displaying dialogue when options are displayed.
    public RectTransform optionsPanel;                           // Reference to options panel for disabling/enabling options.
    public RectTransform continuePrompt;                         // Reference to transform of continue promt for enable/disable and sine movement. 
    public RectTransform optionPointer;                          // Access to optionpointer for highlighting options that are selected.  
    public Image characterPortrait;                              // Reference to sprite for character portrait. Image should be passed in as string resource location.
    public AudioClip textSound;
    AudioSource audioSrc;                                		 // Audiosource for playing text scrolling sound. Sound should be assigned in editor.
    public float characterTimeDelay = 0.01f;                     // The delay in seconds for each character to be drawn.

    string currentText = string.Empty;                           // The current string displayed to player in dialogue box.
    string targetText = string.Empty;                            // The full string that should be displayed to the player.
    List<string> richTextStack = new List<string>();             // Stack storing previously encountered unfinished rich text.                  

    bool showDialogueBox = false;                                // True when the dialogue box is currently active on screen.                                                
    bool isBranchedText = false;                                 // True if the current dialogue phase contains options for the player to choose from.
    float textFrames = 0f;                                       // Used alongside characterDelay to keep track of time to display characters
    string[] branchedTextChoices;                                // Array containing choices for player to choose from in a dialogue phase.
    List<GameObject> optionGameObjects = new List<GameObject>(); // Reference to the dialogue option buttons for testing

    int currentBranchChoice = 0;                                 // The currently selected option in the dialogue menu if any.
    StringBuilder strBuilder = new StringBuilder();              // Used to created dialogue on the fly for typewrited style effect.
    bool isTextNode = false;                                     // True when Dialoguer passes a text phase down to be handled.
    int textCharCount = 0;                                       // Counter used to keep track of current position in targetText.
    bool reachedEnd = false;                                     // True when textCharCount hits the end of targetText.

    bool showDialogue = false;
    public bool isWaiting { get; set; }
    bool hasShownOptions = false;

    float origContinuePosX;                                     // The original x position of the continue promt pointer, used to reset it on disabling.

    //Setup
        void SetupDialogueEvents()
        {
            Dialoguer.events.onStarted += OnStart;
            Dialoguer.events.onEnded += OnEnd;
            Dialoguer.events.onInstantlyEnded += OnInstantEnd;
            Dialoguer.events.onTextPhase += OnTextPhase;
            Dialoguer.events.onWindowClose += OnWindowClose;
            Dialoguer.events.onMessageEvent += OnMessageEvent;
            Dialoguer.events.onWaitStart += OnWaitStart;
            Dialoguer.events.onWaitComplete += OnWaitEnd;
        }

        void OnDestroy()
        {
        	if (Dialoguer.events == null) return;

        	Dialoguer.events.onStarted -= OnStart;
            Dialoguer.events.onEnded -= OnEnd;
            Dialoguer.events.onInstantlyEnded -= OnInstantEnd;
            Dialoguer.events.onTextPhase -= OnTextPhase;
            Dialoguer.events.onWindowClose -= OnWindowClose;
            Dialoguer.events.onMessageEvent -= OnMessageEvent;
            Dialoguer.events.onWaitStart -= OnWaitStart;
            Dialoguer.events.onWaitComplete -= OnWaitEnd;
        }

    //Monobehaviour
        InputManager inputMgr;
        DialogueManager dialogueMgr;
        public InputManager.InputButton continueButton = InputManager.InputButton.Space;
        public InputManager.InputButton escapeButton = InputManager.InputButton.Escape;
        void Start()
        {
            inputMgr = GameManager.Inst().InputManager();     
            dialogueMgr = GameManager.Inst().DialogueManager();
            Dialoguer.Initialize();
            SetupDialogueEvents();
            origContinuePosX = continuePrompt.position.x;
            audioSrc = gameObject.AddComponent<AudioSource>();
            audioSrc.clip = textSound;
        }

        void Update()
        {
            if (!showDialogueBox) return;

            if (isTextNode)
            {
                //Check for Inputs
                if (!isBranchedText) TextNodeUpdate();
                else BranchedNodeUpdate();
            }

            //GUI pointer animations
            if (continuePrompt.gameObject.activeSelf)
                continuePrompt.position += new Vector3(Mathf.Sin(Time.unscaledTime * 6), 0, 0);

            if (optionPointer.gameObject.activeSelf)
                optionPointer.position += new Vector3(Mathf.Sin(Time.unscaledTime * 10) * 0.5f, 0, 0);
        }
        
        void TextNodeUpdate()
        {
            //Update TextBox
            if (!reachedEnd)
            {
                if (textFrames < characterTimeDelay)
                {
                    textFrames += Time.unscaledDeltaTime;
                }
                else
                {
                    CalculateNextText();
                    textBox.text = currentText;
                    textFrames = 0f;
                }
            }
            else
            {
                continuePrompt.gameObject.SetActive(true);
            }

            //Input Test
            if (inputMgr.GetKeyDown(continueButton))
            {
                if (currentText == targetText)
                {
                    ContinueDialogue();
                }
                else
                {
                    textBox.text = currentText = targetText;
                    reachedEnd = true;
                }
            }            
        }

        void BranchedNodeUpdate()
        {
            if (hasShownOptions)
            {
                OnOptionChange(0);
                hasShownOptions = false;
            }

            if (!reachedEnd)
            {
                if (textFrames < characterTimeDelay)
                {
                    textFrames += Time.unscaledDeltaTime;
                }
                else
                {
                    CalculateNextText();
                    optionDialogue.text = currentText;
                    textFrames = 0f;
                }

                if (inputMgr.GetKeyDown(continueButton))
                {
                    optionDialogue.text = currentText = targetText;
                    reachedEnd = true;
                }

                if (reachedEnd)
                {
                    optionsPanel.gameObject.SetActive(true);
                    optionPointer.gameObject.SetActive(true);
                    hasShownOptions = true;
                }
            }
            else
            {
                if (inputMgr.GetDownFirstPress())
                {
                    currentBranchChoice = (int)Mathf.Repeat(currentBranchChoice + 1, branchedTextChoices.Length);
                    OnOptionChange(currentBranchChoice);
                }

                if (inputMgr.GetUpFirstPress())
                {
                    currentBranchChoice = (int)Mathf.Repeat(currentBranchChoice - 1, branchedTextChoices.Length);
                    OnOptionChange(currentBranchChoice);
                }

                if (inputMgr.GetKeyDown(continueButton))
                {
                    ContinueDialogue(currentBranchChoice);
                }
            }
        }

    public void OnContinueClick()
    {

        if (isTextNode)
        {
            if (!isBranchedText)
            {
                if (!reachedEnd)
                {
                    if (currentText == targetText)
                    {
                        ContinueDialogue();
                    }
                    else
                    {
                        textBox.text = currentText = targetText;
                        reachedEnd = true;
                    }
                }
                else
                {
                    ContinueDialogue();
                }
            }
            else
            {
                if (hasShownOptions)
                {
                    OnOptionChange(0);
                    hasShownOptions = false;
                }

                if (!reachedEnd)
                {
                    optionDialogue.text = currentText = targetText;
                    reachedEnd = true;
                }

                if (reachedEnd)
                {
                    optionsPanel.gameObject.SetActive(true);
                    optionPointer.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            if (!isWaiting)
            {
                ContinueDialogue();
            }
        }
    }


    //Continue/Next
        public void ContinueToNextNode()
        {
            ContinueDialogue();
        }

        public void PlayDialogue(DialoguerDialogues dialogue)
        {
            Dialoguer.StartDialogue(dialogue);
        }

        void ContinueDialogue()
        {
            if (isTextNode) OnTextPhaseEnd();
            Dialoguer.ContinueDialogue();
        }

        void ContinueDialogue(int option)
        {
            if (isTextNode) OnTextPhaseEnd();
            Dialoguer.ContinueDialogue(option);
        }

    // Dialoguer events
        public void OnStart()
        {
            dialogueMgr.OnBeginDialogue();
            gameObject.SetActive(true);
        }

        public void OnWindowClose()
        {
            showDialogueBox = false;
            dialogueMgr.OnEndDialogue();
        }

        public void OnMessageEvent(string message, string metadata)
        {
            dialogueMgr.OnSendMessage(message, metadata);
        }

        public void OnEnd()
        {
            if (isTextNode) OnTextPhaseEnd();
            showDialogueBox = false;
        }

        public void OnInstantEnd()
        {
            showDialogueBox = false;
            if (isTextNode) OnTextPhaseEnd();
        }

        public void OnWaitStart()
        {
            isWaiting = true;
        }

        public void OnWaitEnd()
        {
            isWaiting = false;
        }

    //Input
        public void OnOptionSelect(GameObject button)
        {
            ContinueDialogue(System.Convert.ToInt32(button.name));
        }

        public void OnOptionEnter(GameObject data)
        {
            OnOptionChange(System.Convert.ToInt32(data.name));
        }

        void OnOptionChange(int newOption)
        {
            float buttonY = optionGameObjects[newOption].transform.position.y;
            optionPointer.position = new Vector3(optionPointer.position.x, buttonY, optionPointer.position.z);
        }

    //TextPhase
    public void SetPortrait(string portraitPath)
    {
        if (portraitPath == "") return;
        Sprite temp = null;

        if (temp == null) temp = Resources.Load("Portraits/" + portraitPath, typeof(Sprite)) as Sprite;
        if (temp == null) return;

        characterPortrait.sprite = temp;
        characterPortrait.gameObject.SetActive(true);
    }

    public void OnTextPhase(DialoguerTextData data)
    {
        gameObject.SetActive(true);
        isTextNode = true;

        // Init dialogue data
        characterName.text = data.name;
        targetText = data.text;

        // Loading any new dialogue options
        isBranchedText = (data.windowType == DialoguerTextPhaseType.BranchedText);
        if (isBranchedText)
        {
            optionsPanel.gameObject.SetActive(false);
            branchedTextChoices = data.choices;
            for (int i = 0; i < branchedTextChoices.Length; ++i)
            {
                // Setup button
                GameObject option = GameObject.Instantiate(dialogueOptionPrefab) as GameObject;
                option.GetComponent<Text>().text = branchedTextChoices[i];
                option.transform.SetParent(optionsPanel);
                option.name = i.ToString();
                optionGameObjects.Add(option);
                option.GetComponent<Button>().onClick.AddListener(delegate { OnOptionSelect(option); });

                // Set up event on hover callback
                EventTrigger trigger = option.GetComponent<EventTrigger>();
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerEnter;
                entry.callback = new EventTrigger.TriggerEvent();
                entry.callback.AddListener(delegate { OnOptionEnter(option); });
                trigger.triggers.Add(entry);

            }

            float buttonY = optionGameObjects[0].transform.position.y;
            optionPointer.position = new Vector3(optionPointer.position.x, buttonY, optionPointer.position.z);
            currentBranchChoice = 0;

            //optionPointer.gameObject.SetActive(false);
        }
        else
        {
            Vector3 vec = continuePrompt.transform.position;
            vec.x = origContinuePosX;
            continuePrompt.transform.position = vec;

            continuePrompt.gameObject.SetActive(false);
        }

        // Set portrait if any
        SetPortrait(data.portrait);

        showDialogueBox = true;
    }

    void OnTextPhaseEnd()
    {
        characterName.text = string.Empty;
        currentText = targetText = textBox.text = optionDialogue.text = string.Empty;
        ClearOptions();
        characterPortrait.gameObject.SetActive(false);
        strBuilder.Remove(0, strBuilder.Length);
        reachedEnd = false;
        richTextStack.Clear();

        isTextNode = isBranchedText = false;
        currentBranchChoice = 0;
        textCharCount = 0;
        textFrames = 0f;

        Vector3 vec = continuePrompt.transform.position;
        vec.x = origContinuePosX;
        continuePrompt.transform.position = vec;

        continuePrompt.gameObject.SetActive(false);
        optionPointer.gameObject.SetActive(false);
    }

    public void ClearOptions()
    {
        if (optionGameObjects.Count > 0)
        {
            for (int i = 0; i < optionGameObjects.Count; ++i)
            {
                GameObject.Destroy(optionGameObjects[i]);
            }
            optionGameObjects.Clear();
        }
        optionsPanel.gameObject.SetActive(false);
    }

    private string GetEndOfRichNode(string richNode)
    {
        if (richNode.Contains("<i")) return "</i>";
        if (richNode.Contains("<b")) return "</b>";
        if (richNode.Contains("<size")) return "</size>";
        if (richNode.Contains("<color")) return "</color>";

        return "";
    }

    private void CalculateNextText()
    {
        if (targetText.Length != 0)
        {
            char nextChar = targetText[textCharCount];
            int initTextCharCount = textCharCount;

            while (nextChar == '<')
            {
                char lastChar = ' ';
                while (lastChar != '>')
                {
                    textCharCount++;
                    lastChar = targetText[textCharCount];
                }
                string richNode = targetText.Substring(initTextCharCount, textCharCount - initTextCharCount + 1);

                if (targetText[initTextCharCount + 1] != '/') richTextStack.Add(richNode);
                else richTextStack.RemoveAt(richTextStack.Count - 1);

                textCharCount++;
                nextChar = targetText[textCharCount];
            }

            strBuilder.Append(targetText.Substring(initTextCharCount, textCharCount - initTextCharCount + 1));
            currentText = strBuilder.ToString();

            for (int i = richTextStack.Count - 1; i >= 0; i--)
            {
                currentText += GetEndOfRichNode(richTextStack[i]);
            }
        }

        textCharCount++;
        if (!audioSrc.isPlaying) audioSrc.Play();
        if (currentText == targetText)
            reachedEnd = true;
    }

}
