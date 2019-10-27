using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeAircraftInfo : UIComponent
{
    public GameObject info;
    public override void MouseClick()
    {
        
    }

    public override void MouseEnter()
    {
        isHover = true;
        //info.SetActive(true);
    }

    public override void MouseExit()
    {
        isHover = false;
        //info.SetActive(false);
    }

    public void Update()
    {
        if (isHover)
        {
            info.SetActive(true);
        }
        else
        {
            info.SetActive(false);
        }
    }
}
