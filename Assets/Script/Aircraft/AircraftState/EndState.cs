using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndState : IAircraftState {
    
    private Aircraft aircraft;
    private bool isOver;
    public EndState(AircraftStateMachine stateMachine)
    {
        aircraft = stateMachine.GetAircraft();
        isOver = false;
    }

    public void Do()
    {
        aircraft.isGameEnd = true;
        if (!isOver)
        {
            isOver = true;
            GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(true);
            Debug.Log("GameOver!!! Press space to continue");

        }
        if (Input.GetKey(KeyCode.Space))
        {
            PhotonNetwork.LeaveRoom();
            aircraft.StartCoroutine(LoadingScene());
        }
    }

    public void Activate()
    {
        Debug.Log("Game is over");
    }

    public void Crash()
    {
        Debug.Log("Game is over");
    }

    public void EndGame()
    {
        Debug.Log("Game is over");
    }

    public void Respawn()
    {
        Debug.Log("Game is over");
    }

    private IEnumerator LoadingScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("UI");
        while (!operation.isDone)
        {
            yield return new WaitForEndOfFrame();
        }
        operation.allowSceneActivation = true;
    }
}
