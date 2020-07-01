using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class Enemy : MonoBehaviour
{

    public int speed;
    public int health;
    public GameObject explosion;
    public int scorePoints;
    public GameObject[] dropItems;



    [HideInInspector]
    public bool isDead = false;

    private SpriteRenderer sprite;
    private LevelController lvlController;
    private SpawnController spawnController;
    private Transform player;

    // Use this for initialization
    void Start()
    {

        sprite = GetComponent<SpriteRenderer>();

        lvlController = GameObject.Find("score").GetComponent<LevelController>();
        spawnController = GameObject.Find("spawner").GetComponent<SpawnController>();

        player = FindObjectOfType<Player>().transform;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (this.gameObject.tag == "enemy_0")
        {
            var speedd = Random.Range(-2, -8);
            rb.velocity = new Vector2(0, speedd);

            // Faz o inimigo rodar em si mesmo aleatoriamentre entre -200 e 200
            rb.angularVelocity = Random.Range(-180, 180);

            // Destroi o inimigo após 3s, que ele não está mais visível na tela 
            Destroy(gameObject, 15);
        }
        if (this.gameObject.tag == "enemy_2_1")
        {
            
            rb.angularVelocity = Random.Range(-200, 200);
        }
        if (this.gameObject.tag == "enemy_1_3")
        {
            rb.velocity = transform.up * speed;
        }
        if (this.gameObject.tag == "enemy_1_1" || this.gameObject.tag == "enemy_1_2")
        {
            var speedd = Random.Range(-1, -4);
            rb.velocity = new Vector2(0, speedd);
        }
    }

    void Update()
    {

        if (!lvlController.isDead)
        {
            if (this.gameObject.tag == "enemy_0")
            {
            }else
            if (this.gameObject.tag == "enemy_2_1")
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }else
            if (this.gameObject.tag == "enemy_1_3")
            {
                Vector3 target = player.position - transform.position;
                float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 2);
            }else
            if (this.gameObject.tag == "enemy_1_1" || this.gameObject.tag == "enemy_1_2")
            {
                Vector3 target = player.position;
                Vector3 moveDirection = gameObject.transform.position - target;
                if (moveDirection != Vector3.zero)
                {
                    float angle = Mathf.Atan2(moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
                }
            }else
			if (this.gameObject.tag == "boss_1_child")
            {
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.gameObject.tag == "player_ShotTag")
        {
            Destroy(outro.gameObject);
            TakeDamage(1);
        }
        if (this.gameObject.tag == "enemy_0" && outro.gameObject.tag == "shieldTag")
        {
            if (explosion != null)
                Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            health -= damage;
            if (health <= 0)
            {
                isDead = true;

                if (explosion != null) { 
                    GameObject newExplosion = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
                    if (this.gameObject.tag == "enemy_0")
                    {
                        newExplosion.transform.localScale = new Vector3(0.2F, 0.2F, 0);
                        lvlController.count_1_conquista++;
                        if(lvlController.count_1_conquista == 100)
                        {
                            StartCoroutine(registrarConquista(1));
                        }

                    }

                    if (this.gameObject.tag == "enemy_1_3")
                    {
                        lvlController.count_2_conquista++;
                        if (lvlController.count_2_conquista == 5)
                        {
                            StartCoroutine(registrarConquista(2));
                        }
                    }
                }

                if (this.GetComponent<Player>() != null)
                {
                    GetComponent<Player>().Respawn();
                }
                else
                {
                    spawnController.chanceToDroptItem++;
                    int random = Random.Range(0, 100);

                    if (random < spawnController.chanceToDroptItem && dropItems.Length > 0)
                    {
                        Instantiate(dropItems[Random.Range(0, dropItems.Length)], transform.position, Quaternion.identity);

                        spawnController.chanceToDroptItem = 0;
                    }
                    lvlController.score+=scorePoints;
                    Destroy(gameObject);
                }
            }
            else
            {
                StartCoroutine(TakingDamage());
            }
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

    IEnumerator registrarConquista(int i)
    {
        WWWForm wwwf = new WWWForm();
        
         wwwf.AddField("sql", "INSERT INTO achievement_user (id_user,id_achievement)  VALUES('" + PlayerPrefs.GetString("id_user") + "', '"+i+"')" , System.Text.Encoding.UTF8);

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

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
