using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterEvent : IMouseEvent {
	public void Execute(Transform transform){
		UIComponent component = transform.GetComponent<UIComponent>();
		if(component.isHover) return;
		transform.GetComponent<Renderer>().material = component.highlightMat;
		component.isHover = true;
	}
}
