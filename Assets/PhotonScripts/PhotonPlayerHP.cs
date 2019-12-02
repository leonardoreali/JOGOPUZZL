using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonPlayerHP : MonoBehaviour
{
    private Image HP;
    PlayerView playerView;
    private float hpAmount;

    // Start is called before the first frame update
    void Start()
    {
        HP = transform.Find("bar").GetComponent<Image>();
        if(GameObject.Find("PhotonPlayer") != null)
        {
            playerView = GameObject.Find("PhotonPlayer").GetComponent<PlayerView>();
            hpAmount = playerView.HP;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("PhotonPlayer") != null)
        {
            HP.fillAmount = playerView.HP / hpAmount;
        }
    }
}
