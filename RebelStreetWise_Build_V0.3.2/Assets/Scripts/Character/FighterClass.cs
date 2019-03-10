//Created By Ethan Quandt 8/29/18
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0414

public class FighterClass : MonoBehaviour {
	
	//Class Variables
	BaseMovement movement;
	HitDetection hitBoxes;
	//Frame Data
	public enum FrameType{
		Startup,
		Active,
		Recovery,
		Regular
	}
	public Sprite charSprite;
	private FrameType CurrFrameType;
	//public bool flip;
    public int playerNumber;
	public int teamNumber;
    //Variables For Designers
    public int currentHealth;
	public int totalHealth;
	public float defValue;
	//Individual Attack Variables
	[System.Serializable]
	public class AttackStats{
		public enum HitType{High, Mid,Low}
		public HitType hitType;
		public Vector3 knockBackDirection;
		public float knockBackForce;
		public int attDam;
		public GameObject hitBox1;
		public GameObject hitBox2;
	}
	//List of Variables Per Attack
	[System.Serializable]
	public class AttackVariables{
		public AttackStats lightAttack = new AttackStats ();
		public AttackStats mediumAttack = new AttackStats();
		public AttackStats heavyAttack = new AttackStats();
		public AttackStats jumpLightAttack = new AttackStats ();
		public AttackStats jumpMediumAttack = new AttackStats();
		public AttackStats jumpHeavyAttack = new AttackStats();
		public AttackStats crouchLightAttack = new AttackStats ();
		public AttackStats crouchMediumAttack = new AttackStats();
		public AttackStats crouchHeavyAttack = new AttackStats();
	}
	public AttackVariables attackVariables = new AttackVariables();
	//Movement Animation Variables
	[System.Serializable]
	public class MovementAnimationSpeeds{
		[Range(.1f,2f)]
		public float forwardWalk;
		[Range(.1f,2f)]
		public float backwardWalk;
		[Range(.1f,2f)]
		public float forwardDash;
		[Range(.1f,2f)]
		public float backwardDash;
		[Range(.1f,2f)]
		public float crouch;
		[Range(.1f,2f)]
		public float jump;
		[Range(.1f,2f)]
		public float forwardJump;
		[Range(.1f,2f)]
		public float backwardJump;
		[Range(.1f,2f)]
		public float block;
		[Range(.1f,2f)]
		public float takeDamage;
		[Range(.1f,2f)]
		public float idle;


	}
	public MovementAnimationSpeeds moveAnimSpeeds;

	[System.Serializable]
	public class ControllerVariables{
		public float horiDeadZone;
		public float vertDeadZone;
		public float comboTimeOut;
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
	}
	public ControllerVariables controllerVariables = new ControllerVariables ();
	//HiddenVariables
	[HideInInspector]
	public float damage;
	[HideInInspector]
	public Vector3 knockBackDirection;
	[HideInInspector]
	public float knockBackForce;
	[HideInInspector]
	public List<GameObject> lockOnTargets;
	[HideInInspector]
	public GameObject lockOnTarget;
	[HideInInspector]
	public bool facingRight;
	[HideInInspector]
	public bool canMove = true;
	[HideInInspector]
	public bool canAttack = true;
	[HideInInspector]
	public bool canRecieveDamage = true;
	public bool blocking;

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
	StageManager stageManager;
	public Animator anim;



	void Start () {
		CurrFrameType = FrameType.Regular;
		movement = GetComponent<BaseMovement> ();
		hitBoxes = GetComponent<HitDetection> ();
		currentHealth = totalHealth;
		try{
			stageManager = FindObjectOfType<StageManager>();
			if (teamNumber == 1) {
				lockOnTargets.AddRange(stageManager.team2);
				lockOnTarget = lockOnTargets [0];
			}else if(teamNumber == 2){
				lockOnTargets.AddRange(stageManager.team1);
				lockOnTarget = lockOnTargets [0];
			}
		}catch{
			foreach (FighterClass fighter in FindObjectsOfType<FighterClass>()) {
				if (fighter.teamNumber != teamNumber) {
					lockOnTargets.Add(fighter.gameObject);
				}
			}
			lockOnTarget = lockOnTargets [0];
		}
		if (teamNumber == 2) {
			ToggleDirection ();
		}
	}
		
