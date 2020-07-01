using UnityEngine;
using System.Collections;

public class StarGenerator : MonoBehaviour {

	public GameObject star; //GameObject estrela
	public int max;//maximo de estrelas

	// Use this for initialization
	void Start () {
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		Vector2 maxx = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		//Instanciando as estrelas
		for (int i=0; i < max; i++) {
			GameObject str = (GameObject)Instantiate(star);
			str.transform.position = new Vector2(Random.Range(min.x,maxx.x), Random.Range(min.y,maxx.y));
			str.GetComponent<StarScript>().speed = -(1f*Random.value+0.5f);
		}
	}


	// Update is called once per frame
	void Update () {
	
	}
}
