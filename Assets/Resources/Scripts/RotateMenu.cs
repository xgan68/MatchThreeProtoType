using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMenu : MonoBehaviour {
	float rotateSpeed = 50f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.up * Time.deltaTime * rotateSpeed, Space.World);
	}
}
