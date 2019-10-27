using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCollision : MonoBehaviour {
    private void OnTriggerEnter(Collider collider)
    {
        transform.parent.GetComponent<KillPlayer>().Kill(collider.transform);
    }
}
