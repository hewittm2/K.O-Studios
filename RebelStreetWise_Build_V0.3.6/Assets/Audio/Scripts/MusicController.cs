/*Created by: Mitchell Hewitt,
 * This script handles the music throughout the menus and fighting scene
 * Updated: 12th April, 2019*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    public AudioClip music1; //Fighting Music 
    public AudioClip music2; //Menu Music

    [SerializeField] private AudioSource musicSource; // The audio source that will be present in all scenes and plays all the music
    //Audio listener should be placed on the Main Camera and set to not destroy as well

    private void Awake()
    {
        musicSource.clip = music2;
        musicSource.Play();
    }

    private void LateUpdate()
    {
        CheckClip();
        musicSource.Play();
    }

    void CheckClip()
    {
        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            musicSource.Stop();
            new WaitForSeconds(1);
            musicSource.clip = music1;
        }
        else
        {
            musicSource.clip = music2;
        }
    }
}
