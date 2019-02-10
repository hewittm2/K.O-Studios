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
	public Sprite charSprite;
	private FrameType CurrFrameType;
	public bool flip;
    public int playerNumber;
	public int teamNumber;
    //Variables For Designers
    public int currentHealth;
	public int totalHealth;
	public float defValue;
	public float horiDeadZone;
	public float vertDeadZone;
	public float comboTimeOut;
	public int damage;

	//Parts
	public GameObject wholeBody;
	public GameObject upperBody;
	public GameObject lowerBody;
	public GameObject highHit;
	public GameObject midHit;
	public GameObject lowHit;
	public List<GameObject> lockOnTargets;
	public GameObject lockOnTarget;
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
    public string lockOnInput;
	public string startButton;
	//Controller Inputs: Need to Implement
	//public string horiDpad;
	//public string vertDpad;
	//[HideInInspector]
	public bool facingRight;
	[HideInInspector]
	public bool canMove = true;
	[HideInInspector]
	public bool canAttack = true;

	public bool canRecieveDamage = true;

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
	HitBoxes hitBoxes;
	public Animator anim;
	// Use this for initialization
	void Start () {
		CurrFrameType = FrameType.Regular;
		movement = GetComponent<BaseMovement> ();
		hitBoxes = GetComponent<HitBoxes> ();
		currentHealth = totalHealth;
		if (teamNumber == 1) {
			lockOnTargets.AddRange(FindObjectOfType<StageManager> ().team2);
			lockOnTarget = lockOnTargets [0];
		}else if(teamNumber == 2){
			lockOnTargets.AddRange(FindObjectOfType<StageManager> ().team1);
			lockOnTarget = lockOnTargets [0];
		}


	}

	// Update is called once per frame
	void Update () {
		//print ("hor = " + horiInput);
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
		if(flip){
			ToggleDirection ();
		}
		if (canAttack) {
			QueueAttackInput ();
		}
		if (canMove) {
			QueueMovementInput ();
		}

		CheckForCombo ();
		if (lockOnTargets.Count != 0){
			if (Input.GetButtonDown (lockOnInput)) {
				if (lockOnTarget == lockOnTargets [0]) {
					lockOnTarget = lockOnTargets [1];
				} else {
					lockOnTarget = lockOnTargets [0];
				}
				CheckTarget (lockOnTarget);
			}
//			if (checkDelay < 10) {
//				print ("check");
//
//			}
//			checkDelay++;
		}
        if (Input.GetButtonDown(startButton))
        {
            Debug.Log("yes");
            PauseGame pauseGame = FindObjectOfType<PauseGame>();
            pauseGame.Pause(playerNumber);
        }
    }
	void FixedUpdate(){
		if (lockOnTarget != null) {
			CheckTarget (lockOnTarget);
		}

	}
	public void QueueMovementInput(){
		
		//Assume Idle
		anim.SetBool("Walking",false);
		anim.SetBool ("Walking Backwards", false);
		anim.SetBool ("Crouching Idle", false);
		//Right Input Facing Right
		if (Input.GetAxis (horiInput) > horiDeadZone && facingRight) {
			//Forward+Up(R)
			if (Input.GetAxis (vertInput) > vertDeadZone && movement.character.isGrounded) {
				canMove = false;
				movement.DiagonalJump ();
				anim.SetTrigger ("Jumping");
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
					movement.Dash (Input.GetAxis(horiInput));
					anim.SetTrigger ("Dash");
				} else {
					movement.Walk ();
					anim.SetBool ("Walking", true);
				}
			}
		//Left Input Facing Left
		} else if (Input.GetAxis (horiInput) < -horiDeadZone && !facingRight) {
			//Forward+Up(L)
			if (Input.GetAxis (vertInput) > vertDeadZone && movement.character.isGrounded) {
				canMove = false;
				movement.DiagonalJump ();
				anim.SetTrigger ("Jumping");
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
					movement.Dash (Input.GetAxis(horiInput));
					anim.SetTrigger ("Dash");
				} else {
					movement.Walk ();
					anim.SetBool ("Walking", true);
				}
			}
		
		//Left Input Facing Right
		} else if (Input.GetAxis (horiInput) < -horiDeadZone && facingRight) {
			//Backward+Up(R)
			if (Input.GetAxis (vertInput) > horiDeadZone) {
				canMove = false;
				movement.DiagonalJump ();
				anim.SetTrigger ("Jumping");
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
					movement.Dash (Input.GetAxis(horiInput));
					anim.SetTrigger ("Dash");

				} else {
					movement.Walk ();
					anim.SetBool ("Walking Backwards", true);
				}
			}
		//Right Input Facing Left
		} else if (Input.GetAxis (horiInput) > horiDeadZone && !facingRight) {
			//Backward+Up(L)
			if (Input.GetAxis (vertInput) > vertDeadZone) {
				canMove = false;
				movement.DiagonalJump ();
				anim.SetTrigger ("Jumping");
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
					movement.Dash (Input.GetAxis(horiInput));
					anim.SetTrigger ("Dash");
				} else {
					movement.Walk ();
					anim.SetBool ("Walking Backwards", true);
				}
			}
		//Up input Facing Right
		} else if (Input.GetAxis (vertInput) > vertDeadZone && facingRight && movement.character.isGrounded) {
			if (Input.GetAxis (horiInput) > horiDeadZone) {
				canMove = false;
				movement.DiagonalJump ();
				anim.SetTrigger ("Jumping");
			} else if (Input.GetAxis (horiInput) < -horiDeadZone) {
				canMove = false;
				movement.DiagonalJump ();
				anim.SetTrigger ("Jumping");
			} else {
				canMove = false;
				movement.Jump ();
				anim.SetTrigger ("Jumping");
			}
		//Up Input Facing Left
		} else if (Input.GetAxis (vertInput) > vertDeadZone && !facingRight && movement.character.isGrounded) {
			if (Input.GetAxis (horiInput) > horiDeadZone) {
				canMove = false;
				movement.DiagonalJump ();
				anim.SetTrigger ("Jumping");
			} else if (Input.GetAxis (horiInput) < -horiDeadZone) {
				canMove = false;
				movement.DiagonalJump ();
				anim.SetTrigger ("Jumping");
			} else {
				canMove = false;
				movement.Jump ();
				anim.SetTrigger ("Jumping");
			}
		//Crouch Input
		} else if (Input.GetAxis (vertInput) < -vertDeadZone && Input.GetAxis(horiInput) < horiDeadZone && Input.GetAxis(horiInput) > -horiDeadZone && movement.character.isGrounded) {
			if (breakDownStep2) {
				breakDownStep3 = true;
			}
			anim.SetBool ("Crouching Idle", true);
		}
	}

	public void QueueAttackInput(){
		//TeamButton
		if(Input.GetButtonDown(teamInput)){
			if (coupDeGraceStep3) {
				Debug.Log ("CoupDeGrace");
				anim.SetTrigger ("Coup De Grace");
				StartCoroutine (attackDelay ());
			} else {
				//Address this semester 2
				Debug.Log ("Parry");
				anim.SetTrigger ("Block");
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
						anim.SetTrigger("Break Down");
						StartCoroutine (attackDelay ());
					}else if (Input.GetAxis (vertInput) < -vertDeadZone) {
						Debug.Log ("Crouch_LightAttack(R),");
						anim.SetTrigger ("Crouching Light Attack");
						StartCoroutine (attackDelay ());
					} else {
						Debug.Log ("LightAttack(R),");
						anim.SetTrigger ("Light Attack");
						StartCoroutine (attackDelay (hitBoxes.rightHand, 10));
					}
				} else {
					Debug.Log ("Jump_LightAttack(R),");
					anim.SetTrigger ("Jumping Light Attack");
					StartCoroutine (attackDelay ());
				}
			} else {
				if (movement.character.isGrounded) {
					if (breakDownStep3) {
						Debug.Log ("BreakDown");
						anim.SetTrigger ("Break Down");
						StartCoroutine (attackDelay ());
					}else if (Input.GetAxis (vertInput) < -vertDeadZone) {
						Debug.Log ("Crouch_LightAttack(L),");
						anim.SetTrigger ("Crouching Light Attack");
						StartCoroutine (attackDelay ());
					} else {
						Debug.Log ("LightAttack(L),");
						anim.SetTrigger ("Light Attack");
						StartCoroutine (attackDelay ());
					}
				} else {
					Debug.Log ("Jump_LightAttack(L),");
					anim.SetTrigger ("Jumping Light Attack");
					StartCoroutine (attackDelay ());
				}
			}
		//Medium Attack
		} else if (Input.GetButtonDown (medInput)) {
			if (facingRight) {
				if (movement.character.isGrounded) {
					if (Input.GetAxis (vertInput) < -vertDeadZone) {
						Debug.Log ("Crouch_MedAttack(R),");
						anim.SetTrigger ("Crouching Medium Attack");
						StartCoroutine (attackDelay (hitBoxes.leftElbow, hitBoxes.leftHand, 10));
					} else {
						Debug.Log ("MedAttack(R),");
						anim.SetTrigger ("Medium Attack");
						StartCoroutine (attackDelay (hitBoxes.leftElbow, 10));
					}
				} else {
					Debug.Log ("Jump_MedAttack(R),");
					anim.SetTrigger ("Jumping Medium Attack");
					StartCoroutine (attackDelay ());
				}
			} else {
				if (movement.character.isGrounded) {
					if (Input.GetAxis (vertInput) < -vertDeadZone) {
						Debug.Log ("Crouch_MedAttack(L),");
						anim.SetTrigger ("Crouching Medium Attack");
						StartCoroutine (attackDelay (hitBoxes.leftElbow, hitBoxes.leftHand, 10));
					} else {
						Debug.Log ("MedAttack(L),");
						anim.SetTrigger ("Medium Attack");
						StartCoroutine (attackDelay (hitBoxes.leftElbow, 10));
					}

				} else {
					Debug.Log ("Jump_MedAttack(L),");
					anim.SetTrigger ("Jumpimg Medium Attack");
					StartCoroutine (attackDelay ());
				}
			}
		//Heavy Attack
		} else if (Input.GetButtonDown (heavyInput)) {
			if (facingRight) {
				if (movement.character.isGrounded) {
					if (Input.GetAxis (vertInput) < -vertDeadZone) {
						Debug.Log ("Crouch_HeavyAttack(R),");
						anim.SetTrigger ("Crouching Heavy Attack");
						StartCoroutine (attackDelay (hitBoxes.rightFoot, 10));
					} else {
						Debug.Log ("HeavyAttack(R),");
						anim.SetTrigger ("Heavy Attack");
						StartCoroutine (attackDelay (hitBoxes.leftElbow, hitBoxes.leftHand, 10));
					}
				} else {
					Debug.Log ("Jump_HeavyAttack(R),");
					anim.SetTrigger ("Jumping Heavy Attack");
					StartCoroutine (attackDelay ());
				}
			} else {
				if (movement.character.isGrounded) {
					if (Input.GetAxis (vertInput) < -vertDeadZone) {
						Debug.Log ("Crouch_HeavyAttack(L),");
						anim.SetTrigger ("Crouching Heavy Attack");
						StartCoroutine (attackDelay (hitBoxes.rightFoot, 10));
					} else {
						Debug.Log ("HeavyAttack(L),");
						anim.SetTrigger ("Heavy Attack");
						StartCoroutine (attackDelay (hitBoxes.leftElbow, hitBoxes.leftHand, 10));
					}
				} else {
					Debug.Log ("Jump_HeavyAttack(L),");
					anim.SetTrigger ("Jumping Heavy Attack");
					StartCoroutine (attackDelay ());
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
	public void ToggleDirection(){
		flip = false;
		facingRight = !facingRight;
		gameObject.transform.Rotate (new Vector3 (0, 180, 0));
	}
	public void CheckTarget(GameObject target){
		if (gameObject.transform.position.x > target.transform.position.x) {
			facingRight = false;
			gameObject.transform.rotation = Quaternion.Euler(0,270,0);

		}else if(gameObject.transform.position.x < target.transform.position.x){
			facingRight = true;
			gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
		}
	}







	//Temp Location, move to attackFramework;
	IEnumerator attackDelay(){
		canAttack = false;
		canMove = false;
		yield return new WaitForSeconds (anim.GetCurrentAnimatorClipInfo (0).Length);
		canAttack = true;
		canMove = true;
	}
	IEnumerator attackDelay(GameObject activeHit1, int damageToDo){
		canAttack = false;
		canMove = false;
		damage = damageToDo;
		activeHit1.SetActive (true);
		yield return new WaitForSeconds (anim.GetCurrentAnimatorClipInfo (0).Length);
		activeHit1.SetActive (false);
		damage = 0;
		canAttack = true;
		canMove = true;
	}
	IEnumerator attackDelay(GameObject activeHit1, GameObject activeHit2, int damageToDo){
		canAttack = false;
		canMove = false;
		damage = damageToDo;
		activeHit1.SetActive (true);
		activeHit2.SetActive (true);
		yield return new WaitForSeconds (anim.GetCurrentAnimatorClipInfo (0).Length);
		activeHit1.SetActive (false);
		activeHit2.SetActive (false);
		damage = 0;
		canAttack = true;
		canMove = true;
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
