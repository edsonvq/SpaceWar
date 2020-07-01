using UnityEngine;
using System.Collections;

public class EnemyShoter : MonoBehaviour {
	
	public GameObject bullet;
	public float fire_rate;
	public Transform[] shotSpawns;
	
	private LevelController lvlController;
	
	//Instancia as referenciass
	void Start () {
        lvlController = GameObject.Find("score").GetComponent<LevelController> ();
		InvokeRepeating("Fire", fire_rate, fire_rate); //Chama o metodo fire para atirar
	}
	
	void Fire()
	{
		if (lvlController.isDead)return;
        if (!IsVisibleToCamera(this.transform)) return;

        //faz loop nos GameObjects - onde sairam as balas do inimigo
        for (int i = 0; i < shotSpawns.Length; i++)
		{
			Instantiate(bullet, shotSpawns[i].position, shotSpawns[i].rotation); //instancia a bala no objeto.
			
		}
	}


    public static bool IsVisibleToCamera(Transform transform)
    {
        Vector3 visTest = Camera.main.WorldToViewportPoint(transform.position);
        return (visTest.x >= 0 && visTest.y >= 0) && (visTest.x <= 1 && visTest.y <= 1) && visTest.z >= 0;
    }
}
