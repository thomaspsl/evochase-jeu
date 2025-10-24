using UnityEngine;
using System.Collections;
using Application = UnityEngine.Application;
using UnityEngine.SceneManagement;
using System;

public class MenuDeath : MonoBehaviour
{

	public GameObject mainMenu, background;

	private Animator mainMenuAnim, backgroundAnim;
	private bool isMainMenuOnLeft;

	// Use this for initialization
	void Start()
	{
		Screen.fullScreen = true;
		mainMenuAnim = mainMenu.GetComponent<Animator>();
		backgroundAnim = background.GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public GameObject obj;

	public void OnDestroySound()
    {
		obj = GameObject.FindWithTag("SoundManager");
		Destroy(obj);
    }

    private UnityEngine.Object DontDestroyOnLoad()
    {
        throw new NotImplementedException();
    }

    public void OnMenuButtonClick()
	{
		SceneManager.LoadScene(sceneName: "Launcher");
	}

	public void OnQuitButtonClick()
	{
		Application.Quit();
	}
}