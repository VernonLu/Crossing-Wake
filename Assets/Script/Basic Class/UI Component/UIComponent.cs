using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIComponent : MonoBehaviour {
	public bool isHover;
	public bool isFocus;
	public Material normalMat;
	public Material highlightMat;
	public abstract void MouseEnter();
	public abstract void MouseClick();
	public abstract void MouseExit();


}
