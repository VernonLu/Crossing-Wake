using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public List<Transform> cameraPos;
    public float moveTime = 2f;

    private Transform currentPos;
    private Transform targetPos;
    private bool moving;

	// Use this for initialization
	void Start () {
		if(User.Instance.userName != null)
        {
            transform.SetPositionAndRotation(cameraPos[2].position, cameraPos[2].rotation);
        }
        else
        {
            transform.SetPositionAndRotation(cameraPos[0].position, cameraPos[0].rotation);
        }
        moving = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (moving)
        {
            Move();
        }
	}

    private void Move()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos.position, moveTime * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetPos.rotation, moveTime * Time.deltaTime);
        if((transform.position - targetPos.position).magnitude <= 0.01f) { moving = false; }
    }
    public void SetTargetUI(int index)
    {
        index -= 1;
        if(index <= cameraPos.Count)
        {
            moving = true;
            targetPos = cameraPos[index];
        }
    }

}
