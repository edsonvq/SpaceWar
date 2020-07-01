using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy3_script : MonoBehaviour {
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
		rb.velocity = transform.up * speed;
		sprite = GetComponent<SpriteRenderer>();
	}

	//Verifica se jogador n esta morto e direciona objeto para o jogador
	void Update () {
		if (!score.isDead) {
			Vector3 target = player.position - transform.position;
			float angle = Mathf.Atan2 (target.y, target.x) * Mathf.Rad2Deg;
			Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp (transform.rotation, q, Time.deltaTime * 2);
		} else {
			Destroy(this.gameObject); 
		}
	}


	//verifica a tag da bala do jogador
	//tira vida do inimigo
	//instancia uma explosao
	//e soma pontos
	void OnTriggerEnter2D (Collider2D outro){
		if(outro.gameObject.tag == "player_ShotTag"){
			StartCoroutine(TakingDamage());
			life = life - 1;
			if(life==0){
				score.score = score.score + 2000;
				Instantiate(explosion, transform.position, transform.rotation);
				Destroy(this.gameObject); 
			}
			Destroy(outro);
		} 
	}

	//Muda cor do sprite para vermelho ao tomar dano
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

	//Destroi depois de sair da tela
	void OnBecameInvisible() {
		Destroy (gameObject);
	}
}
