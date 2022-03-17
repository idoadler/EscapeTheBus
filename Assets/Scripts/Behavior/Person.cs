using UnityEngine;
using System.Collections;

public class Person : MonoBehaviour {

	public Transform bodyPrefab;

	public AudioSource personAudio;

	private Transform body;
	public void killed()
	{
		//personAudio.Play();
		body = Instantiate(bodyPrefab, new Vector3(transform.position.x,-1.08f,transform.position.z), transform.rotation) as Transform;
		if(isPlayer)
		{
			isPlayer=false;
			stopRecording();
			isRecorded = true;
		}
		gameObject.SetActive(false);
	}

	
	public float sensitivityX = 3F;
	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;
	public bool isPlayer = false;
	private bool isRecorded = false;
	
	public float recordedMovmentX;
	public float recordedMovmentY;
	public float recordedMovmentZ;
	public float recordedRotation;

	private Vector3 startPos;
	private Quaternion startRot;

	void Awake()
	{
		PersonAnimation = GetComponent<Animation>();
	}

	void Start()
	{
		startPos = transform.position;
		startRot = transform.localRotation;
	}
	
	public void init()
	{
		if (body != null)
			DestroyObject(body.gameObject);
		transform.position = startPos;
		transform.localRotation = startRot;

		if (isPlayer)
		{
			startRecording();
		}
		else if(isRecorded)
		{
			playRecording();
		}
	}
	
	void Update() 
	{
		if (CroudControl.isPlaying)
		{
			CharacterController controller = GetComponent<CharacterController>();
			if (isPlayer)
			{
				if (controller.isGrounded) 
				{
					moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
					moveDirection = transform.TransformDirection(moveDirection);
					moveDirection *= speed;
					if (Input.GetButton("Jump"))
						moveDirection.y = jumpSpeed;
					
				}
				moveDirection.y -= gravity * Time.deltaTime;
				float rotation = Input.GetAxis("Mouse X");
				
				// RECORD
				currentRecordingTime+=Time.deltaTime;
				MovmentX.AddKey(currentRecordingTime,moveDirection.x);
				MovmentY.AddKey(currentRecordingTime,moveDirection.y);
				MovmentZ.AddKey(currentRecordingTime,moveDirection.z);
				Rotation.AddKey(currentRecordingTime,rotation);

				UpdateTangentsFromMode(MovmentX,MovmentX.length-3);
				UpdateTangentsFromMode(MovmentY,MovmentY.length-3);
				UpdateTangentsFromMode(MovmentZ,MovmentZ.length-3);
				UpdateTangentsFromMode(Rotation,Rotation.length-3);
				
				// PLAY
				controller.Move(moveDirection * Time.deltaTime);
				transform.Rotate(0, rotation * sensitivityX, 0);
			}
			else if (isRecorded)
			{
				controller.Move(new Vector3(recordedMovmentX,recordedMovmentY,recordedMovmentZ) * Time.deltaTime);
				transform.Rotate(0, recordedRotation * sensitivityX, 0);
			}
		}
	}

	private void UpdateTangentsFromMode(AnimationCurve curve, int index)
	{
		if (index < 0 || index >= curve.length)
			return;
		Keyframe key = curve[index];
		if (index >= 1)
		{
			key.inTangent = CalculateLinearTangent(curve, index, index - 1);
			curve.MoveKey(index, key);
		}
		if (index + 1 < curve.length)
		{
			key.outTangent = CalculateLinearTangent(curve, index, index + 1);
			curve.MoveKey(index, key);
		}
	}

	// UnityEditor.CurveUtility.cs (c) Unity Technologies
	private float CalculateLinearTangent(AnimationCurve curve, int index, int toIndex)
	{
		return (float) (((double) curve[index].value - (double) curve[toIndex].value) / ((double) curve[index].time - (double) curve[toIndex].time));
	}
	
	// Record
	
	private AnimationClip RecordedClip;
	private AnimationCurve MovmentX;
	private AnimationCurve MovmentY;
	private AnimationCurve MovmentZ;
	private AnimationCurve Rotation;
	private float currentRecordingTime;
	private Animation PersonAnimation;
	
	public void startRecording() {
		RecordedClip=new AnimationClip();
		currentRecordingTime=0f;
		MovmentX = AnimationCurve.Linear(0, 0, 1001, 0);
		MovmentY = AnimationCurve.Linear(0, 0, 1001, 0);
		MovmentZ = AnimationCurve.Linear(0, 0, 1001, 0);
		Rotation = AnimationCurve.Linear(0, 0, 1001, 0);
	}
	
	public void stopRecording() {
		MovmentX.RemoveKey(MovmentX.length-1);
		MovmentY.RemoveKey(MovmentY.length-1);
		MovmentZ.RemoveKey(MovmentZ.length-1);
		Rotation.RemoveKey(Rotation.length-1);
		RecordedClip.SetCurve("", typeof(Person), "recordedMovmentX", MovmentX);
		RecordedClip.SetCurve("", typeof(Person), "recordedMovmentY", MovmentY);
		RecordedClip.SetCurve("", typeof(Person), "recordedMovmentZ", MovmentZ);
		RecordedClip.SetCurve("", typeof(Person), "recordedRotation", Rotation);
		PersonAnimation.AddClip(RecordedClip, "recordedClip");
	}
	
	public void playRecording() {
		PersonAnimation.Play("recordedClip");
	}
}