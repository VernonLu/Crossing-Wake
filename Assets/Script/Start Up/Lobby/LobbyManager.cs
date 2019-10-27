using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class LobbyManager : PunBehaviour {
    
    public int maxPlayerNumber = 2;
    public string gameVersion = "0.0.1";
    private bool connecting;


    public Canvas matchingCanvas;
    public float waitTime = 2f;
    public float waitTimeLeft;

    public LoadingPanelManager loadingManager;


	void Start () {
        //打开连接
        connecting = true;
        if(PhotonNetwork.connectionState == ConnectionState.Disconnected)
        {
            ConnectToServer();
            StartCoroutine(HideNav());
        }
        else
        {
            loadingManager.gameObject.SetActive(false);
        }
        
	}

    //连接服务器
    private void ConnectToServer()
    {
        waitTimeLeft = waitTime + Time.time;
        PhotonNetwork.ConnectUsingSettings(gameVersion);
    }
    public void Reconnect()
    {
        loadingManager.Reconnect();
        ConnectToServer();
    }
    
    public override void OnConnectedToPhoton()
    {
        connecting = false;
    }

    //连接服务器失败
    public override void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        loadingManager.ConnectFail();
    }

    //开始匹配
    public void StartMatching()
    {
        //随即加入房间
        PhotonNetwork.JoinRandomRoom();
        matchingCanvas.gameObject.SetActive(true);
    }

    //停止匹配
    public void StopMatching()
    {
        PhotonNetwork.LeaveRoom();
    }

    //加入随机房间失败
    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        //stateText.text = "Random join failed...";
        RoomOptions option = new RoomOptions() { MaxPlayers = (byte)maxPlayerNumber };
        PhotonNetwork.CreateRoom(GetRandomRoomName(), option, null);
    }

    //创建房间并加入
    public override void OnCreatedRoom()
    {
        //stateText.text = "Create room...";
        Debug.Log("Created room");
    }

    //加入房间
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room");
        //自动同步场景
        PhotonNetwork.automaticallySyncScene = true;
        User.Instance.team = PhotonNetwork.playerList.Length.ToString();
        //StartCoroutine(LoadingScene(MapList.GetRandomMap()));
    }

    //新玩家加入房间
    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        int number = PhotonNetwork.playerList.Length;
        Debug.Log(number + "player have joined the room");

        //判断房间是否人满
        if(number == maxPlayerNumber)
        {
            Debug.Log("Room full");

            //由主机加载场景
            if (PhotonNetwork.isMasterClient)
            {
                PhotonNetwork.room.IsOpen = false;
                StartCoroutine(LoadingScene(MapList.GetRandomMap()));
            }
        }
    }


    #region Loading Progress
    private IEnumerator LoadingScene(string sceneName)
    {
        int displayProgress = 0;
        int toProgress = 0;

        //异步对象
        AsyncOperation op = PhotonNetwork.LoadLevelAsync(sceneName);

        //场景加载完自动跳转
        op.allowSceneActivation = true;

        //加载进度
        while (op.progress < 0.9f)
        {
            toProgress = (int)op.progress * 100;
            while (displayProgress < toProgress)
            {
                ++displayProgress;
                //SetLoadingPercentage(displayProgress);
                yield return new WaitForEndOfFrame();
            }
        }

        toProgress = 100;
        while (displayProgress < toProgress)
        {
            ++displayProgress;
            //SetLoadingPercentage(displayProgress);
            yield return new WaitForEndOfFrame();
        }
        op.allowSceneActivation = true;
    }

    //显示进度
    //private void SetLoadingPercentage(int DisplayProgress)      
    //{
    //    loadingSlider.value = DisplayProgress * 0.01f;
    //    stateText.text = DisplayProgress.ToString() + "%";
    //}
    #endregion


    /// <summary>
    /// 随机生成房间名
    /// </summary>
    /// <returns></returns>
    private string GetRandomRoomName()
    {
        string roomName = "";
        for (int i = 0; i < 8; i++)
        {
            roomName += Random.Range(0, 10).ToString();
        }
        return roomName;
    }


    private IEnumerator HideNav()
    {
        while (connecting || (waitTimeLeft - Time.time > 0))
        {
            yield return new WaitForEndOfFrame();
        }
        loadingManager.gameObject.SetActive(false);

    }
}
