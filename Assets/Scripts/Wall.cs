using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public int WallValue;
    // Start is called before the first frame update
    void Start()
    {
        WallValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (WallValue >= 3 && gameObject.transform.position.y < 2)
        { 
                transform.Translate(0, .15f, 0);
            
        }
    }
    void ValueIncrease()
    {
        WallValue += 1;
    }
}
