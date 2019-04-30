//created by Mike on 3/23
//updated by mike on 4/1
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimation : MonoBehaviour {

	Transform[] Fingers = new Transform[4];
	Transform[][] Digits = new Transform[4][];
	Transform Thumb;
	Transform[] ThumbDigits = new Transform[2];
	public float FistAngle = 90f;
	public float Speed = 1;
	public bool MakeFist = false;
	public bool releaseFist = false;
	public bool FingerGun = false;
	public bool ReleaseFingerGun = false;
	public float t = 0;
	public enum Hand{
		Right,
		Left
	}
	public Hand myHand = Hand.Right;

	private enum HandState{
		Open,
		Fist,
		FingerGun,
		Transition
	}
	[SerializeField]
	private HandState CurrentHandState = HandState.Open;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 4; i++) {
//assigns finger parent
			Fingers [i] = transform.GetChild (i);
			Digits[i] = new Transform[2];
//assigns knuckes/digits
			Digits [i] [0] = Fingers[i].GetChild (0);
			Digits [i] [1] = Fingers[i].GetChild (0).GetChild (0);
		}
		Thumb = transform.GetChild (4);
		ThumbDigits [0] = Thumb.GetChild (0);
		ThumbDigits [1] = ThumbDigits[0].GetChild (0);
	}
	
	// Update is called once per frame
	void LateUpdate () {
		
////MakeFist/FingerGun
		/// 
		if (MakeFist) {
			if (CurrentHandState != HandState.Fist) {
				CurrentHandState = HandState.Transition;
//loop for finger parent rotation
				//fc.anim.GetCurrentAnimatorClipInfo().Length
				for (int i = 0; i < 4; i++) {
				
					Fingers [i].localEulerAngles = new Vector3 (Mathf.LerpAngle (Fingers [i].localEulerAngles.x, FistAngle, t), Fingers [i].localEulerAngles.y, Fingers [i].localEulerAngles.z);
//loop for digit rotation
					for (int j = 0; j < 2; j++) {
						Digits [i] [j].localEulerAngles = new Vector3 (Mathf.LerpAngle (Fingers [i].localEulerAngles.x, FistAngle, t), Digits [i] [j].localEulerAngles.y, Digits [i] [j].localEulerAngles.z);
					}
				}
//thumb control
				for (int x = 1; x < 2; x++) {
					if (myHand == Hand.Left)
						ThumbDigits [x].localEulerAngles = new Vector3 (ThumbDigits [x].localEulerAngles.x, ThumbDigits [x].localEulerAngles.y, Mathf.LerpAngle (ThumbDigits [x].localEulerAngles.z, FistAngle, t));
					else
						ThumbDigits [x].localEulerAngles = new Vector3 (ThumbDigits [x].localEulerAngles.x, ThumbDigits [x].localEulerAngles.y, Mathf.LerpAngle (ThumbDigits [x].localEulerAngles.z, -FistAngle, t));
				}
				t += Speed * 0.1f;
			} else {
				MakeFist = false;
			}



		} else if(FingerGun){
			CurrentHandState = HandState.Transition;
//loop for finger parent rotation
			for (int x = 0; x < 2; x++) {
				Fingers [x].localEulerAngles = new Vector3 (Mathf.LerpAngle (Fingers [x].localEulerAngles.x, 0, t), Fingers [x].localEulerAngles.y, Fingers [x].localEulerAngles.z);
				for (int j = 0; j < 2; j++) {
					Digits [x] [j].localEulerAngles = new Vector3 (Mathf.LerpAngle (Fingers [x].localEulerAngles.x, 0, t), Digits [x] [j].localEulerAngles.y, Digits [x] [j].localEulerAngles.z);
				}
			}
			for (int i = 2; i < 4; i++) {
				Fingers [i].localEulerAngles = new Vector3 (Mathf.LerpAngle(Fingers [i].localEulerAngles.x, FistAngle, t), Fingers [i].localEulerAngles.y, Fingers [i].localEulerAngles.z);
//loop for digit rotation
				for (int j = 0; j < 2; j++) {
					Digits[i][j].localEulerAngles = new Vector3 (Mathf.LerpAngle(Fingers [i].localEulerAngles.x, FistAngle, t), Digits[i][j].localEulerAngles.y, Digits[i][j].localEulerAngles.z);
				}
			}
//thumb control 
//			for (int x = 0; x < 2; x++) {
//				ThumbDigits[x].localEulerAngles = new Vector3 (Thumb.localEulerAngles.x, Thumb.localEulerAngles.y, -Mathf.Lerp(0, FistAngle/2, t));
//			}

			t += Speed * 0.1f;
		}

////Release fist/fingergun
		if (releaseFist) {
			CurrentHandState = HandState.Transition;
//loop for finger parent rotation
			for (int i = 0; i < 4; i++) {

				Fingers [i].localEulerAngles = new Vector3 (Mathf.LerpAngle(FistAngle, 10, t), Fingers [i].localEulerAngles.y, Fingers [i].localEulerAngles.z);
//loop for digit rotation
				for (int j = 0; j < 2; j++) {
					Digits[i][j].localEulerAngles = new Vector3 (Mathf.LerpAngle(FistAngle, 10, t), Digits[i][j].localEulerAngles.y, Digits[i][j].localEulerAngles.z);
				}
			}
//thumb control
			for (int x = 0; x < 2; x++) {
				ThumbDigits[x].localEulerAngles = new Vector3 (ThumbDigits[x].localEulerAngles.x, ThumbDigits[x].localEulerAngles.y, -Mathf.LerpAngle(FistAngle/2, 10, t));
			}
			t += Speed * 0.1f;

//release
		} else if(ReleaseFingerGun){
			CurrentHandState = HandState.Transition;
//loop for finger parent rotation
			for (int i = 2; i < 4; i++) {
				Fingers [i].localEulerAngles = new Vector3 (Mathf.LerpAngle(FistAngle, 0, t), Fingers [i].localEulerAngles.y, Fingers [i].localEulerAngles.z);

//loop for digit rotation
				for (int j = 0; j < 2; j++) {
					Digits[i][j].localEulerAngles = new Vector3 (Mathf.LerpAngle(FistAngle, 0, t), Digits[i][j].localEulerAngles.y, Digits[i][j].localEulerAngles.z);
				}
			}
//thumb control 
//			for (int x = 0; x < 2; x++) {
//				ThumbDigits[x].localEulerAngles = new Vector3 (Thumb.localEulerAngles.x, Thumb.localEulerAngles.y, -Mathf.Lerp(0, FistAngle/2, t));
//			}

			t += Speed * 0.1f;
		}



//keeps fingers in place while not in transition state
		if (t == 0) {
			switch(CurrentHandState){
			case HandState.Fist:
				for (int i = 0; i < 4; i++) {
					Fingers [i].localEulerAngles = new Vector3 (Mathf.LerpAngle(Fingers [i].localEulerAngles.x, FistAngle, 1), Fingers [i].localEulerAngles.y, Fingers [i].localEulerAngles.z);
					//loop for digit rotation
					for (int j = 0; j < 2; j++) {
						Digits[i][j].localEulerAngles = new Vector3 (Mathf.LerpAngle(Fingers [i].localEulerAngles.x, FistAngle, 1), Digits[i][j].localEulerAngles.y, Digits[i][j].localEulerAngles.z);
					}
				}
				//thumb control
				for (int x = 1; x < 2; x++) {
					if(myHand == Hand.Left)
						ThumbDigits[x].localEulerAngles = new Vector3 (ThumbDigits[x].localEulerAngles.x, ThumbDigits[x].localEulerAngles.y, Mathf.LerpAngle(ThumbDigits[x].localEulerAngles.z, FistAngle, 1));
					else
						ThumbDigits[x].localEulerAngles = new Vector3 (ThumbDigits[x].localEulerAngles.x, ThumbDigits[x].localEulerAngles.y, Mathf.LerpAngle(ThumbDigits[x].localEulerAngles.z, -FistAngle, 1));
				}
				break;
			case HandState.FingerGun:
				for (int x = 0; x < 2; x++) {
					Fingers [x].localEulerAngles = new Vector3 (Mathf.LerpAngle (Fingers [x].localEulerAngles.x, 0, 1), Fingers [x].localEulerAngles.y, Fingers [x].localEulerAngles.z);
					for (int j = 0; j < 2; j++) {
						Digits [x] [j].localEulerAngles = new Vector3 (Mathf.LerpAngle (Fingers [x].localEulerAngles.x, 0, 1), Digits [x] [j].localEulerAngles.y, Digits [x] [j].localEulerAngles.z);
					}
				}
				for (int i = 2; i < 4; i++) {
					Fingers [i].localEulerAngles = new Vector3 (Mathf.LerpAngle(Fingers [i].localEulerAngles.x, FistAngle, 1), Fingers [i].localEulerAngles.y, Fingers [i].localEulerAngles.z);
					//loop for digit rotation
					for (int j = 0; j < 2; j++) {
						Digits[i][j].localEulerAngles = new Vector3 (Mathf.LerpAngle(Fingers [i].localEulerAngles.x, FistAngle, 1), Digits[i][j].localEulerAngles.y, Digits[i][j].localEulerAngles.z);
					}
				}
				break;
			default :
				break;
			}
		}
//val reset
		if (t > 1 || t == 1) {

			if (MakeFist)
				CurrentHandState = HandState.Fist;
			if (FingerGun)
				CurrentHandState = HandState.FingerGun;
			if (releaseFist || ReleaseFingerGun)
				CurrentHandState = HandState.Open;
			
			MakeFist = false;
			FingerGun = false;
			ReleaseFingerGun = false;
			releaseFist = false;
			t = 0;
		}
	}
}
