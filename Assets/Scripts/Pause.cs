using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool pauseOn = false;

    public GameObject pauseMenu;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) 
        {
            if (pauseOn)
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
        pauseOn = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        GetComponent<Inventario>().enabled = true;
    }

    public void Pausar() 
    {
        pauseOn = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        GetComponent<Inventario>().enabled = false;
    }
}
