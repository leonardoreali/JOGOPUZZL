using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpiderAI : MonoBehaviour
{
    [SerializeField]
    private int HP = 2;
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject[] _players;
    Rigidbody2D rb;
    Animator anim;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private bool isGrounded = false;
    [SerializeField]
    private float radius;
    
    // Start is called before the first frame update
    void Start()
    {
        _players = GameObject.FindGameObjectsWithTag("Player");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("onWall", false);
        WallCheck();
        if (_players != null)
        {
            
                if (_players[0].transform.position.x < transform.position.x)
                {
                    speed = -100f;
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
                }
                if (_players[0].transform.position.x > transform.position.x)
                {
                    speed = 100f;
                transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
                }
                Rigidbody2D rb;
                rb = GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
            if (isGrounded && _players[0].transform.position.x < transform.position.x)
            {
                rb.AddForce(new Vector2(0, 20f));
                anim.SetBool("onWall", true);              
                
            }
            if (!isGrounded)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
                
            }
            
           
        }
    }
      private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(wallCheck.position, radius);
    }
    void WallCheck()
    {
        isGrounded = Physics2D.OverlapCircle(wallCheck.position, radius, groundLayer);
      
    }
    public void LevarDano(int damage)
    {
        HP -= damage;
        if(HP <= 0)
        {
            Destroy(gameObject);
        }
    }
    
}
