using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy2_script : MonoBehaviour {
	public int life = 5;
	public float speed;

	private SpriteRenderer sprite;
	private Transform player;
	public GameObject explosion;

	private score_script scoreObj;

	// instancia referencias
	void Start () {
		player = FindObjectOfType<player_script>().transform;
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();
		scoreObj = GameObject.Find("score").GetComponent<score_script> ();

		// Faz o inimigo rodar em si mesmo aleatoriamentre entre -200 e 200 
		rb.angularVelocity = Random.Range(-200, 200);
		sprite = GetComponent<SpriteRenderer>();
	}
	
	///Verifica se jogador nao esta morto e direciona o inimigo para o jogador
	void Update () {
		if (!scoreObj.isDead) {
			transform.position = Vector2.MoveTowards (transform.position, player.position, speed * Time.deltaTime);
		} else {
			Destroy(this.gameObject); 
		}
	}

	//verifica a a tag da bala do jogador
	//tira vida do inimigo
	//instancia uma explosao - caso a vida do inimigo for 0
	//e soma pontos
	void OnTriggerEnter2D (Collider2D outro){
		if(outro.gameObject.tag == "player_ShotTag"){
			StartCoroutine(TakingDamage());
			life = life - 1;
			if(life==0){
				scoreObj.score = scoreObj.score + 1500;
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
		yield return new WaitForSeconds(0.11f);//aguarda
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
