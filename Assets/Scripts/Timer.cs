using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 10;
    public TextMeshProUGUI timeText;
    [HideInInspector]
    public static bool isRunning = false;
    Color gray = new Color(77f / 255f, 77f / 255f, 77f / 255f);

    public void StartTimer(int time)
    {
        isRunning = true;
        timeRemaining = time;
        timeText.color = gray;
    }

    void Update()
    {
        if (isRunning)
        {
            if (timeRemaining < 10)
            {
                timeText.color = new Color(250f/255f, 40f/255f, 40f/255f);
            }
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                //Debug.Log("Time has run out!");
                timeRemaining = 0;
                isRunning = false;
                LevelManager.gameIsEnded = true;
            }

            timeText.text = timeRemaining.ToString("F0");
        }
    }
}
