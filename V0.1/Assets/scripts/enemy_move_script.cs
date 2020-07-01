using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_move_script : MonoBehaviour {
	public int life;
	public float speed;
	
	private SpriteRenderer sprite;
	private Transform player;
	private Rigidbody2D rb;

	public GameObject explosion;
	private score_script score;
	
	//Instanciando referencias
	void Start () {
		score = GameObject.Find("score").GetComponent<score_script> ();
		player = FindObjectOfType<player_script>().transform;
		rb = GetComponent<Rigidbody2D> ();
		var speed = Random.Range (-1, -4);
		rb.velocity = new Vector2(0, speed);
		sprite = GetComponent<SpriteRenderer>();
	}
	
	//Verifica se player n esta morto e direciona objeto para o jogador
	void Update () {
		if (!score.isDead) {
			Vector3 target = player.position;
			Vector3 moveDirection = gameObject.transform.position - target; 
			if (moveDirection != Vector3.zero) 
			{
				float angle = Mathf.Atan2(moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
			}
		} else {
			Destroy(this.gameObject); 
		}
	}
	
	
	//verifica a a tag da bala do jogador
	//tira vida do inimigo
	//e soma pontos
	void OnTriggerEnter2D (Collider2D outro){
		if(outro.gameObject.tag == "player_ShotTag"){
			life = life - 1;
			if(life==0){
				score.score = score.score + 800;
				Instantiate(explosion, transform.position, transform.rotation);
				Destroy(this.gameObject); 
			}
			StartCoroutine(TakingDamage());
			Destroy(outro);
		} 
	}
	
	IEnumerator TakingDamage()
	{
		sprite.color = Color.red;
		yield return new WaitForSeconds(0.11f);
		sprite.color = Color.white;
		yield return new WaitForSeconds(0.11f);
		sprite.color = Color.red;
		yield return new WaitForSeconds(0.11f);
		sprite.color = Color.white;
	}
	void OnBecameInvisible() {
		Destroy (gameObject);
	}
}