	void Update () {
		if (canAttack) {
			QueueAttackInput ();
		}
		if (canMove) {
			QueueMovementInput ();
		}
		CheckForCombo ();
		if (lockOnTargets.Count != 0){
			if (Input.GetButtonDown (controllerVariables.lockOnInput)) {
				if (lockOnTarget == lockOnTargets [0]) {
					lockOnTarget = lockOnTargets [1];
				} else {
					lockOnTarget = lockOnTargets [0];
				}
				CheckTarget (lockOnTarget);
			}
		}
		if (Input.GetButtonDown(controllerVariables.startButton))
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
		print (anim.speed);
	}


	public void QueueMovementInput(){
		
		//Assume Idle
		anim.SetBool("Walking",false);
		anim.SetBool ("Walking Backwards", false);
		anim.SetBool ("Crouching Idle", false);
		anim.speed = moveAnimSpeeds.idle;
		//Right Input Facing Right
		if (Input.GetAxis (controllerVariables.horiInput) > controllerVariables.horiDeadZone && facingRight) {
			//Forward+Up(R)
			if (Input.GetAxis (controllerVariables.vertInput) > controllerVariables.vertDeadZone && movement.character.isGrounded) {
				canMove = false;
				anim.speed = moveAnimSpeeds.forwardJump;
				movement.DiagonalJump ();
				anim.SetTrigger ("Jumping");



			//Forward+Down(R)
			}else if(Input.GetAxis(controllerVariables.vertInput)< -controllerVariables.vertDeadZone){
				if (breakDownStep1R) {
					comboTimerStarted = true;
					comboTimerEnd = Time.time + controllerVariables.comboTimeOut;
					breakDownStep2 = true;
				}
			//Forward(R)
			} else {
				if (!comboTimerStarted) {
					comboTimerStarted = true;
					comboTimerEnd = Time.time + controllerVariables.comboTimeOut;
					breakDownStep1R = true;
				} else if (coupDeGraceStep2) {
					comboTimerStarted = true;
					comboTimerEnd = Time.time + controllerVariables.comboTimeOut;
					coupDeGraceStep3 = true;
				} else if (dashReset && breakDownStep1R) {
					canMove = false;
					anim.speed = moveAnimSpeeds.forwardDash;
					movement.Dash (Input.GetAxis(controllerVariables.horiInput));
					anim.SetTrigger ("Dash");
				} else {
					movement.Walk ();
					anim.speed = moveAnimSpeeds.forwardWalk;
					anim.SetBool ("Walking", true);
				}
			}
		//Left Input Facing Left
		} else if (Input.GetAxis (controllerVariables.horiInput) < -controllerVariables.horiDeadZone && !facingRight) {
			//Forward+Up(L)
			if (Input.GetAxis (controllerVariables.vertInput) > controllerVariables.vertDeadZone && movement.character.isGrounded) {
				canMove = false;
				anim.speed = moveAnimSpeeds.forwardJump;
				movement.DiagonalJump ();
				anim.SetTrigger ("Jumping");
			//Forward+Down(L)
			}else if(Input.GetAxis(controllerVariables.vertInput)< -controllerVariables.vertDeadZone){
				if (breakDownStep1L) {
					breakDownStep2 = true;
				}
			//Forward(L)
			} else {
				if (!comboTimerStarted) {
					comboTimerStarted = true;
					comboTimerEnd = Time.time + controllerVariables.comboTimeOut;
					breakDownStep1L = true;
				} else if (coupDeGraceStep2) {
					comboTimerStarted = true;
					comboTimerEnd = Time.time + controllerVariables.comboTimeOut;
					coupDeGraceStep3 = true;
				} else if (dashReset && breakDownStep1L) {
					canMove = false;
					anim.speed = moveAnimSpeeds.forwardDash;
					movement.Dash (Input.GetAxis(controllerVariables.horiInput));
					anim.SetTrigger ("Dash");
				} else {
					anim.speed = moveAnimSpeeds.forwardWalk;
					movement.Walk ();
					anim.SetBool ("Walking", true);
				}
			}
		
		//Left Input Facing Right
		} else if (Input.GetAxis (controllerVariables.horiInput) < -controllerVariables.horiDeadZone && facingRight) {
			//Backward+Up(R)
			if (Input.GetAxis (controllerVariables.vertInput) > controllerVariables.horiDeadZone) {
				canMove = false;
				anim.speed = moveAnimSpeeds.backwardJump;
				movement.DiagonalJump ();
				anim.SetTrigger ("Jumping");
				if (breakDownStep3) {
					comboTimerStarted = true;
					comboTimerEnd = Time.time + controllerVariables.comboTimeOut;
					coupDeGraceStep1 = true;
				}
			//Backward+Down(R)
			}else if(Input.GetAxis(controllerVariables.vertInput) < -controllerVariables.vertDeadZone && breakDownStep3){
				comboTimerStarted = true;
				comboTimerEnd = Time.time + controllerVariables.comboTimeOut;
				coupDeGraceStep1 = true;
			//Backward(R)
			} else {
				if (!comboTimerStarted) {
					comboTimerStarted = true;
					comboTimerEnd = Time.time + controllerVariables.comboTimeOut;
					breakDownStep1L = true;
				} else if (coupDeGraceStep1) {
					comboTimerStarted = true;
					comboTimerEnd = Time.time + controllerVariables.comboTimeOut;
					coupDeGraceStep2 = true;
				} else if (dashReset && breakDownStep1L) {
					canMove = false;
					anim.speed = moveAnimSpeeds.backwardDash;
					movement.Dash (Input.GetAxis(controllerVariables.horiInput));
					anim.SetTrigger ("Dash");

				} else {
					anim.speed = moveAnimSpeeds.backwardWalk;
					movement.Walk ();
					anim.SetBool ("Walking Backwards", true);
				}
			}
		//Right Input Facing Left
		} else if (Input.GetAxis (controllerVariables.horiInput) > controllerVariables.horiDeadZone && !facingRight) {
			//Backward+Up(L)
			if (Input.GetAxis (controllerVariables.vertInput) > controllerVariables.vertDeadZone) {
				canMove = false;
				anim.speed = moveAnimSpeeds.backwardJump;
				movement.DiagonalJump ();
				anim.SetTrigger ("Jumping");
				if (breakDownStep3) {
					comboTimerStarted = true;
					comboTimerEnd = Time.time + controllerVariables.comboTimeOut;
					coupDeGraceStep1 = true;
				}
			//Backward+Down(L)
			} else if(Input.GetAxis(controllerVariables.vertInput)< -controllerVariables.vertDeadZone && breakDownStep3){
				comboTimerStarted = true;
				comboTimerEnd = Time.time + controllerVariables.comboTimeOut;
				coupDeGraceStep1 = true;
			//Backward(L)
			}else {
				if (!comboTimerStarted) {
					comboTimerStarted = true;
					comboTimerEnd = Time.time + controllerVariables.comboTimeOut;
					breakDownStep1R = true;
				} else if (coupDeGraceStep1) {
					comboTimerStarted = true;
					comboTimerEnd = Time.time + controllerVariables.comboTimeOut;
					coupDeGraceStep2 = true;
				} else if (dashReset && breakDownStep1R) {
					canMove = false;
					anim.speed = moveAnimSpeeds.backwardDash;
					movement.Dash (Input.GetAxis(controllerVariables.horiInput));
					anim.SetTrigger ("Dash");
				} else {
					anim.speed = moveAnimSpeeds.backwardWalk;
					movement.Walk ();
					anim.SetBool ("Walking Backwards", true);
				}
			}
		//Up input Facing Right
		} else if (Input.GetAxis (controllerVariables.vertInput) > controllerVariables.vertDeadZone && facingRight && movement.character.isGrounded) {
			if (Input.GetAxis (controllerVariables.horiInput) > controllerVariables.horiDeadZone) {
				canMove = false;
				anim.speed = moveAnimSpeeds.forwardJump;
				movement.DiagonalJump ();
				anim.SetTrigger ("Jumping");
			} else if (Input.GetAxis (controllerVariables.horiInput) < -controllerVariables.horiDeadZone) {
				canMove = false;
				anim.speed = moveAnimSpeeds.backwardJump;
				movement.DiagonalJump ();
				anim.SetTrigger ("Jumping");
			} else {
				canMove = false;
				anim.speed = moveAnimSpeeds.jump;
				movement.Jump ();
				anim.SetTrigger ("Jumping");
			}
		//Up Input Facing Left
		} else if (Input.GetAxis (controllerVariables.vertInput) > controllerVariables.vertDeadZone && !facingRight && movement.character.isGrounded) {
			if (Input.GetAxis (controllerVariables.horiInput) > controllerVariables.horiDeadZone) {
				canMove = false;
				anim.speed = moveAnimSpeeds.forwardJump;
				movement.DiagonalJump ();
				anim.SetTrigger ("Jumping");
			} else if (Input.GetAxis (controllerVariables.horiInput) < -controllerVariables.horiDeadZone) {
				canMove = false;
				anim.speed = moveAnimSpeeds.backwardJump;
				movement.DiagonalJump ();
				anim.SetTrigger ("Jumping");
			} else {
				canMove = false;
				anim.speed = moveAnimSpeeds.jump;
				movement.Jump ();
				anim.SetTrigger ("Jumping");
			}
		//Crouch Input
		} else if (Input.GetAxis (controllerVariables.vertInput) < -controllerVariables.vertDeadZone && Input.GetAxis(controllerVariables.horiInput) < controllerVariables.horiDeadZone && Input.GetAxis(controllerVariables.horiInput) > -controllerVariables.horiDeadZone && movement.character.isGrounded) {
			if (breakDownStep2) {
				breakDownStep3 = true;
			}
			anim.speed = moveAnimSpeeds.crouch;
			anim.SetBool ("Crouching Idle", true);
		}
	}

