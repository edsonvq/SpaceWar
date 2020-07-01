using UnityEngine;
using System.Collections;

public class boss_shot_script : MonoBehaviour {
		
	public GameObject bullet;
	public GameObject bullet2;

	public float fire_rate;
	public float fire_rate2;

	public Transform[] shotSpawns;
	
	private LevelController lvlController;
	
	//Instancia as referenciass
	void Start () {
        lvlController = GameObject.Find("score").GetComponent<LevelController> ();
		InvokeRepeating("Fire", fire_rate, fire_rate); //Chama o metodo fire para atirar
		InvokeRepeating("Fire2", fire_rate2, fire_rate2); //Chama o metodo fire para atirar
	}
	
	void Fire()
	{
		if (lvlController.isDead)return;
		if (!IsVisibleToCamera(this.transform)) return;
		
		Instantiate(bullet, shotSpawns[1].position, shotSpawns[1].rotation); //instancia a bala no objeto.
		Instantiate(bullet, shotSpawns[2].position, shotSpawns[2].rotation); //instancia a bala no objeto.

	}
	void Fire2()
	{
		if (lvlController.isDead)return;
		if (!IsVisibleToCamera(this.transform)) return;
		
		Instantiate(bullet2, shotSpawns[0].position, shotSpawns[0].rotation); //instancia a bala no objeto.
	
	}
	
	public static bool IsVisibleToCamera(Transform transform)
    {
        Vector3 visTest = Camera.main.WorldToViewportPoint(transform.position);
        return (visTest.x >= 0 && visTest.y >= 0) && (visTest.x <= 1 && visTest.y <= 1) && visTest.z >= 0;
    }
}
