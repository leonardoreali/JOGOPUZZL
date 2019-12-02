using System.Collections;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviourPunCallbacks,IPunObservable
{ 
    public PhotonView photonView;
    private Vector2 direction = Vector2.zero;
    public float speed;
    Rigidbody2D rb;
    private float speedOne;
    private float speedTwo;
    public Transform attackPos;
    private Vector3 startAttackPos;
    public float jumpForce = 100;
    private bool noChao = false;
    public Transform footPos;
    public float radius = 0.25f;
    public int HP = 3;
    public LayerMask groundLayer;
    public GameObject swordPurpl;
    [SerializeField]
    private float attackRadius = 0.25f;
    Animator anim;
    private float moving;
    [SerializeField]
    private GameObject swordSlash;
    private int EulerZ;
    private int EulerY;
    [SerializeField]
    private float attackRate = 1;
    private float nextAttack;
    [SerializeField]
    private LayerMask enemyMask;

    public GameObject GameOver;
    public GameObject players;
    private Color randomCol;
    public bool endGame = false;
    [SerializeField]
    private GameObject hpBar;
    public static GameObject localPlayerInstance;
    public static GameObject LocalPlayerInstance;
    float swordPosX = 0.68f;

    // Start is called before the first frame update
    private void Awake()
    {
      
        randomCol = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        photonView = GetComponent<PhotonView>();
        photonView.ObservedComponents.Add(this);
        if (!photonView.IsMine) 
        { enabled = false;

        }
        if (photonView.IsMine)
        {
            PlayerView.LocalPlayerInstance = this.gameObject;
            DontDestroyOnLoad(this.gameObject);
        }
        
        enabled = photonView.IsMine;
    }
    void Start()
    {
      
        if (!photonView.IsMine)
        {
            return;
        }
        attackPos = GameObject.Find("espada").transform;
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
        GetComponent<SpriteRenderer>().color = randomCol;
        randomCol = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
      
        transform.parent = transform.Find("Players");

    }
    
    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine) { photonView.RPC("syncRot", RpcTarget.All, "rotationS");
            if (endGame) { photonView.RPC("activateBar", RpcTarget.All, null); Physics2D.SetLayerCollisionMask(9, 9); }
        }
        if (!photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
      
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        startAttackPos = new Vector3(gameObject.transform.position.x + swordPosX, gameObject.transform.position.y - 0.003f, gameObject.transform.position.z);
        moving = Input.GetAxisRaw("Walking1");
        if (moving == 0)
        {
            anim.SetBool("Andando", false);
        }
        else if (moving != 0)
        {
            anim.SetBool("Andando", true);
        }
        GroundCheck();
        if (Input.GetKeyDown(KeyCode.W)) { attackPos.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 1); EulerZ = 90; }
        if (Input.GetKeyDown(KeyCode.S)) { }
        if (Input.GetKeyUp(KeyCode.W)) { attackPos.transform.position = startAttackPos; EulerZ = 0; }
        if (Input.GetKeyUp(KeyCode.S)) { }
        if (Input.GetKey(KeyCode.D))
        {
            EulerY = 0;
            speedOne = speed; rb.velocity = new Vector2(speedOne, rb.velocity.y);
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
        }
        else
        {
            speedOne = 0f; rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.A))
        {

            EulerY = 180;
            speedOne = speed; rb.velocity = -new Vector2(speedOne, -rb.velocity.y);
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
            Camera.main.transform.rotation = Quaternion.Euler(transform.rotation.x,0, transform.rotation.z);
        }
        else
        {
            speedOne = 0f;
        }
        if (Input.GetKey(KeyCode.Space) && noChao == true)
        {
            anim.SetBool("pulou", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (Input.GetKeyUp(KeyCode.K) && photonView.IsMine)
        {
            photonView.RPC("DealDamage", RpcTarget.All, null);
            
        }


    }
    [PunRPC]
    void syncRot(Quaternion rotationS) {
        transform.rotation = rotationS;
     }
    [PunRPC]
    void activateBar()
    {
        hpBar.SetActive(true);
    }
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(endGame);
            stream.SendNext(HP);
            stream.SendNext(transform.position);
            
            
        }
        else
        
            {
            // Network player, receive data



            Vector3 syncPosition = (Vector3)stream.ReceiveNext();
               
             endGame = (bool)stream.ReceiveNext();
            HP = (int)stream.ReceiveNext();
            }

        
    }
    private void LateUpdate()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        if (Input.GetKey(KeyCode.UpArrow)) { swordSlash.GetComponent<SpriteRenderer>().flipX = false; }
    }
    [PunRPC]
    void DealDamage()
    {

        if (Time.time > nextAttack)
        {
         PhotonNetwork.Instantiate("Golpe", swordPurpl.transform.position, Quaternion.Euler(0, EulerY, EulerZ));
               
            Collider2D[] dealingDmg = Physics2D.OverlapCircleAll(swordPurpl.transform.position, attackRadius, enemyMask);
            for (int i = 0; i < dealingDmg.Length; i++)
            {
                if (Collider.FindObjectOfType<Atirarador>()) { dealingDmg[i].GetComponent<Atirarador>().TakeDamage(1); }
                if (Collider.FindObjectOfType<SkeletonAI>()) { dealingDmg[i].GetComponent<SkeletonAI>().LevarDano(1); }
                if (Collider.FindObjectOfType<PhotonSkeleton>()) { dealingDmg[i].GetComponent<PhotonSkeleton>().LevarDano(1); }
                if (Collider.FindObjectOfType<SpiderAI>()) { dealingDmg[i].GetComponent<SpiderAI>().LevarDano(1); }
                if (Collider.FindObjectOfType<PhotonAtirarador>()) { dealingDmg[i].GetComponent<PhotonAtirarador>().TakeDamage(1); }
                if (Collider.FindObjectOfType<PlayerView>()){dealingDmg[i].GetComponent<PlayerView>().TakeDamage(1); }


            }
            nextAttack = Time.time + attackRate;
        }
    }
    void GroundCheck()
    {
        float restraint = 1;
        if (Time.time > restraint)
        {
            noChao = Physics2D.OverlapCircle(footPos.position, radius, groundLayer);
            restraint = Time.time + 1;
        }
        if (noChao)
        {
            anim.SetBool("pulou", false);
           
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(footPos.position, radius);
        Gizmos.color = Color.red;
    }
    public void TakeDamage(int damage)
    {
        if (endGame)
        {
            HP = HP - damage;
            if (HP <= 0)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "MorteInst")
        {

        }
    }

}
