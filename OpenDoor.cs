﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OpenDoor : MonoBehaviour {

	public float smooth = 2.0f;
	public float DoorOpenAngle = 90.0f;

	public string Password;
	public GameObject panelF;
	public InputField inputF;
	public bool passwordOff;
	private bool pasProv;

	public AudioClip OpenAudio;
	public AudioClip CloseAudio;
	private bool AudioS;

	private Vector3 defaultRot;
	private Vector3 openRot;
	private bool open;
	private bool enter;

	// Use this for initialization
	void Start () {
		
			defaultRot = transform.eulerAngles;
			openRot = new Vector3 (defaultRot.x, defaultRot.y + DoorOpenAngle, defaultRot.z);
		}
	
	// Update is called once per frame
	void Update () {
		if(pasProv == true)
		{
			if (inputF.text == Password) {
				passwordOff = true;
				pasProv = false;
				panelF.SetActive (false);
			}
			}
		if (passwordOff == true) {
			if (open) {
				if (AudioS == false) {
					gameObject.GetComponent<AudioSource> ().PlayOneShot (OpenAudio);
					AudioS = true;
				}
				transform.eulerAngles = Vector3.Slerp (transform.eulerAngles, openRot, Time.deltaTime * smooth);
			
			} else {
				if (AudioS == true) {
					gameObject.GetComponent<AudioSource> ().PlayOneShot (CloseAudio);
					AudioS = false;
				}
				transform.eulerAngles = Vector3.Slerp (transform.eulerAngles, defaultRot, Time.deltaTime * smooth);

			}
		}
		if (Input.GetKeyDown (KeyCode.F) && enter) {
			if (passwordOff == false) {
				pasProv = true;
				panelF.SetActive (true);
			}
			open = !open;
		}
}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") {
			enter = true;
			}
		}

    void OnTriggerExit(Collider col)
{
	if (col.tag == "Player") {
		enter = false;
	}
}
}