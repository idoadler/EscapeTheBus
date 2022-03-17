using UnityEngine;
using System.Collections;

public class CanvasScreensManager : MonoBehaviour {

	public static CanvasScreensManager Instance;

	public RectTransform CanvasTransform;

	public GameObject MainMenuPrefab;
	public GameObject GameTimeScreenPrefab;
	public GameObject EndMenuPrefab;

	public CanvasScreenBase MainMenu;
	public CanvasScreenBase GameTimeScreen;
	public CanvasScreenBase EndMenu;

	void Awake()
	{
		Instance=this;
		if(CanvasTransform == null)
		{
			CanvasTransform = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
		}
	}

	// Use this for initialization
	void Start () {
		InstantiateMainMenu();
	}

	public void InstantiateMainMenu()
	{
		GameObject clone = Instantiate(MainMenuPrefab) as GameObject;
		clone.GetComponent<RectTransform>().SetParent(CanvasTransform,false);
		MainMenu = clone.GetComponent<CanvasScreenBase>();
	}

	public void InstantiateGameTimeScreen()
	{
		GameObject clone = Instantiate(GameTimeScreenPrefab) as GameObject;
		clone.GetComponent<RectTransform>().SetParent(CanvasTransform,false);
		GameTimeScreen = clone.GetComponent<CanvasScreenBase>();
	}

	public void InstantiateEndMenu()
	{
		GameObject clone = Instantiate(EndMenuPrefab) as GameObject;
		clone.GetComponent<RectTransform>().SetParent(CanvasTransform,false);
		EndMenu = clone.GetComponent<CanvasScreenBase>();
	}
}
