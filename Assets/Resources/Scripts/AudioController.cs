using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

	[SerializeField]
	private AudioClip cubeExplode;
	private AudioClip cubeCombo;
	[SerializeField]
	private AudioSource SFX;
	[SerializeField]
	private AudioSource BGM;

	private readonly float startingPitch = 1f;


	// Use this for initialization
	void Start () {
		cubeExplode = (AudioClip)Resources.Load ("SFX/Cube_Explode");
		cubeCombo = (AudioClip)Resources.Load ("SFX/Cube_Combo");
		//audioSource.clip = cubeExplode;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onCubeExplode(float pitch) {
		SFX.pitch = startingPitch + pitch;
		SFX.PlayOneShot(cubeExplode, 1f);
		//audioSource.Play();
	}

	public void onCubeCombo(float pitch) {
		SFX.pitch = startingPitch + pitch;
		SFX.PlayOneShot(cubeCombo, 1f);
		//audioSource.Play();
	}
}
