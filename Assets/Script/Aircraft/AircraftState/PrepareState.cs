using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareState : IAircraftState {

    private bool preparing;
    private float timeLeft;
    private float flickerTimeLeft;
    private float prepareTime;
    private Aircraft aircraft;
    private Transform transform;
    private Transform spawnPoint;
    private AircraftStateMachine stateMachine;
    private GameObject trailGenerator;
    private Transform generatorPos;

    public PrepareState(AircraftStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        aircraft = stateMachine.GetAircraft();
        transform = aircraft.transform;
        spawnPoint = aircraft.GetSpawnPoint();
        prepareTime = aircraft.prepareTime;
        trailGenerator = aircraft.generatorPrefab;
        generatorPos = aircraft.generatorPos;
        preparing = false;
    }
    public void Do()
    {
        if (!preparing)
        {
            preparing = true;

            //Set prepare time
            timeLeft = prepareTime + Time.time;

            //Show aircraft
            aircraft.transform.GetChild(0).gameObject.SetActive(true);
            
            //Reset player to spawn point
            aircraft.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);

            Debug.Log("Waiting for activate...");
        }

        //Activate aircraft when prepared
        if (timeLeft - Time.time <= 0 && preparing)
        {
            preparing = false;

            aircraft.StartCoroutine(ActivateTrail());

            aircraft.StartCoroutine(Flicker());

            //Activate aircraft
            Activate();

            return;
        }
    }

    public void Activate()
    {
        //Set state machine to play state
        if (!aircraft.isGameEnd)
        {
            stateMachine.SetState(stateMachine.playState);
        }
    }

	public void Crash(){
        Debug.LogWarning("Can't be killed while preparing");
	}

	public void Respawn(){
        Debug.LogWarning("Respawning...");
	}

	public void EndGame()
    {
        if (!aircraft.isGameEnd)
        {
            stateMachine.SetState(stateMachine.endState);
        }
    }

    public IEnumerator ActivateTrail()
    {
        //Instantiate trail generator
        aircraft.trailGenerator = PhotonNetwork.Instantiate("Trail/" + trailGenerator.name, generatorPos.position, generatorPos.rotation, 0);

        string team = User.Instance.team;
        //Get material name
        string matName = "TrailMat"+team;

        //Set material for trail renderer
        PhotonView pv = aircraft.trailGenerator.GetComponent<PhotonView>();
        pv.RPC("SetMaterial", PhotonTargets.All, matName);

        //Set parent transform for trail generator
        pv.RPC("SetParent", PhotonTargets.All, team);

        //The generator should not work immediately to avoid killing yourself when respawned
        //yield return new WaitForSeconds(delayTime);
        yield return null;
        //Activate trail renderer
        pv.RPC("Activate", PhotonTargets.All);
    }

    private IEnumerator Flicker()
    {
        flickerTimeLeft = aircraft.invincibleTime + Time.time;
        aircraft.invincible = true;
        bool isActive = false;
        while (flickerTimeLeft - Time.time > 0)
        {
            yield return new WaitForSeconds(0.2f);
            isActive = !isActive;
            transform.GetChild(0).gameObject.SetActive(isActive);
        }
        transform.GetChild(0).gameObject.SetActive(true);
        aircraft.invincible = false;
    }
}
