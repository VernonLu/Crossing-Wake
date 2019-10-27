using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonClickEvent : IMouseEvent
{
	private CubeButton button;
	private Transform transform;
	private Vector3 startPos;
	private Vector3 endPos;
	public void Execute(Transform transform){
		button = transform.GetComponent<CubeButton>();
		if(button.isFocus) return;
		button.isFocus = true;
		this. transform = transform;
		startPos = transform.position;
		endPos = transform.position + button.movement;
		button.StartCoroutine(Press());
	}
	private IEnumerator Press(){
		//Press the button
		while (!MoveTo(endPos)){
			yield return null;
		}
		yield return new WaitForSeconds(button.waitTime);
		//Release the button
		while (!MoveTo(startPos)){
			yield return null;
		}
		button.isFocus = false;
	}

	private bool MoveTo(Vector3 targetPos){
		transform.position = Vector3.Lerp(transform.position, targetPos, button.smoothTime * Time.fixedDeltaTime);
		return (Mathf.Abs((transform.position - targetPos).magnitude) < 0.01f);
	}
}