	public void QueueAttackInput(){
		//TeamButton
		if(Input.GetButtonDown(controllerVariables.teamInput)){
			if (coupDeGraceStep3) {
				Debug.Log ("CoupDeGrace");
				anim.SetTrigger ("Coup De Grace");
				StartCoroutine (attackDelay ());
			} else {
				//Address this semester 2
				Debug.Log ("Parry");
				anim.speed = moveAnimSpeeds.block;
				anim.SetTrigger ("Block");
			}
		//Grab/Throw
		} if ((Input.GetButton (controllerVariables.lightInput) && Input.GetButton (controllerVariables.medInput))||Input.GetAxis(controllerVariables.throwInput) > controllerVariables.horiDeadZone) {
			if (facingRight) {
				if (Input.GetAxis (controllerVariables.horiInput) < -controllerVariables.horiDeadZone) {
					Debug.Log ("BackwardThrow(R),");
				} else {
					Debug.Log ("ForwardThrow(R)");
				}
			} else {
				if (Input.GetAxis (controllerVariables.horiInput) > controllerVariables.horiDeadZone) {
					Debug.Log ("BackwardThrow(L),");
				} else {
					Debug.Log ("ForwardThrow(L)");
				}
			}
		//Light Attack
		}else if (Input.GetButtonDown (controllerVariables.lightInput)) {
			if (facingRight) {
				if (movement.character.isGrounded) {
					if (breakDownStep3) {
						Debug.Log ("BreakDown");
						anim.SetTrigger("Break Down");
						StartCoroutine (attackDelay ());
					}else if (Input.GetAxis (controllerVariables.vertInput) < -controllerVariables.vertDeadZone) {
						Debug.Log ("Crouch_LightAttack(R),");
						anim.SetTrigger ("Crouching Light Attack");
						StartCoroutine (attackDelay (attackVariables.crouchLightAttack));
					} else {
						Debug.Log ("LightAttack(R),");
						anim.SetTrigger ("Light Attack");
						StartCoroutine (attackDelay (attackVariables.lightAttack));
					}
				} else {
					Debug.Log ("Jump_LightAttack(R),");
					anim.SetTrigger ("Jumping Light Attack");
					StartCoroutine (attackDelay (attackVariables.jumpLightAttack));
				}
			} else {
				if (movement.character.isGrounded) {
					if (breakDownStep3) {
						Debug.Log ("BreakDown");
						anim.SetTrigger ("Break Down");
						StartCoroutine (attackDelay ());
					}else if (Input.GetAxis (controllerVariables.vertInput) < -controllerVariables.vertDeadZone) {
						Debug.Log ("Crouch_LightAttack(L),");
						anim.SetTrigger ("Crouching Light Attack");
						StartCoroutine (attackDelay (attackVariables.crouchLightAttack));
					} else {
						Debug.Log ("LightAttack(L),");
						anim.SetTrigger ("Light Attack");
						StartCoroutine (attackDelay (attackVariables.lightAttack));
					}
				} else {
					Debug.Log ("Jump_LightAttack(L),");
					anim.SetTrigger ("Jumping Light Attack");
					StartCoroutine (attackDelay (attackVariables.jumpLightAttack));
				}
			}
		//Medium Attack
		} else if (Input.GetButtonDown (controllerVariables.medInput)) {
			if (facingRight) {
				if (movement.character.isGrounded) {
					if (Input.GetAxis (controllerVariables.vertInput) < -controllerVariables.vertDeadZone) {
						Debug.Log ("Crouch_MedAttack(R),");
						anim.SetTrigger ("Crouching Medium Attack");
						StartCoroutine (attackDelay (attackVariables.crouchMediumAttack));
					} else {
						Debug.Log ("MedAttack(R),");
						anim.SetTrigger ("Medium Attack");
						StartCoroutine (attackDelay (attackVariables.mediumAttack));
					}
				} else {
					Debug.Log ("Jump_MedAttack(R),");
					anim.SetTrigger ("Jumping Medium Attack");
					StartCoroutine (attackDelay (attackVariables.jumpMediumAttack));
				}
			} else {
				if (movement.character.isGrounded) {
					if (Input.GetAxis (controllerVariables.vertInput) < -controllerVariables.vertDeadZone) {
						Debug.Log ("Crouch_MedAttack(L),");
						anim.SetTrigger ("Crouching Medium Attack");
						StartCoroutine (attackDelay (attackVariables.crouchMediumAttack));
					} else {
						Debug.Log ("MedAttack(L),");
						anim.SetTrigger ("Medium Attack");
						StartCoroutine (attackDelay (attackVariables.mediumAttack));
					}

				} else {
					Debug.Log ("Jump_MedAttack(L),");
					anim.SetTrigger ("Jumpimg Medium Attack");
					StartCoroutine (attackDelay (attackVariables.jumpMediumAttack));
				}
			}
		//Heavy Attack
		} else if (Input.GetButtonDown (controllerVariables.heavyInput)) {
			if (facingRight) {
				if (movement.character.isGrounded) {
					if (Input.GetAxis (controllerVariables.vertInput) < -controllerVariables.vertDeadZone) {
						Debug.Log ("Crouch_HeavyAttack(R),");
						anim.SetTrigger ("Crouching Heavy Attack");
						StartCoroutine (attackDelay (attackVariables.crouchHeavyAttack));
					} else {
						Debug.Log ("HeavyAttack(R),");
						anim.SetTrigger ("Heavy Attack");
						StartCoroutine (attackDelay (attackVariables.heavyAttack));
					}
				} else {
					Debug.Log ("Jump_HeavyAttack(R),");
					anim.SetTrigger ("Jumping Heavy Attack");
					StartCoroutine (attackDelay (attackVariables.jumpHeavyAttack));
				}
			} else {
				if (movement.character.isGrounded) {
					if (Input.GetAxis (controllerVariables.vertInput) < -controllerVariables.vertDeadZone) {
						Debug.Log ("Crouch_HeavyAttack(L),");
						anim.SetTrigger ("Crouching Heavy Attack");
						StartCoroutine (attackDelay (attackVariables.crouchHeavyAttack));
					} else {
						Debug.Log ("HeavyAttack(L),");
						anim.SetTrigger ("Heavy Attack");
						StartCoroutine (attackDelay (attackVariables.heavyAttack));
					}
				} else {
					Debug.Log ("Jump_HeavyAttack(L),");
					anim.SetTrigger ("Jumping Heavy Attack");
					StartCoroutine (attackDelay (attackVariables.jumpHeavyAttack));
				}
			}
		} 
	}
		
