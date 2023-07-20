using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : MonoBehaviour
{
	public delegate void InputButtonAction(InputButton inputButton);
	public event InputButtonAction onInputButton;

	Dictionary<InputButton, KeyCode> inputToKey = new Dictionary<InputButton, KeyCode>();

	public KeyCode Up1Key = KeyCode.UpArrow;
	public KeyCode Down1Key = KeyCode.DownArrow;
	public KeyCode Left1Key = KeyCode.LeftArrow;
	public KeyCode Right1Key = KeyCode.RightArrow;

	public KeyCode Up2Key = KeyCode.W;
	public KeyCode Down2Key = KeyCode.S;
	public KeyCode Left2Key = KeyCode.A;
	public KeyCode Right2Key = KeyCode.D;

	public KeyCode Action1Key = KeyCode.Space;
	public KeyCode Action2Key = KeyCode.Z;
	public KeyCode Action3Key = KeyCode.X;
	public KeyCode Action4Key = KeyCode.C;

	public KeyCode EscapeKey = KeyCode.Escape;
	public KeyCode EnterKey = KeyCode.Return;
	public KeyCode SpaceKey = KeyCode.Space;

	public enum InputButton
	{
		MouseLeftClick = 0,
		MouseRightClick = 1,
		MouseMiddleClick = 2,
		Up1,
		Down1,
		Left1,
		Right1,
		Up2,
		Down2,
		Left2,
		Right2,		
		Action1,
		Action2,
		Action3,
		Action4,
		Escape,
		Enter,
		Space
	}

	// Use this for initialization
	void Start () 
	{
		inputToKey.Add(InputButton.Up1, Up1Key);
		inputToKey.Add(InputButton.Down1, Down1Key);
		inputToKey.Add(InputButton.Left1, Left1Key);
		inputToKey.Add(InputButton.Right1, Right1Key);
		inputToKey.Add(InputButton.Up2, Up2Key);
		inputToKey.Add(InputButton.Down2, Down2Key);
		inputToKey.Add(InputButton.Left2, Left2Key);
		inputToKey.Add(InputButton.Right2, Right2Key);
		inputToKey.Add(InputButton.Action1, Action1Key);
		inputToKey.Add(InputButton.Action2, Action2Key);
		inputToKey.Add(InputButton.Action3, Action3Key);
		inputToKey.Add(InputButton.Action4, Action4Key);

		inputToKey.Add(InputButton.Escape, EscapeKey);
		inputToKey.Add(InputButton.Enter, EnterKey);
		inputToKey.Add(InputButton.Space, SpaceKey);
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	bool isMouse(InputButton inBut)
	{
		return inBut == InputButton.MouseLeftClick || inBut == InputButton.MouseRightClick || inBut == InputButton.MouseMiddleClick;
	}

	public bool GetUp()
	{
		return Input.GetKey(inputToKey[InputButton.Up1]) || Input.GetKey(inputToKey[InputButton.Up2]);
	}
	public bool GetUpFirstPress()
	{
		return Input.GetKeyDown(inputToKey[InputButton.Up1]) || Input.GetKeyDown(inputToKey[InputButton.Up2]);
	}

	public bool GetDown()
	{
		return Input.GetKey(inputToKey[InputButton.Down1]) || Input.GetKey(inputToKey[InputButton.Down2]);
	}
	
	public bool GetDownFirstPress()
	{
		return Input.GetKeyDown(inputToKey[InputButton.Down1]) || Input.GetKeyDown(inputToKey[InputButton.Down2]);
	}

	public bool GetLeft()
	{
		return Input.GetKey(inputToKey[InputButton.Left1]) || Input.GetKey(inputToKey[InputButton.Left2]);
	}

	public bool GetRight()
	{
		return Input.GetKey(inputToKey[InputButton.Right1]) || Input.GetKey(inputToKey[InputButton.Right2]);
	}

	public bool GetKey(InputButton inBut)
	{
		return (isMouse(inBut)) ? Input.GetMouseButton((int)inBut) : Input.GetKey(inputToKey[inBut]);
	}

	public bool GetKeyDown(InputButton inBut)
	{
		return (isMouse(inBut)) ? Input.GetMouseButtonDown((int)inBut) :  Input.GetKeyDown(inputToKey[inBut]);
	}

	public bool GetKeyUp(InputButton inBut)
	{
		return (isMouse(inBut)) ? Input.GetMouseButtonUp((int)inBut) :  Input.GetKeyUp(inputToKey[inBut]);
	}

}
