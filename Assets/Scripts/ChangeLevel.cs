using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    public string Cena;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("X1 2 L");
        if (collision.gameObject.tag == "Player")
        {
            TrocaCena(Cena);
        }
    }

    public void TrocaCena(string NomeCena)
    {
        Debug.Log("Trocou para a cena: " + NomeCena);
        SceneManager.LoadScene(NomeCena);
        Time.timeScale = 1;

    }
}
