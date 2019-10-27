using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class TrailManager : PunBehaviour {

    public string matPath = "TrailMat/";
    private bool activate;
    public GameObject trailCollider;
    private Transform playerTrail;
    private TrailRenderer trailRenderer;
    private Material trailMat;

    private void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.enabled = false;
    }
    private void FixedUpdate()
    {
        GeneratePath();
    }

    //
    [PunRPC]
    public void Detach()
    {
        transform.parent = null;
        activate = false;
    }

    //
    [PunRPC]
    public void Activate()
    {
        activate = true;
        trailRenderer.enabled = true;

    }

    [PunRPC]
    public void SetMaterial(string materialName)
    {
        trailMat = Resources.Load(matPath + materialName, typeof(Material)) as Material;
        GetComponent<TrailRenderer>().material = trailMat;
    }

    [PunRPC]
    public void SetParent(string team)
    {
        GameObject player = GameObject.Find("Player " + team);
        transform.parent = player.transform.GetChild(0);
        playerTrail = player.transform.GetChild(1);
    }

    [PunRPC]
    public void Remove()
    {
        transform.parent = null;
        activate = false;
    }

    //生成路径碰撞盒
    private void GeneratePath()
    {
        if (activate)
        {
            Instantiate(trailCollider, transform.position, transform.rotation, playerTrail);
        }
    }
}
