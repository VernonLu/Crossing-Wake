using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour {
    
    public Slider timeSlider;
    public AudioClip finalClip;
    public AudioSource source;

    private float timeLeft;
    public float prepareTime;
    public float gameTime;
    private bool preparing;
    private bool counting;

    public List<string> players = new List<string>();
    
	void Start () {
        preparing = true;
        prepareTime += Time.time;
        timeLeft = prepareTime;
        timeSlider.maxValue = gameTime;
	}
	
	void Update () {

        //Countdown before start game
        PrepareTimer();
        
        GameTimer();

    }
    private void PrepareTimer()
    {
        if (preparing)
        {
            timeLeft = prepareTime - Time.time;

            if (timeLeft < 0)
            {
                timeLeft = 0;
                preparing = false;
                
                counting = true;
                gameTime += Time.time;
                timeLeft = gameTime;
            }
        }

    }

    public void GameTimer()
    {
        if(counting)
        {
            timeLeft = gameTime - Time.time;
            timeSlider.value = timeLeft;
            if (timeLeft < 10 && source.isPlaying == false)
            {
                source.clip = finalClip;
                source.Play();
            }

            if(timeLeft < 0)
            {
                counting = false;
                //Notify observer
                if (PhotonNetwork.isMasterClient)
                {
                    foreach (string player in players)
                    {
                        GameObject.Find(player).transform.GetChild(0).GetComponent<PhotonView>().RPC("EndGame", PhotonTargets.All);
                    }
                }
            }
        }
    }

    [PunRPC]
    public void Register(string playerName)
    {
        if(players.IndexOf(playerName) == -1)
        {
            players.Add(playerName);
        }
    }

}
