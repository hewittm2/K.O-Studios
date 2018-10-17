//Created By Ethan Quandt 8/29/18
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterClass : MonoBehaviour {
	
	//Class Variables

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

	//Movement
	public float jumpHeight;
	public float jumpTime;
	public float moveSpeed;
	public bool jumping = false;
	public bool canAttack;
	public bool facingRight = true;
	public bool canInput;

	//Parts
	public GameObject wholeBody;
	public GameObject upperBody;
	public GameObject lowerBody;
	public GameObject highHit;
	public GameObject midHit;
	public GameObject lowHit;

	//Attacks
	public float comboTimer;
	//Light Attack
	//Medium Attack
	//Heavy Attack
	//Crouching Light Attack
	//Crouching Medium Attack
	//Crouching Heavy Attack
	//Jumping Light Attack
	//Jumnping Medium Attack
	//Jumping Heavy Attack
	//Special Attack
	//Special Attack Forward
	//Special Attack Backward
	//Special Attack Jumping
	//Special Attack Crouching
    //Grab
	//

    //Controller
    public string horiInput;
	public string rightDpad;
	public string leftDpad;
	public string vertInput;
    public string downInput;
    public string upInput;
    public string lightInput;
    public string medInput;
    public string heavyInput;
    public string specialInput;
	public string grabInput;
	public string dashInput;

    //combo list / registering
    public List<string> comboNames = new List<string>();
	public List<string> comboInputs = new List<string>();

	private string inputQueue = "";
	private List<string> possibleComboQueue = new List<string> ();

	BaseMovement movement;
	//private bool checkForCombo = false;

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
		if (canInput) {
			QueueInput ();
			RegisterQueue ();
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





	public void QueueInput(){
		//TODO change to be called on input press
		//Right Input Facing Right
		if(Input.GetKeyDown(KeyCode.Space)){
			facingRight = !facingRight;
			gameObject.transform.Rotate (new Vector3 (0, 180, 0));
		}
			
		if (Input.GetAxis (horiInput) > 0.1f && facingRight) {
			if (Input.GetAxis (vertInput) > 0.1f) {
				inputQueue += "Forward+Up,";
			} else if(Input.GetButtonDown(dashInput)){
				canInput = false;
				movement.Dash ();
				inputQueue += "Dash";
				//Down Input
			} else {
				inputQueue += "Forward,";
				movement.Walk();
			}
			//Right Input Facing Left
		} else if (Input.GetAxis (horiInput) > 0.1f && !facingRight) {
			if (Input.GetAxis (vertInput) > 0.1f) {
				inputQueue += "Backward+Up,";
			}else if(Input.GetButtonDown(dashInput)){
				canInput = false;
				movement.Dash ();
				inputQueue += "Dash";
				//Down Input
			}  else {
				inputQueue += "Backward,";
				movement.Walk();
			}
			//Left Input Facing Right
		} else if (Input.GetAxis (horiInput) < -0.1f && facingRight) {
			if (Input.GetAxis (vertInput) > 0.1f) {
				inputQueue += "Backward+Up";
			} else if(Input.GetButtonDown(dashInput)){
				canInput = false;
				movement.Dash ();
				inputQueue += "Dash";
				//Down Input
			} else {
				inputQueue += "Backward,";
				movement.Walk();
			}        
			//Left Input Facing Left
		} else if (Input.GetAxis (horiInput) < -0.1f && !facingRight) {
			if (Input.GetAxis (vertInput) > 0.1f) {
				inputQueue += "Forward+Up,";
			} else if(Input.GetButtonDown(dashInput)){
				gameObject.GetComponent<BaseMovement> ().Dash ();
				inputQueue += "Dash";
				//Down Input
			} else {
				inputQueue += "Forward,";
				movement.Walk();
			}
			//Up input Facing Right
		} else if (Input.GetAxis (vertInput) > 0.1f && facingRight && !jumping) {
			if (Input.GetAxis (horiInput) > 0.1f) {
				inputQueue += "Up+Forward,";
			} else if (Input.GetAxis (horiInput) < -0.1f) {
				inputQueue += "Up+Backward,";
			} else {
				inputQueue += "Up,";
				movement.Jump ();
			}
			//Up Input Facing Left
		} else if (Input.GetAxis (vertInput) > 0.1f && !facingRight && !jumping) {
			if (Input.GetAxis (horiInput) > 0.1f) {
				inputQueue += "Up+Backward,";
			} else if (Input.GetAxis (horiInput) < -0.1f) {
				inputQueue += "Up+Forward,";
			} else {
				inputQueue += "Up,";
				jumping = true;
				movement.Jump ();
			}
		}else if(Input.GetButtonDown(dashInput)){
			gameObject.GetComponent<BaseMovement> ().Dash ();
		//Down Input
		} else if (Input.GetAxis(vertInput)< -0.1f && !jumping) {
			if (Input.GetButtonDown (lightInput)) {
				inputQueue += "CrouchLightAttack,";
			} else if (Input.GetButtonDown (medInput)) {
				inputQueue += "CrouchMedAtt,";
			} else if (Input.GetButtonDown (heavyInput)) {
				inputQueue += "CrouchHeavyAtt,";
			} else {
				inputQueue += "Crouch,";
			}
		//Light Attack
//		}else if(lightInput){
//			inputQueue += "lightAttack";
//			lightInput = false;
		}



        Debug.Log (inputQueue);
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
//			foreach(){
//				
//			}
		}
		if (possibleComboQueue.Count == 0) {
			ResetQueue ();
		}

	}

	public void ResetQueue(){
		inputQueue = "";
		possibleComboQueue = comboInputs;
	}

}
