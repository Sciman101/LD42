using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    [Header("Endgame")]
    public Image fadeImage;
    public string endScene, menuScene;

    float startTime;

    public static GameController instance;

	// Use this for initialization
	void Start () {
        //Set ref instance
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        //Basic get started stuff
        startTime = Time.time;
        fadeImage.color = new Color(0, 0, 0, 1);
        FadeTo(0);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(menuScene);
        }
	}

    public void EndGame()
    {
        FadeTo(1);
        ScoreHolder.time = Time.time - startTime;
        Invoke("EndScene", 1);
    }

    void EndScene()
    {
        SceneManager.LoadScene(endScene);
    }

    //Fade to
    void FadeTo(float target)
    {
        fadeImage.CrossFadeAlpha(target, 1f, false);
    }
}
