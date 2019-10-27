using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyWall : MonoBehaviour {

    private Camera instance;
    public Camera Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GetComponent<Camera>();
            }
            return instance;
        }
    }

    void OnEnable()
    {
        Instance.depthTextureMode |= DepthTextureMode.Depth;
    }
}
