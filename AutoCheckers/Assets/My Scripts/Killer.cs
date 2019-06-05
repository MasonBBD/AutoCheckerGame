using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void RefreshInstance(ref Killer aKiller, Killer Prefab)
    {
        var position = Vector3.zero;
        var rotation = Quaternion.identity;
        aKiller = PhotonNetwork.Instantiate(Prefab.gameObject.name, position, rotation).GetComponent<Killer>();
    }
}
