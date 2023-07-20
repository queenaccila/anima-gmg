using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCombatManager : MonoBehaviour {

	public GameObject PrefabContainer;
	public Camera PrefabCamera;
	public GameObject PrefabCameraGO;
	private GameObject MainCamera;
	GameManager gameMgr;

	//ATTACK MEMBERS
	public AudioClip fightSound;
	AudioSource audioSrc;

	float shaketimer = 0f;
	Vector3 initialCameraPosition;
	public ParticleSystem hitburstParticles;
	float pausetimer = 0f;

	//GAME STATS
	private enum scenestate : int {
		none,
		fightbegin,
		playerchooseattack,
		playerchoosetarget,
		playerattack,
		playershowattackresults,
		playertryrunaway,
		enemyattack,
		enemyshowattackresults,
		fightoverdead,
		fightoverwin,
		fightoverranaway
	};
	scenestate state = scenestate.none;

	//micro states used for character attack
	private enum combatstate : int {
		runto,
		attack,
		runback,
		done,
	};
	combatstate attackstate = combatstate.runto;

	//PLAYERS AND ENEMIES
	public Transform[] turnPlayerSlots = new Transform[4];
	public Transform[] turnEnemySlots = new Transform[4];
	public Transform enemyHolder;
	public Transform playerHolder;
	private float enemyHolderInitialY = 0;
	private float playerHolderInitialY = 0;
	private List<GameObject> turnPlayers = new List<GameObject>();
	private List<GameObject> turnEnemies = new List<GameObject>();

	public GameObject[] turnPlayerPrefab;
	public GameObject[] turnEnemyPrefab;

	public int minNumEnemies = 2;
	public int maxNumEnemies = 4;

	int currentAttackingPlayer = 0;
	int currentAttackNum = 0;
	int currentEnemyTarget = 0;
	int currentAttackingEnemy = 0;
	int currentPlayerTarget = 0;

	//HUD
	public Transform attackDialog;
	public TextMesh attackDialogText;

	//INPUT
	bool waitingForInput = false;


	// Use this for initialization
	void Start () {
		initialCameraPosition = PrefabCamera.transform.position;

		PrefabContainer.transform.position = new Vector3 (0, 1000f, 0);
		//PrefabContainer.SetActive (false);

		MainCamera = GameObject.Find ("Main Camera");
		if (!MainCamera) {
			print ("can't find camera with name Main Camera, REQUIRED FOR TurnCombat TO WORK");
		}

		PrefabCameraGO.SetActive (false);

		gameMgr = GameManager.Inst();

		enemyHolderInitialY = enemyHolder.transform.localPosition.y;
		playerHolderInitialY = playerHolder.transform.localPosition.y;
		//InitFight (); //use for testing

		audioSrc = gameObject.AddComponent<AudioSource>();
		audioSrc.clip = fightSound;
		audioSrc.loop = false;
		audioSrc.playOnAwake = false;
	}

	public void StartTurnCombat(GameObject[] turnEnemiesForThisFightPrefab)
	{
		gameMgr.PauseGame ();

		audioSrc.Stop ();
		hitburstParticles.gameObject.SetActive(false);

		currentAttackingPlayer = 0;
		currentAttackNum = 0;
		currentEnemyTarget = 0;
		currentAttackingEnemy = 0;
		currentPlayerTarget = 0;
		waitingForInput = false;

		MainCamera.SetActive (false);
		PrefabCameraGO.SetActive (true);
		PrefabContainer.SetActive (true);
		InitFight (turnEnemiesForThisFightPrefab);
		state = scenestate.fightbegin;
	}

	void EndTurnCombat()
	{
		gameMgr.UnpauseGame ();

		MainCamera.SetActive (true);
		PrefabCameraGO.SetActive (false);
		PrefabContainer.SetActive (false);

		for (int x = 0; x < turnEnemies.Count; x++) {
			Destroy (turnEnemies [x].gameObject);
		}
		for (int x = 0; x < turnPlayers.Count; x++) {
			Destroy (turnPlayers [x].gameObject);
		}
	}

	public void InitFight(GameObject[] turnEnemiesForThisFightPrefab)
	{
		//INIT PLAYERS
		turnPlayers.Clear();

		int playersinfight = 0;
		int enemiesinfight = 0;

		enemyHolder.transform.localPosition = new Vector2 (enemyHolder.transform.localPosition.x,enemyHolderInitialY);
		playerHolder.transform.localPosition = new Vector2 (playerHolder.transform.localPosition.x,playerHolderInitialY);

		if (!TurnCombatGlobals.inited) {
			//making the fighters for the first time, use HP and SP in the prefab
			TurnCombatGlobals.inited = true;
			for (int x = 0; x < TurnCombatGlobals.turnFighterCurrentHitPoints.Length; x++) {

				GameObject player = Instantiate (turnPlayerPrefab [x], playerHolder);
				player.transform.localPosition = turnPlayerSlots [x].localPosition;
				turnPlayers.Add (player);
				turnPlayers [x].GetComponent<TurnFighter> ().currentHitPoints = turnPlayers [x].GetComponent<TurnFighter> ().maxHitPoints;
				turnPlayers [x].GetComponent<TurnFighter> ().currentSpecialPoints = turnPlayers [x].GetComponent<TurnFighter> ().maxSpecialPoints;

				turnPlayers [x].GetComponent<TurnFighter> ().HUDText.text = turnPlayers [x].GetComponent<TurnFighter> ().Name + "\n" +
				"HP " + turnPlayers [x].GetComponent<TurnFighter> ().currentHitPoints + "\n";

				if(!turnPlayers [x].GetComponent<TurnFighter> ().IgnoreSpecialPoints())
					turnPlayers [x].GetComponent<TurnFighter> ().HUDText.text+="SP " + turnPlayers [x].GetComponent<TurnFighter> ().currentSpecialPoints; 

				if (turnPlayers [x].GetComponent<TurnFighter> ().currentHitPoints <= 0)
					turnPlayers [x].SetActive (false);
			}
		}
		else {
			//used stored values for HP and SP, we've already been in a fight
			for (int x = 0; x < TurnCombatGlobals.turnFighterCurrentHitPoints.Length; x++) {
	
				GameObject player = Instantiate (turnPlayerPrefab [x], playerHolder);
				player.transform.localPosition = turnPlayerSlots [x].localPosition;
				turnPlayers.Add (player);
				turnPlayers [x].GetComponent<TurnFighter> ().currentHitPoints = TurnCombatGlobals.turnFighterCurrentHitPoints[x];
				turnPlayers [x].GetComponent<TurnFighter> ().currentSpecialPoints = TurnCombatGlobals.turnFighterCurrentSpecialPoints[x];

				turnPlayers [x].GetComponent<TurnFighter> ().HUDText.text = turnPlayers [x].GetComponent<TurnFighter> ().Name + "\n" +
				"HP " + turnPlayers [x].GetComponent<TurnFighter> ().currentHitPoints + "\n";

				if(!turnPlayers [x].GetComponent<TurnFighter> ().IgnoreSpecialPoints())
					turnPlayers [x].GetComponent<TurnFighter> ().HUDText.text+="SP " + turnPlayers [x].GetComponent<TurnFighter> ().currentSpecialPoints; 

				if (turnPlayers [x].GetComponent<TurnFighter> ().currentHitPoints <= 0)
					turnPlayers [x].SetActive (false);
			}
		}
			
		//INIT ENEMIES
		//init 2-4 enemies from the list of prefabs
		turnEnemies.Clear();
		bool randomEnemies = true;
		if (turnEnemiesForThisFightPrefab.Length > 0) {

			for(int x = 0; x < turnEnemiesForThisFightPrefab.Length; x++)
			{
				if(turnEnemiesForThisFightPrefab[x])
					randomEnemies = false;
			}
		}


		int numenemies = 0;
		if (randomEnemies) {
			if (minNumEnemies < 1)
				minNumEnemies = 1;
			if (maxNumEnemies > 4)
				maxNumEnemies = 4;
			numenemies = Random.Range (minNumEnemies, (maxNumEnemies + 1));
		}
		else {
			numenemies = turnEnemiesForThisFightPrefab.Length;
		}


		for (int x = 0; x < numenemies; x++) {
			int enemynum = Random.Range (0, turnEnemyPrefab.Length);
			GameObject enemy;

			if (randomEnemies)
				enemy = Instantiate (turnEnemyPrefab [enemynum], enemyHolder);
			else
				enemy = Instantiate (turnEnemiesForThisFightPrefab [x], enemyHolder);
			
			enemy.transform.localPosition = turnEnemySlots [x].localPosition;
			turnEnemies.Add (enemy);

			turnEnemies [x].GetComponent<TurnFighter> ().HUDText.text = turnEnemies [x].GetComponent<TurnFighter> ().Name + "\n" +
				"HP " + turnEnemies [x].GetComponent<TurnFighter> ().currentHitPoints;
		}
	}

	void InitialFightDescription()
	{
		string desc;
		desc = "You encounter...\n\n";
		for (int x = 0; x < turnEnemies.Count; x++) {
			if (turnEnemies [x].GetComponent<TurnFighter> ().currentHitPoints > 0)
				desc += turnEnemies [x].GetComponent<TurnFighter> ().Name + "\n";
		}
		desc += "\n";
		int rand = Random.Range (0, 5);
		if(rand==0)
			desc += "They look angry...\n\n";
		else if(rand==1)
			desc += "They look grumpy...\n\n";
		else if(rand==2)
			desc += "They look hungry...\n\n";
		else if(rand==3)
			desc += "They look offended...\n\n";
		else
			desc += "They look huffy...\n\n";

		desc += "<SPACE> to continue\n";

		attackDialogText.text = desc;
		waitingForInput = true;
	}

	void SaveStats()
	{
		//save player HP and SP for next battle
		for (int x = 0; x < turnPlayers.Count; x++) {
			TurnCombatGlobals.turnFighterCurrentHitPoints[x] = turnPlayers [x].GetComponent<TurnFighter> ().currentHitPoints;
			TurnCombatGlobals.turnFighterCurrentSpecialPoints[x] = turnPlayers [x].GetComponent<TurnFighter> ().currentSpecialPoints;
		}
	}

	void FightOverRanAway()
	{
		SaveStats ();

		string desc;
		desc = "The foul beasts were too\nfrightening. You ran away.\n\n";

		desc += "<SPACE> to continue\n";

		attackDialogText.text = desc;
		waitingForInput = true;
	}

	void FightOverLost()
	{
		string desc;
		desc = "You have been defeated\n\n";

		desc += "<SPACE> to restart\n";

		attackDialogText.text = desc;
		waitingForInput = true;
	}

	void FightOverWin()
	{
		SaveStats ();

		string desc;
		desc = "The foul beasts have been\ndefeated. You have won.\n\n";

		desc += "<SPACE> to continue\n";

		attackDialogText.text = desc;
		waitingForInput = true;
	}
		
	void PlayerAttackDescription()
	{
		for (int x = 0; x < turnEnemies.Count; x++) {
			turnEnemies [x].GetComponent<TurnFighter> ().Highlight.gameObject.SetActive (false);
		}
		for (int x = 0; x < turnPlayers.Count; x++) {
			turnPlayers [x].GetComponent<TurnFighter> ().Highlight.gameObject.SetActive (false);
		}
		turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter> ().Highlight.gameObject.SetActive (true);

		//is this player alive?
		if (turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter> ().currentHitPoints > 0) {
			string desc;
			desc = turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter> ().Name + "\n\n";
			desc += "Choose attack...\n";
			for (int x = 0; x < turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter> ().attackName.Length; x++) {
				desc += "<" + (x+1) + "> " + turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter> ().attackName[x] + "\n";

				if(turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter> ().attackSpecialPoints[x]>0)
				{
					desc += "    (uses " + turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter> ().attackSpecialPoints[x] + " SP)\n";
					if (turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter> ().currentSpecialPoints >= turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter> ().attackSpecialPoints [x]) {
					}
					else {
						desc += "    NOT ENOUGH SP!)\n";
					}
				}
			}
			desc += "\nOr...\n<4> Run Away";
			attackDialogText.text = desc;
			waitingForInput = true;
		}
	}

	void PlayerTargetDescription()
	{
		//is this player alive?
		if (turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter> ().currentHitPoints > 0) {
			string desc;
			desc = turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter> ().Name + "\n\n";
			desc += "Choose target...\n";
			for (int x = 0; x < turnEnemies.Count; x++) {
				if (turnEnemies [x].GetComponent<TurnFighter> ().currentHitPoints > 0)
					desc += "<" + (x+1) + "> " + turnEnemies [x].GetComponent<TurnFighter> ().Name + "\n";
			}
				
			attackDialogText.text = desc;
			waitingForInput = true;
		}

	}

	void PerformAttack ()
	{
		string desc;
		desc = turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter> ().Name + " attacks ";
		desc += turnEnemies [currentEnemyTarget].GetComponent<TurnFighter> ().Name + "\nwith ";
		desc += turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter> ().attackName[currentAttackNum] + "\n\n";

		desc += turnEnemies [currentEnemyTarget].GetComponent<TurnFighter> ().Name + " suffers\n";
		int damagemiddle = turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter> ().attackStrength [currentAttackNum];
		int damage = Random.Range (damagemiddle - 1, damagemiddle + 2);
		desc += damage + " damage\n\n";

		turnEnemies [currentEnemyTarget].GetComponent<TurnFighter> ().currentHitPoints -= damage;
		if(!IsEnemyAlive(currentEnemyTarget))
		{
			desc += turnEnemies [currentEnemyTarget].GetComponent<TurnFighter> ().Name + " has been defeated\n\n";
		}

		desc += "<SPACE> to continue\n";

		attackDialogText.text = desc;
		waitingForInput = true;
	}

	bool IsEnemyAlive(int enemynum)
	{
		return (turnEnemies [enemynum].GetComponent<TurnFighter> ().currentHitPoints > 0);
	}
	bool AreAllEnemiesDefeated()
	{
		bool enemyalive = false;
		for (int x = 0; x < turnEnemies.Count; x++) {
			enemyalive = IsEnemyAlive (x);
			if (enemyalive)
				return false;
		}
		return true;
	}

	void ChooseNextAttackingCharacter()
	{
		bool isDead = true;
		bool MorePlayers = true;

		do {
			currentAttackingPlayer++;
			if (currentAttackingPlayer < turnPlayers.Count) {
				isDead = (turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter> ().currentHitPoints <= 0);
			}
			else {
				MorePlayers = false;
			}
			
		} while(MorePlayers && isDead);


		if (!MorePlayers) {

			waitingForInput = false;
			currentAttackingEnemy = -1;
			ChooseNextAttackingEnemy ();
		}
		else {
			waitingForInput = false;
			state = scenestate.playerchooseattack;
		}
	}

	void TryRunAway()
	{
		int num = Random.Range(0,3);

		if (num == 0) {
			state = scenestate.fightoverranaway;
			waitingForInput = false;
		}
		else {
			string desc;
			desc = "You try to run away\nbut are blocked!\n\n";

			desc += "<SPACE> to continue\n";

			attackDialogText.text = desc;
			waitingForInput = true;
		}

	}

		
	void EnemyPerformAttack ()
	{
		for (int x = 0; x < turnPlayers.Count; x++) {
			turnPlayers [x].GetComponent<TurnFighter> ().Highlight.gameObject.SetActive (false);
		}
		for (int x = 0; x < turnEnemies.Count; x++) {
			turnEnemies [x].GetComponent<TurnFighter> ().Highlight.gameObject.SetActive (false);
		}
		turnEnemies [currentAttackingEnemy].GetComponent<TurnFighter> ().Highlight.gameObject.SetActive (true);

		string desc;
		desc = turnEnemies [currentAttackingEnemy].GetComponent<TurnFighter> ().Name + " attacks ";

		//pick a player
		int playernum = Random.Range(0,turnPlayers.Count);
		bool playerdead = true;
		do{
			playernum++;
			if(playernum>=turnPlayers.Count)
				playernum = 0;
			if(IsPlayerAlive(playernum))
				playerdead = false;
		}
		while(playerdead);
		currentPlayerTarget = playernum;
		desc += turnPlayers [currentPlayerTarget].GetComponent<TurnFighter> ().Name + "\nwith ";

		//pick an attack
		int attacknum = Random.Range(0,turnEnemies [currentAttackingEnemy].GetComponent<TurnFighter> ().attackName.Length);
		desc += turnEnemies [currentAttackingEnemy].GetComponent<TurnFighter> ().attackName[attacknum] + "\n\n";

		desc += turnPlayers [currentPlayerTarget].GetComponent<TurnFighter> ().Name + " suffers\n";
		int damagemiddle = turnEnemies [currentAttackingEnemy].GetComponent<TurnFighter> ().attackStrength [attacknum];
		int damage = Random.Range (damagemiddle - 1, damagemiddle + 2);

		desc += damage + " damage\n\n";

		turnPlayers [currentPlayerTarget].GetComponent<TurnFighter> ().currentHitPoints -= damage;
		if (!IsPlayerAlive (currentPlayerTarget)) {
			desc += turnPlayers [currentPlayerTarget].GetComponent<TurnFighter> ().Name + " has been defeated\n\n";
		}
			
		desc += "<SPACE> to continue\n";

		attackDialogText.text = desc;
		waitingForInput = true;
	}

	bool IsPlayerAlive(int playernum)
	{
		return (turnPlayers [playernum].GetComponent<TurnFighter> ().currentHitPoints > 0);
	}

	bool AreAllPlayersDefeated()
	{
		bool playeralive = false;
		for (int x = 0; x < turnPlayers.Count; x++) {
			playeralive = IsPlayerAlive (x);
			if (playeralive)
				return false;
		}
		return true;
	}

	void ChooseNextAttackingEnemy()
	{

		bool isDead = true;
		bool MoreEnemies = true;

		do {
			currentAttackingEnemy++;
			if (currentAttackingEnemy < turnEnemies.Count) {
				isDead = (turnEnemies [currentAttackingEnemy].GetComponent<TurnFighter> ().currentHitPoints <= 0);
			}
			else {
				MoreEnemies = false;
			}

		} while(MoreEnemies && isDead);
			
		if (!MoreEnemies) {
			waitingForInput = false;
			currentAttackingPlayer = -1;
			ChooseNextAttackingCharacter ();
		}
		else {
			waitingForInput = false;
			state = scenestate.enemyattack;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//print (state);
		HandleCameraShake ();
		//Time.unscaledDeltaTime
		if (pausetimer > 0) {
			pausetimer -= Time.unscaledDeltaTime;//Time.deltaTime;
			return;
		}

		if(state == scenestate.fightbegin)
		{
			if (!waitingForInput) {
				InitialFightDescription ();
			}
			else {
				if (Input.GetKeyDown (KeyCode.Space)) {
					waitingForInput = false;
					state = scenestate.playerchooseattack;
				}
			}

		}
		else if(state == scenestate.playerchooseattack)
		{
			if (!waitingForInput) {
				PlayerAttackDescription ();
			}
			else {
				if (Input.GetKeyDown (KeyCode.Alpha1) && (turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter> ().currentSpecialPoints >= turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter> ().attackSpecialPoints [0])){
					currentAttackNum = 0;
					waitingForInput = false;
					state = scenestate.playerchoosetarget;
				}
				else if (Input.GetKeyDown (KeyCode.Alpha2) && (turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter> ().currentSpecialPoints >= turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter> ().attackSpecialPoints [1])){
					currentAttackNum = 1;
					waitingForInput = false;
					state = scenestate.playerchoosetarget;
				}
				else if (Input.GetKeyDown (KeyCode.Alpha3) && (turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter> ().currentSpecialPoints >= turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter> ().attackSpecialPoints [2])){
					currentAttackNum = 2;
					waitingForInput = false;
					state = scenestate.playerchoosetarget;
				}
				else if (Input.GetKeyDown (KeyCode.Alpha4)){
					currentAttackNum = 2;
					waitingForInput = false;
					state = scenestate.playertryrunaway;
				}
			}

		}
		else if(state == scenestate.playerchoosetarget)
		{
			if (!waitingForInput) {
				PlayerTargetDescription ();
			}
			else {
				if (Input.GetKeyDown (KeyCode.Alpha1) && turnEnemies.Count>=1) {
					if (turnEnemies [0].GetComponent<TurnFighter> ().currentHitPoints > 0) {
						currentEnemyTarget = 0;
						waitingForInput = false;
						state = scenestate.playerattack;
					}
				}
				else if (Input.GetKeyDown (KeyCode.Alpha2) && turnEnemies.Count>=2) {
					if (turnEnemies [1].GetComponent<TurnFighter> ().currentHitPoints > 0) {
						currentEnemyTarget = 1;
						waitingForInput = false;
						state = scenestate.playerattack;
					}
				}
				else if (Input.GetKeyDown (KeyCode.Alpha3) && turnEnemies.Count>=3) {
					if (turnEnemies [2].GetComponent<TurnFighter> ().currentHitPoints > 0) {
						currentEnemyTarget = 2;
						waitingForInput = false;
						state = scenestate.playerattack;
					}
				}
				else if (Input.GetKeyDown (KeyCode.Alpha4) && turnEnemies.Count>=4) {
					if (turnEnemies [3].GetComponent<TurnFighter> ().currentHitPoints > 0) {
						currentEnemyTarget = 3;
						waitingForInput = false;
						state = scenestate.playerattack;
					}
				}
			}
		}
		else if(state == scenestate.playertryrunaway)
		{
			if (!waitingForInput) {
				TryRunAway ();
			}
			else {
				if (Input.GetKeyDown (KeyCode.Space)) {
					//anyone else still waiting to attack?
					ChooseNextAttackingCharacter();
				}
			}
		}
		else if(state == scenestate.playerattack)
		{
			if (!waitingForInput) {
				HideAttackDialog ();
				PerformAttack ();
				attackstate = combatstate.runto;
			}
			else {
				//run to enemy
				if (attackstate == combatstate.runto) {
					Vector3 currpos = turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter>().CharacterSprite.position;
					Vector3 target = turnEnemies[currentEnemyTarget].transform.position;
					target.x -= 2f;
					turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter>().CharacterSprite.position = Vector3.MoveTowards (currpos, target, 20f*Time.unscaledDeltaTime);
					if(turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter>().CharacterSprite.position == target)
						attackstate = combatstate.attack;
				}
				else if (attackstate == combatstate.attack) {
					if (!audioSrc.isPlaying) audioSrc.Play();
					//AudioSource.PlayClipAtPoint (fightSound, PrefabCamera.transform.position);
					shaketimer = .5f;
					pausetimer = 1f;
					hitburstParticles.transform.position = new Vector3 (turnEnemies [currentEnemyTarget].transform.position.x, turnEnemies [currentEnemyTarget].transform.position.y, hitburstParticles.transform.position.z);
					//hitburstParticles.Play ();
					hitburstParticles.gameObject.SetActive(false);
					hitburstParticles.gameObject.SetActive(true);
					attackstate = combatstate.runback;
				}
				else if (attackstate == combatstate.runback) {
					Vector3 currpos = turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter>().CharacterSprite.position;
					Vector3 target = turnPlayerSlots [currentAttackingPlayer].position;
					turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter>().CharacterSprite.position = Vector3.MoveTowards (currpos, target, 20f*Time.unscaledDeltaTime);
					if (turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter>().CharacterSprite.position == target) {
						waitingForInput = false;
						state = scenestate.playershowattackresults;
					}
				}
			}
		}
		else if(state == scenestate.playershowattackresults)
		{
			if (!waitingForInput) {
				if(!IsEnemyAlive(currentEnemyTarget))
				{
					turnEnemies [currentEnemyTarget].SetActive (false);
				}
				else {
					turnEnemies [currentEnemyTarget].GetComponent<TurnFighter> ().HUDText.text = turnEnemies [currentEnemyTarget].GetComponent<TurnFighter> ().Name + "\n" +
						"HP " + turnEnemies [currentEnemyTarget].GetComponent<TurnFighter> ().currentHitPoints;
				}

				turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter> ().currentSpecialPoints -= turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter> ().attackSpecialPoints [currentAttackNum];
				turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter> ().HUDText.text = turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter> ().Name + "\n" +
				"HP " + turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter> ().currentHitPoints + "\n";

				if(!turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter> ().IgnoreSpecialPoints())
					turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter> ().HUDText.text+="SP " + turnPlayers [currentAttackingPlayer].GetComponent<TurnFighter> ().currentSpecialPoints; 


				ShowAttackDialog ();
			}
			else {

				if (Input.GetKeyDown (KeyCode.Space)) {

					waitingForInput = false;

					if (AreAllEnemiesDefeated ()) {
						//fight is over! we won!!
						state = scenestate.fightoverwin;
					}
					else {
						//anyone else still waiting to attack?
						ChooseNextAttackingCharacter();
					}
				}
			}

		}
		else if(state == scenestate.enemyattack)
		{
			if (!waitingForInput) {
				HideAttackDialog ();
				EnemyPerformAttack ();
				attackstate = combatstate.runto;
			}
			else {
				//run to player
				if (attackstate == combatstate.runto) {
					Vector3 currpos = turnEnemies [currentAttackingEnemy].GetComponent<TurnFighter>().CharacterSprite.position;
					Vector3 target = turnPlayers[currentPlayerTarget].transform.position;
					target.x += 2f;
					turnEnemies [currentAttackingEnemy].GetComponent<TurnFighter>().CharacterSprite.position = Vector3.MoveTowards (currpos, target, 20f*Time.unscaledDeltaTime);
					if(turnEnemies [currentAttackingEnemy].GetComponent<TurnFighter>().CharacterSprite.position == target)
						attackstate = combatstate.attack;
				}
				else if (attackstate == combatstate.attack) {
					if (!audioSrc.isPlaying) audioSrc.Play();
					//AudioSource.PlayClipAtPoint (fightSound, PrefabCamera.transform.position);
					shaketimer = .5f;
					pausetimer = 1f;
					hitburstParticles.transform.position = new Vector3 (turnPlayers [currentPlayerTarget].transform.position.x, turnPlayers [currentPlayerTarget].transform.position.y, hitburstParticles.transform.position.z);
					//hitburstParticles.Play ();
					hitburstParticles.gameObject.SetActive(false);
					hitburstParticles.gameObject.SetActive(true);
					attackstate = combatstate.runback;
				}
				else if (attackstate == combatstate.runback) {
					Vector3 currpos = turnEnemies [currentAttackingEnemy].GetComponent<TurnFighter>().CharacterSprite.position;
					Vector3 target = turnEnemySlots [currentAttackingEnemy].position;
					turnEnemies [currentAttackingEnemy].GetComponent<TurnFighter>().CharacterSprite.position = Vector3.MoveTowards (currpos, target, 20f*Time.unscaledDeltaTime);
					if (turnEnemies [currentAttackingEnemy].GetComponent<TurnFighter>().CharacterSprite.position == target) {
						waitingForInput = false;
						state = scenestate.enemyshowattackresults;
					}
				}
			}
		}
		else if(state == scenestate.enemyshowattackresults)
		{
			if (!waitingForInput) {
				if (!IsPlayerAlive (currentPlayerTarget)) {
					turnPlayers [currentPlayerTarget].SetActive (false);
				}
				else {
					turnPlayers [currentPlayerTarget].GetComponent<TurnFighter> ().HUDText.text = turnPlayers [currentPlayerTarget].GetComponent<TurnFighter> ().Name + "\n" +
					"HP " + turnPlayers [currentPlayerTarget].GetComponent<TurnFighter> ().currentHitPoints + "\n";

					if(!turnPlayers [currentPlayerTarget].GetComponent<TurnFighter> ().IgnoreSpecialPoints())
						turnPlayers [currentPlayerTarget].GetComponent<TurnFighter> ().HUDText.text+="SP " + turnPlayers [currentPlayerTarget].GetComponent<TurnFighter> ().currentSpecialPoints; 
	
				}
				ShowAttackDialog ();
			}
			else {
				if (Input.GetKeyDown (KeyCode.Space)) {

					waitingForInput = false;

					if (AreAllPlayersDefeated ()) {
						//fight is over! we lost!!
						state = scenestate.fightoverdead;
					}
					else {
						//anyone else still waiting to attack?
						ChooseNextAttackingEnemy();
					}
				}
			}

		}
		else if(state == scenestate.fightoverranaway)
		{
			if (!waitingForInput) {
				FightOverRanAway ();
			}
			else {
				if (Input.GetKeyDown (KeyCode.Space)) {
					waitingForInput = false;
					EndTurnCombat ();
				}
			}
		}
		else if(state == scenestate.fightoverdead)
		{
			if (!waitingForInput) {
				FightOverLost ();
			}
			else {
				if (Input.GetKeyDown (KeyCode.Space)) {
					waitingForInput = false;
					string SceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
					UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
				}
			}
		}
		else if(state == scenestate.fightoverwin)
		{
			if (!waitingForInput) {
				FightOverWin ();
			}
			else {
				if (Input.GetKeyDown (KeyCode.Space)) {
					waitingForInput = false;
					EndTurnCombat ();
				}
			}
		}
	}


	void HandleCameraShake()
	{
		Vector3 newPos;

		newPos = initialCameraPosition;

		if(shaketimer>0)
		{
			shaketimer = shaketimer - Time.unscaledDeltaTime;
			newPos.x =  newPos.x + Random.Range(shaketimer*-.25f,shaketimer*.25f);//Mathf.PerlinNoise( newPos.x * minPerlin, newPos.x * maxPerlin);
			newPos.y =  newPos.y + Random.Range(shaketimer*-.25f,shaketimer*.25f);//Mathf.PerlinNoise( newPos.y * minPerlin, newPos.y * maxPerlin);
		}

		PrefabCamera.transform.position = newPos;
	}

	void HideAttackDialog()
	{
		attackDialog.gameObject.SetActive (false);
	}
	void ShowAttackDialog()
	{
		attackDialog.transform.localScale = new Vector2 (.1f, .1f);
		attackDialog.gameObject.SetActive (true);
		attackDialog.GetComponent<GrowAndShrink> ().StartEffect ();
		waitingForInput = true;
	}

}
