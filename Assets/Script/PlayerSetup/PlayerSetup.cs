using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour {
    
    public Behaviour[] scriptsToDisable;

    private PhotonView photonView;
    private Transform aircraft;
    private Transform[] spawnPoints;

	// Use this for initialization
	void Awake () {
        photonView = GetComponent<PhotonView>();

        //关闭不属于自己的组件
        if (!photonView.isMine)
        {

            foreach (Behaviour script in scriptsToDisable)
            {
                script.enabled = false;
            }
        }
        else
        {
            aircraft = transform.Find("Aircraft");
            Camera.main.GetComponent<CameraFollow>().SetTarget(aircraft);


            //设置玩家昵称
            
            PhotonNetwork.player.NickName = User.Instance.nickname;

            //设置玩家队伍
            string team = User.Instance.team;

            //设置玩家出生点
            Debug.Log("SpawnPoint" + team);
            string spawnPointName = "SpawnPoint" + team;
            aircraft.GetComponent<Aircraft>().SetSpawnPoint(GameObject.Find(spawnPointName).transform);
        }
	}
	
}
