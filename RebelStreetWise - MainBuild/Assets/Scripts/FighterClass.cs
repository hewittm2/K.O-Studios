//Created By Ethan Quandt 8/29/18
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0414

public class FighterClass : MonoBehaviour {
	
	//Class Variables
	//Frame Data
	public enum FrameType{
		Startup,
		Active,
		Recovery,
		Regular
	}
	private FrameType CurrFrameType;


	//Variables For Designers
	public int currentHealth;
	public int totalHealth;
	public float defValue;
	public float horiDeadZone;
	public float vertDeadZone;
	public float comboTimeOut;

	//Parts
	public GameObject wholeBody;
	public GameObject upperBody;
	public GameObject lowerBody;
	public GameObject highHit;
	public GameObject midHit;
	public GameObject lowHit;

	//Attacks

    //Controller Inputs
	//Axis
    public string horiInput;
	public string vertInput;
    //Buttons
    public string lightInput;
    public string medInput;
    public string heavyInput;
    public string specialInput;
	public string throwInput;
	public string teamInput;
    public string dashInput;

	//Controller Inputs: Need to Implement
	//public string horiDpad;
	//public string vertDpad;
	[HideInInspector]
	public bool facingRight = true;
	[HideInInspector]
	public bool canMove = true;
	[HideInInspector]
	public bool canAttack = true;
	[HideInInspector]
	public bool canRecieveDamage;

	bool breakDownStep1L = false;
	bool breakDownStep1R = false;
	bool breakDownStep2 = false;
	bool breakDownStep3 = false;
	bool coupDeGraceStep1 = false;
	bool coupDeGraceStep2 = false;
	bool coupDeGraceStep3 = false;
	bool comboTimerStarted = false;
	bool dashReset = false;

	float comboTimerEnd = 0;
	BaseMovement movement;

	// Use this for initialization
	void Start () {
		CurrFrameType = FrameType.Regular;
		movement = GetComponent<BaseMovement> ();
	}

	// Update is called once per frame
	void Update () {
//		switch(CurrFrameType){
//		case FrameType.Active:
//			break;
//		case FrameType.Recovery:
//			break;
//		case FrameType.Startup:
//			break;
//		case FrameType.Regular:
//			QueueInput ();
//			RegisterQueue ();
//			break;
//		}
		if (canMove) {
			QueueMovementInput ();
		}
		if (canAttack) {
			QueueAttackInput ();
		}
		CheckForCombo ();
	}

