using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowRankList : MonoBehaviour {

    public AudioClip rankListClip;
    public AudioSource source;
    public Transform rankListPanel;
    public GameObject rankListPrefab;
    private GameObject item;
    private Player[] players;

    public void ShowList()
    {
        source.PlayOneShot(rankListClip);
        GetPlayers();
        Sort();
        Show();
    }

    private void GetPlayers()
    {
        int index = 0;
        players = new Player[PhotonNetwork.playerList.Length];
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {
            string nickName = player.NickName;
            int score = player.GetScore();
            players[index++] = new Player(nickName, score);
        }
    }

    private void Sort()
    {
        for(int i = 0; i < players.Length; i++)
        {
            for(int j = 0; j < players.Length; j++)
            {
                if(players[i].score > players[j].score)
                {
                    Player temp = players[i];
                    players[i] = players[j];
                    players[j] = temp;
                }
            }
        }
    }
    private void Show()
    {
        for(int i = 0; i < players.Length; i++)
        {
            item = Instantiate(rankListPrefab, rankListPanel);
            SetText(item.transform, i + 1, players[i]);
        }
    }

    private void SetText(Transform item, int index, Player player)
    {
        item.GetChild(0).GetComponent<Text>().text = index.ToString();
        item.GetChild(1).GetComponent<Text>().text = player.nickName;
        item.GetChild(2).GetComponent<Text>().text = player.score.ToString();
    }
}
