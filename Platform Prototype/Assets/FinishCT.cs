using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCT : MonoBehaviour
{
    public float yRot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        yRot= yRot+ 0.5f;
        transform.rotation = Quaternion.Euler(0, yRot, 0);
    }
}
