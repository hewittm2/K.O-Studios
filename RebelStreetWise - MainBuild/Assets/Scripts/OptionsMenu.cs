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

    public void SoundSettings()
    {

    }

    public void ResSettings()
    {

    }

    public void KeyBindingSettings()
    {

    }
}
