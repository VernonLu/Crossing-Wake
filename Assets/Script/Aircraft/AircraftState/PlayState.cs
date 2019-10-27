using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Vernon
/// 2018.05.28
/// </summary>
public class PlayState : IAircraftState {

    private Aircraft aircraft;
    private Transform transform;
    private AircraftStateMachine stateMachine;

    public PlayState(AircraftStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        aircraft = stateMachine.GetAircraft();
        transform = aircraft.transform;
    }

    public void Do()
    {
        SimpleModeRotate();
        //ExpertModeRotate();
        MoveForward();
    }

    public void Activate()
    {
        Debug.LogWarning("Play state: Already activated");
    }
    public void Crash()
    {
        if (!aircraft.invincible && !aircraft.isGameEnd)
        {
            stateMachine.SetState(stateMachine.crashState);
        }
    }

    public void Respawn()
    {
        Debug.LogWarning("Play state: Is alive");
    }
    public void EndGame()
    {
        if (!aircraft.isGameEnd)
        {
            stateMachine.SetState(stateMachine.endState);
        }
    }


    private void MoveForward()
    {
        Vector3 forward = transform.forward;
        transform.position += forward * aircraft.speed * Time.deltaTime; 
    }

    private void SimpleModeRotate()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 rotation = new Vector3(v, h, 0);
        transform.Rotate(rotation * Time.deltaTime * aircraft.motility);
        //Return to horizontal when player is not pressing key
        if (h == 0 && v == 0)
        {
            ReturnHorizontal();
        }

    }
    
    private void ReturnHorizontal()
    {
        Vector3 targetAngle = transform.eulerAngles;
        targetAngle.z = 0;
        Quaternion targetRotation = Quaternion.Euler(targetAngle);
        targetRotation = Quaternion.Slerp(transform.rotation, targetRotation, aircraft.smoothTime * Time.deltaTime);
        transform.SetPositionAndRotation(transform.position, targetRotation);
    }


    //private void ExpertModeRotate()
    //{
    //    float h = Input.GetAxis("Horizontal");
    //    float v = Input.GetAxis("Vertical");
    //    Vector3 rotation = new Vector3(v, 0, -h);
    //    transform.Rotate(rotation * Time.deltaTime * player.motility);
    //}
    

}
