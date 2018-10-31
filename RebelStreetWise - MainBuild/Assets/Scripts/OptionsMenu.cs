using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixerGroup masterMixer;
    public AudioMixerGroup musicMixer;
    public AudioMixerGroup effectsMixer;

    AudioSource mainAudioSource;

    public Slider musicSlider;
    public Slider effectsSlider;
    public Slider masterSlider;

    int resWidth;
    int resHeight;
    public bool fullScreen;
    public Toggle fullToggle;

    public RES res;

    public enum RES
    {
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        H
    }

    private void OnEnable()
    {
        //mainAudioSource = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>();
    }

    public void ChangeMusicVolume(float musicVolume)
    {
        musicMixer.audioMixer.SetFloat("MusicVol", musicVolume);
    }

    public void ChangeEffectsVolume(float effectsVolume)
    {
        effectsMixer.audioMixer.SetFloat("EffectsVol", effectsVolume);
    }

    public void ChangeMasterVolume(float masterVolume)
    {
        masterMixer.audioMixer.SetFloat("MasterVol", masterVolume);
    }

    public void ResSettings()
    {
        switch (res)
        {
            case RES.A:
                resWidth = 3840;
                resHeight = 2160;
                break;
            case RES.B:
                resWidth = 2560;
                resHeight = 1440;
                break;
            case RES.C:
                resWidth = 1920;
                resHeight = 1080;
                break;
            case RES.D:
                resWidth = 1600;
                resHeight = 900;
                break;
            case RES.E:
                resWidth = 1366;
                resHeight = 768;
                break;
            case RES.F:
                resWidth = 1280;
                resHeight = 720;
                break;
            case RES.G:
                resWidth = 1152;
                resHeight = 648;
                break;
            case RES.H:
                resWidth = 1024;
                resHeight = 576;
                break;
        }
    }

    public void Fullscreen()
    {
        fullScreen = fullToggle;
    }

    public void SetRes()
    {
        Screen.SetResolution(resWidth, resHeight, fullScreen);
        print("ha");
    }

    public void KeyBindingSettings()
    {

    }
}
