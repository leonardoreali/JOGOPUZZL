using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class photonplayer : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
   
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
           
            stream.SendNext(transform.position);          


        }
        else

        {         

            Vector3 syncPosition = (Vector3)stream.ReceiveNext();
        
        }


    }
}
