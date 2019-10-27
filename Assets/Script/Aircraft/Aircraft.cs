    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Vernon
/// 2018.05.28
/// </summary>
public class Aircraft : MonoBehaviour {

    //How fast the aircraft can move forward
    public float speed;

    //How fast the aircraft can change it's direction
    public float motility;

    //How long the aircraft needs to return to horizontal when player is not changing it's direction
    public float smoothTime;

    //How long the aircraft needs to respawn after crashed
    public float respawnTime;

    //How long the aircraft needs to prepare after respawned
    public float prepareTime;
    
    //Aircraft can not be destroyed shortly after respawn;
    public float invincibleTime;

    //Where the aircraft will be spawned
    public Transform spawnPoint;

    //Current trail generator
    public GameObject trailGenerator;

    //Trail generator prefab
    public GameObject generatorPrefab;

    //Where the trail generator should be
    public Transform generatorPos;

    //State machine for the aircraft 
    private AircraftStateMachine FSM;

    //Whether aircraft can be destroyed or not
    public bool invincible;

    //Whether the game is end
    public bool isGameEnd;

    //Audio clip to play when player is respawned
    public AudioClip respawnClip;

    //Player Audio source 
    public AudioSource source;

	private void Start ()
    {
        source = GameObject.Find("PlayerAudio").GetComponent<AudioSource>();
        //Create state machine
        FSM = new AircraftStateMachine(this);
        GameObject.Find("Timer").GetComponent<PhotonView>().RPC("Register", PhotonTargets.All, transform.parent.name);
        source.PlayOneShot(respawnClip);
    }

    private void FixedUpdate()
    {
        FSM.Do();
    }

    private void OnTriggerEnter(Collider collider)
    {
        //Kill player when hit obstacles
        if (collider.tag == "obstacle") { Crash(); }
    }

    //Activate the aircraft
    [PunRPC]
    public void Activate()
    {
        FSM.Activate();
    }

    //Kill player when hit obstacles
    [PunRPC]
    public void Crash()
    {
        FSM.Crash();
    }

    //Reset player to the spawn point
    [PunRPC]
    public void Respawn()
    {
        FSM.Respawn();
    }

    //End game
    [PunRPC]
    public void EndGame()
    {
        FSM.EndGame();
    }

    //Set where the aircraft will spawn
    public void SetSpawnPoint(Transform spawnPoint)
    {
        this.spawnPoint = spawnPoint;
    }

    //Get the spawn point
    public Transform GetSpawnPoint()
    {
        return spawnPoint;
    }
}
