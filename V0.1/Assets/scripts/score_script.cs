using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class score_script : MonoBehaviour {
	
	public Text scoreUI;
	public Text recordUI;
	public int score = 0;
	public bool isDead;

	void Start () {
	}

	//Update nas informaçoes do jogo
	public void Update () { 
		if(score > PlayerPrefs.GetInt("record")){ 
			PlayerPrefs.SetInt("record", score); 
		}
		string score_str = string.Format ("{0:000000}", score);
		scoreUI.text = score_str; 
		recordUI.text = string.Format ("{0:000000}", PlayerPrefs.GetInt("record"));
	}
	
	
	
}
