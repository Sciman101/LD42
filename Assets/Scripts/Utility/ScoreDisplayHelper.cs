using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreDisplayHelper : MonoBehaviour {

    public float delay;
    public GameObject display;
    public Text scoreText;

    public Image fadeImage;
    public string menuScene;

    public CameraTweenHelper cam;

	// Use this for initialization
	void Start () {
        int t = (int)ScoreHolder.time;
        scoreText.text = "Time: " + string.Format("{0}:{1:00}",t/60,t%60);
        display.SetActive(false);
        Invoke("Show", delay);
	}
	
	void Show()
    {
        display.SetActive(true);
    }

    public void Menu()
    {
        Invoke("GotoMenu", 1);
        cam.end += Vector3.up * 50;
    }

    void GotoMenu()
    {
        SceneManager.LoadScene(menuScene);
    }
}
