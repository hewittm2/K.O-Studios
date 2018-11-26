//Created By Ethan Quandt 8/29/18
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


	//Health
	public int currentHealth;
	public int totalHealth;
	public float defValue;
	public bool canRecieveDamage;

	//Movement Variables
	BaseMovement movement;
	public bool facingRight = true;
	public bool canMove = true;
	public bool canAttack = true;

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
	public string dashInput;

	//Controller Inputs: Need to Implement
	//public string horiDpad;
	//public string vertDpad;

    //combo list / registering
    public List<string> comboNames = new List<string>();
	public List<string> comboInputs = new List<string>();
	private string inputQueue = "";

	private List<string> possibleComboQueue = new List<string> ();
	public float comboTimer;
	//private bool checkForCombo = false;

	public Animator anim;


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
		RegisterQueue ();
		//Debug.Log (inputQueue);
	}

	public void QueueMovementInput(){
		//test
		anim.SetBool ("Walking", false);
		
		if(Input.GetKeyDown(KeyCode.Space)){
			facingRight = !facingRight;
			gameObject.transform.Rotate (new Vector3 (0, 180, 0));
		}
		//Right Input Facing Right
		if (Input.GetAxis (horiInput) > 0.1f && facingRight) {
			if (Input.GetAxis (vertInput) > 0.1f) {
				inputQueue += "Forward+Up(R),";
			} else if (Input.GetButtonDown (dashInput)) {
				canMove = false;
				movement.Dash ();
				inputQueue += "Forward+Dash(R),";
				//Down Input
			} else {
				canMove = false;
				inputQueue += "Forward(R),";
				anim.SetBool ("Walking", true);
				movement.Walk ();
			}
			//Right Input Facing Left
		} else if (Input.GetAxis (horiInput) > 0.1f && !facingRight) {
			if (Input.GetAxis (vertInput) > 0.1f) {
				inputQueue += "Backward+Up(L),";
			} else if (Input.GetButtonDown (dashInput)) {
				canMove = false;
				movement.Dash ();
				inputQueue += "Backward+Dash(L)";
				//Down Input
			} else {
				canMove = false;
				inputQueue += "Backward(L),";
				anim.SetBool ("Walking", true);
				movement.Walk ();
			}
			//Left Input Facing Right
		} else if (Input.GetAxis (horiInput) < -0.1f && facingRight) {
			if (Input.GetAxis (vertInput) > 0.1f) {
				inputQueue += "Backward+Up(R),";
			} else if (Input.GetButtonDown (dashInput)) {
				canMove = false;
				movement.Dash ();
				inputQueue += "Backward+Dash(R),";
				//Down Input
			} else {
				canMove = false;
				inputQueue += "Backward(R),";
				anim.SetBool ("Walking", true);
				movement.Walk ();
			}        
			//Left Input Facing Left
		} else if (Input.GetAxis (horiInput) < -0.1f && !facingRight) {
			if (Input.GetAxis (vertInput) > 0.1f) {
				inputQueue += "Forward+Up(L),";
			} else if (Input.GetButtonDown (dashInput)) {
				canMove = false;
				movement.Dash ();
				inputQueue += "Forward+Dash(L)";
				//Down Input
			} else {
				canMove = false;
				inputQueue += "Forward(L),";
				anim.SetBool ("Walking", true);
				movement.Walk ();
			}
			//Up input Facing Right
		} else if (Input.GetAxis (vertInput) > 0.1f && facingRight && movement.character.isGrounded) {
			if (Input.GetAxis (horiInput) > 0.1f) {
				inputQueue += "Up+Forward(R),";
			} else if (Input.GetAxis (horiInput) < -0.1f) {
				inputQueue += "Up+Backward(R),";
			} else {
				canMove = false;
				inputQueue += "Up(R),";
				movement.Jump ();
			}
			//Up Input Facing Left
		} else if (Input.GetAxis (vertInput) > 0.1f && !facingRight && movement.character.isGrounded) {
			if (Input.GetAxis (horiInput) > 0.1f) {
				inputQueue += "Up+Backward(L),";
			} else if (Input.GetAxis (horiInput) < -0.1f) {
				inputQueue += "Up+Forward(L),";
			} else {
				inputQueue += "Up(L),";
				canMove = false;
				movement.Jump ();
			}
			//Crouch Input
		} else if (Input.GetAxis (vertInput) < -0.1f && movement.character.isGrounded) {
//			if (Input.GetButtonDown (lightInput)) {
//				if (facingRight) {
//					inputQueue += "CrouchLightAttack(R),";
//				} else {
//					inputQueue += "CrouchLightAttack(L),";
//				}
//			} else if (Input.GetButtonDown (medInput)) {
//				if (facingRight) {
//					inputQueue += "CrouchMedAtt(R),";
//				} else {
//					inputQueue += "CrouchMedAtt(L),";
//				}
//			} else if (Input.GetButtonDown (heavyInput)) {
//				if (facingRight) {
//					inputQueue += "CrouchHeavyAtt(R),";
//				} else {
//					inputQueue += "CrouchHeavyAtt(L),";
//				}
//			} else {
				inputQueue += "Crouch,";
			}
		Debug.Log (inputQueue);
	}

	public void QueueAttackInput(){
		//Grab/Throw
		if ((Input.GetButton (lightInput) && Input.GetButton (medInput))||Input.GetAxis(throwInput) > .1) {
			if (facingRight) {
				if (Input.GetAxis (horiInput) < -0.1f) {
					Debug.Log ("BackwardThrow(R),");
					canAttack = false;
					StartCoroutine (AttackDelay());
				} else {
					Debug.Log ("ForwardThrow(R)");
					canAttack = false;
					StartCoroutine (AttackDelay());
				}
			} else {
				if (Input.GetAxis (horiInput) > 0.1f) {
					Debug.Log ("BackwardThrow(L),");
					canAttack = false;
					StartCoroutine (AttackDelay());
				} else {
					Debug.Log ("ForwardThrow(L)");
					canAttack = false;
					StartCoroutine (AttackDelay());
				}
			}
		//Light Attack
		}else if (Input.GetButtonDown (lightInput)) {
			if (facingRight) {
				if (movement.character.isGrounded) {
					if (Input.GetAxis (vertInput) < -0.1f) {
						//inputQueue += "Crouch_LightAttack(R),";
						Debug.Log ("Crouch_LightAttack(R),");
						canAttack = false;
						StartCoroutine (AttackDelay());
					} else {
						//inputQueue += "LightAttack(R),";
						anim.SetTrigger("Light Attack");
						Debug.Log ("LightAttack(R),");
						canAttack = false;
						StartCoroutine (AttackDelay());
					}
				} else {
					//inputQueue += "Jump_lightAttack(R)";
					Debug.Log ("Jump_LightAttack(R),");
					canAttack = false;
					StartCoroutine (AttackDelay());
				}
			} else {
				if (movement.character.isGrounded) {
					if (Input.GetAxis (vertInput) < -0.1f) {
						//inputQueue += "Crouch_LightAttack(L)";
						Debug.Log ("Crouch_LightAttack(L),");
						canAttack = false;
						StartCoroutine (AttackDelay());
					} else {
						//inputQueue += "LightAttack(L)";
						anim.SetTrigger("Light Attack");
						Debug.Log ("LightAttack(L),");
						canAttack = false;
						StartCoroutine (AttackDelay());
					}
				} else {
					//inputQueue += "Jump_LightAttack(L)";
					Debug.Log ("Jump_LightAttack(L),");
					canAttack = false;
					StartCoroutine (AttackDelay());
				}
			}
			//Medium Attack
		} else if (Input.GetButtonDown (medInput)) {
			if (facingRight) {
				if (movement.character.isGrounded) {
					if (Input.GetAxis (vertInput) < -0.1f) {
						//inputQueue += "Crouch_MedAttack(R)";
						Debug.Log ("Crouch_MedAttack(R),");
						canAttack = false;
						StartCoroutine (AttackDelay());
					} else {
						//inputQueue += "MedAttack(R)";
						anim.SetTrigger("Medium Attack");
						Debug.Log ("MedAttack(R),");
						canAttack = false;
						StartCoroutine (AttackDelay());
					}
				} else {
					//inputQueue += "Jump_MedAttack(R)";
					Debug.Log ("Jump_MedAttack(R),");
					canAttack = false;
					StartCoroutine (AttackDelay());
				}
			} else {
				if (movement.character.isGrounded) {
					if (Input.GetAxis (vertInput) < -0.1f) {
						//inputQueue += "Crouch_MedAttack(L)";
						Debug.Log ("Crouch_MedAttack(L),");
						canAttack = false;
						StartCoroutine (AttackDelay());
					} else {
						//inputQueue += "MedAttack(L)";
						anim.SetTrigger("Medium Attack");
						Debug.Log ("MedAttack(L),");
						canAttack = false;
						StartCoroutine (AttackDelay());
					}

				} else {
					//inputQueue += "Jump_MedAttack(L)";
					Debug.Log ("Jump_MedAttack(L),");
					canAttack = false;
					StartCoroutine (AttackDelay());
				}
			}
			//Heavy Attack
		} else if (Input.GetButtonDown (heavyInput)) {
			if (facingRight) {
				if (movement.character.isGrounded) {
					if (Input.GetAxis (vertInput) < -0.1f) {
						//inputQueue += "Crouch_HeavyAttack(R)";
						Debug.Log ("Crouch_HeavyAttack(R),");
						canAttack = false;
						StartCoroutine (AttackDelay());
					} else {
						//inputQueue += "HeavyAttack(R)";
						anim.SetTrigger("Heavy Attack");
						Debug.Log ("HeavyAttack(R),");
						canAttack = false;
						StartCoroutine (AttackDelay());
					}
				} else {
					//inputQueue += "Jump_HeavyAttack(R)";
					Debug.Log ("Jump_HeavyAttack(R),");
					canAttack = false;
					StartCoroutine (AttackDelay());
				}
			} else {
				if (movement.character.isGrounded) {
					if (Input.GetAxis (vertInput) < -0.1f) {
						//inputQueue += "Crouch_HeavyAttack(L)";
						Debug.Log ("Crouch_HeavyAttack(L),");
						canAttack = false;
						StartCoroutine (AttackDelay());
					} else {
						//inputQueue += "HeavyAttack(L)";
						anim.SetTrigger("Heavy Attack");
						Debug.Log ("HeavyAttack(L),");
						canAttack = false;
						StartCoroutine (AttackDelay());
					}
				} else {
					//inputQueue += "Jump_HeavyAttack(L)";
					Debug.Log ("Jump_HeavyAttack(L),");
					canAttack = false;
					StartCoroutine (AttackDelay());
				}
			}
		} 
		//Debug.Log (inputQueue);
	}

	public void RegisterQueue(){
		List<string> temp = possibleComboQueue;
		foreach (string _Combo in possibleComboQueue) {
			if (!_Combo.Contains (inputQueue)) {
				temp.Remove(_Combo);
			}
		}
		possibleComboQueue = temp;
		temp = null;
		if (possibleComboQueue.Count == 1 && possibleComboQueue.Count != comboInputs.Count) {
			//do combo
			Debug.Log("Combo Met = " + possibleComboQueue[0]);
		}
		if (possibleComboQueue.Count == 0) {
			ResetQueue ();
		}

	}

	public IEnumerator AttackDelay (){
		yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length);
		Debug.Log ("Can attack again");
		canAttack = true;
	}

	public void ResetQueue(){
		inputQueue = "";
		possibleComboQueue = comboInputs;
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
