using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;

public class LoadConquista : MonoBehaviour {
	[SerializeField]
	
	public Text txt1;
	public Text txt2;
    public Text txt3;



    void Start()
    {
        StartCoroutine(aa());
        StartCoroutine(bb());
        StartCoroutine(cc());
    }
    

	
	IEnumerator aa() {
		WWWForm wwwf = new WWWForm ();
		wwwf.AddField ("sql", "SELECT * FROM achievement_user where id_user = '"+ PlayerPrefs.GetString("id_user") + "' AND id_achievement = 1 ", System.Text.Encoding.UTF8); 

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
                    txt1.color = new Color32(170, 255, 153, 255);
                }
			}
		}
	}
    IEnumerator bb()
    {
        WWWForm wwwf = new WWWForm();
        wwwf.AddField("sql", "SELECT * FROM achievement_user where id_user = '" + PlayerPrefs.GetString("id_user") + "' AND id_achievement = 2 ", System.Text.Encoding.UTF8);

        using (var w = UnityWebRequest.Post("https://systemplugin.com/spacewar/sql.php", wwwf))
        {
            yield return w.SendWebRequest();
            if (w.isNetworkError || w.isHttpError)
            {
                Debug.Log(w.error);
            }
            else
            {

                var json = JSON.Parse(w.downloadHandler.text);
                Debug.Log(json);

                var status = json["status"];

                if (status == "success")
                {
                    txt2.color = new Color32(170, 255, 153, 255);
                }
            }
        }
    }
    IEnumerator cc()
    {
        WWWForm wwwf = new WWWForm();
        wwwf.AddField("sql", "SELECT * FROM achievement_user where id_user = '" + PlayerPrefs.GetString("id_user") + "' AND id_achievement = 3 ", System.Text.Encoding.UTF8);

        using (var w = UnityWebRequest.Post("https://systemplugin.com/spacewar/sql.php", wwwf))
        {
            yield return w.SendWebRequest();
            if (w.isNetworkError || w.isHttpError)
            {
                Debug.Log(w.error);
            }
            else
            {

                var json = JSON.Parse(w.downloadHandler.text);
                Debug.Log(json);

                var status = json["status"];

                if (status == "success")
                {
                    txt3.color = new Color32(170, 255, 153, 255);
                }
            }
        }
    }
}
