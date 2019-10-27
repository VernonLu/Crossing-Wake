using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InputClickEvent : IMouseEvent {
	public void Execute(Transform transform){
		transform.GetComponentInChildren<InputField>().ActivateInputField();
		//EventSystem.current.SetSelectGameObject(inputField.gameobject);
	}
}
