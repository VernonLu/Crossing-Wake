using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPanelManager : MonoBehaviour {

    public Text loadingLabel;
    public SetColor loading;
    public Transform buttons;

    private void Start()
    {
        loading = transform.Find("Loading").GetComponent<SetColor>();
        buttons.localScale = Vector3.zero;
    }

    public void ConnectFail()
    {
        loading.SetErrorColor();
        buttons.localScale = Vector3.one;
    }

    public void Reconnect()
    {
        loading.SetNormalColor();
        buttons.localScale = Vector3.zero;
    }
}
