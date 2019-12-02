using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bolaSe : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _players;
    [SerializeField]
    private Vector3 playerTransform;
    Rigidbody2D rb;
    [SerializeField]
    private int _randoN;
    // Start is called before the first frame update
    void Start()
    {
        _players = GameObject.FindGameObjectsWithTag("Player"); //Achar todos jogadores
        rb = GetComponent<Rigidbody2D>();
       
            if (_players.Length == 1)
            {
               
                _randoN = 0;
                
            } else if (_players.Length == 2)
            {
                _randoN = Random.Range(0, 2);
            }
        
        playerTransform = _players[_randoN].GetComponent<Transform>().position - transform.position;
        Destroy(gameObject, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        
        //movetowards posição do jogador pega pelo atirador
    }
    private void FixedUpdate()
    {
        Atirar();

    }
    void Atirar()
    {
        rb.velocity = playerTransform;
       
    }
}
