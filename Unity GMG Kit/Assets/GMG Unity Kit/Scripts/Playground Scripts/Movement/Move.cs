using UnityEngine;
using System.Collections;

[AddComponentMenu("Playground/Movement/Move Input System")]
[RequireComponent(typeof(Rigidbody2D))]
public class Move : Physics2DObject
{
	[Header("Input keys")]
	public Enums.KeyGroups typeOfControl = Enums.KeyGroups.ArrowKeys;

	[Header("Movement")]
	[Tooltip("Speed of movement")]
	public float speed = 5f;
	public Enums.MovementType movementType = Enums.MovementType.AllDirections;

	[Header("Orientation")]
	public bool orientToDirection = false;
	// The direction that will face the player
	public Enums.Directions lookAxis = Enums.Directions.Up;

	private Vector3 movement, cachedDirection;
	private float moveHorizontal;
	private float moveVertical;

    //Animations
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        Debug.Log(animator);
    }
    // Update gets called every frame
    void Update ()
	{
        //#if UNITY_STANDALONE || !UNITY_EDITOR
        // Moving with the arrow keys
        if(typeOfControl == Enums.KeyGroups.ArrowKeys)
		{
			moveHorizontal = Input.GetAxis("Horizontal");
			moveVertical = Input.GetAxis("Vertical");
		}
		else if (typeOfControl == Enums.KeyGroups.WASD)
		{
			moveHorizontal = Input.GetAxis("Horizontal2");
			moveVertical = Input.GetAxis("Vertical2");
		}
        else if (typeOfControl == Enums.KeyGroups.Joystick)
        {
	        moveHorizontal = Input.GetAxis("Joystick Horizontal");
	        moveVertical = Input.GetAxis("Joystick Vertical");
        }
        //#endif
    
        //zero-out the axes that are not needed, if the movement is constrained
        switch(movementType)
		{
			case Enums.MovementType.OnlyHorizontal:
				moveVertical = 0f;
				break;
			case Enums.MovementType.OnlyVertical:
				moveHorizontal = 0f;
				break;
		}
			
		movement = new Vector3(moveHorizontal, moveVertical);
        if(animator != null) animator.SetFloat("runSpeed", Mathf.Abs(moveHorizontal) *2f);


		//rotate the GameObject towards the direction of movement
		//the axis to look can be decided with the "axis" variable
		if(orientToDirection)
		{
			if(movement.sqrMagnitude >= 0.01f)
			{
				cachedDirection = movement;
			}
			Utils.SetAxisTowards(lookAxis, transform, cachedDirection);
		}
	}

	// FixedUpdate is called every frame when the physics are calculated
	void FixedUpdate ()
	{
        // Apply the force to the Rigidbody2d
        //rigidbody2D.AddForce(movement * speed * 10f);
        transform.position += movement * Time.deltaTime * speed * 10f;
	}


    // Set the horizontal move direction to left (-1) or right (1)
    // Called by mobile input UI buttons
    public void SetHorizontalInput(int input)
    {
        moveHorizontal = input;
    }

    public void SetMoveLeft()
    {
        //if (animator != null) animator.SetFloat("runSpeed", speed);
        //if (animator != null) animator.SetBool("Moving", true);
        Debug.Log("Left");
        moveHorizontal = -1;
    }

    public void SetMoveRight()
    {
        //if (animator != null) animator.SetFloat("runSpeed", speed);
        //if (animator != null) animator.SetBool("Moving", true);
        Debug.Log("Left");
        moveHorizontal = 1;
    }

    public void StopMoving()
    {
        //if (animator != null) animator.SetFloat("runSpeed", 0f);
        //if (animator != null) animator.SetBool("Moving", false);
        moveHorizontal = 0;
    }
}