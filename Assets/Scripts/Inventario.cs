using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    public bool inventarioOn = false;
    public GameObject inventarioMenu;

    private int allSlots;
    private int enabledSlots;
    private GameObject[] slot;

    public GameObject slotHolder;
    void Start()
    {
        allSlots = 21;
        slot = new GameObject[allSlots];
        for (int i = 0; i < allSlots; i++) 
        {
            slot[i] = slotHolder.transform.GetChild(i).gameObject;
        }
    }
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.I))
        {       
            if (inventarioOn)
            {
                Continuar();
            }
            else
            {
                Pausar();
            }            
        }       
    }

    public void Continuar()
    {
        inventarioOn = false;
        Time.timeScale = 1;
        inventarioMenu.SetActive(false);
        GetComponent<Pause>().enabled = true;
    }

    public void Pausar()
    {
        inventarioOn = true;
        Time.timeScale = 0;
        inventarioMenu.SetActive(true);
        GetComponent<Pause>().enabled = false;
    }
}
