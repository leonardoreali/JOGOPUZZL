using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject pickupEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Personagem personagem = collision.GetComponent<Personagem>();
            if (personagem != null)
            {
                personagem.SwordPowerUpPickedUp();
                Pickup();
            }
            

        }
    }
    void Pickup()
    {

        Debug.Log("working");
        //  Instantiate(pickupEffect, transform.position, transform.rotation);

        Destroy(gameObject);

    }
    
}
