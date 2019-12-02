using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBox : MonoBehaviour
{
    private float H;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        H = Input.GetAxisRaw("Horizontal");
        transform.Translate(Mathf.RoundToInt(H), 0, 0);
    }
}
