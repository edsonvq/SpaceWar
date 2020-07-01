using UnityEngine; 
using System.Collections; 
using UnityEngine.UI;


public class menu_script : MonoBehaviour{
	public GameObject gameOver;

	void Start(){
		gameOver.SetActive(false);
		if (PlayerPrefs.GetString("dead") == "true") {
			gameOver.SetActive (true);
		}
	}

	//Quit
	void Update() { 
		if (Input.GetKey ("escape")) { 
			Application.Quit(); 
		}

	}

	//abre nivel 1
	public void OnClickStartGame(){ 
		Application.LoadLevel("nivel_1");
	}


	//fecha o game
	public void OnClickExitGame(){ 
		Application.Quit(); 
	}
}
