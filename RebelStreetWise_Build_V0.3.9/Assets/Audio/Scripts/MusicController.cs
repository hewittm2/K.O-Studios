/*Created by: Mitchell Hewitt,
 * This script handles the music throughout the menus and fighting scene
 * Updated: 12th April, 2019
 * Edit by Cale Toburen 7th May, 2019*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    public AudioClip stageOne; //Stage One Music 
    public AudioClip stageTwo; //Stage Two Music
    public AudioClip[] menuMusic; //Menu Music
    private int audioNum;

    [SerializeField] private AudioSource musicSource; // The audio source that will be present in all scenes and plays all the music
    //Audio listener should be placed on the Main Camera and set to not destroy as well

    private void Awake()
    {
        audioNum = Random.Range(0, menuMusic.Length);
        musicSource.clip = menuMusic[audioNum];
        musicSource.Play();
    }

    private void LateUpdate()
    {
        CheckClip();
    }

    void CheckClip()
    {
        if(SceneManager.GetActiveScene().buildIndex == 3 || SceneManager.GetActiveScene().buildIndex == 4)
        {

            if (SceneManager.GetActiveScene().buildIndex == 3 )
            {

                musicSource.Stop();
                new WaitForSeconds(1);
                musicSource.clip = stageOne;
            }
            else
            {
                musicSource.Stop();
                new WaitForSeconds(1);
                musicSource.clip = stageTwo;
            }
        }

        else
        {
            musicSource.clip = menuMusic[audioNum];
        }
    }
}
