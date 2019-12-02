using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZ : MonoBehaviour
{
    private float DistanceBtwPlayers;
    public GameObject Player1;
    public GameObject Player2;
    public GameObject MiddleS;
    public Transform limitEsq;
    private Vector3 dude;
    private Vector3 screenPos;
    Camera cam;
    private float DDis;
    private float DDis2;
    // Start is called before the first frame update
    void Start()
    {
        dude = transform.localScale;
        cam = Camera.main;
        screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, -10));
    }

    // Update is called once per frame
    void Update()
    {

      RaycastHit2D hit2D = Physics2D.Raycast(Player1.gameObject.transform.position,new Vector2(Input.GetAxisRaw("Horizontal"),0));
      RaycastHit2D hit2D2 = Physics2D.Raycast(Player2.gameObject.transform.position,new Vector2(Input.GetAxisRaw("Horizontal"),0));
        if (hit2D.collider != null)
        {
            float distance = Mathf.Abs(hit2D.point.x);
            DDis = distance;
        }
        if (hit2D2.collider != null)
        {
            float distance2 = Mathf.Abs(hit2D2.point.x);

            DDis2 = distance2;

        }
        Debug.Log(DDis - DDis2);
        CameraFollow();
        MiddleS.transform.Translate(Mathf.RoundToInt(DDis - DDis2),0,0);
    }
    void CameraFollow()
    {
        //  if (Player1.transform)
      
    }
    private void LateUpdate()
    {
        Vector3 posCam = new Vector3(MiddleS.transform.position.x,0 /*Player1.transform.position.y + Player2.transform.position.y*/, -10);
        if (Player1.transform.position.x < screenPos.x || Player2.transform.position.x < screenPos.x)
        {

        }
        cam.transform.position = posCam;
      
    }
}  // Distance between player 1 & player 2 = DistanceBtwplayers;

