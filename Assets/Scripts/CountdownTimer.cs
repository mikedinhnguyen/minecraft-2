using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public SoundManager sm;
    public Image countdownOverlay;
    public Timer timer;
    public int countdownTimer;
    public TextMeshProUGUI countdownDisplay;
    int seconds;
    public static bool canPause;

    public void StartTimer(int num)
    {
        seconds = num;
        countdownTimer = 3;
        sm.PlayCountdownNoise();
        StartCoroutine(CountdownToStart());
    }
    
    IEnumerator CountdownToStart()
    {
        while (countdownTimer > 0)
        {
            countdownDisplay.text = countdownTimer.ToString();
            yield return new WaitForSeconds(1f);
            countdownTimer--;
        }

        countdownDisplay.text = "GO!";
        // start game
        canPause = true;
        countdownOverlay.gameObject.SetActive(false);
        timer.StartTimer(seconds);

        yield return new WaitForSeconds(1f);
        
        countdownDisplay.gameObject.SetActive(false);
    }
}
