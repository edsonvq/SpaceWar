using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpawnController : MonoBehaviour {
    public static SpawnController spawnController;

    public int enemyCount = 1;
    public int enemyCountMax;
    public float startWait;
    public Vector2 spawnWait;
    public float spawnWaitMin;
    public float waveWait;
    public float waveWaitMin;
    public GameObject[] enemies;
    public int chanceToDroptItem;

	public GameObject boss_1;
	public bool boss_spawned;
	
    private LevelController lvlController;

	
    // Use this for initialization
    void Start()
    {
        spawnController = this;
        lvlController = GameObject.Find("score").GetComponent<LevelController>();
        StartCoroutine(SpawnWaves());
    }

    // Update is called once per frame
    public void Update() {

    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while (!lvlController.gameOver)
        {
			if (lvlController.score  < 30000) {
                for (int i = 0; i < enemyCount; i++)
                {
                    GameObject enemy = enemies[0];
                    Renderer renderer = GetComponent<Renderer>();
                    var x1 = transform.position.x - renderer.bounds.size.x / 2;
                    var x2 = transform.position.x + renderer.bounds.size.x / 2;

                    Instantiate(enemy, new Vector2(Random.Range(x1, x2), transform.position.y), Quaternion.identity);
                    yield return new WaitForSeconds(Random.Range(spawnWait.x, spawnWait.y));
                }
			}

            if (lvlController.score > 500 && lvlController.score  < 30000) {
                int e = getEnemySpawn(lvlController.score, 1);

                for (int i = 0; i < e; i++)
                {
                    //GameObject enemy = enemies[Random.Range(0, enemies.Length)];
                    GameObject enemy = enemies[1];
                    Renderer renderer = GetComponent<Renderer>();
                    var x1 = transform.position.x - renderer.bounds.size.x / 2;
                    var x2 = transform.position.x + renderer.bounds.size.x / 2;

                    Instantiate(enemy, new Vector2(Random.Range(x1, x2), transform.position.y), Quaternion.identity);

                    yield return new WaitForSeconds(Random.Range(spawnWait.x, spawnWait.y));
                }
            }
            
			if (lvlController.score > 8000 && lvlController.score < 30000){
                int e = getEnemySpawn(lvlController.score, 2);

                for (int i = 0; i < e; i++)
                {
                    //GameObject enemy = enemies[Random.Range(0, enemies.Length)];
                    GameObject enemy = enemies[2];
                    Renderer renderer = GetComponent<Renderer>();
                    var x1 = transform.position.x - renderer.bounds.size.x / 2;
                    var x2 = transform.position.x + renderer.bounds.size.x / 2;

                    Instantiate(enemy, new Vector2(Random.Range(x1, x2), transform.position.y), Quaternion.identity);

                    yield return new WaitForSeconds(Random.Range(spawnWait.x, spawnWait.y));
                }
            }
            
			if (lvlController.score > 15000 && lvlController.score < 30000){
                int e = getEnemySpawn(lvlController.score, 3);
                //Debug.Log("D-> " + e);
                for (int i = 0; i < e; i++)
                {
                    //GameObject enemy = enemies[Random.Range(0, enemies.Length)];
                    GameObject enemy = enemies[3];
                    Renderer renderer = GetComponent<Renderer>();
                    var x1 = transform.position.x - renderer.bounds.size.x / 2;
                    var x2 = transform.position.x + renderer.bounds.size.x / 2;

                    Instantiate(enemy, new Vector2(Random.Range(x1, x2), transform.position.y), Quaternion.identity);

                    yield return new WaitForSeconds(Random.Range(spawnWait.x, spawnWait.y));
                }
            }
			
			if (lvlController.score > 20000 && lvlController.score < 30000){
                int e = getEnemySpawn(lvlController.score, 1);
                //Debug.Log("D-> " + e);
                for (int i = 0; i < e; i++)
                {
                    //GameObject enemy = enemies[Random.Range(0, enemies.Length)];
                    GameObject enemy = enemies[4];
                    Renderer renderer = GetComponent<Renderer>();
                    var x1 = transform.position.x - renderer.bounds.size.x / 2;
                    var x2 = transform.position.x + renderer.bounds.size.x / 2;

                    Instantiate(enemy, new Vector2(Random.Range(x1, x2), transform.position.y), Quaternion.identity);

                    yield return new WaitForSeconds(Random.Range(spawnWait.x, spawnWait.y));
                }
            }
			
			if(lvlController.score >= 30000){
				
				if(boss_spawned == false){
					boss_spawned = true;
                    Renderer renderer = GetComponent<Renderer>();
                    var x1 = transform.position.x - renderer.bounds.size.x / 2;
                    var x2 = transform.position.x + renderer.bounds.size.x / 2;

                    Instantiate(boss_1, new Vector2(Random.Range(x1, x2), transform.position.y-1), Quaternion.identity);

                    yield return new WaitForSeconds(Random.Range(spawnWait.x, spawnWait.y));
				}
			}
			
            enemyCount++;

            if (enemyCount >= enemyCountMax)
                enemyCount = enemyCountMax;

            spawnWait.x -= 0.1f;
            spawnWait.y -= 0.1f;

            if (spawnWait.y <= spawnWaitMin)
                spawnWait.y = spawnWaitMin;
            if (spawnWait.x <= spawnWaitMin)
                spawnWait.x = spawnWaitMin;

            yield return new WaitForSeconds(waveWait);

            waveWait -= 0.1f;
            if (waveWait <= waveWaitMin)
                waveWait = waveWaitMin;
        }

    }
  
    public int getEnemySpawn(int score, int enemy)
    {
        int e = 1;

        if (score >= 500)
        {
            e = Random.Range(1, 2);
            if (enemy == 2)
            {
                e = Random.Range(1, 2);
            }
            if (enemy == 3)
            {
                e = Random.Range(0, 1);
            }
        }
        if (score >= 1000)
        {
            e = Random.Range(1, 3);
            if (enemy == 2)
            {
                e = Random.Range(1, 3);
            }
        }
        if (score >= 2500)
        {
            e = Random.Range(1, 4);
            if (enemy == 2)
            {
                e = Random.Range(2, 3);
            }
            if (enemy == 3)
            {
                e = 0;
            }
        }
		
		
        if (score >= 5000)
        {
            e = Random.Range(1, 5);
            if (enemy == 2)
            {
                e = Random.Range(2, 4);
            }
            if (enemy == 3)
            {
                e = 1;
            }
        }
        if (score >= 7500)
        {
            e = Random.Range(1, 5);
			
            if (enemy == 2)
            {
                e = Random.Range(1, 3);
            }
            if (enemy == 3)
            {
                e = Random.Range(0, 2);
            }
        }





        if (score >= 10000)
        {
            e = Random.Range(1, 6);
            if (enemy == 2)
            {
                e = Random.Range(1, 4);
            }
            if (enemy == 3)
            {
                e = Random.Range(0, 2);
            }
        }
        if (score >= 15000)
        {
            e = Random.Range(1, 6);

            if (enemy == 2)
            {
                e = Random.Range(1, 4);
            }

            if (enemy == 3)
            {
                e = Random.Range(0, 3);
            }
        }
        if (score >= 20000)
        {
            e = Random.Range(1, 3);

            if (enemy == 2)
            {
                e = Random.Range(2, 4);
            }

            if (enemy == 3)
            {
                e = Random.Range(1, 4);
            }
        }
		
        if (score >= 20000)
        {
            e = Random.Range(2, 5);

            if (enemy == 2)
            {
                e = Random.Range(3, 5);
            }

            if (enemy == 3)
            {
                e = Random.Range(2, 5);
            }
        }
        return e;
    }

}
