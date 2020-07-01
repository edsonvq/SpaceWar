using UnityEngine;
using System.Collections;

public class enemy1_script : MonoBehaviour {
	public GameObject explosion;
	private score_script scoreObj;

	void Start () {
		// Adicionar speed à velocidade do inimigo 
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();
		scoreObj = GameObject.Find("score").GetComponent<score_script> ();

		var speed = Random.Range (-2, -6);
		rb.velocity = new Vector2(0, speed);
		
		// Faz o inimigo rodar em si mesmo aleatoriamentre entre -200 e 200
		rb.angularVelocity = Random.Range(-180, 180);
		
		// Destroi o inimigo após 3s, que ele não está mais visível na tela 
		Destroy(gameObject, 5);

	}

	void Update () {

	}

	//verifica a a tag da bala do jogador
	//instancia uma explosao
	//e soma pontos
	void OnTriggerEnter2D (Collider2D outro){
		if(outro.gameObject.tag == "player_ShotTag"){ 
			scoreObj.score = scoreObj.score +100;
			Instantiate(explosion, transform.position, transform.rotation);
			Destroy(this.gameObject);
		} 
	}

	//Destroi depois que saiu da tela
	void OnBecameInvisible() {
		Destroy (gameObject);
	}

}
