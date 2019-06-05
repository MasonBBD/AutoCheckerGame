using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheGameManager : MonoBehaviourPunCallbacks
{
    public Killer PlayerPrefab;
    public Killer LocalPlayer;

    private void Awake()
    {
        if (!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene("Main Menu");
        }
    }

    void Start()
    {
        Killer.RefreshInstance(ref LocalPlayer, PlayerPrefab);
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Killer.RefreshInstance(ref LocalPlayer, PlayerPrefab);
    }
}
