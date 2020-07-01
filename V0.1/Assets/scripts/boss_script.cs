using UnityEngine;
using System.Collections;

public class boss_script : MonoBehaviour {
	public int life = 5;
	public float speed;
	
	private SpriteRenderer sprite;
	private Transform player;

	public GameObject explosion;
	private score_script scoreObj;
	// Use this for initialization
	void Start () {
		scoreObj = GameObject.Find("score").GetComponent<score_script> ();
		player = FindObjectOfType<player_script>().transform;
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();

		sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (scoreObj.isDead)return;
		transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

		if (transform.position.y <= 2.2f) {
			// Criando o limite 
			float yPos = Mathf.Clamp (transform.position.y, 2.0f, 2.0f); 
			// Limitando 
			transform.position = new Vector3 (transform.position.x, yPos, transform.position.z); 
		}

	}

	void OnTriggerEnter2D (Collider2D outro){
		if(outro.gameObject.tag == "player_ShotTag"){
			StartCoroutine(TakingDamage());
			life = life - 1;
			if(life==0){
				Instantiate(explosion, transform.position, transform.rotation);
				Destroy(this.gameObject); 
			}
			Destroy(outro);
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
