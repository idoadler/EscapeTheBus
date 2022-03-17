using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : CanvasScreenBase {

	public GameObject CreditsGameObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayGame()
	{
		Destroy(gameObject);
		BusGameManager.Instance.PlayGame();
	}

	public void OpenCreditsScreen()
	{
		CreditsGameObject.SetActive(true);
	}

	public void CloseCreditsScreen()
	{
		CreditsGameObject.SetActive(false);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
