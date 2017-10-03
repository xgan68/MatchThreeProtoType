using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour {

	private readonly string[] COLORMAP = {"Blue", "Red", "Green", "Yellow"};

	string color;
	GameObject gemCube;

	//public GameObject board;

	public bool onPosition;
	public bool isMatched;

	private Gem[] neighbors = new Gem[4];

	[SerializeField]
	private string[] nColors = new string[4];

	// Use this for initialization
	void Start () {
		gemCube = transform.Find ("Cube").gameObject;
		generateGem ();
		onPosition = false;
		//neighbors = new ArrayList<> ();
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < 4; i++) {
			if (neighbors[i] != null) 
				nColors [i] = neighbors [i].color;
		}
	}

	public void generateGem() {
		color = COLORMAP [Random.Range (0, COLORMAP.Length)];
		Material gemMaterial = (Material)Resources.Load ("Materials/" + color);
		gemCube.GetComponent<Renderer> ().material = gemMaterial;
		isMatched = false;
		for (int i = 0; i < 4; i ++) 
			neighbors [i] = null;
		

		transform.Find ("SensorHolder").gameObject.SetActive (true);
	}

	void OnMouseDown() {
		BoardManager.onGemSelected (this);
	}

	public string getColor() {
		return color;
	}

	public void addNeighbor(Gem gem, int dir) {
		neighbors [dir] = gem;

	}

	public void removeNeighbor(Gem gem, int dir) {
		//neighbors[dir] = null;
	}

	public Gem getNeighbor(int dir) {
		return neighbors [dir];
	}
}