	public void QueueMovementInput(){
		
		if(Input.GetKeyDown(KeyCode.Space)){
			facingRight = !facingRight;
			gameObject.transform.Rotate (new Vector3 (0, 180, 0));
		}
		//Right Input Facing Right
		if (Input.GetAxis (horiInput) > horiDeadZone && facingRight) {
			//Forward+Up(R)
			if (Input.GetAxis (vertInput) > vertDeadZone && movement.character.isGrounded) {
				canMove = false;
				movement.DiagonalJump ();
			//Forward+Down(R)
			}else if(Input.GetAxis(vertInput)< -vertDeadZone){
				if (breakDownStep1R) {
					comboTimerStarted = true;
					comboTimerEnd = Time.time + comboTimeOut;
					breakDownStep2 = true;
				}
			//Forward(R)
			} else {
				if (!comboTimerStarted) {
					comboTimerStarted = true;
					comboTimerEnd = Time.time + comboTimeOut;
					breakDownStep1R = true;
				} else if (coupDeGraceStep2) {
					comboTimerStarted = true;
					comboTimerEnd = Time.time + comboTimeOut;
					coupDeGraceStep3 = true;
				} else if (dashReset && breakDownStep1R) {
					canMove = false;
					movement.Dash ();
				} else {
					movement.Walk ();
				}
			}
		//Left Input Facing Left
		} else if (Input.GetAxis (horiInput) < -horiDeadZone && !facingRight) {
			//Forward+Up(L)
			if (Input.GetAxis (vertInput) > vertDeadZone && movement.character.isGrounded) {
				canMove = false;
				movement.DiagonalJump ();
			//Forward+Down(L)
			}else if(Input.GetAxis(vertInput)< -vertDeadZone){
				if (breakDownStep1L) {
					breakDownStep2 = true;
				}
			//Forward(L)
			} else {
				if (!comboTimerStarted) {
					comboTimerStarted = true;
					comboTimerEnd = Time.time + comboTimeOut;
					breakDownStep1L = true;
				} else if (coupDeGraceStep2) {
					comboTimerStarted = true;
					comboTimerEnd = Time.time + comboTimeOut;
					coupDeGraceStep3 = true;
				} else if (dashReset && breakDownStep1L) {
					canMove = false;
					movement.Dash ();
				} else {
					movement.Walk ();
				}
			}
		
		//Left Input Facing Right
		} else if (Input.GetAxis (horiInput) < -horiDeadZone && facingRight) {
			//Backward+Up(R)
			if (Input.GetAxis (vertInput) > horiDeadZone) {
				canMove = false;
				movement.DiagonalJump ();
				if (breakDownStep3) {
					comboTimerStarted = true;
					comboTimerEnd = Time.time + comboTimeOut;
					coupDeGraceStep1 = true;
				}
			//Backward+Down(R)
			}else if(Input.GetAxis(vertInput) < -vertDeadZone && breakDownStep3){
				comboTimerStarted = true;
				comboTimerEnd = Time.time + comboTimeOut;
				coupDeGraceStep1 = true;
			//Backward(R)
			} else {
				if (!comboTimerStarted) {
					comboTimerStarted = true;
					comboTimerEnd = Time.time + comboTimeOut;
					breakDownStep1L = true;
				} else if (coupDeGraceStep1) {
					comboTimerStarted = true;
					comboTimerEnd = Time.time + comboTimeOut;
					coupDeGraceStep2 = true;
				} else if (dashReset && breakDownStep1L) {
					canMove = false;
					movement.Dash ();

				} else {
					movement.Walk ();
				}
			}
		//Right Input Facing Left
		} else if (Input.GetAxis (horiInput) > horiDeadZone && !facingRight) {
			//Backward+Up(L)
			if (Input.GetAxis (vertInput) > vertDeadZone) {
				canMove = false;
				movement.DiagonalJump ();
				if (breakDownStep3) {
					comboTimerStarted = true;
					comboTimerEnd = Time.time + comboTimeOut;
					coupDeGraceStep1 = true;
				}
			//Backward+Down(L)
			} else if(Input.GetAxis(vertInput)< -vertDeadZone && breakDownStep3){
				comboTimerStarted = true;
				comboTimerEnd = Time.time + comboTimeOut;
				coupDeGraceStep1 = true;
			//Backward(L)
			}else {
				if (!comboTimerStarted) {
					comboTimerStarted = true;
					comboTimerEnd = Time.time + comboTimeOut;
					breakDownStep1R = true;
				} else if (coupDeGraceStep1) {
					comboTimerStarted = true;
					comboTimerEnd = Time.time + comboTimeOut;
					coupDeGraceStep2 = true;
				} else if (dashReset && breakDownStep1R) {
					canMove = false;
					movement.Dash ();
				} else {
					movement.Walk ();
				}
			}
		//Up input Facing Right
		} else if (Input.GetAxis (vertInput) > vertDeadZone && facingRight && movement.character.isGrounded) {
			if (Input.GetAxis (horiInput) > horiDeadZone) {
				canMove = false;
				movement.DiagonalJump ();
			} else if (Input.GetAxis (horiInput) < -horiDeadZone) {
				canMove = false;
				movement.DiagonalJump ();
			} else {
				canMove = false;
				movement.Jump ();
			}
		//Up Input Facing Left
		} else if (Input.GetAxis (vertInput) > vertDeadZone && !facingRight && movement.character.isGrounded) {
			if (Input.GetAxis (horiInput) > horiDeadZone) {
				canMove = false;
				movement.DiagonalJump ();
			} else if (Input.GetAxis (horiInput) < -horiDeadZone) {
				canMove = false;
				movement.DiagonalJump ();
			} else {
				canMove = false;
				movement.Jump ();
			}
		//Crouch Input
		} else if (Input.GetAxis (vertInput) < -vertDeadZone && Input.GetAxis(horiInput) < horiDeadZone && Input.GetAxis(horiInput) > -horiDeadZone && movement.character.isGrounded) {
			if (breakDownStep2) {
				breakDownStep3 = true;
			}
		}
	}

