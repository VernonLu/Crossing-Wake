using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CubeButton : UIComponent {

    public AudioClip enterClip;
    public AudioClip clickClip;
    public AudioSource source;
	public Vector3 movement = new Vector3(0,1f,0);
	public float smoothTime = 20f;
	public float waitTime = 0.01f;
	public UnityEvent onClick;

	private IMouseEvent enterEvent;
	private IMouseEvent clickEvent;
	private IMouseEvent exitEvent;

	private void Start(){
		enterEvent = new EnterEvent();
		clickEvent = new ButtonClickEvent();
		exitEvent = new ExitEvent();
        source = GameObject.Find("ButtonsAudioSource").GetComponent<AudioSource>();
	}
	public override void MouseEnter()
    {
        if (!isHover)
        {
            source.PlayOneShot(enterClip);
        }
        enterEvent.Execute(transform);
    }

	public override void MouseClick(){
		if(!isFocus){
            if (null != clickClip)
            {
                source.PlayOneShot(clickClip);
            }
            clickEvent.Execute(transform);
			onClick.Invoke();
		}
	}
	public override void MouseExit(){
        exitEvent.Execute(transform);
    }
}
