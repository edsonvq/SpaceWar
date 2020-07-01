using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class planet_controller_script : MonoBehaviour {

	//array dos planetas
	public GameObject[] planets;
	Queue<GameObject> avaliablePlanets = new Queue<GameObject>();

	// Use this for initialization
	void Start () {
		avaliablePlanets.Enqueue (planets [0]);
		avaliablePlanets.Enqueue (planets [1]);
		avaliablePlanets.Enqueue (planets [2]);

		InvokeRepeating ("MovePlanetDown", 0, 120f);  // chama metodo para retornar planeta para cima
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//move o planeta para baixo
	void MovePlanetDown(){
		EnqueuePlanets ();
		if (avaliablePlanets.Count == 0)return;

		GameObject aPlanet = avaliablePlanets.Dequeue();
		aPlanet.GetComponent<planet_script>().moving = true;
	}

	//Verificacao no planeta
	void EnqueuePlanets(){
		foreach (GameObject aPlanet in planets) {
			if((aPlanet.transform.position.y < 5) && (!aPlanet.GetComponent<planet_script>().moving)){
				aPlanet.GetComponent<planet_script>().ResetPosition();
				avaliablePlanets.Enqueue(aPlanet);
			}
		}                     
	}
}
