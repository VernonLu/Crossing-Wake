using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeInput : UIComponent {


	private IMouseEvent enterEvent;
	private IMouseEvent clickEvent;
	private IMouseEvent exitEvent; 

	private void Start () {
		enterEvent = new EnterEvent();
		clickEvent = new InputClickEvent();
		exitEvent = new ExitEvent();
	}
	public override void MouseEnter(){
		enterEvent.Execute(transform);
	}
	public override void MouseClick(){
		clickEvent.Execute(transform);
	}
	public override void MouseExit(){
		exitEvent.Execute(transform);
	}
}