	public void CheckForCombo(){
		if (Input.GetAxis (controllerVariables.horiInput) == 0 &&( breakDownStep1L || breakDownStep1R) && !breakDownStep3) {
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
		//flip = false;
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
		
	IEnumerator attackDelay(){
		canAttack = false;
		canMove = false;
		yield return new WaitForSeconds (anim.GetCurrentAnimatorClipInfo (0).Length);
		canAttack = true;
		canMove = true;
	}

	IEnumerator attackDelay(AttackStats attack){
		canAttack = false;
		canMove = false;
		damage = attack.attDam;
		if (facingRight) {
			knockBackDirection = attack.knockBackDirection;
		}else{
			knockBackDirection = -attack.knockBackDirection;
		}
		knockBackForce = attack.knockBackForce;
		if (attack.hitBox1 != null) {
			attack.hitBox1.SetActive (true);
		}
		if (attack.hitBox2 != null) {
			attack.hitBox2.SetActive (true);
		}
		yield return new WaitForSeconds (anim.GetCurrentAnimatorClipInfo (0).Length);
		if (attack.hitBox1 != null) {
			attack.hitBox1.SetActive (false);
		}
		if (attack.hitBox2 != null) {
			attack.hitBox2.SetActive (false);
		}
		damage = 0;
		canAttack = true;
		canMove = true;
	}
}
