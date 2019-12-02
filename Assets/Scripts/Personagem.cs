using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personagem : MonoBehaviour
{
    private Vector2 direction = Vector2.zero;
    public float speed;
    [Range(1,2)]
    public int player;
    Rigidbody2D rb;
    private float speedOne;
    private float speedTwo;
    public Transform attackPos;
    private Vector3 startAttackPos;
    public float jumpForce = 10;
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
    [SerializeField]
    private float attackRate = 1;
    private float nextAttack;
    [SerializeField]
    private LayerMask enemyMask;

    public GameObject GameOver;
    public GameObject players;

    float swordPosX = 0.68f;

	public Transform myTransform;


	// Start is called before the first frame update
	void Start()
    {
		myTransform = transform;
		

        attackPos = GameObject.Find("espada").transform;
        rb = GetComponent<Rigidbody2D>();
        
        anim = GetComponent<Animator>();
        GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    // Update is called once per frame
    void Update()
    {
		

		startAttackPos = new Vector3(gameObject.transform.position.x + swordPosX,gameObject.transform.position.y - 0.003f, gameObject.transform.position.z);
        moving = Input.GetAxisRaw("Walking2"); 
        noChao = false;
        GroundCheck();
        //footPos.position = transform.position(gameObject.transform.position.x, gameObject.transform.position.y - 0, 25);
        if (moving == 0)
        {
            anim.SetBool("Andando", false);
        } else if (moving != 0)
        {
          anim.SetBool("Andando", true);
        }
        if (swordSlash.GetComponent<SpriteRenderer>().flipX == true)
        {
            swordPosX = -0.68f;
        }else if(swordSlash.GetComponent<SpriteRenderer>().flipX == false)
        {
            swordPosX = 0.68f;
        }
        
        
    }
    private void FixedUpdate()
    {
        Movement();
    }
    void Movement()
    {
        if (player == 1)
        {
            if (Input.GetKeyDown(KeyCode.W)) { attackPos.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 1); }
            if (Input.GetKeyDown(KeyCode.S)) { }
            if (Input.GetKeyUp(KeyCode.W)) { attackPos.transform.position = startAttackPos; }
            if (Input.GetKeyUp(KeyCode.S)) { }
            if (Input.GetKey(KeyCode.D))
            {
                swordSlash.GetComponent<SpriteRenderer>().flipX = false;
                speedOne = speed; rb.velocity = new Vector2(speedOne, rb.velocity.y);
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
            }
            else
            {
                speedOne = 0f; rb.velocity = new Vector2(0, rb.velocity.y);
            }
            if (Input.GetKey(KeyCode.A))
            {
                if (!(Input.GetKeyUp(KeyCode.UpArrow))){
                   
                    swordSlash.GetComponent<SpriteRenderer>().flipX = true;
                }
                speedOne = speed; rb.velocity = -new Vector2(speedOne, -rb.velocity.y);
                transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
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

            if (Input.GetKeyUp(KeyCode.K))
            {
                DealDamage();
            }

        }
        if (player == 2)
        {

            if (Input.GetKeyDown(KeyCode.UpArrow)) { attackPos.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 1); EulerZ = 90; }
            if (Input.GetKeyUp(KeyCode.UpArrow)) { attackPos.transform.position = startAttackPos;  EulerZ = 0; }
           
            if (Input.GetKeyDown(KeyCode.DownArrow)) { }
            if (Input.GetKeyUp(KeyCode.DownArrow)) { }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                
                swordSlash.GetComponent<SpriteRenderer>().flipX = true;
                speedTwo = speed; rb.velocity = -new Vector2(speedTwo, -rb.velocity.y);
                transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
            }
            else
            {
                speedTwo = 0f; rb.velocity = new Vector2(0, rb.velocity.y);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                swordSlash.GetComponent<SpriteRenderer>().flipX = false;
                speedTwo = speed; rb.velocity = new Vector2(speedTwo, rb.velocity.y);
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
            }
            else
            {
                speedTwo = 0f;
            }
            if (Input.GetKey(KeyCode.KeypadEnter) && noChao == true)
            {
                anim.SetBool("pulou", true);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            
            if (Input.GetKeyUp(KeyCode.Keypad0))
            {
                DealDamage();
            }
        }
    }
    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow)) { swordSlash.GetComponent<SpriteRenderer>().flipX = false; }
    }
    void GroundCheck()
    {
        
          noChao = Physics2D.OverlapCircle(footPos.position, radius,groundLayer);
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
    
    void Attack()
    {
        if (swordPurpl.GetComponent<SpriteRenderer>().enabled == false)
        {
            return;
        }
        if (swordPurpl.GetComponent<SpriteRenderer>().enabled == true)
        {
            
            DealDamage();
        }
    }
    public void SwordPowerUpPickedUp()
    {
        StartCoroutine(SwordPowerPickedUp());
    }
   public IEnumerator SwordPowerPickedUp()
    {
        swordPurpl.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(30f);
        swordPurpl.GetComponent<SpriteRenderer>().enabled = false;
    }
    public void TakeDamage()
    {
        HP--;
        if(HP <= 0)
        {
            Destroy(gameObject);
        }
    }

	

	void DealDamage()
    {
       
        if (Time.time > nextAttack)
        {
            Instantiate(swordSlash, swordPurpl.transform.position, Quaternion.Euler(0, 0, EulerZ));
            Collider2D[] dealingDmg = Physics2D.OverlapCircleAll(swordPurpl.transform.position, attackRadius, enemyMask);
            for (int i = 0; i < dealingDmg.Length; i++)
            {
                if (Collider.FindObjectOfType<Atirarador>()) { dealingDmg[i].GetComponent<Atirarador>().TakeDamage(1); }
                if (Collider.FindObjectOfType<SkeletonAI>()) { dealingDmg[i].GetComponent<SkeletonAI>().LevarDano(1); }
                if (Collider.FindObjectOfType<SpiderAI>()) { dealingDmg[i].GetComponent<SpiderAI>().LevarDano(1); }
              
              
            }
            nextAttack = Time.time + attackRate;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(swordPurpl.transform.position, attackRadius);
           
    }
    void dodge()
    {
        // executar animação de rolar
    }


    //Perder Vida
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") 
        {
            Vida.instace.health--;
			StartCoroutine(PiscaPisca());
            if (Vida.instace.health == 0)
            {
                Debug.Log("Irineu, não prestou atenção morreu "+ Vida.instace.health);
                GameOver.SetActive(true);
                Destroy(players);

            }

        }

		if (collision.transform.tag == "Plataform")
		{
			myTransform.parent = collision.transform;
		}

		if (collision.transform.tag == "MorteInst")
		{
		  
		}

	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.transform.tag == "Plataform")
		{
			myTransform.parent = null;
		}
	}
	IEnumerator PiscaPisca()
	{
		anim.SetBool("danado", true);
		yield return new WaitForSeconds(1f);
		anim.SetBool("danado", false);
	}

	
	

}


