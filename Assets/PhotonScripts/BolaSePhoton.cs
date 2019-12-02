using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BolaSePhoton : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    private GameObject[] _players;
    [SerializeField]
    private Vector3 playerTransform;
    Rigidbody2D rb;
    [SerializeField]
    private int _randoN;
    private float dTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        _players = GameObject.FindGameObjectsWithTag("Player"); //Achar todos jogadores
        rb = GetComponent<Rigidbody2D>();

        if (_players.Length == 1)
        {

            _randoN = 0;

        }
        else if (_players.Length == 2)
        {
            _randoN = Random.Range(0, 2);
        }

        playerTransform = _players[_randoN].GetComponent<Transform>().position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void FixedUpdate()
    {
        Atirar();

    }
    void Atirar()
    {
        rb.velocity = playerTransform;
       
            Destroy(gameObject, 3f);
           
        

    }
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(transform.position);
        }
        else

        {
            // Network player, receive data
            Vector3 syncPosition = (Vector3)stream.ReceiveNext();
        }


    }

}
