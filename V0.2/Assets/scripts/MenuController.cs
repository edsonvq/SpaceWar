using UnityEngine; 
using System.Collections; 
using UnityEngine.UI;


public class MenuController : MonoBehaviour{

    public string window;
    public Text txt;
    public Text txt2;

    void Start(){

        if (PlayerPrefs.GetString("nick") != "" && window == "menu")
        {
            txt.text = "Bem vindo " + PlayerPrefs.GetString("nick");
        }
        if (PlayerPrefs.GetInt("record") != 0 && window == "menu")
        {
            txt2.text = "Recorde: " + PlayerPrefs.GetInt("record");
        }

        if (window == "over")
        {
            txt.text = "Pontuação: " + PlayerPrefs.GetInt("score");
            txt2.text = "Recorde: " + PlayerPrefs.GetInt("record");
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
	
	//abre anking
	public void OnClickRanking(){ 
		Application.LoadLevel("ranking");
	}

	public void OnClickBack(){ 
		Application.LoadLevel("menu");
	}

    public void OnClickConquistas()
    {
        Application.LoadLevel("conquistas");
    }
	
    public void OnClickTutorial()
    {
        Application.LoadLevel("tutorial");
    }
	
    //fecha o game
    public void OnClickExitGame(){ 
		Application.Quit(); 
	}
}
