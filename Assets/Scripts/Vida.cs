using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vida : MonoBehaviour
{
    public static Vida instace;

    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHearth;
    public Sprite emptyHearth;

    private void Start()
    {
        health = 5;
        instace = this;
    }   
    private void Update()
    {

        if (health > numOfHearts)
        {
            health = numOfHearts;
        }        

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHearth;
            }
            else
            {
                hearts[i].sprite = emptyHearth;
            }


            if (i < numOfHearts) 
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }       
    }
}
