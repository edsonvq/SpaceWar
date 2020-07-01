using UnityEngine;
using System.Collections;

public class PlanetScript : MonoBehaviour {
	public float speed = -0.5f;
	public bool moving;//verificar se planata esta movendo

	Vector2 min;
	Vector2 max;


	void Start () {
		moving = true;
		min = new Vector2 (-6f, -15f);
		max = new Vector2 (6f, 15f);
		if(gameObject.GetComponent<Rigidbody2D>() != null){
			Rigidbody2D rb = GetComponent<Rigidbody2D>();
			rb.angularVelocity = Random.Range(-15, 15);
		}

    }


	void Update () {
		if (!moving)return;

		//Movimenta planeta para baixo
		Vector2 position = transform.position;
		position = new Vector2 (position.x, position.y + speed * Time.deltaTime);
		transform.position = position;
		if (transform.position.y < min.y) {
			moving = false;
		}


	}

	//Restaura posicao do planeta
	public void ResetPosition(){
		transform.position = new Vector2 (Random.Range (min.x, max.x), max.y);
	}
}
