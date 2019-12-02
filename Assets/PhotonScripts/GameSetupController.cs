using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameSetupController : MonoBehaviour
{
    [SerializeField]
    private GameObject PhotonPlayer;
    PhotonView photonView;
   
    // Start is called before the first frame update
   
    void Start()
    {
            CreatePlayer();
       
    }
    private void CreatePlayer()
    {
        
            if (PlayerView.LocalPlayerInstance == null)
            {
                Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
                Debug.Log("Creating Player");
                
                PhotonNetwork.Instantiate(this.PhotonPlayer.name, Vector3.zero, Quaternion.identity, 0);
          
            }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  

}
