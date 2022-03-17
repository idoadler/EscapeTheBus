using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class ChasePeople : MonoBehaviour {

	public Vector3 finalTarget;
	public Animation BladeAnimation;
	private NavMeshAgent directionNavAgent;

	private Vector3 startPos;
	private Quaternion startRot;
	
	// Use this for initialization
	void Start() {
		directionNavAgent = GetComponent<NavMeshAgent>();
		if(!rigidbody.isKinematic)
		{
			rigidbody.useGravity=false;
			rigidbody.isKinematic=true;
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
			if(rigidbody.isKinematic)
			{
				directionNavAgent.enabled=true;
				rigidbody.useGravity=true;
				rigidbody.isKinematic=false;
				BladeAnimation.Play();
			}
			MoveToClosest();
		}else{
			if(!rigidbody.isKinematic)
			{
				directionNavAgent.enabled=false;
				rigidbody.useGravity=false;
				rigidbody.isKinematic=true;
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
