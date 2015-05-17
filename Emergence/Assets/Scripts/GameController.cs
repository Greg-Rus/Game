using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject interactPrompt;
	public GameObject missionSelectPanel;
	public GameObject mainMenuPanel;
	public static GameController instance;
	public bool inMissionMenu = false;
	public bool inMainMenu = false;
	string targetedUIInteractable;
	
	void Awake()
	{
		instance = this;
	}
	// Use this for initialization
	void Start () {

		interactPrompt.SetActive(false);
		missionSelectPanel.SetActive(false);
		mainMenuPanel.SetActive(false);
		Screen.showCursor = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.E))
		{
			if(targetedUIInteractable != null)
			{
				showMissionSelectMenu();
			}
		}
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(inMissionMenu)	hideMissionSelectMenu();
			else if(!inMainMenu) showMainMenu();
			else hideMainMenu();
		}
	}
	
	public void playerTargetingInteractable(string interactableName)
	{
		interactPrompt.SetActive(true);
		targetedUIInteractable = interactableName;
		
	}
	public void playerStoppedTargetingInteractable()
	{
		interactPrompt.SetActive(false);
		targetedUIInteractable = null;
		
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
