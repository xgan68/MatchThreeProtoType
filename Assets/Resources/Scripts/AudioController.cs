using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

	private AudioClip cubeExplode;
	private AudioClip cubeCombo;
	private AudioClip invailedMove;
	private AudioClip clockTicking;
	[SerializeField]
	private AudioSource SFX;
	[SerializeField]
	private AudioSource BGM;
	[SerializeField]
	private AudioSource ClockSFX;

	private readonly float startingPitch = 1f;


	// Use this for initialization
	void Start () {
		cubeExplode = (AudioClip)Resources.Load ("SFX/Cube_Explode");
		cubeCombo = (AudioClip)Resources.Load ("SFX/Cube_Combo");
		invailedMove = (AudioClip)Resources.Load ("SFX/Invailed_Move");
		clockTicking = (AudioClip)Resources.Load ("SFX/Clock_Ticking");

		ClockSFX.clip = clockTicking;
		//audioSource.clip = cubeExplode;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onCubeExplode(float pitch) {
		SFX.pitch = startingPitch + pitch;
		SFX.PlayOneShot(cubeExplode, 1f);
	}

	public void onCubeCombo(float pitch) {
		SFX.pitch = startingPitch + pitch;
		SFX.PlayOneShot(cubeCombo, 1f);
	}

	public void onInvailedMove() {
		//SFX.pitch = 0.5f;
		SFX.PlayOneShot(invailedMove, 1f);
		//SFX.pitch = 1f;
	}

	public void playClockTicking(bool play) {
		if (play) {
			if (ClockSFX.isPlaying) {
				return;
			}
			ClockSFX.Play ();
		} else {
			ClockSFX.Stop();
		}
	}
}
