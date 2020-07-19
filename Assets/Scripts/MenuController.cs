using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    [Header("Menu Animation")]
    public Transform[] roomSegments;
    public float roomMoveSpeed;
    public Animator animator;

    public Image fadeImage;
    public GameObject creditsObject;

    [Header("Scenes")]
    public string gameScene;

	// Use this for initialization
	void Start () {
        animator.SetBool("Walking", true);
        fadeImage.color = new Color(0, 0, 0, 1);
        fadeImage.CrossFadeAlpha(0, 1, false);

    }
	
	// Update is called once per frame
	void Update () {
		foreach (Transform t in roomSegments)
        {
            t.position -= (Vector3.right * roomMoveSpeed * Time.deltaTime);
            if (t.position.x < -22) t.position += Vector3.right * 54;
        }
	}

    //Functions
    public void Play()
    {
        fadeImage.CrossFadeAlpha(1,1,false);
        Invoke("LoadScene", 1);
    }

    void LoadScene()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void ToggleCredits()
    {
        creditsObject.SetActive(!creditsObject.activeInHierarchy);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
