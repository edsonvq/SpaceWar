using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	//Configs do player
	public int speed = 6;
	private int life = 5;

	public float fire_rate; 
	public int fire_level = 2;

    public float spawnTime;
    private bool invencibility;
    public float invencibilityTime;


    public GameObject shot;
	private float nextFire;
	public Transform[] shot_spawn; //Onde sairam as balas da nave

	private SpriteRenderer sprite;
    private Vector3 startPosition;

    private LevelController lvlController;

    public GameObject shield;
    public GameObject shieldImg;
    public float shield_time=0;

    public bool shield_=false;
    public GameObject explosion;

    //Para animacao
    private bool right;
	private bool left;
	private Animator anim;

	public Text lifeUI;
    public Text shieldUI;

    private IEnumerator shield_coroutine;

    public enum ItemEffect{
		shield,
		fire_lvl_up,
		fire_rate_up,
		health_up,
        speed_up,
		especial_shot_1,
	}

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetString("dead", "false");
		//Pegando referencias
		anim = GetComponent<Animator>();
		sprite = GetComponent<SpriteRenderer>();
        startPosition = transform.position;

        lvlController = GameObject.Find("score").GetComponent<LevelController>();
        shieldImg.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        //Mostra vida atual
        lifeUI.text = "" + life;
        shieldUI.text = "" + string.Format("{0:0.00}", shield_time)+"s";

        if (lvlController.isDead) return;
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
	}

	//Tomou dano
	void OnTriggerEnter2D (Collider2D outro){
        if (outro.gameObject.tag == "enemy_0" || outro.gameObject.tag == "enemy2_ShotTag" || outro.gameObject.tag == "enemy3_ShotTag" || outro.gameObject.tag == "enemy5_ShotTag")
        {

            if (!invencibility && !lvlController.isDead) { 
                Respawn();
            }
            //Para piscar vermelho
            //StartCoroutine(TakingDamage());
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

    public void Respawn(){
        life--;
        if (life > 0)
        {
            if (explosion != null)
                Instantiate(explosion, transform.position, transform.rotation);
            StartCoroutine(Spawning());
        }
        else
        {
            life = 0;
            lvlController.isDead = true;
            sprite.enabled = false;

            lvlController.GameOver();
        }
    }

    IEnumerator Spawning(){
        lvlController.isDead = true;
        sprite.enabled = false;
        fire_level = 1;
        yield return new WaitForSeconds(spawnTime);
        lvlController.isDead = false;

        transform.position = startPosition;
        invencibility = true;

        for (float i = 0; i < invencibilityTime; i += 0.1f)
        {
            sprite.enabled = !sprite.enabled;
            yield return new WaitForSeconds(0.2f);
        }

        shieldImg.SetActive(false);
        sprite.enabled = true;
        invencibility = false;
    }

    IEnumerator Shield()
    {
        GameObject newShield = Instantiate(shield, transform.position, Quaternion.identity) as GameObject;
        GameObject newShield2 = Instantiate(shield, transform.position, Quaternion.identity) as GameObject;

        newShield.transform.parent = transform;
        newShield2.transform.parent = transform;

        invencibility = true;
        shieldImg.SetActive(true);
        
        for (float i = shield_time; i > 0; i -= 0.1f)
        {
            shield_time = shield_time - 0.1f;
            if (shield_ == true)
            {
                shield_ = false;
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        
        shieldImg.SetActive(false);
        invencibility = false;

        GameObject[] obs = GameObject.FindGameObjectsWithTag("shieldTag");

        for (var i = 0; i < obs.Length; i++)
            Destroy(obs[i]);
    }


    private Coroutine shield_c;
    public void SetItemEffect(ItemEffect effect){
        if (effect == ItemEffect.shield) {
            if (shield_time < 0.1f) {

                shield_time += 10;
                if (lvlController.score >= 5000)
                {
                    shield_time += 10;
                }
                if (lvlController.score >= 7500)
                {
                    shield_time += 5;
                }
                shield_c = StartCoroutine(Shield());
            }
            else
            {
                StopCoroutine(shield_c);
                shield_time += 10;
                shield_c = StartCoroutine(Shield());
            }
        }
        if (effect == ItemEffect.health_up) {
            life++;
        }
        if (effect == ItemEffect.speed_up) {
            speed++;
            if (speed >= 12)
            {
                fire_level = 12;
            }
        }
        if (effect == ItemEffect.fire_rate_up) {
            fire_rate-=0.02f;
            if (fire_rate <= 0.2f)
            {
                fire_rate = 0.2f;
            }
        }
        if (effect == ItemEffect.fire_lvl_up) {
			fire_level++;
			if(fire_level>=3){
				fire_level=3;
			}
		}

	}
}
