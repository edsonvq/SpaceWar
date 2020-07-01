using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SimpleJSON;

public class LevelController : MonoBehaviour {

    public static LevelController levelController;
    public int level = 1;
    public int score;

    public Text scoreText;
    public Text recordText;

    public bool isDead = false;
    public bool gameOver = false;

    public int count_1_conquista;
    public int count_2_conquista;


    //0 == enemy_1
    //1 == enemy_2_1
    //2 == enemy_2_2
    //3 == enemy_2_3

    // enemy 
    string[][] enemys = new string[4][];
	
    // Use this for initialization
    void Start()
    {
        levelController = this;
    }

    // Update is called once per frame
    public void Update() {

        if (score > PlayerPrefs.GetInt("record"))
        {
            PlayerPrefs.SetInt("record", score);
        }

        string score_str = string.Format("{0:000000}", score);
        scoreText.text = score_str;
        recordText.text = string.Format("{0:000000}", PlayerPrefs.GetInt("record"));
    }


    public void GameOver()
    {
        gameOver = true;
		isDead = true;
		
		PlayerPrefs.SetInt("score", score);

        StartCoroutine(SaveGame());

        Destroy(this.gameObject);
		
        LoadScene("game_over");
    }

    IEnumerator SaveGame(){
        WWWForm wwwf = new WWWForm();

        wwwf.AddField("id", PlayerPrefs.GetString("id_user"), System.Text.Encoding.UTF8);
        wwwf.AddField("win", "0", System.Text.Encoding.UTF8);
        wwwf.AddField("score", ""+PlayerPrefs.GetInt("score"), System.Text.Encoding.UTF8);

        using (var w = UnityWebRequest.Post("https://systemplugin.com/spacewar/game.php", wwwf))
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
            }
        }
    }
	
	
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
