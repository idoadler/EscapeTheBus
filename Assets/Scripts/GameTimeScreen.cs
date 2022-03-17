using UnityEngine;
using System.Collections;

public class GameTimeScreen : CanvasScreenBase {

	public GameObject WhatDoWeDoNowGroup;
	public GameObject ControlsGroup;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void ShowWhatDoWeDoNowGroup()
	{
		WhatDoWeDoNowGroup.SetActive(true);
	}
	
	public void HideWhatDoWeDoNowGroup()
	{
		WhatDoWeDoNowGroup.SetActive(false);
	}
	
	public void ShowControlsGroup()
	{
		ControlsGroup.SetActive(true);
	}
	
	public void HideControlsGroup()
	{
		ControlsGroup.SetActive(false);
	}
}
