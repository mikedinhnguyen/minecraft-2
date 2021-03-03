using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 10;
    private bool _isRunning = false;
    public Text timeText;

    private void Start()
    {
        // Starts the timer automatically
        _isRunning = true;
    }

    void Update()
    {
        if (_isRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                _isRunning = false;
            }
            int timeInt = (int)timeRemaining;
            timeText.text = timeInt.ToString();
        }
    }
}
