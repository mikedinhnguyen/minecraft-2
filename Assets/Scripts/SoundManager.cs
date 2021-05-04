using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource sound;
    public AudioClip click;
    public AudioClip win;
    public AudioClip finished;
    public AudioClip[] clearSounds;
    public AudioClip[] passSounds;
    int rand;

    public void PlayClickNoise()
    {
        sound.PlayOneShot(click, 0.5f);
    }

    public void PlayWinNoise()
    {
        sound.PlayOneShot(win, 0.5f);
    }

    public void PlayFinishedNoise()
    {
        sound.PlayOneShot(finished, 0.5f);
    }

    public void PlayClearNoise()
    {
        rand = Random.Range(0, clearSounds.Length);
        sound.PlayOneShot(clearSounds[rand], 0.5f);
    }

    public void PlayPassNoise()
    {
        rand = Random.Range(0, passSounds.Length);
        sound.PlayOneShot(passSounds[rand], 0.5f);
    }
}
