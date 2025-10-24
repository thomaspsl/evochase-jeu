using UnityEngine;
using System.Collections;
using Application = UnityEngine.Application;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public GameObject mainMenu, settingsMenu, background;

	private Animator mainMenuAnim, settingsMenuAnim, backgroundAnim;
	private bool isMainMenuOnLeft;

	// Use this for initialization
	void Start () {
		Screen.fullScreen = true;
		mainMenuAnim = mainMenu.GetComponent<Animator>();
		settingsMenuAnim = settingsMenu.GetComponent<Animator>();
		backgroundAnim = background.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void LancementJeu()
    {
		SceneManager.LoadScene(sceneName: "Game");
	}

	// fonction des boutons du menu
	public void OnSettingsButtonClick(){
		if(isMainMenuOnLeft==false)
		{
			mainMenuAnim.SetBool("isMovingLeft",true);
			settingsMenuAnim.SetBool("isMovingIn",true);
			backgroundAnim.SetBool("isTurning",true);
			isMainMenuOnLeft = true;
		}
		else
		{
			mainMenuAnim.SetBool("isMovingLeft",false);
			settingsMenuAnim.SetBool("isMovingIn",false);
			backgroundAnim.SetBool("isTurning",false);
			isMainMenuOnLeft = false;
		}
	}

    // Fonction bouton sous menu
    public void OnFullSreenButtonClick()
    {
        if (!Screen.fullScreen)
            Screen.fullScreen = true;
    }

	public void OnWindowedButtonClick()
    {
        if (Screen.fullScreen)
            Screen.fullScreen = false;
    }

	public void OnSoundButtonClick()
	{
	}

	public void OnRetryButtonClick()
    {
		SceneManager.LoadScene(sceneName: "Game");
	}

	public void OnBackButtonClick()
    {
		SceneManager.LoadScene(sceneName: "Launcher");
	}

	// Quitter l'application
	public void Exit()
    {
		Application.Quit();
    }
}