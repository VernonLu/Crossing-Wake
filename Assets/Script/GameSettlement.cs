using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettlement : MonoBehaviour {

    private int kill;
    private int killed;

    private int cash;
    private int score;

    public Text cashText;
    public Text scoreText;
    private void OnEnable()
    {
        transform.GetComponent<ShowRankList>().ShowList();
        CalcuateCashAndScore();
        ShowCashAndScore();
    }

    private void CalcuateCashAndScore()
    {
        cash = PhotonNetwork.player.GetScore();
        score = cash;
        User.Instance.score += score;
        User.Instance.cash += cash;
        DataBaseManager.UpdateScoreAndCash(User.Instance.userID, User.Instance.score, User.Instance.cash);
    }
    private void ShowCashAndScore()
    {
        cashText.text = cash.ToString();
        scoreText.text = score.ToString();
    }

}
