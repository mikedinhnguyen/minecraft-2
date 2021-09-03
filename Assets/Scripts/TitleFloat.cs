using System.Collections;
using UnityEngine;

public class TitleFloat : MonoBehaviour
{
    public float amplitude = 10;
    public float frequency  = 1;
    
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    void Start()
    {
        // Store the starting position & rotation of the object
        posOffset = transform.position;
    }
    
    void Update()
    {
        // Float up/down with a Sin()

        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;

        // slow down
        //if (transform.position.y == 341.9f || transform.position.y == 361.9f)
        //{
        //    StartCoroutine(SlowDown());
        //}
    }

    public void ChangeTransformPosition()
    {
        // Store the starting position & rotation of the object
        posOffset = transform.position;
    }

    public IEnumerator SlowDown()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 1;
    }
}
