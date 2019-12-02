using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DelayStartRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int waitingRoomSceneIndex;
    // Start is called before the first frame update
    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
    }
    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        SceneManager.LoadScene(waitingRoomSceneIndex);
    }
}
