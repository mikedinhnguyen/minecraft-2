using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollMechanic : MonoBehaviour
{
    public Scrollbar sbar;
    bool isScrolling;
    float startPos;
    float startValue;

    private void Start()
    {
        sbar = transform.parent.parent.GetComponentInChildren<Scrollbar>();
    }

    private void Update()
    {
        if (isScrolling)
        {
            sbar.value = startValue + (startPos - Input.mousePosition.y) / 350;
            if (sbar.value < 0)
            {
                sbar.value = 0;
                sbar.size = 0.431f;
            }
            if (sbar.value > 1)
            {
                sbar.value = 1;
                sbar.size = 0.431f;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0f) // forward
        {
            float mouseSpeed = Input.GetAxis("Mouse ScrollWheel");
            sbar.value += mouseSpeed / 150f;
            if (sbar.value < 0)
            {
                sbar.value = 0;
                sbar.size = 0.431f;
            }
            if (sbar.value > 1)
            {
                sbar.value = 1;
                sbar.size = 0.431f;
            }
        }
    }

    public void Scroll()
    {
        isScrolling = true;
        startPos = Input.mousePosition.y;
        startValue = sbar.value;
    }

    public void StopScroll()
    {
        isScrolling = false;
    }

    public static void ResetScrollBar(Transform inventory)
    {
        Scrollbar tempBar;
        tempBar = inventory.parent.GetComponentInChildren<Scrollbar>();
        if (tempBar.value != 1)
        {
            tempBar.value = 1;
        }
    }
}
