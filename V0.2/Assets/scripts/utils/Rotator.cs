using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
    private Transform player;
    float rotation;

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<Player>().transform;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 target = player.position;
        Vector3 moveDirection = gameObject.transform.position - target;
        if (moveDirection != Vector3.zero)
        {
            //float angle = Mathf.Atan2(moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;
            float angle = Mathf.Atan2(moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
        }
    }
}
