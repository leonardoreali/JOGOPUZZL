using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class DelayWaitingRoom : MonoBehaviourPunCallbacks
{
    private PhotonView myPhotonView;
    [SerializeField]
    private int multiplayerSceneIndex;
    [SerializeField]
    private int menuSceneIndex;
    private int playerCount;
    private int roomSize;
    [SerializeField]
    private int minPlayersToStart;
    [SerializeField]
    private Text roomCountDisplay;
    [SerializeField]
    private Text timerToStartDisplay;

    private bool readyToCountdown;
    private bool readyToStart;
    private bool startingGame;

    private float timerToStartGame;
    private float notFullGameTimer;
    private float fullGameTimer;

    [SerializeField] 
    private float maxWaittime;
    [SerializeField]
    private float maxFullWaitTime;
    // Start is called before the first frame update
    void Start()
    {
        myPhotonView = GetComponent<PhotonView>();
        fullGameTimer = maxFullWaitTime;
        notFullGameTimer = maxWaittime;
        timerToStartGame = maxWaittime;

        PlayerCountUpdate();
    }
    void PlayerCountUpdate()
    {
        playerCount = PhotonNetwork.PlayerList.Length;
        roomSize = PhotonNetwork.CurrentRoom.MaxPlayers;
        roomCountDisplay.text = playerCount + "/" + roomSize;
        if (playerCount == roomSize)
        {
            readyToStart = true;
        }
        else if (playerCount >= minPlayersToStart)
        {
            readyToCountdown = true;
        }
        else
        {
            readyToCountdown = false;
            readyToStart = false;
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        PlayerCountUpdate();
        if (PhotonNetwork.IsMasterClient)
        {
            myPhotonView.RPC("RPC_SendTimer", RpcTarget.Others, timerToStartGame);
        }
    }
    [PunRPC]
    private void RPC_SendTimer(float timeIn)
    {
        timerToStartGame = timeIn;
        notFullGameTimer = timeIn;
        if(timeIn < fullGameTimer)
        {
            fullGameTimer = timeIn;
        }
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        PlayerCountUpdate();
    }
    // Update is called once per frame
    void Update()
    {
        WaitingForMorePlayers();
    }
    void WaitingForMorePlayers()
    {
        if (playerCount <= 1)
        {
            ResetTimer();
        }
        if (readyToStart)
        {
            fullGameTimer -= Time.deltaTime;
            timerToStartGame = fullGameTimer;
        }
        else if (readyToCountdown)
        {
            notFullGameTimer -= Time.deltaTime;
            timerToStartGame = notFullGameTimer;
        }
        string tempTimer = string.Format("{0:00}", timerToStartGame);
        timerToStartDisplay.text = tempTimer;
        if(timerToStartGame <= 0f)
        {
            if (startingGame)
            {
                return;
            }
            StartGame();
        }
    }
    void ResetTimer()
    {
        timerToStartGame = maxWaittime;
        notFullGameTimer = maxWaittime;
        fullGameTimer = maxFullWaitTime;
    }
    public void StartGame()
    {
        startingGame = true;
        if (!PhotonNetwork.IsMasterClient)
            return;
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel(multiplayerSceneIndex);
    }
    public void DelayCancel()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(menuSceneIndex);
    }
}
