using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUp : MonoBehaviour, Subject
{
    public InputField usernameInput;
    public InputField passwordInput;
    private string username;
    private string password;
    private CameraMove cameraMove;

    private List<Observer> observers;

    void Awake()
    {
        observers = new List<Observer>();
    }

    void Start()
    {
        DataBaseManager.Open();
        cameraMove = Camera.main.GetComponent<CameraMove>();
        DataBaseManager.GetMapList();
        NotifyObserver();
    }

    public void Login()
    {
        username = usernameInput.text;
        password = passwordInput.text;

        if (username.Equals("") || password.Equals(""))
        {
            Debug.Log("Please Enter username and password");
            return;
        }
        Debug.Log(username + " " + password);

        //Search database to find user;
        if (DataBaseManager.Login(username, password))
        {
            usernameInput.text = "";
            passwordInput.text = "";
            cameraMove.SetTargetUI(3);
            DataBaseManager.GetOwnership();
            for(int i = 0; i < User.Instance.aircrafts.Count; i++)
            {
                DataBaseManager.GetUserAircrafts(i);
            }
            PhotonNetwork.player.NickName = User.Instance.nickname;
            //DataBaseManager.Close();
            NotifyObserver();
            return;
        }
        Debug.Log("Invalid username or password");
    }

    public void Purchase(string aircraftID)
    {
        //DataBaseManager.Open();
        DataBaseManager.InsertOwnership(User.Instance.userID, aircraftID);
        AircraftInfo newAircraft = new AircraftInfo(aircraftID);
        newAircraft = DataBaseManager.GetAircraftInfo(aircraftID);
        User.Instance.aircrafts.Add(newAircraft);
        //DataBaseManager.Close();
        NotifyObserver();
    }
    
    public void GoToUI(int index)
    {
        cameraMove.SetTargetUI(index);
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }

    public void NotifyObserver()
    {
        foreach(Observer observer in observers)
        {
            observer.Notify();
        }
    }

    public void AddObserver(Observer observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(Observer observer)
    {
        observers.Remove(observer);
    }

    private void OnApplicationQuit()
    {
        DataBaseManager.Close();
    }
}
