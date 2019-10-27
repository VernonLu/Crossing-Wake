using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using System;

public class CreatePlayer : PunBehaviour{

    public string aircraftPrefabPath;
    private string aircraftID;
    private GameObject player;

    void Start()
    {
        //获取玩家飞机ID
        aircraftID = User.Instance.currentAircraft.aircraftID;
        //aircraftID = "4";

        Debug.Log(aircraftPrefabPath + aircraftID);
        //生成玩家飞机
        player = PhotonNetwork.Instantiate(aircraftPrefabPath + aircraftID, new Vector3(5, 5, 5), Quaternion.identity, 0);

        //设置玩家飞机名称
        player.GetComponent<PhotonView>().RPC("SetPlayerName", PhotonTargets.All, User.Instance.team);
        //PhotonNetwork.InstantiateSceneObject("StartCountDown", Vector3.zero, Quaternion.identity, 0);
    }
}
