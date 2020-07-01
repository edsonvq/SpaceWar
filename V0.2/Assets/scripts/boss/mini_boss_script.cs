using UnityEngine;
using System.Collections;

public class mini_boss_script : MonoBehaviour {
	public int life = 5;

	private SpriteRenderer sprite;
	private Transform player;

	public GameObject explosion;


    // Use this for initialization
    void Start () {
		player = FindObjectOfType<Player>().transform;
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();

		sprite = GetComponent<SpriteRenderer>();
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
