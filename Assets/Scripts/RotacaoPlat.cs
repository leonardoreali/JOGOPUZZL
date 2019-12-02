using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacaoPlat : MonoBehaviour
{
    private Vector3 _currentRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.eulerAngles.z > 30  && transform.eulerAngles.z  > 29) { transform.eulerAngles = _currentRotation; transform.rotation = Quaternion.FromToRotation(_currentRotation, _currentRotation); }
        if (transform.eulerAngles.z < -30 && transform.eulerAngles.z < - 29 ) { transform.eulerAngles = _currentRotation; transform.rotation = Quaternion.FromToRotation(_currentRotation, _currentRotation);  }
        
    }
}
