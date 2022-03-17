using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class ChasePeople : MonoBehaviour {

	public Vector3 finalTarget;
	public Animation BladeAnimation;
	private UnityEngine.AI.NavMeshAgent directionNavAgent;

	private Vector3 startPos;
	private Quaternion startRot;
	
	// Use this for initialization
	void Start() {
		directionNavAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		if(!GetComponent<Rigidbody>().isKinematic)
		{
			GetComponent<Rigidbody>().useGravity=false;
			GetComponent<Rigidbody>().isKinematic=true;
		}
		startPos = transform.position;
		startRot = transform.localRotation;
	}

	public void init()
	{
		transform.position = startPos;
		transform.localRotation = startRot;
	}

	// Update is called once per frame
	void Update () {
		if (CroudControl.isPlaying)
		{
			if(GetComponent<Rigidbody>().isKinematic)
			{
				directionNavAgent.enabled=true;
				GetComponent<Rigidbody>().useGravity=true;
				GetComponent<Rigidbody>().isKinematic=false;
				BladeAnimation.Play();
			}
			MoveToClosest();
		}else{
			if(!GetComponent<Rigidbody>().isKinematic)
			{
				directionNavAgent.enabled=false;
				GetComponent<Rigidbody>().useGravity=false;
				GetComponent<Rigidbody>().isKinematic=true;
				BladeAnimation.Stop();
			}
		}
	}

	private void MoveToClosest()
	{ 
		GameObject closest = FindClosestTarget();
		if (closest != null)
			directionNavAgent.destination = closest.transform.position;
		else
		{
			directionNavAgent.destination = finalTarget;
			CroudControl.NEXT();
		}
	}
	
	
	
	private GameObject FindClosestTarget () {
		
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("Person"); 
		GameObject closest = null;
		float distance = Mathf.Infinity; 
		Vector3 position = transform.position; 
		
		foreach (GameObject go in gos)  { 
			Vector3 diff = (go.transform.position - position);
			float curDistance = diff.sqrMagnitude; 
			if (curDistance < distance) { 
				closest = go; 
				distance = curDistance; 
			} 
		} 

		return closest; 
	}
}
