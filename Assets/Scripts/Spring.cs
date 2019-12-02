using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    Personagem personagem;
    Rigidbody2D rb;
    public Vector2 jumpForce = new Vector2(0,50);
    Wall wall;
    // Start is called before the first frame update
    void Start()
    {
        personagem = GetComponent<Personagem>();
        rb = GetComponent<Rigidbody2D>();
        wall = GetComponent<Wall>();
        wall = GameObject.Find("Wall").GetComponent<Wall>();

    }

    // Update is called once per frame
    void Update()
    {
        if(wall.WallValue >= 3)
        {
            wall = GetComponent<Wall>();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //   FindObjectOfType<Personagem>().GetComponent<Rigidbody2D>().AddForce(jumpForce);
            ButtonPressed(); 
               
        }
    }
    void ButtonPressed()
    {
        wall.WallValue += 1;
    }
}
