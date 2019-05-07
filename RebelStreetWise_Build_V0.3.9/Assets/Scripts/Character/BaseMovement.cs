//Ethan Quandt
//Edited 5/6/19
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
	float dashSpeed;
	public bool dashing;
	public float dashCD;
	//Jump
    public float vertJumpForce;
	public float horiJumpForce;
	public float jumpCD;
    float verticalVelocity;
	Vector2 jump;
	HitDetection hitDetect;

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
		hitDetect = gameObject.GetComponent<HitDetection> ();
        for(int i = 0; i < 2; i++){
            centerArray[i] = character.center - new Vector3(0, centerOffset, 0);
            radiusArray[i] = character.radius / (i + 1);
            heightArray[i] = character.height / (i + 1);
            centerOffset = 0.25f;
        }
        //fightLine = GameObject.Find("FightLine");
        //rightBoundingWall = GameObject.Find("RightBoundary");
        //leftBoundingWall = GameObject.Find("LeftBoundary");
        //rend = fightLine.GetComponent<Renderer>();
        //leftWallRend = leftBoundingWall.GetComponent<Renderer>();
        //rightWallRend = rightBoundingWall.GetComponent<Renderer>();
    }

    private void Update(){
		input = new Vector2 (Input.GetAxis(fighter.controllerVariables.horiInput), Input.GetAxis(fighter.controllerVariables.vertInput));
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
		if (dashing) {
			movement.x = dashSpeed;
			movement *= Time.deltaTime;
			character.Move(movement);
		} else {
			movement *= Time.deltaTime;
			ApplyGravOnly();
		}


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
			if (!dashing) {
				if (character.isGrounded) {
					character.Move (new Vector2 (0, verticalVelocity));
				} else {
					character.Move (new Vector2 (jump.x, verticalVelocity));
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
	//Ethan
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
	//Ethan
	public void Dash(float input){
		//print ("x = " + input);
		if(!dashing){
			dashing = true;
			if (fighter.facingRight) {
				if (input > 0) {
					dashSpeed = forwardDashSpeed;
					StartCoroutine (Dashing (dashSpeed));
					return;
				} else {
					dashSpeed = -backDashSpeed;
					StartCoroutine (Dashing (dashSpeed));
				}
			} else {
				if (input < 0) {
					dashSpeed = -forwardDashSpeed;
					StartCoroutine (Dashing (dashSpeed));
				} else {
					dashSpeed = backDashSpeed;
					StartCoroutine (Dashing (dashSpeed));
				}
			}
		}
    }

    //Part of Dash
//	IEnumerator Dashing(int direction, float dashSpeed){
//        dashing = true;
//		character.enabled = false;
//		rigid.constraints = RigidbodyConstraints.None;
//		rigid.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
//		rigid.velocity = new Vector3(0, 0, 0);
//		rigid.angularVelocity = new Vector3(0, 0, 0);
//		rigid.velocity += (new Vector3(dashSpeed * direction,0, 0));
//        yield return new WaitForSeconds(dashSpeed/100);
//		rigid.velocity = new Vector3(0, 0, 0);
//        rigid.angularVelocity = new Vector3(0, 0, 0);
//        yield return new WaitForSeconds(.01f);
//		rigid.constraints = RigidbodyConstraints.FreezeAll;
//		character.enabled = true;
//        dashing = false;
//		fighter.canMove = true;
//    }
	//Ethan
	IEnumerator Dashing(float dashSpeed){
		foreach (GameObject enemy in fighter.lockOnTargets) {
			hitDetect.IgnoreFighter (fighter.gameObject, enemy, true);
		}
		yield return new WaitForSeconds (dashCD);
		foreach (GameObject enemy in fighter.lockOnTargets) {
			hitDetect.IgnoreFighter (fighter.gameObject, enemy, false);
		}
		fighter.anim.SetTrigger ("Idle");
		fighter.canMove = true;
		dashing = false;
	}

//    public void Duck(){
//        if (Input.GetKey(KeyCode.S)){
//            Debug.Log("duck (also beans)");
//            character.center = centerArray[1];
//            character.radius = radiusArray[1];
//            character.height = heightArray[1];
//        }
//        else{
//            character.center = centerArray[0];
//            character.radius = radiusArray[0];
//            character.height = heightArray[0];
//        }
//    }

//    public void Block(){
//        //just block how hard can it be
//    }

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
