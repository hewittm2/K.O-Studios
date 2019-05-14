//Created By Mike K and Brian A
//remade by Ryan
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraSystem : MonoBehaviour
{



    public List<Transform> Characters = new List<Transform>();
    //public float MinCloseness;
    //public float MaxCloseness;
    //default start = 0,1,-4 = FOV 65
    public bool ReadyToTrack = false;
    //public float MaxDistanceToResize;
    //public float MinDistanceToResize;
    //public float resizeIncrement;

    public List<Transform> furtherstApartPair = new List<Transform>();
    ////lerp vals
    //public float LerpSpeed;
    //float a;
    //float b = -10;
    //float t = 0;
    //bool CanLerp = false;
    float currDist;
    Vector3 center;

    //Ryans additions
    public Vector3 midpoint;
    public Vector3 cameraDestination;
    public Camera cam;

    public Vector3 SolarisSpawnSpot = new Vector3(2, 0, -40);
    public Vector3 GroovyGraveyardSpawnSpot = new Vector3(2, 0, -40);

    public float distance;
    public float zoomFactor = 1.5f;
    public float followTimeDelta = 0.8f;
    public float decreaseZoom;
    bool growing = false;
    private float math;
    private float math2;
    public string SolarisSceneName = "FighterTest";
    public float SolarisCamHeight = 9;
    public float SolarisMaxDistance = -40;
    public float SolarisMinDistance = -22;
    public string GroovySceneName = "Groovy Graveyard";
    public float GroovyCamHeight = 3;
    public float GroovyMaxDistance = 140;
    public float GroovyMinDistance = 112;
    private float timer;

    private float camHeight = 9;

    private int stage;

    public bool isReady;


    // Use this for initialization
    void Start()
    {
        //yield return new WaitForSeconds(0.2f);
        //isReady = true;
        
        if (SceneManager.GetActiveScene().name == GroovySceneName)
        {
            stage = 1;
            cam.transform.position = GroovyGraveyardSpawnSpot;
            camHeight = GroovyCamHeight;

        }

        if (SceneManager.GetActiveScene().name == SolarisSceneName)
        {
            stage = 0;
            cam.transform.position = SolarisSpawnSpot;
            camHeight = SolarisCamHeight;


        }
       
        Debug.Log(stage + "READ");
        //Characters.Add(FindObjectOfType<FighterClass>().transform);
        Characters.Clear();
         StartCoroutine(StartDelay());

        //sets start values

    }


    // Update is called once per frame
    void Update()
    {
        if (stage == 1 && timer == 1)
        {
            if (ReadyToTrack)

            {

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
                center = new Vector3((Characters[0].position.x + Characters[1].position.x + Characters[2].position.x + Characters[3].position.x) / 4f,
                                    (Characters[0].position.y + Characters[1].position.y + Characters[2].position.y + Characters[3].position.y) / 4f + 1f, 0f);

                //clac furtherest apart pair
                currDist = Mathf.Abs(Characters[0].transform.position.x - Characters[1].transform.position.x);

                if (Mathf.Abs(Characters[1].transform.position.x - Characters[2].transform.position.x) > currDist)
                {
                    currDist = Mathf.Abs(Characters[1].transform.position.x - Characters[2].transform.position.x);
                    furtherstApartPair.Clear();
                    furtherstApartPair.Add(Characters[1]);
                    furtherstApartPair.Add(Characters[2]);
                }
                if (Mathf.Abs(Characters[2].transform.position.x - Characters[3].transform.position.x) > currDist)
                {
                    currDist = Mathf.Abs(Characters[2].transform.position.x - Characters[3].transform.position.x);
                    furtherstApartPair.Clear();
                    furtherstApartPair.Add(Characters[2]);
                    furtherstApartPair.Add(Characters[3]);
                }
                if (Mathf.Abs(Characters[3].transform.position.x - Characters[0].transform.position.x) > currDist)
                {
                    currDist = Mathf.Abs(Characters[3].transform.position.x - Characters[0].transform.position.x);
                    furtherstApartPair.Clear();
                    furtherstApartPair.Add(Characters[3]);
                    furtherstApartPair.Add(Characters[0]);
                }
                if (Mathf.Abs(Characters[2].transform.position.x - Characters[0].transform.position.x) > currDist)
                {
                    currDist = Mathf.Abs(Characters[2].transform.position.x - Characters[0].transform.position.x);
                    furtherstApartPair.Clear();
                    furtherstApartPair.Add(Characters[2]);
                    furtherstApartPair.Add(Characters[0]);
                }
                if (Mathf.Abs(Characters[3].transform.position.x - Characters[1].transform.position.x) > currDist)
                {
                    currDist = Mathf.Abs(Characters[3].transform.position.x - Characters[1].transform.position.x);
                    furtherstApartPair.Clear();
                    furtherstApartPair.Add(Characters[3]);
                    furtherstApartPair.Add(Characters[1]);
                }


                //midpoint between x and z
                midpoint = new Vector3((furtherstApartPair[0].position.x + furtherstApartPair[1].position.x) / 2f, camHeight, (furtherstApartPair[0].position.z + furtherstApartPair[1].position.z) / 2f);
                distance = (furtherstApartPair[0].position - furtherstApartPair[1].position).magnitude;

                cameraDestination = midpoint - cam.transform.forward * distance * zoomFactor;
                cam.transform.position = Vector3.Slerp(cam.transform.position, cameraDestination, followTimeDelta);

                if (cam.transform.position.x > GroovyMaxDistance)
                {

                    cam.transform.position = new Vector3(GroovyMaxDistance, cameraDestination.y, cameraDestination.z);
                }
                //prevents camera from zooming in to much
                if (cam.transform.position.x < GroovyMinDistance)
                {

                    cam.transform.position = new Vector3(GroovyMinDistance, cameraDestination.y, cameraDestination.z);
                }

            }
        }
            if (stage == 0 && timer == 1)
            {
                if (ReadyToTrack)
                {
                   
                    
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
                        center = new Vector3((Characters[0].position.x + Characters[1].position.x + Characters[2].position.x + Characters[3].position.x) / 4f,
                                            (Characters[0].position.y + Characters[1].position.y + Characters[2].position.y + Characters[3].position.y) / 4f + 1f, 0f);

                        //clac furtherest apart pair
                        currDist = Mathf.Abs(Characters[0].transform.position.x - Characters[1].transform.position.x);

                        if (Mathf.Abs(Characters[1].transform.position.x - Characters[2].transform.position.x) > currDist)
                        {
                            currDist = Mathf.Abs(Characters[1].transform.position.x - Characters[2].transform.position.x);
                            furtherstApartPair.Clear();
                            furtherstApartPair.Add(Characters[1]);
                            furtherstApartPair.Add(Characters[2]);
                        }
                        if (Mathf.Abs(Characters[2].transform.position.x - Characters[3].transform.position.x) > currDist)
                        {
                            currDist = Mathf.Abs(Characters[2].transform.position.x - Characters[3].transform.position.x);
                            furtherstApartPair.Clear();
                            furtherstApartPair.Add(Characters[2]);
                            furtherstApartPair.Add(Characters[3]);
                        }
                        if (Mathf.Abs(Characters[3].transform.position.x - Characters[0].transform.position.x) > currDist)
                        {
                            currDist = Mathf.Abs(Characters[3].transform.position.x - Characters[0].transform.position.x);
                            furtherstApartPair.Clear();
                            furtherstApartPair.Add(Characters[3]);
                            furtherstApartPair.Add(Characters[0]);
                        }
                        if (Mathf.Abs(Characters[2].transform.position.x - Characters[0].transform.position.x) > currDist)
                        {
                            currDist = Mathf.Abs(Characters[2].transform.position.x - Characters[0].transform.position.x);
                            furtherstApartPair.Clear();
                            furtherstApartPair.Add(Characters[2]);
                            furtherstApartPair.Add(Characters[0]);
                        }
                        if (Mathf.Abs(Characters[3].transform.position.x - Characters[1].transform.position.x) > currDist)
                        {
                            currDist = Mathf.Abs(Characters[3].transform.position.x - Characters[1].transform.position.x);
                            furtherstApartPair.Clear();
                            furtherstApartPair.Add(Characters[3]);
                            furtherstApartPair.Add(Characters[1]);
                        }


                        //midpoint between x and z
                        midpoint = new Vector3((furtherstApartPair[0].position.x + furtherstApartPair[1].position.x) / 2f, camHeight, (furtherstApartPair[0].position.z + furtherstApartPair[1].position.z) / 2f);
                        distance = (furtherstApartPair[0].position - furtherstApartPair[1].position).magnitude;

                        cameraDestination = midpoint - cam.transform.forward * distance * zoomFactor;
                        cam.transform.position = Vector3.Slerp(cam.transform.position, cameraDestination, followTimeDelta);
                    
                }

                //prevents camera from zooming out to far
                if (cam.transform.position.z < SolarisMaxDistance)
                {
                    cam.transform.position = new Vector3(cameraDestination.x, cameraDestination.y, SolarisMaxDistance);
                }
                //prevents camera from zooming in to much
                if (cam.transform.position.z > SolarisMinDistance)
                {
                    cam.transform.position = new Vector3(cameraDestination.x, cameraDestination.y, SolarisMinDistance);
                }


                math = 8;
                //stops from showing beneth stage, the bool and math vars are for smoothing
                if (cam.transform.position.z < -20)
                {
                    //camHeight = 8;
                    math = camHeight;
                    math2 = 8.5f;
                    if (cam.transform.position.z < -25)
                    {
                        growing = true;
                        //camHeight = 8.5f;
                        math = camHeight;
                        math2 = 9f;
                        if (cam.transform.position.z < -30)
                        {
                            growing = true;
                            //camHeight = 9;
                            math = camHeight;
                            math2 = 9.5f;
                            if (cam.transform.position.z < -35)
                            {
                                growing = true;
                                //camHeight = 9.5f;
                                math = camHeight;
                                math2 = 10f;
                                if (cam.transform.position.z < -38)
                                {
                                    growing = true;
                                    camHeight = 10;
                                }
                                else
                                {
                                    math2 = 9.5f;
                                    growing = false;
                                }
                            }
                            else
                            {
                                math2 = 9;
                                growing = false;
                            }
                        }
                        else
                        {
                            math2 = 8.5f;
                            growing = false;
                        }
                    }
                    else
                    {
                        math2 = 8;
                        growing = false;
                    }
                }
                if (growing == true)
                {
                    if (math < math2)
                        math += .1f;
                }
                if (growing == false)
                {
                    if (math > math2)
                        math -= .1f;
                }
            }

        }
    
    public void AddPlayerToList(Transform trans)
    {
        Characters.Add(trans);
        if (Characters.Count == 4)
            ReadyToTrack = true;
    }
    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(.2F);
        
        foreach (FighterClass f in FindObjectsOfType<FighterClass>())
        {
            Characters.Add(f.transform);
        }
        furtherstApartPair.Add(Characters[0]);
        furtherstApartPair.Add(Characters[1]);
        midpoint = new Vector3((furtherstApartPair[0].position.x + furtherstApartPair[1].position.x) / 2f, camHeight, (furtherstApartPair[0].position.z + furtherstApartPair[1].position.z) / 2f);
        distance = (furtherstApartPair[0].position - furtherstApartPair[1].position).magnitude;
        timer = 1;
        isReady = true;
    }
}
