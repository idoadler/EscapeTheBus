using UnityEngine;
using System.Collections;

public class Knife : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
		if (CroudControl.isPlaying)
		{
			Person person = other.GetComponent<Person>();
			if (person != null)
			{
				person.killed();
			}
		}
	}
}