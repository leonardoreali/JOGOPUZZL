using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalHP : MonoBehaviour
{
    
    private Image HP;
    [SerializeField]
    private float hpAmount;
    Atirarador atirara;
    PhotonAtirarador atiraraP;
    // Start is called before the first frame update
    void Start()
    {
        HP = transform.Find("bar").GetComponent<Image>();
        if (GameObject.Find("Bolona") != null)
        {
            atirara = GameObject.Find("Bolona").GetComponent<Atirarador>();
            hpAmount = atirara.HP;
        } else if (GameObject.Find("BolonaMulti") != null)
        {
            atiraraP = GameObject.Find("BolonaMulti").GetComponent<PhotonAtirarador>();
            hpAmount = atiraraP.HP;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        if (GameObject.Find("Bolona") != null)
        {
            HP.fillAmount = atirara.HP / hpAmount;
        }
        else if (GameObject.Find("BolonaMulti") != null)
        {
            HP.fillAmount = atiraraP.HP / hpAmount;
        }

    }
   
}
