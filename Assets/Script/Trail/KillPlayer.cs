using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour {
    
    private List<Transform> killList = new List<Transform>();

    private Aircraft myAircraft;

    private void Start()
    {
        myAircraft = transform.parent.GetComponentInChildren<Aircraft>();
    }

    public void Kill(Transform aircraft)
    {
        if (killList.IndexOf(aircraft) == -1)
        {
            Debug.Log("Enter");
            killList.Add(aircraft);
            if(aircraft != myAircraft.transform)
            {
                PhotonNetwork.player.AddScore(1);
            }
            Debug.Log(PhotonNetwork.player.NickName + ":" + PhotonNetwork.player.GetScore());
            PhotonView view = aircraft.GetComponent<PhotonView>();
            view.RPC("Crash", PhotonTargets.All);
            StartCoroutine(RemoveFromKillList(aircraft));
        }
    }

    private IEnumerator RemoveFromKillList(Transform player)
    {
        yield return new WaitForSeconds(1f);
        killList.Remove(player);
    }
}
