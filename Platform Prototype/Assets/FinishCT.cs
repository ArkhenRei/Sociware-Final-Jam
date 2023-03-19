using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCT : MonoBehaviour
{
    public float amplitude = 0.1f; // The amount the image moves up and down
    public float speed = 1f; // The speed at which the image moves

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        float newY = startPos.y + amplitude * Mathf.Sin(speed * Time.time);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
