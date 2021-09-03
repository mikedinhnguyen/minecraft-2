using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource sound;
    public AudioClip click;
    public AudioClip win;
    public AudioClip countdown;
    public AudioClip[] clearSounds;
    public AudioClip[] passSounds;
    public AudioClip[] hintSounds;
    public AudioClip[] chestSounds;
    public AudioClip[] mobSounds;
    int rand;

    public void PlayCountdownNoise()
    {
        sound.PlayOneShot(countdown, 1f);
    }

    public void PlayClickNoise()
    {
        sound.PlayOneShot(click, 0.5f);
    }

    public void PlayWinNoise()
    {
        sound.PlayOneShot(win, 0.5f);
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

    public void PlayHintNoise()
    {
        rand = Random.Range(0, hintSounds.Length);
        sound.PlayOneShot(hintSounds[rand], 0.5f);
    }

    public void PlayMobNoise(int mobNum)
    {
        sound.PlayOneShot(mobSounds[mobNum], 0.5f);
    }

    public void PlayChestOpen()
    {
        sound.PlayOneShot(chestSounds[0], 0.5f);
    }

    public void PlayChestClose()
    {
        rand = Random.Range(1, 3);
        sound.PlayOneShot(chestSounds[rand], 0.5f);
    }
}
