using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class player_script : MonoBehaviour {
	//Configs do player
	public int speed = 13;
	private int life = 15;

	public float fire_rate; 
	public int fire_level = 0;
	public GameObject shot;
	private float nextFire;
	public Transform[] shot_spawn; //Onde sairam as balas da nave

	private SpriteRenderer sprite;
	private score_script score;

	//Para animacao
	private bool right;
	private bool left;
	private Animator anim;

	public Text lifeUI;

	public enum ItemEffect{
		shild,
		fire_lvl_up,
		fire_rate_up,
		health_up,
		especial_shot_1,
	}

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetString("dead", "false");
		//Pegando referencias
		anim = GetComponent<Animator>();
		sprite = GetComponent<SpriteRenderer>();
		score = GameObject.Find("score").GetComponent<score_script> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Movendo player
		float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;

		//se apertou pros lados
		right = Input.GetButton("Right");
		left = Input.GetButton("Left");

		//setando condicao de animacao
		anim.SetBool("right", right);
		anim.SetBool("left", left);

		transform.Translate (horizontal, vertical, 0);

		if (score.score >= 15000) {
			//fire_level=2;
		}
		if (score.score >= 25000) {
			fire_level=2;
		}

		//Limite Vertical
		if (transform.position.x <= -5.7f || transform.position.x >= 5.7f) {
			// Criando o limite 
			float xPos = Mathf.Clamp (transform.position.x, -5.7f, 5.7f); 
			// Limitando 
			transform.position = new Vector3 (xPos, transform.position.y, transform.position.z); 
		}


		//Limite horizontal
		if (transform.position.y <= -4.4f || transform.position.y >= 2.4f) {
			// Criando o limite 
			float yPos = Mathf.Clamp (transform.position.y, -4.4f, 2.4f); 
			// Limitando 
			transform.position = new Vector3 (transform.position.x, yPos, transform.position.z); 
		}
		
		// Quando a barra de espaços é pressionada ele atira
		if (Input.GetButton("Jump")  && Time.time > nextFire) {
			nextFire = Time.time + fire_rate;

			// Cria uma bala na posiçao se fire level = 1
			if (fire_level == 1)
			{
				Instantiate(shot, shot_spawn[0].position, Quaternion.identity);
			}
			// Cria duas bala na posiçao se fire level = 2
			if (fire_level == 2)
			{
				Instantiate(shot, shot_spawn[1].position, Quaternion.identity);
				Instantiate(shot, shot_spawn[2].position, Quaternion.identity);
			}
			// Cria tres bala na posiçao se fire level = 3
			if (fire_level == 3)
			{
				Instantiate(shot, shot_spawn[0].position, Quaternion.identity);
				Instantiate(shot, shot_spawn[1].position, Quaternion.identity);
				Instantiate(shot, shot_spawn[2].position, Quaternion.identity);
			}

		}

		//Mostra vida atual
		lifeUI.text = "" + life;
	}

	//Tomou dano
	void OnTriggerEnter2D (Collider2D outro){
		if(outro.gameObject.tag == "enemy1Tag" || outro.gameObject.tag == "enemy2_ShotTag" || outro.gameObject.tag == "enemy3_ShotTag" || outro.gameObject.tag == "enemy5_ShotTag"){
			life = life - 1; // Cada colisao perde uma vida 

			if(life==0){// Quando vida = 0 jogador morre
				score.isDead=true;
				PlayerPrefs.SetString("dead", "true");
				Destroy(this.gameObject); 
				Application.LoadLevel("menu");
			}

			//Para piscar vermelho
			StartCoroutine(TakingDamage());

			Destroy(outro.gameObject);
		}
	}

	//Muda a cor do sprite do player
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

	public void SetItemEffect(ItemEffect effect){
		if (effect == ItemEffect.shild) {

		}
		if (effect == ItemEffect.fire_lvl_up) {
			fire_level++;
			if(fire_level>=3){
				fire_level=3;
			}

		}
	}
}
