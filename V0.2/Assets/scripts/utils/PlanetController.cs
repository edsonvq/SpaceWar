using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetController : MonoBehaviour {

	//array dos planetas
	public GameObject[] planets;
	Queue<GameObject> avaliablePlanets = new Queue<GameObject>();

	// Use this for initialization
	void Start () {
        foreach (GameObject planet in planets)
        {
            avaliablePlanets.Enqueue(planet);
        }
        InvokeRepeating ("MovePlanetDown", 0, 16f);  // chama metodo para retornar planeta para cima
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//move o planeta para baixo
	void MovePlanetDown(){
		EnqueuePlanets ();
		if (avaliablePlanets.Count == 0)return;

		GameObject aPlanet = avaliablePlanets.Dequeue();
		aPlanet.GetComponent<PlanetScript>().moving = true;
	}

	//Verificacao no planeta
	void EnqueuePlanets(){
		foreach (GameObject aPlanet in planets) {
			if((aPlanet.transform.position.y < 5) && (!aPlanet.GetComponent<PlanetScript>().moving)){
				aPlanet.GetComponent<PlanetScript>().ResetPosition();
				avaliablePlanets.Enqueue(aPlanet);
			}
		}                     
	}
}
