using UnityEngine;
using System.Collections;

public class boss_shot_script : MonoBehaviour {
		
	public GameObject bullet;
	public GameObject bullet2;

	public float fire_rate;
	public float fire_rate2;

	public Transform[] shotSpawns;
	
	private score_script scoreObj;
	
	//Instancia as referenciass
	void Start () {
		scoreObj = GameObject.Find("score").GetComponent<score_script> ();
		InvokeRepeating("Fire", fire_rate, fire_rate); //Chama o metodo fire para atirar
		InvokeRepeating("Fire2", fire_rate2, fire_rate2); //Chama o metodo fire para atirar
	}
	
	void Fire()
	{
		if (scoreObj.isDead)return;
		
		Instantiate(bullet, shotSpawns[1].position, shotSpawns[1].rotation); //instancia a bala no objeto.
		Instantiate(bullet, shotSpawns[2].position, shotSpawns[2].rotation); //instancia a bala no objeto.

	}
	void Fire2()
	{
		if (scoreObj.isDead)return;
		Instantiate(bullet2, shotSpawns[0].position, shotSpawns[0].rotation); //instancia a bala no objeto.
	
	}
}
