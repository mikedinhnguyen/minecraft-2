using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 10;
    public TextMeshProUGUI timeText;
    [HideInInspector]
    public static bool isRunning = false;

    private void Start()
    {
        // Starts the timer automatically
        isRunning = true;
        timeText.color = Color.white;
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

            timeText.text = timeRemaining.ToString("F1");
        }
    }
}
