using UnityEngine;
using System.Collections;

public class startSceneController : MonoBehaviour {

	public GameObject missionSelectPanel;
	public GameObject mainMenuPanel;
	public static startSceneController instance;
	public bool inMissionMenu = false;
	public bool inMainMenu = true;
	string targetedUIInteractable;
	// Use this for initialization
	void Awake()
	{
		instance = this;
	}
	
	void Start () {

		missionSelectPanel.SetActive(false);
		mainMenuPanel.SetActive(true);
		Screen.showCursor = true;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(inMissionMenu)
			{
				hideMissionSelectMenu();
				showMainMenu();
			}
		}
	}
	public void loadTestScene()
	{
		hideMissionSelectMenu();
		Application.LoadLevel(2);
	}
	public void loadRAINScene()
	{
		hideMissionSelectMenu();
		Application.LoadLevel(3);
	}
	public void showMissionSelectMenu()
	{
		if(inMainMenu)
		{
			hideMainMenu();
		}
		inMissionMenu = true;
		missionSelectPanel.SetActive(true);
		Time.timeScale =0;
		Screen.showCursor = true;
	}
	private void hideMissionSelectMenu()
	{
		inMissionMenu = false;
		missionSelectPanel.SetActive(false);
		Time.timeScale =1;
		Screen.showCursor = false;
	}
	private void showMainMenu()
	{
		inMainMenu=true;
		mainMenuPanel.SetActive(true);
		Time.timeScale =0;
		Screen.showCursor =true;
	}
	
	private void hideMainMenu()
	{
		inMainMenu=false;
		mainMenuPanel.SetActive(false);
		Time.timeScale =1;
		Screen.showCursor =false;
	}
	
	public void startNewGame()
	{
		hideMainMenu();
		Application.LoadLevel(1);
	}
	public void quitGame()
	{
		Application.Quit();
	}
	
	
}
