using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 10;
    public Text timeText;
    [HideInInspector]
    public static bool isRunning = false;

    private void Start()
    {
        // Starts the timer automatically
        isRunning = true;
    }

    void Update()
    {
        if (isRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                //Debug.Log("Time has run out!");
                timeRemaining = 0;
                isRunning = false;
            }
            int timeInt = (int)timeRemaining;
            timeText.text = timeInt.ToString();
        }
    }
}
