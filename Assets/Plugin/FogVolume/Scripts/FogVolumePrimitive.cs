﻿using UnityEngine;
[ExecuteInEditMode]
public class FogVolumePrimitive : MonoBehaviour
{
    GameObject Primitive;
    public  Material PrimitiveMaterial;
    Renderer _Renderer;

    void OnEnable()
    {
        Primitive = this.gameObject;
        _Renderer = Primitive.GetComponent<MeshRenderer>();
        if(!PrimitiveMaterial)
        PrimitiveMaterial = (Material)Resources.Load("PrimitiveMaterial");
        _Renderer.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
        _Renderer.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
        _Renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        _Renderer.receiveShadows = false;
      //  if(GetComponent<BoxCollider>())
      //  DestroyImmediate(GetComponent<BoxCollider>());
        GetComponent<MeshRenderer>().material = PrimitiveMaterial;
       // print("Created primitive");
      //  print(PrimitiveMaterial.name);
    }

   // public string DebugSize, DebugPosition;
    Vector3 Size/*, Position*/;
   
    float MinScale = .0001f;
    // Update is called once per frame
    void Update()
    {

       // Position = transform.position;
        Size.x = Mathf.Max(MinScale, transform.lossyScale.x);
        Size.y = Mathf.Max(MinScale, transform.lossyScale.y);
        Size.z = Mathf.Max(MinScale, transform.lossyScale.z);
        //transform.localScale = Size;
     //   DebugSize = Size.ToString("0.000");
     //   DebugPosition = Position.ToString("0.000");
    }

    public Transform GetTransform
    {
        get
        {
            return Primitive.transform;
        }
    }


    public Vector3 GetPrimitiveScale
    {
        get
        {
            return Size;
        }

    }
}
