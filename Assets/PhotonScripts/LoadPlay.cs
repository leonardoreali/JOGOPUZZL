using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Pun;

public class LoadPlay : MonoBehaviourPunCallbacks
{
   
    // Start is called before the first frame update
    void Start()
    {
    
        PhotonNetwork.Instantiate("PhotonPlayer", new Vector2(0,0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
