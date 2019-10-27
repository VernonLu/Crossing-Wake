using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetColor : MonoBehaviour {

    public Image loadingImage;
    public Text loadingLabel;

    public Color normalColor;
    public Color errorColor;
    public void SetErrorColor()
    {
        loadingImage.color = errorColor;
        loadingLabel.color = errorColor;
        loadingLabel.text =  "连接\r\n服务器\r\n失败";
    }
    public void SetNormalColor()
    {
        loadingImage.color = normalColor;
        loadingLabel.color = normalColor;
        loadingLabel.text = "加载中...";
    }
}
