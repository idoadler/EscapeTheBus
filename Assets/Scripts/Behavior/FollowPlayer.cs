using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
	public GameObject player;

		// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(player != null)
		{
			transform.position = new Vector3(player.transform.position.x, player.transform.position.y+1f, player.transform.position.z);
			transform.rotation = player.transform.rotation;
		}
	}
}
