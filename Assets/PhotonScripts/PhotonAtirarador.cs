using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonAtirarador : MonoBehaviourPunCallbacks, IPunObservable
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject[] _players;
    [SerializeField]
    private GameObject bola;
    private int _nextFire = 1;
    public LayerMask levelMask;
    private int randomInt = 1;
    private int randomDirection = 1;
    private bool bolaTel = false;
    public int HP;
    private float WaitTime;
    bool damaged = false;
    private float floatX;
    private float floatY;
    

  void Start()
    {
        _players = GameObject.FindGameObjectsWithTag("Player");//Achar todos jogadores
        PhotonView photonView = PhotonView.Get(this);
        
    

    }

    // Update is called once per frame
    void Update()
    {
              
        if (Time.time > _nextFire)
        {
           PhotonNetwork.Instantiate("BolaAtirada", transform.position, Quaternion.identity);
            _nextFire = Mathf.RoundToInt(Time.time + 1);

            if (randomDirection == 1)
            {

                StartCoroutine(Teleport(Vector2.right * randomInt));
                randomDirection = Random.Range(1, 3);

            }
            if (randomDirection == 2)
            {
                StartCoroutine(Teleport(Vector2.up * randomInt));
                randomDirection = Random.Range(1, 3);

            }
        }
        if (transform.position.y >= 4)
        {
            transform.position = new Vector2(transform.position.x, 4);
        }
        if (transform.position.x > 38)
        {
            transform.position = new Vector2(38, transform.position.y);
        }
        if (transform.position.y <= -2)
        {
            transform.position = new Vector2(transform.position.x, -2);
        }
        if (transform.position.x < -9)
        {
            transform.position = new Vector2(20, transform.position.y);
        }
        

    }

    IEnumerator Teleport(Vector2 direction)
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 50f,levelMask);
        if (hit.collider != null)
        {

            if (randomDirection == 1 && bolaTel == false)
            {
                randomInt = Random.Range(0, 2) * 2 - 1;
                Debug.DrawRay(transform.position, direction, Color.red, 1000f);
                float distance = hit.point.x / 4 * randomInt;
                Debug.Log(distance);
                transform.position = new Vector2(transform.position.x + distance, transform.position.y);
                bolaTel = true;
                yield return new WaitForSeconds(WaitTime);
                bolaTel = false;
            }
            if (randomDirection == 2 && bolaTel == false)
            {
                randomInt = Random.Range(0, 2) * 2 - 1;
                Debug.DrawRay(transform.position, direction, Color.red, 1000f);
                float distance = hit.point.y / 2 * randomInt;
                Debug.Log(distance);
                transform.position = new Vector2(transform.position.x, transform.position.y + distance);
                bolaTel = true;
                yield return new WaitForSeconds(WaitTime);
                bolaTel = false;
            }

        }
        yield return new WaitForSeconds(3f);
        WaitTime = 1f;
    }
    
    public void TakeDamage(int damage)
    {
        
            StartCoroutine(TookDamage(damage));
        if (HP <= 0)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < players.Length; i++)
            {
                players[i].GetComponent<PlayerView>().endGame = true;
                
            }            
             PhotonNetwork.AutomaticallySyncScene = true;
            photonView.RPC("des", RpcTarget.All, null);
        }

    }
  
    [PunRPC]
    private void des()
    {
        Destroy(gameObject);
    }
    IEnumerator TookDamage(int dmg)
    {
        Animator anim = GetComponent<Animator>();
        if (!damaged)
        {
            HP -= dmg;
            anim.SetBool("dano", true);
            damaged = true;
            WaitTime = 0f;
            yield return new WaitForSeconds(3f);
            anim.SetBool("dano", false);
            damaged = false;
            WaitTime = 5f;
        }

    }
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(HP);
            stream.SendNext(gameObject);
        }
        else if (stream.IsReading)

        {
            // Network player, receive data
            Vector3 syncPosition = (Vector3)stream.ReceiveNext();
            HP = (int)stream.ReceiveNext();
        }


    }
}
