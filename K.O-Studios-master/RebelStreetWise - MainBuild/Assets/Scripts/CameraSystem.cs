﻿//Created By Mike K and Brian A
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

	List<Transform> furtherstApartPair = new List<Transform>();
	//lerp vals
	public float LerpSpeed;
	float a;
	float b = -10;
	float t = 0;
	bool CanLerp = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(ReadyToTrack){

			//have camera track midpoint of character locations
			Vector3 center = new Vector3((Characters [0].position.x + Characters [1].position.x + Characters [2].position.x + Characters [3].position.x) / 4f,
				(Characters [0].position.y + Characters [1].position.y + Characters [2].position.y + Characters [3].position.y) / 4f + 1f, 0f);

			//clac furtherest apart pair
			float currDist = Mathf.Abs(Characters [0].transform.position.x - Characters [1].transform.position.x);
			furtherstApartPair.Add (Characters[0]);
			furtherstApartPair.Add (Characters[1]);
			if(Mathf.Abs(Characters [1].transform.position.x - Characters [2].transform.position.x) > currDist){
				currDist = Mathf.Abs(Characters [1].transform.position.x - Characters [2].transform.position.x);
				furtherstApartPair.Clear ();
				furtherstApartPair.Add (Characters[1]);
				furtherstApartPair.Add (Characters[2]);
			}
			if(Mathf.Abs(Characters [2].transform.position.x - Characters [3].transform.position.x) > currDist){
				currDist = Mathf.Abs(Characters [2].transform.position.x - Characters [3].transform.position.x);
				furtherstApartPair.Clear ();
				furtherstApartPair.Add (Characters[2]);
				furtherstApartPair.Add (Characters[3]);
			}
			if(Mathf.Abs(Characters [3].transform.position.x - Characters [0].transform.position.x) > currDist){
				currDist = Mathf.Abs(Characters [3].transform.position.x - Characters [0].transform.position.x);
				furtherstApartPair.Clear ();
				furtherstApartPair.Add (Characters[3]);
				furtherstApartPair.Add (Characters[0]);
			}
			if(Mathf.Abs(Characters [2].transform.position.x - Characters [0].transform.position.x) > currDist){
				currDist = Mathf.Abs(Characters [2].transform.position.x - Characters [0].transform.position.x);
				furtherstApartPair.Clear ();
				furtherstApartPair.Add (Characters[2]);
				furtherstApartPair.Add (Characters[0]);
			}
			if(Mathf.Abs(Characters [3].transform.position.x - Characters [1].transform.position.x) > currDist){
				currDist = Mathf.Abs(Characters [3].transform.position.x - Characters [1].transform.position.x);
				furtherstApartPair.Clear ();
				furtherstApartPair.Add (Characters[3]);
				furtherstApartPair.Add (Characters[1]);
			}

	//check zoom out condition

			if(currDist > MaxDistanceToResize){
				if (transform.position.z - resizeIncrement > MaxCloseness) {
					a = transform.position.z;
					b = transform.position.z - resizeIncrement;
					CanLerp = true;
					Debug.Log ("Zoomout");
					MaxDistanceToResize += 1f;
					MinDistanceToResize += 1f;
				}
				
			} else {
	//check zoom in condition
				if(currDist < MinDistanceToResize){
					Debug.Log ("Here");
					if (transform.position.z + resizeIncrement < MinCloseness) {
						a = transform.position.z;
						b = transform.position.z + resizeIncrement;
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
			Debug.Log (b);
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
