using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public bool TriesToConnectToMaster;
    public bool TriesToCreateRoom;
    public bool TriesToJoinRoom;

    public Button BtnConnectToMaster;
    public Button BtnCreateLobby;
    public Button BtnJoinLobby;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        TriesToConnectToMaster = false;
        TriesToCreateRoom = false;
        TriesToJoinRoom = false;
    }

    void Update()
    {
        BtnConnectToMaster.gameObject.SetActive(!PhotonNetwork.IsConnected && !TriesToConnectToMaster);
        BtnCreateLobby.gameObject.SetActive(PhotonNetwork.IsConnected && !TriesToConnectToMaster && !TriesToCreateRoom);
        BtnJoinLobby.gameObject.SetActive(PhotonNetwork.IsConnected && !TriesToConnectToMaster && !TriesToJoinRoom);
    }

    public void MasterConnection()
    {
        PhotonNetwork.OfflineMode = false;
        PhotonNetwork.NickName = "PlayerName";
        //PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "v1";

        TriesToConnectToMaster = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        TriesToConnectToMaster = false;

        Debug.Log(cause);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        TriesToConnectToMaster = false;
        TriesToJoinRoom = true;
        Debug.Log("Connected To Master");
    }

    public void JoinRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        PhotonNetwork.JoinRoom("Room 1");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        TriesToJoinRoom = false;
        SceneManager.LoadScene("BoardGameScene");
    }

    public void CreateRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        TriesToCreateRoom = false;
        TriesToJoinRoom = false;
        
        PhotonNetwork.CreateRoom("Room 1");
        PhotonNetwork.JoinRoom("Room 1");
    }
}
