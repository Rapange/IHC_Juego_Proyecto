using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Game : MonoBehaviour {
	private System.Object threadLocker = new System.Object ();

	// Use this for initialization
	private const int NUM_KEYS = 7;
	private GameObject[] keys;
	private List<int> sequence;
	private string[] key_names;
	private bool flag, start, ini;
	private int rnd_num, num_seq;
	private MaterialPropertyBlock props;
	void Start () {
		flag = false;
		start = false;
		ini = true;
		num_seq = -1;
		key_names = new string[7] {"Do", "Re", "Mi", "Fa", "Sol", "La", "Si"};
		sequence = new List<int>{};
		keys = new GameObject[NUM_KEYS];
		for (int i = 0; i < NUM_KEYS; i++) {
			keys [i] = GameObject.Find ("ImageTarget/VirtualButton"+key_names[i]+"/"+key_names[i]);
		}
		sequence.Add (Random.Range (0, NUM_KEYS));
		//StartCoroutine (Play ());
	}

	public void decision(bool passed){
		if (passed)
			num_seq++;
		else {
			if (ini) {
				num_seq = 0;
			} else
				num_seq = -1;
		}
	}

	public bool checkCorrect(int index){
		lock(threadLocker){
			if (num_seq > -1 && num_seq < sequence.Count && sequence [num_seq] == index) {
				//num_seq++;
				return true;
			} else {
				
				//num_seq = -1;
				return false;
			}
		}
	}

	IEnumerator finalResult(bool result){
		if (result) {
			for (int j = 0; j < 3; j++) {
				for (int i = 0; i < NUM_KEYS; i++) {
					ColorGreen (i, true);
					soundKey (i);
				}
				yield return new WaitForSeconds (1.0f);
				for (int i = 0; i < NUM_KEYS; i++) {
					ColorGreen (i, false);
				}
				yield return new WaitForSeconds (1.0f);
			}
		} else {
			for (int j = 0; j < 3; j++) {
				for (int i = 0; i < NUM_KEYS; i++) {
					ColorRed (i, true);
					soundKey (i);
				}
				yield return new WaitForSeconds (1.0f);
				for (int i = 0; i < NUM_KEYS; i++) {
					ColorRed (i, false);
				}
				yield return new WaitForSeconds (1.0f);
			}
		}
	}

	public void soundKey(int index){
		keys [index].GetComponent<AudioSource> ().Play ();
	}

	public void ColorRed(int index, bool activate){
		props = new MaterialPropertyBlock ();
		if (activate)
			props.SetColor ("_Color", Color.red);
		else
			props.SetColor ("_Color", Color.white);
		keys[index].GetComponent<Renderer> ().SetPropertyBlock (props);
	}

	public void ColorGreen(int index, bool activate){
		props = new MaterialPropertyBlock ();
		if (activate)
			props.SetColor ("_Color", Color.green);
		else
			props.SetColor ("_Color", Color.white);
		keys[index].GetComponent<Renderer> ().SetPropertyBlock (props);
	}

	public void ColorYellow(int index, bool activate){
		props = new MaterialPropertyBlock ();
		if (activate)
			props.SetColor ("_Color", Color.yellow);
		else
			props.SetColor ("_Color", Color.white);
		keys[index].GetComponent<Renderer> ().SetPropertyBlock (props);
	}

	IEnumerator PlaySequence(){
		for (int i = 0; i < sequence.Count; i++) {
			ColorYellow (sequence [i], true);
			keys [sequence [i]].GetComponent<AudioSource> ().Play ();
			yield return new WaitForSeconds (0.5f);
			ColorYellow (sequence [i], false);
			yield return new WaitForSeconds (0.5f);
		}
		//yield return new WaitForSeconds (3.0f);
	}

	IEnumerator Play(){
		for (int i = 0; i < 10; i++) {
			yield return new WaitForSeconds (2.0f);
			num_seq = 0;
			rnd_num = Random.Range (0, NUM_KEYS);
			sequence.Add (rnd_num);
			yield return PlaySequence();
			while (num_seq != sequence.Count) {
				if (num_seq == -1)
					goto finale;
				yield return null;
			}
			//PlaySequence ();
		}
		finale:
		if (num_seq == -1) {
			yield return finalResult (false);
		} else {
			yield return finalResult (true);
		}
		flag = false;
		sequence.Clear ();
		sequence.Add (Random.Range (0, NUM_KEYS));
		start = false;
	}

	IEnumerator Repeat(){
		num_seq = 0;
		ini = true;
		yield return new WaitForSeconds (1.0f);
		while (num_seq < 1) {
			yield return PlaySequence ();
		}
		flag = true;
		ini = false;
	}

	// Update is called once per frame
	void Update () {
		//keys [0].GetComponent<Renderer> ().material.color = Color.red;
		//ColorRed (Random.Range(0,1),true);
		if (!start) {
			StartCoroutine (Repeat ());
			start = true;
		}
		else if (flag && start) {
			StartCoroutine (Play ());
			flag = false;
		}
	}
}
