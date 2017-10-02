using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour {

	private readonly string[] COLORMAP = {"Blue", "Red", "Green", "Yellow"};

	string color;
	GameObject gemCube;

	public GameObject board;

	public bool onPosition;
	public bool isMatched;

	// Use this for initialization
	void Start () {
		gemCube = transform.Find ("Cube").gameObject;
		generateGem ();
		onPosition = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void generateGem() {
		color = COLORMAP [Random.Range (0, COLORMAP.Length)];
		Material gemMaterial = (Material)Resources.Load ("Materials/" + color);
		gemCube.GetComponent<Renderer> ().material = gemMaterial;
	}

	void OnMouseDown() {
		BoardManager.onGemSelected (this);
	}
		
}
