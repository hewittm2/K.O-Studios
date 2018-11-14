using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BaseMovement : MonoBehaviour
{
	//Movement
    public float moveSpeed = 3;
	public float gravity = -.7f;
	public Vector2 input;
	public Vector2 movement;
	//Jump
    public float jumpForce = .25f;
	public float jumpCD;
    private float verticalVelocity;
    //Dash
	bool dashing;
    public float dashSpeed = 3;
    public float maxDashTime = 2;
    public float dashStopSpeed = 0.1f;
    private float currDashTime;
	[HideInInspector]
    public CharacterController character;
	[HideInInspector]
	public FighterClass fighter;
	Rigidbody rigid;
    private Vector3[] centerArray = new Vector3[2];
    private float[] radiusArray = new float[2];
    private float[] heightArray = new float[2];
    private float centerOffset = 0;
    

    private void Start(){
		rigid = gameObject.GetComponent < Rigidbody> ();
        character = gameObject.GetComponent<CharacterController>();
		fighter = gameObject.GetComponent<FighterClass> ();
        for(int i = 0; i < 2; i++)
        {
            centerArray[i] = character.center - new Vector3(0, centerOffset, 0);
            radiusArray[i] = character.radius / (i + 1);
            heightArray[i] = character.height / (i + 1);
            centerOffset = 0.25f;
        }
    }

    private void Update(){
		input = new Vector2 (Input.GetAxis(fighter.horiInput), Input.GetAxis(fighter.vertInput));
		if (character.isGrounded) {
			
			verticalVelocity = gravity;
		} else {
			verticalVelocity += gravity * Time.deltaTime;
		}
		ApplyGravOnly();
    }

	public void ApplyGravOnly(){
		//verticalVelocity += gravity * Time.deltaTime / 2;
		if(!dashing)
			character.Move (new Vector2(0,verticalVelocity));
	}

    public void Walk(){
		//float deltaX = Input.GetAxis(fighter.horiInput) * moveSpeed;
		movement = new Vector2(input.x * moveSpeed, verticalVelocity);
        movement = Vector2.ClampMagnitude(movement, moveSpeed);
        movement *= Time.deltaTime;
        character.Move(movement);
		fighter.canMove = true;
    }
		
    public void Jump(){
		StartCoroutine(Jumping());
    }
	IEnumerator Jumping(){
		verticalVelocity = jumpForce;
		Vector2 jump = new Vector2(0, verticalVelocity);
		character.Move(jump);
		yield return new WaitForSeconds (jumpCD);
		fighter.canMove = true;
	}
	public void DiagonalJump(){
		StartCoroutine (DiagonalJumping());
	}
	IEnumerator DiagonalJumping(){
		verticalVelocity = jumpForce;
		Vector2 jump = new Vector2(movement.x, verticalVelocity);
		character.Move(jump);
		yield return new WaitForSeconds (jumpCD);
		fighter.canMove = true;
	}
    //HEY THIS WORKS TOO! //Mike, Jacob, Cale, Too Awesome, Put me in the credits, I want royalties
	public void Dash(){
		if(!dashing){
			if (fighter.facingRight) {
				if (Input.GetAxis (fighter.horiInput) > 0) {
					StartCoroutine (Dashing (1));
				} else {
					StartCoroutine (Dashing (-1));
				}
			} else {
				if (Input.GetAxis (fighter.horiInput) < 0) {
					StartCoroutine (Dashing (-1));
				} else {
					StartCoroutine (Dashing (1));
				}
			}
		}
    }

    //Part of Dash
    IEnumerator Dashing(int direction){
        dashing = true;
		character.enabled = false;
		rigid.constraints = RigidbodyConstraints.None;
		rigid.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
		rigid.velocity = new Vector3(0, 0, 0);
		rigid.angularVelocity = new Vector3(0, 0, 0);
		rigid.velocity += (new Vector3(dashSpeed * direction, 0, 0));
        yield return new WaitForSeconds(maxDashTime);
		rigid.velocity = new Vector3(0, 0, 0);
        rigid.angularVelocity = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(.3f);
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
}
