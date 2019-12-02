using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void TrocaCena(string NomeCena) 
    {
        Debug.Log("Trocou para a cena: "+NomeCena);
        SceneManager.LoadScene(NomeCena);
        Time.timeScale = 1;     
        
    }

    public void Bye() 
    {        
        Application.Quit();
    }
}
