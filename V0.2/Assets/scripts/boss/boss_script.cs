using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using SimpleJSON;

public class boss_script : MonoBehaviour {
	public int life = 150;
	public float speed;
	
	private SpriteRenderer sprite;
	private Transform player;

	public GameObject explosion;
	private LevelController lvlController;


    public int enemyCount = 1;
    public int enemyCountMax;
    public float startWait;
    public Vector2 spawnWait;
    public float spawnWaitMin;
    public float waveWait;
    public float waveWaitMin;
    public GameObject[] enemies;


    // Use this for initialization
    void Start () {
        lvlController = GameObject.Find("score").GetComponent<LevelController> ();
		player = FindObjectOfType<Player>().transform;
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();

		sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(SpawnWaves());
		
		transform.position = Vector2.MoveTowards(transform.position, player.position, speed);
    }
	
	// Update is called once per frame
	void Update () {
		if (lvlController.isDead)return;
		//transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
		//Vector3 target = player.position;
		//Vector3 moveDirection = gameObject.transform.position - target;
		//if (moveDirection != Vector3.zero)
		//{
		//	float angle = Mathf.Atan2(moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;
		//	transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
		//}
		if (transform.position.y <= 3.5f) {
			// Criando o limite 
			float yPos = Mathf.Clamp (transform.position.y, 3.5f, 3.5f); 
			// Limitando 

			transform.position = new Vector3 (transform.position.x, yPos, transform.position.z);

        }
            //Limite Vertical
            if (transform.position.x <= -5.7f || transform.position.x >= 5.7f)
            {
                // Criando o limite 
                float xPos = Mathf.Clamp(transform.position.x, -5.7f, 5.7f);
                // Limitando 
                transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
            }

    }

	void OnTriggerEnter2D (Collider2D outro){
		if(outro.gameObject.tag == "player_ShotTag"){
			StartCoroutine(TakingDamage());
			life = life - 1;
			if(life==0){
				Instantiate(explosion, transform.position, transform.rotation);
                if (explosion != null)
                {
                    GameObject newExplosion = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
                    newExplosion.transform.localScale = new Vector3(0.5F, 0.5F, 0);
                }

                StartCoroutine(registrarConquista());

                Destroy(this.gameObject); 
			}
			Destroy(outro);
		} 
	}


    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while (!lvlController.gameOver && !lvlController.isDead)
        {

                for (int i = 0; i < enemyCount; i++)
                {
                    GameObject enemy = enemies[0];
                    Renderer renderer = GetComponent<Renderer>();
                    var x1 = transform.position.x - renderer.bounds.size.x / 2;
                    var x2 = transform.position.x + renderer.bounds.size.x / 2;

                    Instantiate(enemy, new Vector2(Random.Range(x1, x2), transform.position.y), Quaternion.identity);
                    yield return new WaitForSeconds(Random.Range(spawnWait.x, spawnWait.y));
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

    IEnumerator registrarConquista()
    {
        WWWForm wwwf = new WWWForm();

        wwwf.AddField("sql", "INSERT INTO achievement_user (id_user,id_achievement)  VALUES('" + PlayerPrefs.GetString("id_user") + "', '3')", System.Text.Encoding.UTF8);

        using (var w = UnityWebRequest.Post("https://systemplugin.com/spacewar/sql.php", wwwf))
        {
            yield return w.SendWebRequest();

            if (w.isNetworkError || w.isHttpError)
            {
                Debug.Log(w.error);
            }
            else
            {
                var json = JSON.Parse(w.downloadHandler.text);
                Debug.Log(json);
            }
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
}
