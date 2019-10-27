using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeNav : MonoBehaviour {

    public float interval = 1.6f;
    public Transform[] navPanel;
    private int index;
	// Use this for initialization
	void OnEnable () {
        index = -1;
        InvokeRepeating("ChangePanel", 0, interval);
	}
    private void ChangePanel()
    {
        for(int i = 0; i < navPanel.Length; i++)
        {
            navPanel[i].localScale = Vector3.zero;
        }
        index = (index + 1) % navPanel.Length;
        navPanel[index].localScale = Vector3.one;
    }
}
