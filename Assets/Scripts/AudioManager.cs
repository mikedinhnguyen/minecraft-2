using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] mobNoises;
    public AudioClip click;
    public AudioMixer mixer;
    public AudioSource sound;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    float masterVol = .5f;
    float musicVol = .5f;
    float sfxVol = .5f;

    private void Start()
    {
        //float vol = PlayerPrefs.GetFloat("Volume", 1f);
        mixer.SetFloat("MasterVolume", Mathf.Log10(masterVol) * 20);
        masterSlider.value = masterVol;
        mixer.SetFloat("MusicVolume", Mathf.Log10(musicVol) * 20);
        musicSlider.value = musicVol;
        mixer.SetFloat("SFXVolume", Mathf.Log10(sfxVol) * 20);
        sfxSlider.value = sfxVol;
    }

    public void SetMasterVolume(float sliderVal)
    {
        // log 10 will turn decimal val into int value and up it by 20
        // PlayerPrefs.SetFloat("Volume", sliderVal);
        masterVol = sliderVal;
        mixer.SetFloat("MasterVolume", Mathf.Log10(masterVol) * 20);
    }

    public void SetMusicVolume(float sliderVal)
    {
        musicVol = sliderVal;
        mixer.SetFloat("MusicVolume", Mathf.Log10(musicVol) * 20);
    }

    public void SetSFXVolume(float sliderVal)
    {
        sfxVol = sliderVal;
        mixer.SetFloat("SFXVolume", Mathf.Log10(sfxVol) * 20);
        int rand = Random.Range(0, mobNoises.Length);
        sound.clip = mobNoises[rand];
        sound.PlayDelayed(0.2f);
    }

    public void PlayClick()
    {
        sound.clip = click;
        sound.Play();
    }
}
