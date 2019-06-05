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
    public bool TriesToConnectToLobby;

    public Button BtnConnectToMaster;
    public Button BtnConnectToLobby;

    // Start is called before the first frame update
    void Start()
    {
        TriesToConnectToMaster = false;
        TriesToConnectToLobby = false;
    }

    void Update()
    {
        BtnConnectToMaster.gameObject.SetActive(!PhotonNetwork.IsConnected && !TriesToConnectToMaster);
        BtnConnectToLobby.gameObject.SetActive(!PhotonNetwork.IsConnected && !TriesToConnectToMaster && !TriesToConnectToLobby);
    }

    public void OnClickConnectToMaster()
    {
        TriesToConnectToMaster = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        Debug.Log(cause);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        TriesToConnectToMaster = false;
        Debug.Log("Connected To Master");
    }
}
