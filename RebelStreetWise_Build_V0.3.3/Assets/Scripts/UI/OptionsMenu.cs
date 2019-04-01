using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixerGroup masterMixer;

    AudioSource mainAudioSource;

    public Slider musicSlider;
    public Slider effectsSlider;
    public Slider masterSlider;

    public Dropdown dropDown;

    int resWidth;
    int resHeight;
    public bool fullScreen;
    public Toggle fullToggle;

    public RES res;

    void Start()
    {
        ChangeEffectsVolume(-10);
        effectsSlider.value = -10;
        ChangeMusicVolume(-10);
        musicSlider.value = -10;
        ChangeMasterVolume(-10);
        masterSlider.value = -10;
    }
    void Update()
    {
        if(dropDown.value == 0)
        {
            res = RES.A;
        }
        if (dropDown.value == 1)
        {
            res = RES.B;
        }
        if (dropDown.value == 2)
        {
            res = RES.C;
        }
        if (dropDown.value == 3)
        {
            res = RES.D;
        }
        if (dropDown.value == 4)
        {
            res = RES.E;
        }
        if (dropDown.value == 5)
        {
            res = RES.F;
        }
        if (dropDown.value == 6)
        {
            res = RES.G;
        }
        if (dropDown.value == 7)
        {
            res = RES.H;
        }
    }

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

    public void ChangeMusicVolume(float musicVolume)
    {
        print("music");
        masterMixer.audioMixer.SetFloat("MusicVol", musicVolume);
    }

    public void ChangeEffectsVolume(float effectsVolume)
    {
        print("Effects");
        masterMixer.audioMixer.SetFloat("EffectsVol", effectsVolume);
    }

    public void ChangeMasterVolume(float masterVolume)
    {
        print("master");
        masterMixer.audioMixer.SetFloat("MasterVol", masterVolume);
    }

    public void ResSettings(RES res)
    {
        switch (res)
        {
            case RES.A:
                resWidth = 3840;
                resHeight = 2160;
                print("Height = "+resHeight+ "Width = " + resWidth );
                break;
            case RES.B:
                resWidth = 2560;
                resHeight = 1440;
                print("Height = " + resHeight + "Width = " + resWidth);
                break;
            case RES.C:
                resWidth = 1920;
                resHeight = 1080;
                print("Height = " + resHeight + "Width = " + resWidth);
                break;
            case RES.D:
                resWidth = 1600;
                resHeight = 900;
                print("Height = " + resHeight + "Width = " + resWidth);
                break;
            case RES.E:
                resWidth = 1366;
                resHeight = 768;
                print("Height = " + resHeight + "Width = " + resWidth);
                break;
            case RES.F:
                resWidth = 1280;
                resHeight = 720;
                print("Height = " + resHeight + "Width = " + resWidth);
                break;
            case RES.G:
                resWidth = 1152;
                resHeight = 648;
                print("Height = " + resHeight + "Width = " + resWidth);
                break;
            case RES.H:
                resWidth = 1024;
                resHeight = 576;
                print("Height = " + resHeight + "Width = " + resWidth);
                break;
        }
    }

    public void Fullscreen()
    {
        fullScreen = fullToggle.isOn;
    }

    public void SetRes()
    {
        Screen.SetResolution(resWidth, resHeight, fullScreen);
        print("SetRes");
    }

    public void Apply()
    {
        ResSettings(res);
        Fullscreen();
        SetRes();
        print("Height = " + resHeight + "Width = " + resWidth);
    }
}
