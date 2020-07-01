using UnityEngine;
using System.Collections;

public class spawn_script : MonoBehaviour {

	public GameObject enemy1;
	public GameObject enemy2; 
	public GameObject enemy3; 
	public GameObject enemy4; 
	public GameObject enemy5; 

	public float spawnTime;

	private float spawnTime1 = 4f; 
	private float nextSpawnTime1;

	private float spawnTime2 = 3f; 
	private float nextSpawnTime2;

	private float spawnTime3 = 13f; 
	private float nextSpawnTime3;

	private float spawnTime4 = 8f; 
	private float nextSpawnTime4;

	private float spawnTime5 = 10f; 
	private float nextSpawnTime5;

	private score_script scoreObj;

	private bool boss_spawned = false;
	public GameObject boss;

	void Start() { 
		scoreObj = GameObject.Find("score").GetComponent<score_script> ();
		// Chamar a função 'addEnemy' a cada 'spawnTime' segundos 
		InvokeRepeating("addEnemy", spawnTime, spawnTime);
	}


	//Adiciona os inimigos se o jogador n estiver morto
	void addEnemy(){
		if (!scoreObj.isDead && boss_spawned == false) {


			//Pegando tamanho do clonador
			Renderer renderer = GetComponent<Renderer> (); 
			var x1 = transform.position.x - renderer.bounds.size.x / 2; 
			var x2 = transform.position.x + renderer.bounds.size.x / 2;


			var score = scoreObj.score; //pontos
			
			if(Time.time > nextSpawnTime1) {//Verifica tempo de spawn do inimigo2
				nextSpawnTime1 = Time.time + spawnTime1;
				addEnemy1(x1,x2, score); //Add asteroides
			}


			if(Time.time > nextSpawnTime2) {//Verifica tempo de spawn do inimigo2
				nextSpawnTime2 = Time.time + spawnTime2;
				if (score >= 1000) {//Controle de dificuldade por pontos
					addEnemy4(x1,x2,score);
				}
			}

			if(Time.time > nextSpawnTime2) {//Verifica tempo de spawn do inimigo2
				nextSpawnTime2 = Time.time + spawnTime2;
				if (score >= 10000) {//Controle de dificuldade por pontos
					addEnemy5(x1,x2,score);
				}
			}
			if(Time.time > nextSpawnTime3) {//Verifica tempo de spawn do inimigo2
				nextSpawnTime3 = Time.time + spawnTime3;
				if (score >= 15000) {//Controle de dificuldade por pontos
					addEnemy2(x1,x2,score);
				}
			}
			if(Time.time > nextSpawnTime4) {//Verifica tempo de spawn do inimigo2
				nextSpawnTime4 = Time.time + spawnTime4;
				if (score >= 25000) {//Controle de dificuldade por pontos
					addEnemy3(x1,x2,score);
				}
			}
			if(score >= 100000 && boss_spawned == false){
				boss_spawned = true;
				Instantiate (boss, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
			}
		}
	}

	//instanciando asteroides
	void addEnemy1(float x1, float x2, int score) {
		var randomInt = Random.Range (1, 6);

		Instantiate (enemy1, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);

		if (randomInt == 1) {
			for (int i=0; i<=1; i++) {
				Instantiate (enemy1, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
			}
		}
		if (randomInt == 2) {
			for (int i=0; i<=2; i++) {
				Instantiate (enemy1, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
			}
		}
		if (randomInt == 3) {
			for (int i=0; i<=2; i++) {
				Instantiate (enemy1, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
			}
		}

		if (score > 1000) {
				for (int i=0; i<=2; i++) {
					Instantiate (enemy1, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
				}
		}
		if (score > 10000) {
				for (int i=0; i<=3; i++) {
					Instantiate (enemy1, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
			}
		}

	}

	//instanciando inimigos2 - com controle de dificuldade por pontos
	//gera um numero aleatorio para spawnar mais inimigo
	void addEnemy2(float x1, float x2, int score) {

		var randomInt = Random.Range (1, 10);

		Instantiate (enemy2, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
		if (score > 15000 && score < 20000) {

			if (randomInt == 1) {
				for(int i=0;i<=1;i++){
					Instantiate (enemy2, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
				}
			}
			if (randomInt == 5) {
				for(int i=0;i<=2;i++){
					Instantiate (enemy2, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
				}
			}
			if (randomInt == 10) {
				for(int i=0;i<=2;i++){
					Instantiate (enemy2, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
				}
			}

		}
		if (score > 20000 && score < 25000) {
			if (randomInt == 1) {
				for(int i=0;i<=1;i++){
					Instantiate (enemy2, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
				}
			}
			if (randomInt == 5) {
				for(int i=0;i<=1;i++){
					Instantiate (enemy2, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
				}
			}
			if (randomInt == 10) {
				for(int i=0;i<=2;i++){
					Instantiate (enemy2, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
				}
			}
		}
		if (score > 25000 && score < 50000) {
			if (randomInt == 1) {
				for(int i=0;i<=2;i++){
					Instantiate (enemy2, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
				}
			}
			if (randomInt == 5) {
				for(int i=0;i<=2;i++){
					Instantiate (enemy2, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
				}
			}
			if (randomInt == 10) {
				for(int i=0;i<=3;i++){
					Instantiate (enemy2, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
				}
			}
		}
		if (score > 50000 && score < 10000) {
			if (randomInt == 1) {
				for(int i=0;i<=3;i++){
					Instantiate (enemy2, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
				}
			}
			if (randomInt == 5) {
				for(int i=0;i<=3;i++){
					Instantiate (enemy2, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
				}
			}
			if (randomInt == 10) {
				for(int i=0;i<=3;i++){
					Instantiate (enemy2, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
				}
			}
		}
	}

	void addEnemy3(float x1, float x2, int score) {
		Instantiate (enemy3, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
	}
	void addEnemy4(float x1, float x2, int score) {
		Instantiate (enemy4, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);

		var randomInt = Random.Range (1, 10);
		if (score > 10000 && score < 25000) {
			if (randomInt == 1) {
				for(int i=0;i<=1;i++){
					Instantiate (enemy4, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
				}
			}
			if (randomInt == 5) {
				for(int i=0;i<=1;i++){
					Instantiate (enemy4, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
				}
			}
			if (randomInt == 10) {
				for(int i=0;i<=2;i++){
					Instantiate (enemy4, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
				}
			}
		}
		if (score > 25000 && score < 50000) {
			if (randomInt == 1) {
				for(int i=0;i<=2;i++){
					Instantiate (enemy4, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
				}
			}
			if (randomInt == 5) {
				for(int i=0;i<=2;i++){
					Instantiate (enemy4, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
				}
			}
			if (randomInt == 10) {
				for(int i=0;i<=3;i++){
					Instantiate (enemy4, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
				}
			}
		}
		
		if (score > 50000 && score < 100000) {
			if (randomInt == 1) {
				for(int i=0;i<=2;i++){
					Instantiate (enemy4, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
				}
			}
			if (randomInt == 5) {
				for(int i=0;i<=3;i++){
					Instantiate (enemy4, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
				}
			}
			if (randomInt == 10) {
				for(int i=0;i<=3;i++){
					Instantiate (enemy4, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
				}
			}
		}
	}
	void addEnemy5(float x1, float x2, int score) {
		Instantiate (enemy5, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
		var randomInt = Random.Range (1, 10);
		if (score > 10000 && score < 25000) {
			if (randomInt == 1) {
				for(int i=0;i<=1;i++){
					Instantiate (enemy5, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
				}
			}
			if (randomInt == 5) {
				for(int i=0;i<=1;i++){
					Instantiate (enemy5, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
				}
			}
			if (randomInt == 10) {
				for(int i=0;i<=2;i++){
					Instantiate (enemy5, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
				}
			}
		}
		if (score > 25000 && score < 50000) {
			if (randomInt == 1) {
				for(int i=0;i<=2;i++){
					Instantiate (enemy5, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
				}
			}
			if (randomInt == 5) {
				for(int i=0;i<=2;i++){
					Instantiate (enemy5, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
				}
			}
			if (randomInt == 10) {
				for(int i=0;i<=3;i++){
					Instantiate (enemy5, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
				}
			}
		}
		
		if (score > 50000 && score < 100000) {
			if (randomInt == 1) {
				for(int i=0;i<=2;i++){
					Instantiate (enemy5, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
				}
			}
			if (randomInt == 5) {
				for(int i=0;i<=3;i++){
					Instantiate (enemy5, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
				}
			}
			if (randomInt == 10) {
				for(int i=0;i<=3;i++){
					Instantiate (enemy5, new Vector2 (Random.Range (x1, x2), transform.position.y), Quaternion.identity);
				}
			}
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
