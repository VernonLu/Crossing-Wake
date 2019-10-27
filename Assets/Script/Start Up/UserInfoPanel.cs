using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UserInfoPanel : MonoBehaviour, Observer {

    public Text nicknameText;
    public Text scoreText;
    public Text cashText;

	void Start () {
        GameObject.Find("EventSystem").GetComponent<StartUp>().AddObserver(this);
        ShowUserInfo();
    }

    public void Notify()
    {
        ShowUserInfo();
    }
    public void ShowUserInfo()
    {
        nicknameText.text = User.Instance.nickname;
        scoreText.text = User.Instance.score.ToString();
        cashText.text = User.Instance.cash.ToString();
    }
}
