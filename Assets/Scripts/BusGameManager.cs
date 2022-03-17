using UnityEngine;
using System.Collections;

public class BusGameManager : MonoBehaviour {

	public static BusGameManager Instance;

	public AudioSource BackgroundMusic;

	public AudioSource BackgroundScreams;

	private bool isPlaying=false;

	void Awake()
	{
		Instance=this;
	}

	// Use this for initialization
	void Start () {
		BackgroundMusic.gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		if(isPlaying && Input.GetKeyUp(KeyCode.Escape))
		{
			EndGame();
		}
	}

	public void PlayGame()
	{
		isPlaying=true;
		CanvasScreensManager.Instance.InstantiateGameTimeScreen();
		BackgroundScreams.gameObject.SetActive(true);
		CroudControl.PLAY();
	}

	public void EndGame()
	{
		isPlaying=false;
		CanvasScreensManager.Instance.InstantiateEndMenu();
		BackgroundScreams.gameObject.SetActive(false);
		CroudControl.EndGame();
	}
}
