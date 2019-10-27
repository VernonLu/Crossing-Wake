using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour, Observer
{
    public AudioClip purchaseClip;
    private AudioSource source;
    public int cost;
    public string itemID;
    public Text priceText;
    private AircraftInfo aircraft;

    private bool unlock;


    private void SetText()
    {                                                                                                                                                                                  
        if (unlock)
        {
            priceText.text = "已解锁";
        }
        else
        {
            priceText.text = "$ " + cost.ToString();
        }
    }

	void Start () {
        GameObject.Find("EventSystem").transform.GetComponent<StartUp>().AddObserver(this);
        source = GameObject.Find("ButtonsAudioSource").GetComponent<AudioSource>();

    }

    public void Notify()
    {
        unlock = false;
        foreach(AircraftInfo aircraft in User.Instance.aircrafts)
        {
            if(aircraft.aircraftID == itemID)
            {
                unlock = true;
            }
        }
        SetText();
    }

    public void Purchase()
    {
        if (!unlock)
        {
            if (User.Instance.cash >= cost)
            {
                source.PlayOneShot(purchaseClip);
                GameObject.Find("EventSystem").GetComponent<StartUp>().Purchase(itemID);
                Debug.Log("Unlock " + itemID);
                unlock = true;
                SetText();
            }
        }
    }
}
