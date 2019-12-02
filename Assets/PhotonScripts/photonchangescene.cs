using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class photonchangescene : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int playC;
    private GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            _player = GameObject.FindGameObjectWithTag("Player");//Achar todos jogadores
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playC == 2)
        {
            if (PhotonNetwork.IsMasterClient)
               
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.LoadLevel("TorreMulti");
            if (_player != null)
            {
                SceneManager.MoveGameObjectToScene(_player, SceneManager.GetSceneByName("TorreMulti"));
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playC = playC + 1;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playC = playC - 1;
        }
    }
}
