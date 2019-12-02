using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonSkeleton : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    private GameObject[] _viewLimiters;//Area de visão do esqueleto
    [SerializeField]
    private GameObject[] _attackRegion;
    [SerializeField]
    private GameObject[] _players;
    [SerializeField]
    private int currentPlayer = 2;
    [SerializeField]
    private float _speed;
    private float _startSpeed;
    private Vector3 _movePosition;
    private int damage = 1;
    Animator anim;
    public int health = 3;
    private int _knockback = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        
        _movePosition = transform.position;
        _players = GameObject.FindGameObjectsWithTag("Player"); //Achar todos jogadores
        anim = GetComponent<Animator>();
        _startSpeed = _speed;
    }

    // Update is called once per frame
    void Update()
    {

        photonView.RPC("PlayerFound", RpcTarget.All, null);
        if (_speed == 0)
        {
            anim.SetTrigger("React");
        }


    }
    private void FixedUpdate()
    {
        if (currentPlayer == 0 || currentPlayer == 1)
        {
            _movePosition.x = Mathf.MoveTowards(transform.position.x, _players[currentPlayer].transform.position.x, _speed * Time.deltaTime);
            GetComponent<Rigidbody2D>().MovePosition(new Vector2(_movePosition.x, transform.position.y));
            anim.SetBool("Walking", true);
        }
        else if (currentPlayer == 2)
        {
            anim.SetBool("Walking", false);
        }
        currentPlayer = 2;



    }


    [PunRPC]
    private void PlayerFound()
    {
        if (_players[0] != null || _players[1] != null)
        {
            if (_players[0].transform.position.x >= _attackRegion[1].transform.position.x && _players[0].transform.position.x < _attackRegion[0].transform.position.x ||
             _players[1].transform.position.x >= _attackRegion[1].transform.position.x && _players[1].transform.position.x < _attackRegion[0].transform.position.x)
            {
                //  _speed = 0;
                anim.SetBool("Attacking", true);

            }
            else anim.SetBool("Attacking", false);

            if (_players[0].transform.position.x > _viewLimiters[0].transform.position.x && _players[0].transform.position.x < _viewLimiters[1].transform.position.x
               && (_players[0].transform.position.x > _attackRegion[0].transform.position.x || _players[0].transform.position.x < _attackRegion[1].transform.position.x))
            //Se o jogador n°1 estiver dentro da visão do esqueleto
            {
                currentPlayer = 0;
                Debug.Log("To te vendo cuzão");
            }





            if (_players[1].transform.position.x > _viewLimiters[0].transform.position.x && _players[1].transform.position.x < _viewLimiters[1].transform.position.x
            && (_players[1].transform.position.x > _attackRegion[0].transform.position.x || _players[1].transform.position.x < _attackRegion[1].transform.position.x))
            //Se o jogador n°2 estiver dentro da visão do esqueleto
            {
                currentPlayer = 1;
                Debug.Log("To te vendo cuzão");
            }

            if (_players[0].transform.position.x > _viewLimiters[0].transform.position.x && _players[0].transform.position.x < transform.position.x || (_players[1].transform.position.x > _viewLimiters[0].transform.position.x && _players[1].transform.position.x < transform.position.x))
            {
                GetComponent<SpriteRenderer>().flipX = true;
                _knockback = 20;
            }
            if (_players[0].transform.position.x < _viewLimiters[1].transform.position.x && _players[0].transform.position.x > transform.position.x || _players[1].transform.position.x < _viewLimiters[1].transform.position.x && _players[1].transform.position.x > transform.position.x)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                _knockback = -20;
            }

        }


        // Aconteçe quando o jogador entra na area de visão do esqueleto
        // usar Raycast para determinar qual jogador estar mais perto e ir até ele    
    }
    [PunRPC]
    public void LevarDano(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        if (damage > 0)
        {
            StartCoroutine(KnockbackRoutine());
        }

    }
    IEnumerator KnockbackRoutine()
    {
        _speed = 0;
        transform.Translate(_knockback * Time.deltaTime, 0, 0);
        yield return new WaitForSeconds(1f);
        _speed = _startSpeed;
    }
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data

            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation); //rotation of the character
            stream.SendNext(health);
            

        }
        else

        {
            // Network player, receive data



            Vector3 syncPosition = (Vector3)stream.ReceiveNext();
            Quaternion syncRotation = (Quaternion)stream.ReceiveNext();
             health = (int)stream.ReceiveNext();
        }

    }
}

