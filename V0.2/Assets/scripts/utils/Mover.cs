using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public float speed;

    private Rigidbody2D rb;
    private LevelController lvlController;
    private Transform player;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;

        lvlController = GameObject.Find("score").GetComponent<LevelController>();
        player = FindObjectOfType<Player>().transform;
    }


    void Update()
    {
        if (lvlController.isDead)
        {

           Destroy(this.gameObject);
        }
    }

}
