using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastCursor : MonoBehaviour {
    private UIComponent component;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.collider.tag == "UI")
        {
            Debug.DrawLine(ray.origin, hit.point);
            component = hit.collider.GetComponent<UIComponent>();
            component.MouseEnter();
            if (Input.GetMouseButtonDown(0))
            {
                component.MouseClick();
            }

        }
        else if (null != component)
        {
            component.MouseExit();
            component = null;
        }
	}
}
