using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BaseMovement : MonoBehaviour
{
	//Movement
	public Vector2 input;
	Vector2 movement;
	public float forwardMoveSpeed;
    public float backMoveSpeed;
	float moveSpeed;
	public float gravity = -.7f;
	//Dash
	public float forwardDashSpeed;
	public float backDashSpeed;
	public bool dashing;
	//Jump
    public float vertJumpForce;
	public float horiJumpForce;
	public float jumpCD;
    float verticalVelocity;
	Vector2 jump;

    //FightLine Reset Objects
    public GameObject fightLine;
    Renderer rend;
    public GameObject rightBoundingWall;
    Renderer rightWallRend;
    public GameObject leftBoundingWall;
    Renderer leftWallRend;

    //RequiredComponents
    [HideInInspector]
    public CharacterController character;
	//[HideInInspector]
	public FighterClass fighter;
	public Rigidbody rigid;
	//Hit/Hurt Boxes
    private Vector3[] centerArray = new Vector3[2];
    private float[] radiusArray = new float[2];
    private float[] heightArray = new float[2];
    private float centerOffset = 0;

    [HideInInspector] public bool isPaused;
    

    private void Start(){
		rigid = gameObject.GetComponent<Rigidbody>();
        character = gameObject.GetComponent<CharacterController>();
		fighter = this.gameObject.GetComponent<FighterClass> ();

        for(int i = 0; i < 2; i++){
            centerArray[i] = character.center - new Vector3(0, centerOffset, 0);
            radiusArray[i] = character.radius / (i + 1);
            heightArray[i] = character.height / (i + 1);
            centerOffset = 0.25f;
        }
        fightLine = GameObject.Find("FightLine");
        rightBoundingWall = GameObject.Find("RightBoundary");
        leftBoundingWall = GameObject.Find("LeftBoundary");
        rend = fightLine.GetComponent<Renderer>();
        leftWallRend = leftBoundingWall.GetComponent<Renderer>();
        rightWallRend = rightBoundingWall.GetComponent<Renderer>();

    }

    private void Update(){
        // ===== Resets position on the Z-Axis to a defined line ===== \\
        if (transform.position.z > rend.bounds.max.z || transform.position.z < rend.bounds.min.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, rend.bounds.center.z);
        }

        if (transform.position.x > rightWallRend.bounds.min.x)
        {
            transform.position = new Vector3(rightWallRend.bounds.min.x - 1f, transform.position.y, transform.position.z);
        }

        else if(transform.position.x < leftWallRend.bounds.max.x)
        {
            transform.position = new Vector3(leftWallRend.bounds.max.x + 1f, transform.position.y, transform.position.z);
        }

        input = new Vector2 (Input.GetAxis(fighter.controllerVariables.horiInput), Input.GetAxis(fighter.controllerVariables.horiInput));
		if (fighter.facingRight) {
			if (input.x < 0) {
				moveSpeed = backMoveSpeed;
			} else {
				moveSpeed = forwardMoveSpeed;
			}
		} else {
			if (input.x > 0) {
				moveSpeed = backMoveSpeed;
			} else {
				moveSpeed = forwardMoveSpeed;
			}
		}
		movement = new Vector2(input.x * moveSpeed, verticalVelocity);
		movement = Vector2.ClampMagnitude(movement, moveSpeed);
		movement *= Time.deltaTime;
		ApplyGravOnly();
    }

	public void ApplyGravOnly(){
        if (isPaused == false)
        {
            if (character.isGrounded)
            {
                verticalVelocity = gravity;
            }
            else
            {
                verticalVelocity += gravity * Time.deltaTime;
            }
            if (!dashing)
            {
                if (character.isGrounded)
                {
                    character.Move(new Vector2(0, verticalVelocity));
                }
                else
                {
                    character.Move(new Vector2(jump.x, verticalVelocity));
                }
            }
        }
	}

    public void Walk(){
        character.Move(movement);
    }
		
    public void Jump(){
		StartCoroutine(Jumping());
    }
	IEnumerator Jumping(){
		verticalVelocity = vertJumpForce;
		jump = new Vector2(0, verticalVelocity);
		character.Move(jump);
		yield return new WaitForSeconds (jumpCD);
		fighter.canMove = true;
	}
	public void DiagonalJump(){
		StartCoroutine (DiagonalJumping());
	}
	IEnumerator DiagonalJumping(){
		verticalVelocity = vertJumpForce;
		jump.y = verticalVelocity;
		if (fighter.facingRight) {
			if (input.x < 0) {
				jump.x = -horiJumpForce;
			} else if (input.x > 0) {
				jump.x = horiJumpForce;
			}
		} else {
			if (input.x < 0) {
				jump.x = -horiJumpForce;
			} else if (input.x > 0) {
				jump.x = horiJumpForce;
			}
		}
		character.Move(jump);
		yield return new WaitForSeconds (jumpCD);
		fighter.canMove = true;
	}
    //HEY THIS WORKS TOO! //Ethan, Mike, Jacob, Cale, Too Awesome, Put me in the credits, I want royalties
	public void Dash(float input){
		//print ("x = " + input);
		if(!dashing){
			if (fighter.facingRight) {
				if (input > 0) {
					StartCoroutine (Dashing (1,forwardDashSpeed));
					return;
				} else {
					StartCoroutine (Dashing (-1,backDashSpeed));
				}
			} else {
				if (input < 0) {
					StartCoroutine (Dashing (-1,forwardDashSpeed));
				} else {
					StartCoroutine (Dashing (1,backDashSpeed));
				}
			}
		}
    }

    //Part of Dash
	IEnumerator Dashing(int direction, float dashSpeed){
        dashing = true;
		character.enabled = false;
		rigid.constraints = RigidbodyConstraints.None;
		rigid.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
		rigid.velocity = new Vector3(0, 0, 0);
		rigid.angularVelocity = new Vector3(0, 0, 0);
		rigid.velocity += (new Vector3(dashSpeed * direction,0, 0));
        yield return new WaitForSeconds(dashSpeed/100);
		rigid.velocity = new Vector3(0, 0, 0);
        rigid.angularVelocity = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(.01f);
		rigid.constraints = RigidbodyConstraints.FreezeAll;
		character.enabled = true;
        dashing = false;
		fighter.canMove = true;
    }

    public void Duck(){
        if (Input.GetKey(KeyCode.S)){
            Debug.Log("duck (also beans)");
            character.center = centerArray[1];
            character.radius = radiusArray[1];
            character.height = heightArray[1];
        }
        else{
            character.center = centerArray[0];
            character.radius = radiusArray[0];
            character.height = heightArray[0];
        }
    }

    public void Block(){
        //just block how hard can it be
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Wall"){
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.GetComponent<Rigidbody>().AddForce(GetComponent<Rigidbody>().velocity.x * -1, 0, 0);
        }
    }
	// ===== Needed for RoundTracker Movement Reset ===== \\
	public void ResetMovement()
	{
		verticalVelocity = 0;
		movement = new Vector2(0, 0);
	}
}
