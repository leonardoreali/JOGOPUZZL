using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject QuickStartButton;
    [SerializeField]
    private GameObject quickCancelButton;
    [SerializeField]
    private int roomSize;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        QuickStartButton.SetActive(true);
        base.OnConnectedToMaster();
    }
    public void QuickStart()
    {
        QuickStartButton.SetActive(false);
        quickCancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Quick start");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join room");
        CreateRoom();
        base.OnJoinRandomFailed(returnCode, message);
    }
    void CreateRoom()
    {
        Debug.Log("Creating room now");
        int randomRoomNumber = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize };
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps);
        Debug.Log(randomRoomNumber);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room... trying again");
        CreateRoom();
        base.OnCreateRoomFailed(returnCode, message);
    }
    public void QuickCancel()
    {
        quickCancelButton.SetActive(false);
        QuickStartButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
