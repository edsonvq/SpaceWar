using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    private Transform player;
    private LevelController lvlController;
    public float speed;

    // Use this for initialization
    void Start () {
        lvlController = GameObject.Find("score").GetComponent<LevelController>();
        player = FindObjectOfType<Player>().transform;
        Destroy(gameObject, 3);
    }
	
	// Update is called once per frame
	void Update () {
        if (!lvlController.isDead)
        {
            
            Vector3 target = player.position;
            Vector3 moveDirection = gameObject.transform.position - target;
            if (moveDirection != Vector3.zero)
            {
                float angle = Mathf.Atan2(moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
            }
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
