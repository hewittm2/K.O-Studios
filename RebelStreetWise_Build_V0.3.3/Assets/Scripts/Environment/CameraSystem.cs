//Created By Mike K and Brian A
//edited by none
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour {

	public List<Transform> Characters = new List<Transform>();
	public float MinCloseness;
	public float MaxCloseness;
	//default start = 0,1,-4 = FOV 60
	public bool ReadyToTrack = false;
	public float MaxDistanceToResize;
	public float MinDistanceToResize;
	public float resizeIncrement;

	public List<Transform> furtherstApartPair = new List<Transform>();
	//lerp vals
	public float LerpSpeed;
	float a;
	float b = -10;
	float t = 0;
	bool CanLerp = false;
	float currDist;
	Vector3 center;
	// Use this for initialization
	void Start () {
		//Characters.Add (FindObjectOfType<FighterClass>().transform);
		Characters.Clear();
		foreach(FighterClass  f in FindObjectsOfType<FighterClass>()){
			Characters.Add (f.transform);
		}
			furtherstApartPair.Add (Characters [0]);
			furtherstApartPair.Add (Characters [1]);

	}

	
	// Update is called once per frame
	void Update () {
		if(ReadyToTrack){
//			if (Characters.Count == 2) {
//				center = new Vector3 ((Characters [0].position.x + Characters [1].position.x) / 2f,
//					(Characters [0].position.y + Characters [1].position.y) / 2f + 1f, 0f);
//				currDist = Mathf.Abs (Characters [0].transform.position.x - Characters [1].transform.position.x);
//				if (furtherstApartPair.Count == 0) {
//					furtherstApartPair.Add (Characters [0]);
//					furtherstApartPair.Add (Characters [1]);
//
//				}
//	
//
//			} else if (Characters.Count == 4) {
				//have camera track midpoint of character locations
				center = new Vector3 ((Characters [0].position.x + Characters [1].position.x + Characters [2].position.x + Characters [3].position.x) / 4f,
					                (Characters [0].position.y + Characters [1].position.y + Characters [2].position.y + Characters [3].position.y) / 4f + 1f, 0f);

				//clac furtherest apart pair
				currDist = Mathf.Abs (Characters [0].transform.position.x - Characters [1].transform.position.x);

				if (Mathf.Abs (Characters [1].transform.position.x - Characters [2].transform.position.x) > currDist) {
					currDist = Mathf.Abs (Characters [1].transform.position.x - Characters [2].transform.position.x);
					furtherstApartPair.Clear ();
					furtherstApartPair.Add (Characters [1]);
					furtherstApartPair.Add (Characters [2]);
				}
				if (Mathf.Abs (Characters [2].transform.position.x - Characters [3].transform.position.x) > currDist) {
					currDist = Mathf.Abs (Characters [2].transform.position.x - Characters [3].transform.position.x);
					furtherstApartPair.Clear ();
					furtherstApartPair.Add (Characters [2]);
					furtherstApartPair.Add (Characters [3]);
				}
				if (Mathf.Abs (Characters [3].transform.position.x - Characters [0].transform.position.x) > currDist) {
					currDist = Mathf.Abs (Characters [3].transform.position.x - Characters [0].transform.position.x);
					furtherstApartPair.Clear ();
					furtherstApartPair.Add (Characters [3]);
					furtherstApartPair.Add (Characters [0]);
				}
				if (Mathf.Abs (Characters [2].transform.position.x - Characters [0].transform.position.x) > currDist) {
					currDist = Mathf.Abs (Characters [2].transform.position.x - Characters [0].transform.position.x);
					furtherstApartPair.Clear ();
					furtherstApartPair.Add (Characters [2]);
					furtherstApartPair.Add (Characters [0]);
				}
				if (Mathf.Abs (Characters [3].transform.position.x - Characters [1].transform.position.x) > currDist) {
					currDist = Mathf.Abs (Characters [3].transform.position.x - Characters [1].transform.position.x);
					furtherstApartPair.Clear ();
					furtherstApartPair.Add (Characters [3]);
					furtherstApartPair.Add (Characters [1]);
				}
			//}
			//Debug.Log (currDist);
	//check zoom out condition

			if(currDist > MaxDistanceToResize){
				if (transform.position.z - resizeIncrement > MaxCloseness) {
					a = transform.position.z;
					b = transform.position.z - resizeIncrement;
					if (t != 0) {
						t = 0;
					}
					CanLerp = true;
					Debug.Log ("Zoomout");
					MaxDistanceToResize += 1f;
					MinDistanceToResize += 1f;
				}
				
			} else {
	//check zoom in condition
				if(currDist < MinDistanceToResize){
					if (transform.position.z + resizeIncrement < MinCloseness) {
						a = transform.position.z;
						b = transform.position.z + resizeIncrement;
						if (t != 0) {
							t = 0;
						}
						CanLerp = true;
						Debug.Log ("Zoomin");
						MaxDistanceToResize -= 1f;
						MinDistanceToResize -= 1f;
					}

				}
			}
	//Lerp to desired size
			if (CanLerp) {
				t += LerpSpeed * Time.deltaTime;
				transform.position = new Vector3 (transform.position.x, transform.position.y, Mathf.Lerp(a,b,t));
				if (t > 1.0f) {
					
					CanLerp = false;
					t = 0.0f;
				}
			}
			//Debug.Log (b);
			transform.position = new Vector3 (center.x, center.y, Mathf.Clamp(transform.position.z, MaxCloseness, MinCloseness));
			//transform.position = new Vector3(center.x , center.y, 0);
		}
	}

	public void AddPlayerToList(Transform trans){
		Characters.Add (trans);
		if (Characters.Count == 4)
			ReadyToTrack = true;
	}
}
