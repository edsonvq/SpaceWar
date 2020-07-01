using UnityEngine;
using System.Collections;

public class ShotScript : MonoBehaviour {

	public float speed;
	private Rigidbody2D rb;

	private Transform player;
	private LevelController lvlController;


	void Start () {
        lvlController = GameObject.Find("score").GetComponent<LevelController> ();
		player = FindObjectOfType<Player>().transform;
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = transform.up * speed;




        if (this.gameObject.tag == "enemy3_ShotTag")
        {
            Destroy(gameObject, 3);
        }
	}
	

	void Update () {
		if (!lvlController.isDead) {

			//Se for a bala do enemy3 - Faz ela seguir o jogador
			if (this.gameObject.tag == "enemy3_ShotTag" || this.gameObject.tag == "boss_ShotTag") {
				Vector3 target = player.position;
				Vector3 moveDirection = gameObject.transform.position - target; 
				if (moveDirection != Vector3.zero) 
				{
					float angle = Mathf.Atan2(moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;
					transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
				}
				transform.position = Vector2.MoveTowards(transform.position, player.position, 3.6f * Time.deltaTime);
			}
        }
        else
        {
            Destroy(this.gameObject);
        }
	}
    

	void OnTriggerEnter2D (Collider2D outro){

        if (outro.gameObject.tag == "player_ShotTag"){//Para destruir as balas inimigas - menos a propia
            if (this.gameObject.tag != "player_ShotTag")
            {
                Destroy(this.gameObject);
                Destroy(outro);
            }
        }
        if (outro.gameObject.tag == "shieldTag")
        {//Para destruir as balas inimigas - menos a propia
            if (this.gameObject.tag != "player_ShotTag")
            {
                Destroy(this.gameObject);
            }
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
