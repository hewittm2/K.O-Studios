using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class BaseMovement : MonoBehaviour
{
<<<<<<< HEAD:K.O-Studios-master/RebelStreetWise - MainBuild/Assets/Scripts/BaseMovement.cs
	//Movement
    public float moveSpeed = 3;
	public float gravity = -.7f;
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
=======
    //Movement
    public float moveSpeed = 3;
    public float gravity = -.7f;
    //Jump
    public float jumpForce = .25f;
    public float jumpCD;
    private float verticalVelocity;
    //Dash
    bool dashing;
    public float dashSpeed = 3;
    public float maxDashTime = 2;
    public float dashStopSpeed = 0.1f;
    public float dashSpeed1 = 5;
    private float currDashTime;
    [HideInInspector]
    public CharacterController character;
    [HideInInspector]
    public FighterClass fighter;
    Rigidbody rigid;
>>>>>>> origin/JakesScripts:RebelStreetWise - MainBuild/Assets/Scripts/BaseMovement.cs
    private Vector3[] centerArray = new Vector3[2];
    private float[] radiusArray = new float[2];
    private float[] heightArray = new float[2];
    private float centerOffset = 0;
<<<<<<< HEAD:K.O-Studios-master/RebelStreetWise - MainBuild/Assets/Scripts/BaseMovement.cs
    

    private void Start(){
		rigid = gameObject.GetComponent < Rigidbody> ();
=======


    private void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
>>>>>>> origin/JakesScripts:RebelStreetWise - MainBuild/Assets/Scripts/BaseMovement.cs
        character = gameObject.GetComponent<CharacterController>();
        fighter = gameObject.GetComponent<FighterClass>();
        for (int i = 0; i < 2; i++)
        {
            centerArray[i] = character.center - new Vector3(0, centerOffset, 0);
            radiusArray[i] = character.radius / (i + 1);
            heightArray[i] = character.height / (i + 1);
            centerOffset = 0.25f;
        }
    }

<<<<<<< HEAD:K.O-Studios-master/RebelStreetWise - MainBuild/Assets/Scripts/BaseMovement.cs
    private void Update(){
		if (character.isGrounded) {
			verticalVelocity = gravity;
		} else {
			verticalVelocity += gravity * Time.deltaTime;
		}
		ApplyGravOnly();
    }

	public void ApplyGravOnly(){
		verticalVelocity += gravity * Time.deltaTime / 2;
		if(!dashing)
			character.Move (new Vector2(0,verticalVelocity));
	}

    public void Walk(){
		float deltaX = Input.GetAxis(fighter.horiInput) * moveSpeed;
=======
    private void Update()
    {
        if (character.isGrounded)
        {
            verticalVelocity = gravity;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
        ApplyGravOnly();
    }

    public void ApplyGravOnly()
    {
        verticalVelocity += gravity * Time.deltaTime / 2;
        if (!dashing)
            character.Move(new Vector2(0, verticalVelocity));
    }

    public void Walk()
    {
        float deltaX = Input.GetAxis(fighter.horiInput) * moveSpeed;
>>>>>>> origin/JakesScripts:RebelStreetWise - MainBuild/Assets/Scripts/BaseMovement.cs
        Vector2 movement = new Vector2(deltaX, verticalVelocity);
        movement = Vector2.ClampMagnitude(movement, moveSpeed);
        movement *= Time.deltaTime;
        character.Move(movement);
<<<<<<< HEAD:K.O-Studios-master/RebelStreetWise - MainBuild/Assets/Scripts/BaseMovement.cs
		fighter.canMove = true;
    }
		
    public void Jump(){
		StartCoroutine(Jumping());
=======
        fighter.canMove = true;
    }

    public void Jump()
    {
        StartCoroutine(Jumping());
    }
    IEnumerator Jumping()
    {
        verticalVelocity = jumpForce;
        Vector2 jump = new Vector2(0, verticalVelocity);
        character.Move(jump);
        yield return new WaitForSeconds(jumpCD);
        fighter.canMove = true;
>>>>>>> origin/JakesScripts:RebelStreetWise - MainBuild/Assets/Scripts/BaseMovement.cs
    }
	IEnumerator Jumping(){
		verticalVelocity = jumpForce;
		Vector2 jump = new Vector2(0, verticalVelocity);
		character.Move(jump);
		yield return new WaitForSeconds (jumpCD);
		fighter.canMove = true;
	}

    //HEY THIS WORKS TOO! //Mike, Jacob, Cale, Too Awesome, Put me in the credits, I want royalties
<<<<<<< HEAD:K.O-Studios-master/RebelStreetWise - MainBuild/Assets/Scripts/BaseMovement.cs
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
=======
    public void Dash()
    {
        if (!dashing)
        {
            if (fighter.facingRight)
            {
                if (Input.GetAxis(fighter.horiInput) > 0)
                {
                    StartCoroutine(Dashing(1));
                }
                else
                {
                    StartCoroutine(Dashing(-1));
                }
            }
            else
            {
                if (Input.GetAxis(fighter.horiInput) < 0)
                {
                    StartCoroutine(Dashing(-1));
                }
                else
                {
                    StartCoroutine(Dashing(1));
                }
            }
        }
    }

    //Part of Dash
    IEnumerator Dashing(int direction)
    {
        //dashing = true;
        //character.enabled = false;
        //rigid.constraints = RigidbodyConstraints.None;
        //rigid.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
        //rigid.velocity = new Vector3(0, 0, 0);
        //rigid.angularVelocity = new Vector3(0, 0, 0);
        //rigid.velocity += (new Vector3(dashSpeed * direction, 0, 0));
        //yield return new WaitForSeconds(maxDashTime);
        //rigid.velocity = new Vector3(0, 0, 0);
        //rigid.angularVelocity = new Vector3(0, 0, 0);
        //yield return new WaitForSeconds(.3f);
        //rigid.constraints = RigidbodyConstraints.FreezeAll;
        //character.enabled = true;
        //dashing = false;
        //fighter.canMove = true;

        dashing = true;
        character.enabled = false;
        this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        this.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        this.GetComponent<Rigidbody>().velocity += (new Vector3(dashSpeed1 * direction, 0, 0));
>>>>>>> origin/JakesScripts:RebelStreetWise - MainBuild/Assets/Scripts/BaseMovement.cs
        yield return new WaitForSeconds(maxDashTime);
		rigid.velocity = new Vector3(0, 0, 0);
        rigid.angularVelocity = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(.3f);
		rigid.constraints = RigidbodyConstraints.FreezeAll;
		character.enabled = true;
        dashing = false;
<<<<<<< HEAD:K.O-Studios-master/RebelStreetWise - MainBuild/Assets/Scripts/BaseMovement.cs
		fighter.canMove = true;
    }

    public void Duck(){
        if (Input.GetKey(KeyCode.S)){
=======
        fighter.canMove = true;
    }

    public void Duck()
    {
        if (Input.GetKey(KeyCode.S))
        {
>>>>>>> origin/JakesScripts:RebelStreetWise - MainBuild/Assets/Scripts/BaseMovement.cs
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
