using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;

public class LoadRanking : MonoBehaviour {
	[SerializeField]
	
	public Text txt1;
	public Text txt2;

    void Start()
    {
        StartCoroutine(listaDeTop(10));
    }
    

	
	IEnumerator listaDeTop(int i) {
		WWWForm wwwf = new WWWForm ();
		wwwf.AddField ("sql", "SELECT a.*, max(b.score) as score FROM user a, game b where a.id = b.id_user group by id order by score DESC LIMIT " + i, System.Text.Encoding.UTF8); 


		using (var w = UnityWebRequest.Post("https://systemplugin.com/spacewar/sql.php", wwwf))
		{
			yield return w.SendWebRequest();
			if (w.isNetworkError || w.isHttpError) {
				Debug.Log(w.error);
			}
			else {

                var json = JSON.Parse(w.downloadHandler.text);
                Debug.Log(json);

                var status = json["status"];

                if (status == "success")
                {

                    JsonPlayer playerContainer = JsonUtility.FromJson<JsonPlayer>(w.downloadHandler.text);

                    txt1.text = "";
                    txt2.text = "";

                    if (playerContainer.data.Length > 0)

                        foreach (JsonPlayer.Player item in playerContainer.data)
                        {
                            txt1.text += item.nick + "\n";
                            txt2.text += item.score + "\n";
                        }

                }
			}
		}
	}	
	
}
