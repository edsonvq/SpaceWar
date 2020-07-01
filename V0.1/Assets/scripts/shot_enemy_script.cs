using UnityEngine;
using System.Collections;

public class shot_enemy_script : MonoBehaviour {
	
	public GameObject bullet;
	public float fire_rate;
	public Transform[] shotSpawns;
	
	private score_script scoreObj;
	
	//Instancia as referenciass
	void Start () {
		scoreObj = GameObject.Find("score").GetComponent<score_script> ();
		InvokeRepeating("Fire", fire_rate, fire_rate); //Chama o metodo fire para atirar
	}
	
	void Fire()
	{
		if (scoreObj.isDead)return;
		
		//faz loop nos GameObjects - onde sairam as balas do inimigo
		for (int i = 0; i < shotSpawns.Length; i++)
		{
			Instantiate(bullet, shotSpawns[i].position, shotSpawns[i].rotation); //instancia a bala no objeto.
			
		}
	}
}
