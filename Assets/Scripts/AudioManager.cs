using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioSource sound;

    private void Start()
    {
        float vol = PlayerPrefs.GetFloat("Volume", 0.5f);
        mixer.SetFloat("MasterVolume", Mathf.Log10(vol) * 20);
        GameObject.Find("VolumeSlider").GetComponent<Slider>().value = vol;
    }

    public void SetVolume(float sliderVal)
    {
        // log 10 will turn decimal val into int value and up it by 20
        mixer.SetFloat("MasterVolume", Mathf.Log10(sliderVal) * 20);
        PlayerPrefs.SetFloat("Volume", sliderVal);
        sound.Play();
    }
}
