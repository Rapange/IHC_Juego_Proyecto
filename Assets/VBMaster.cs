using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VBMaster : MonoBehaviour, IVirtualButtonEventHandler {

	private GameObject master;
	private int index;
	private bool[] decisions;
	// Use this for initialization
	void Start () {
		index = 0;
		decisions = new bool[7]{false,false,false,false,false,false,false};
		VirtualButtonBehaviour[] vbs = GetComponentsInChildren < VirtualButtonBehaviour> ();
		for (int i = 0; i < vbs.Length; ++i) {
			vbs [i].RegisterEventHandler (this);
		}
		master = GameObject.Find ("ImageTarget/Master");
	}
	
	// Update is called once per frame
	public void OnButtonPressed (VirtualButtonAbstractBehaviour vb){
		switch (vb.VirtualButtonName) {
		case "VirtualButtonDo":
			//buttonDo.GetComponent<AudioSource> ().Play ();
			if (master.GetComponent<Game> ().checkCorrect (0)) {
				decisions [0] = true;
				master.GetComponent<Game> ().ColorGreen (0, true);
			}
			else master.GetComponent<Game> ().ColorRed (0, true);
			master.GetComponent<Game> ().soundKey (0);
			break;
		case "VirtualButtonRe":
			if (master.GetComponent<Game> ().checkCorrect (1)) {
				decisions [1] = true;
				master.GetComponent<Game> ().ColorGreen (1, true);
			}
			else master.GetComponent<Game> ().ColorRed (1, true);
			master.GetComponent<Game> ().soundKey (1);
			break;
			break;
		case "VirtualButtonMi":
			if (master.GetComponent<Game> ().checkCorrect (2)) {
				decisions [2] = true;
				master.GetComponent<Game> ().ColorGreen (2, true);
			}
			else master.GetComponent<Game> ().ColorRed (2, true);
			master.GetComponent<Game> ().soundKey (2);
			break;
			break;
		case "VirtualButtonFa":
				if (master.GetComponent<Game> ().checkCorrect (3)) {
				decisions [3] = true;
					master.GetComponent<Game> ().ColorGreen (3, true);
				}
				else master.GetComponent<Game> ().ColorRed (3, true);
				master.GetComponent<Game> ().soundKey (3);
				break;
			break;
		case "VirtualButtonSol":
				if (master.GetComponent<Game> ().checkCorrect (4)) {
				decisions [4] = true;
					master.GetComponent<Game> ().ColorGreen (4, true);
				}
				else master.GetComponent<Game> ().ColorRed (4, true);
				master.GetComponent<Game> ().soundKey (4);
				break;
			break;
		case "VirtualButtonLa":
				if (master.GetComponent<Game> ().checkCorrect (5)) {
				decisions [5] = true;
					master.GetComponent<Game> ().ColorGreen (5, true);
				}
				else master.GetComponent<Game> ().ColorRed (5, true);
				master.GetComponent<Game> ().soundKey (5);
				break;
			break;
		case "VirtualButtonSi":
				if (master.GetComponent<Game> ().checkCorrect (6)) {
				decisions [6] = true;
					master.GetComponent<Game> ().ColorGreen (6, true);
				}
				else master.GetComponent<Game> ().ColorRed (6, true);
				master.GetComponent<Game> ().soundKey (6);
				break;
			break;
		}
	}

	public void OnButtonReleased (VirtualButtonAbstractBehaviour vb){
		switch (vb.VirtualButtonName) {
			case "VirtualButtonDo":
				//buttonDo.GetComponent<AudioSource> ().Play ();
				master.GetComponent<Game> ().ColorRed (0, false);
				index = 0;
				//master.GetComponent<Game> ().checkCorrect (0);
				break;
			case "VirtualButtonRe":
				master.GetComponent<Game> ().ColorRed (1, false);
				index = 1;
				//master.GetComponent<Game> ().checkCorrect (1);
				break;
			case "VirtualButtonMi":
				master.GetComponent<Game> ().ColorRed (2, false);
				index = 2;
				//master.GetComponent<Game> ().checkCorrect (2);
				break;
			case "VirtualButtonFa":
				master.GetComponent<Game> ().ColorRed (3, false);
				index = 3;
				//master.GetComponent<Game> ().checkCorrect (3);
				break;
			case "VirtualButtonSol":
				master.GetComponent<Game> ().ColorRed (4, false);
				index = 4;
				//master.GetComponent<Game> ().checkCorrect (4);
				break;
			case "VirtualButtonLa":
				master.GetComponent<Game> ().ColorRed (5, false);
				index = 5;
				//master.GetComponent<Game> ().checkCorrect (5);
				break;
			case "VirtualButtonSi":
				master.GetComponent<Game> ().ColorRed (6, false);
				index = 6;
				//master.GetComponent<Game> ().checkCorrect (6);
				break;
			}
			master.GetComponent<Game> ().checkCorrect (index);
		master.GetComponent<Game> ().decision (decisions [index]);
		decisions [index] = false;
	}
}
