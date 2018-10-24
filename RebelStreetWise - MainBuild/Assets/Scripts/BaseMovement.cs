using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BaseMovement : MonoBehaviour
{
    public float moveSpeed = 3;
    public float jumpForce = .25f;
    public float verticalVelocity;
    public float gravity = -.7f;
    public float dashSpeed = 3;
    public float maxDashTime = 2;
    public float dashStopSpeed = 0.1f;

    private float currDashTime;

    private CharacterController character;
	FighterClass fighter;
    private Vector3[] centerArray = new Vector3[2];
    private float[] radiusArray = new float[2];
    private float[] heightArray = new float[2];
    private float centerOffset = 0;

    //public bool facingRight = false;
    bool dashing;

    private void Start(){
		
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


		character.Move (new Vector2(0,verticalVelocity));
		//print(Debug.)
        //Walk();
        //Jump();
        //Dash();
		ApplyGravOnly();
        Duck();
        Block();
    }

	public void ApplyGravOnly(){
	verticalVelocity += gravity * Time.deltaTime / 2;
	character.Move (new Vector2(0,verticalVelocity));
	}

    public void Walk()
    {
		float deltaX = Input.GetAxis(fighter.horiInput) * moveSpeed;
		//float deltaY = Input.GetAxis(fighter.vertInput) * moveSpeed;
        Vector2 movement = new Vector2(deltaX, verticalVelocity);
		//Debug.Log (movement + " Unclamped");
        movement = Vector2.ClampMagnitude(movement, moveSpeed);
		//Debug.Log (movement + " Clamped");
        //movement.y = gravity;

        movement *= Time.deltaTime;
        //movement = transform.TransformDirection(movement);
		//Debug.Log (movement + " final");
        
        character.Move(movement);

//        if (deltaX > 0)
//        {
//            facingRight = true;
//        }
//        else if (deltaX < 0)
//        {
//            facingRight = false;
//        }
    }

    //DO NOT TOUCH THIS IT JUST WORKS
    public void Jump()
    {
		
		if (character.isGrounded)
		{

			verticalVelocity = jumpForce;
		}
		else
		{
			verticalVelocity += gravity * Time.deltaTime;
		}
        Vector2 jump = new Vector2(0, verticalVelocity);
        character.Move(jump);
    }

    //HEY THIS WORKS TOO! //Mike, Jacob, Cale, Too Awesome, Put me in the credits, I want royalties
    public void Dash()
    {
        //Dash Left
//        if (Input.GetKeyDown(KeyCode.Q) && !dashing)
//        {
//            StartCoroutine(Dashing(-1));
//        }
//        if (Input.GetKeyDown(KeyCode.E) && !dashing)
//        {
//            StartCoroutine(Dashing(1));
//        }
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
        this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        this.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        this.GetComponent<Rigidbody>().velocity += (new Vector3(dashSpeed * direction, 0, 0));
        yield return new WaitForSeconds(maxDashTime);
        this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        this.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        character.enabled = true;
        yield return new WaitForSeconds(2);
        dashing = false;
		fighter.canInput = true;

    }

    public void Duck() //ha
    {
        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("duck (also beans)");
            character.center = centerArray[1];
            character.radius = radiusArray[1];
            character.height = heightArray[1];
        }
        else
        {
            character.center = centerArray[0];
            character.radius = radiusArray[0];
            character.height = heightArray[0];
        }
    }

    public void Block()
    {
        //just block how hard can it be
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.GetComponent<Rigidbody>().AddForce(GetComponent<Rigidbody>().velocity.x * -1, 0, 0);
        }
    }
}
