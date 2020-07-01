using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.SceneManagement;

public class Register : MonoBehaviour {
	
	public InputField ifNick;
	public InputField ifLogin;
	public InputField ifPassword;
	public InputField ifConfPassword;
	
	public GameObject buttom;
		
	public Text txt;
	
	
	public void btnLogin(){
		SceneManager.LoadScene("login");
	}
	
	public void btnCadastro(){
        txt.text = "-";
		
		if(ifNick.text == ""){
		    txt.text = "Digite seu nick!";
            return;
        }
		if(ifLogin.text == ""){
		    txt.text = "Digite seu login!";
            return;
        }
		if(ifPassword.text == ""){
		    txt.text = "Digite sua senha!";
            return;
        }
		if(ifConfPassword.text == ""){
		    
		    txt.text = "Digite a senha de confirmação!";
            return;
		}
		
		if(ifPassword.text == ifConfPassword.text )
		{
			StartCoroutine(RegisterCoroutine(ifNick.text, ifLogin.text, ifPassword.text));
		}
		else
		{
			txt.text = "As senhas não conferem!";
            return;
        }
	}


	IEnumerator RegisterCoroutine(string NickTxt, string LoginTxt, string PassTxt)
	{
		WWWForm wwwf = new WWWForm();
		
		
		wwwf.AddField("nick", NickTxt, System.Text.Encoding.UTF8);
		wwwf.AddField("login", LoginTxt, System.Text.Encoding.UTF8);
		wwwf.AddField("password", PassTxt, System.Text.Encoding.UTF8);
		
		using(var w = UnityWebRequest.Post("https://systemplugin.com/spacewar/cadastro.php", wwwf))
		{
			yield return w.SendWebRequest();
			
			if(w.isNetworkError || w.isHttpError) 
			{
				Debug.Log(w.error);
			}
			else
			{
				var json = JSON.Parse(w.downloadHandler.text);
				Debug.Log(json);
				
				var status = json["data"]["status"];
				var message = json["data"]["msg"];
				var id_user = json["data"]["id_user"];
				
				if(status == "success")
				{
					txt.text = message;
					PlayerPrefs.SetString("id_user", id_user);
                    PlayerPrefs.SetString("nick", NickTxt);


                    buttom.SetActive (false);
					
					yield return new WaitForSeconds(2f);
					SceneManager.LoadScene("menu");
				}
				else
				{
					txt.text = message;
				}
			}
		}
	}
	
	
}
