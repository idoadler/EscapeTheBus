using UnityEngine;
using System.Collections;

public class CroudControl : MonoBehaviour {

	private static CroudControl instance;
	public static bool isPlaying = false;
	public Person[] persons;
	public ChasePeople terrorist;

	public static void NEXT() {instance.next();}
	public static void PLAY() {instance.play();}
	public static void EndGame() {instance.endGame();}

	private FollowPlayer CameraFollowPlayer;

	// Use this for initialization
	void Start () {
		instance = this;
		CameraFollowPlayer=Camera.main.GetComponent<FollowPlayer>();
		CameraFollowPlayer.player=persons[0].gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	int player = 0;
	public void play()
	{
		CameraFollowPlayer.player = persons[player].gameObject;
		persons[player].isPlayer = true;
		foreach (Person person in persons)
		{
			person.gameObject.SetActive(true);
			person.init();
		}
		terrorist.init();
		isPlaying = true;
	}
	
	
	public void next()
	{
		player++;
		if (player < persons.Length)
		{
			play();
		}else{
			BusGameManager.Instance.EndGame();
		}
	}

	public void endGame()
	{
		isPlaying = false;
		player = 0;
	}
}
