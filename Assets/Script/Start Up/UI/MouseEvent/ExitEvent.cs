using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitEvent : IMouseEvent{	
	public void Execute(Transform transform){
		UIComponent component = transform.GetComponent<UIComponent>();
		if(!component.isHover) return;
		transform.GetComponent<Renderer>().material = component.normalMat;
		component.isHover = false;
	}
}
