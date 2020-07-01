using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class Login : MonoBehaviour {


	
	public Text txt;
	
	public InputField ifLogin;
	public InputField ifPassword;
	
	public GameObject buttom;
		
    void Start()
    {
		PlayerPrefs.SetString("nick", "");
		PlayerPrefs.SetString("id_user", "");

		PlayerPrefs.SetInt("record", 0);
    }
	
	public void btnCadastrar(){
		SceneManager.LoadScene("cadastro");
	}
	
	public void login () {
        txt.text = "-";
		
        if (ifLogin.text == "")
        {
            txt.text = "Digite seu login!";
            return;
        }
        if (ifPassword.text == "")
        {
            txt.text = "Digite sua senha!";
            return;
        }

		StartCoroutine(LoginCoroutine(ifLogin.text, ifPassword.text));
	}
	
	IEnumerator LoginCoroutine(string LoginTxt, string PassTxt)
	{
		WWWForm wwwf = new WWWForm();
		wwwf.AddField("sql", "SELECT a.*, max(b.score) as score FROM user a, game b WHERE a.id = b.id_user AND LOWER(a.login) = '" + LoginTxt.ToLower() + "'", System.Text.Encoding.UTF8);
		
		using(var w = UnityWebRequest.Post("https://systemplugin.com/spacewar/sql.php", wwwf))
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
				
				var status = json["status"];
				
				if(status == "success")
				{
					JsonPlayer playerContainer = JsonUtility.FromJson<JsonPlayer>(w.downloadHandler.text);
					
					if(ifPassword.text  == playerContainer.data[0].password)
					{
						txt.text = "Login efetuado com sucesso!";
						PlayerPrefs.SetString("nick", playerContainer.data[0].nick);
                        PlayerPrefs.SetString("id_user", playerContainer.data[0].id+"");

                        PlayerPrefs.SetInt("record", playerContainer.data[0].score);
						buttom.SetActive (false);
                        yield return new WaitForSeconds(2f);
					
						SceneManager.LoadScene("menu");
					}
					else{
						txt.text = "Senha invalida!";
					}

				}
				else
				{
					txt.text = "Usuario não encontrado no banco de dados!";
				}
				
			}
		}
	}
}