	public void QueueAttackInput(){
		//TeamButton
		if(Input.GetButtonDown(teamInput)){
			if (coupDeGraceStep3) {
				Debug.Log ("CoupDeGrace");
			} else {
				Debug.Log ("Parry");
			}
		//Grab/Throw
		} if ((Input.GetButton (lightInput) && Input.GetButton (medInput))||Input.GetAxis(throwInput) > horiDeadZone) {
			if (facingRight) {
				if (Input.GetAxis (horiInput) < -horiDeadZone) {
					Debug.Log ("BackwardThrow(R),");
				} else {
					Debug.Log ("ForwardThrow(R)");
				}
			} else {
				if (Input.GetAxis (horiInput) > horiDeadZone) {
					Debug.Log ("BackwardThrow(L),");
				} else {
					Debug.Log ("ForwardThrow(L)");
				}
			}
		//Light Attack
		}else if (Input.GetButtonDown (lightInput)) {
			if (facingRight) {
				if (movement.character.isGrounded) {
					if (breakDownStep3) {
						Debug.Log ("BreakDown");
					}else if (Input.GetAxis (vertInput) < -vertDeadZone) {
						Debug.Log ("Crouch_LightAttack(R),");
					} else {
						Debug.Log ("LightAttack(R),");
					}
				} else {
					Debug.Log ("Jump_LightAttack(R),");
				}
			} else {
				if (movement.character.isGrounded) {
					if (breakDownStep3) {
						Debug.Log ("BreakDown");
					}else if (Input.GetAxis (vertInput) < -vertDeadZone) {
						Debug.Log ("Crouch_LightAttack(L),");
					} else {
						Debug.Log ("LightAttack(L),");
					}
				} else {
					Debug.Log ("Jump_LightAttack(L),");
				}
			}
		//Medium Attack
		} else if (Input.GetButtonDown (medInput)) {
			if (facingRight) {
				if (movement.character.isGrounded) {
					if (Input.GetAxis (vertInput) < -vertDeadZone) {
						Debug.Log ("Crouch_MedAttack(R),");
					} else {
						Debug.Log ("MedAttack(R),");
					}
				} else {
					Debug.Log ("Jump_MedAttack(R),");
				}
			} else {
				if (movement.character.isGrounded) {
					if (Input.GetAxis (vertInput) < -vertDeadZone) {
						Debug.Log ("Crouch_MedAttack(L),");
					} else {
						Debug.Log ("MedAttack(L),");
					}

				} else {
					Debug.Log ("Jump_MedAttack(L),");
				}
			}
		//Heavy Attack
		} else if (Input.GetButtonDown (heavyInput)) {
			if (facingRight) {
				if (movement.character.isGrounded) {
					if (Input.GetAxis (vertInput) < -vertDeadZone) {
						Debug.Log ("Crouch_HeavyAttack(R),");
					} else {
						Debug.Log ("HeavyAttack(R),");
					}
				} else {
					Debug.Log ("Jump_HeavyAttack(R),");
				}
			} else {
				if (movement.character.isGrounded) {
					if (Input.GetAxis (vertInput) < -vertDeadZone) {
						Debug.Log ("Crouch_HeavyAttack(L),");
					} else {
						Debug.Log ("HeavyAttack(L),");
					}
				} else {
					Debug.Log ("Jump_HeavyAttack(L),");
				}
			}
		} 
	}
		
	public void CheckForCombo(){
		if (Input.GetAxis (horiInput) == 0 &&( breakDownStep1L || breakDownStep1R) && !breakDownStep3) {
			dashReset = true;
		}
		if (Time.time >= comboTimerEnd && comboTimerStarted) {
			breakDownStep1R = false;
			breakDownStep1L = false;
			breakDownStep2 = false;
			breakDownStep3 = false;
			coupDeGraceStep1 = false;
			coupDeGraceStep2 = false;
			coupDeGraceStep3 = false;
			comboTimerStarted = false;
			dashReset = false;
			comboTimerEnd = 0;
		}
	}
	//Class Functions
	//Recieve Damage
	public virtual void TakeDamage(){

	}
	//Block Damage
	public virtual void Block(){

	}
	//Stop attacking Combo
	public virtual void ComboBreak(){

	}
	//Lower Stance
	public virtual void Crouch(){

	}
	//Move Character Left and Right
	public virtual void Move(){

	}
	//Jump
	public virtual void Jump(){

	}
	//Jump Forward
	public virtual void JumpForward(){

	}
	//Jump Backward
	public virtual void JumpBackward(){

	}
	//Dash
	public virtual void Dash(){

	}
	//Light Attack
	public virtual void LightAtt(){

	}
	//Medium Attack
	public virtual void MediumAtt(){

	}
	//Heavy Attack
	public virtual void HeavyAtt(){

	}
	//Crouching Light Attack
	public virtual void CrouchLightAtt(){

	}
	//Crouching Medium Attack
	public virtual void CrouchMedAtt(){

	}
	//Crouching Heavy Attack
	public virtual void CrouchHeavyAtt(){

	}
	//Jumping Light Attack
	public virtual void JumpLightAtt(){

	}
	//Jumping Medium Attack
	public virtual void  JumpMedAtt(){

	}
	//Jumping Heavy Attack
	public virtual void JumpHeavyAtt(){

	}
	//Grab Opponent
	public virtual void Grab(){

	}
}
