using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectAircraft : MonoBehaviour,Observer {


    private int index;
    private int maxLength;

    public Transform[] aircrafts;
    public Transform currentAircraft;
    public Transform aircraftList;

    public Text nameText;
    public Slider speedSlider;
    public Slider motilitySlider;

    private void Start()
    {
        GetComponent<StartUp>().AddObserver(this);
        maxLength = 1;
    }
    
    /// <summary>
    /// 选择后一个飞机
    /// </summary>
    public void Next()
    {
        HideAircraft();
        index = (index + 1) % maxLength;
        User.Instance.currentAircraft = User.Instance.aircrafts[index];
        ShowAircraft(User.Instance.currentAircraft.aircraftID);
        ShowAircraft(index.ToString());
        UpdateUI();
    }

    /// <summary>
    /// 选择前一个飞机
    /// </summary>
    public void Prev()
    {
        HideAircraft();
        index = (index + maxLength - 1) % maxLength;
        User.Instance.currentAircraft = User.Instance.aircrafts[index];
        ShowAircraft(User.Instance.currentAircraft.aircraftID);
        ShowAircraft(index.ToString());
        UpdateUI();
    }
    

    /// <summary>
    /// 更新飞机数据显示
    /// </summary>
    public void UpdateUI()
    {
        nameText.text = index.ToString();
        nameText.text = User.Instance.currentAircraft.aircraftName;
        speedSlider.value = User.Instance.currentAircraft.speed;
        motilitySlider.value = User.Instance.currentAircraft.motility;
    }

    private void ShowAircraft(string aircraftID)
    {
        Debug.Log(aircraftID);
        currentAircraft = aircraftList.Find(aircraftID);
        currentAircraft.gameObject.SetActive(true);
    }

    private void HideAircraft()
    {
        Debug.Log("Hide: " + index);
        currentAircraft.gameObject.SetActive(false);
    }

    public void Notify()
    {
        HideAircraft();

        //Get how many aircrafts player have
        maxLength = User.Instance.aircrafts.Count;

        //Set default selected aircraft
        index = 0;
        User.Instance.currentAircraft = User.Instance.aircrafts[index];
        ShowAircraft(User.Instance.currentAircraft.aircraftID);
    }
}
