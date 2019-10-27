using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Vernon
/// 2018.06.02
/// </summary>
public class CrashState : IAircraftState {
    public GameObject explosion;
    private Aircraft aircraft;
	private Transform transform;
    private AircraftStateMachine stateMachine;
    private bool crashed;
    private float timeLeft;
    private float respawnTime;

	public CrashState(AircraftStateMachine stateMachine){
        this.stateMachine = stateMachine;
        aircraft = stateMachine.GetAircraft();
		transform = aircraft.transform;
        respawnTime = aircraft.respawnTime;
        crashed = false;
	}


    public void Do(){
        if (!crashed)
        {
            crashed = true;
            timeLeft = respawnTime + Time.time;
            Debug.Log("Crashed");
            aircraft.transform.GetChild(0).gameObject.SetActive(false);
            PhotonNetwork.Instantiate("Explosion/Explosion" + User.Instance.team, transform.position, Quaternion.identity, 0);

            aircraft.trailGenerator.GetComponent<PhotonView>().RPC("Detach", PhotonTargets.All);
        }
        if (timeLeft - Time.time <= 0)
        {
            crashed = false;
            aircraft.Respawn();
            aircraft.source.PlayOneShot(aircraft.respawnClip);
        }
    }
    public void Activate()
    {
        Debug.Log("Can't be activated before prepare");
    }

    public void Crash(){
        Debug.Log("You have crashed");
    }
    public void Respawn()
    {
        if (!aircraft.isGameEnd)
        {
            stateMachine.SetState(stateMachine.prepareState);
        }
    }
    public void EndGame(){
        if (!aircraft.isGameEnd)
        {
            stateMachine.SetState(stateMachine.endState);
        }
    }
}
