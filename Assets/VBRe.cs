﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VBRe : MonoBehaviour, IVirtualButtonEventHandler {

	private float timer = 0.0f;
	private bool isTimerValid = false;

	private GameObject vbButtonObject;
	private GameObject buttonRe;
	private GameObject master;
	// Use this for initialization
	void Start () {
		vbButtonObject = GameObject.Find ("ImageTarget/VirtualButtonRe");
		buttonRe = GameObject.Find ("ImageTarget/VirtualButtonRe/Re");
		master = GameObject.Find ("ImageTarget/Master");
		vbButtonObject.GetComponent<VirtualButtonBehaviour> ().RegisterEventHandler (this);
	}

	public void OnButtonPressed (VirtualButtonAbstractBehaviour vb){
		buttonRe.GetComponent<AudioSource> ().Play ();
		master.GetComponent<Game> ().ColorRed (1,true);
	}

	public void OnButtonReleased (VirtualButtonAbstractBehaviour vb){
		master.GetComponent<Game> ().ColorRed (1, false);
		master.GetComponent<Game> ().checkCorrect (1);
	}

	/*public void Update(){
		if (isTimerValid) {
			timer -= Time.deltaTime;
			if (timer <= 0.0f) {
				TimeIsUp ();
				InvalidateTimer ();
			}
		}
	}

	public void OnButtonPressed(VirtualButtonAbstractBehaviour vb){
		StartTimer (0.2f);
	}

	public void OnButtonReleased(VirtualButtonAbstractBehaviour vb){
		master.GetComponent<Game> ().ColorRed (0, false);
		InvalidateTimer ();
	}

	private void StartTimer(float seconds){
		timer = seconds;
		isTimerValid = true;
	}

	private void InvalidateTimer(){
		timer = 0.0f;
		isTimerValid = false;
	}

	private void TimeIsUp(){
		buttonDo.GetComponent<AudioSource> ().Play ();
		master.GetComponent<Game> ().ColorRed (0,true);
	}*/
}
