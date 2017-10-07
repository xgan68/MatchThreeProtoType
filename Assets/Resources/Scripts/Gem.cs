using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour {

	private readonly string[] COLORMAP = {"Blue", "Red", "Green", "Yellow", "Purple", "Black", "White"};

	string color;
	GameObject gemCube;

	private GameManager gameManager;
	//public GameObject board;

	public bool onPosition;
	public bool isMatched;


	[SerializeField]
	private Gem[] neighbors = new Gem[4];

	[SerializeField]
	private string[] nColors = new string[4];

	private int particleToEmit = 5;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		gemCube = transform.Find ("Cube").gameObject;
		generateGem ();
		onPosition = false;
		neighbors = new Gem[4];
		//neighbors = new ArrayList<> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void updateColors() {
		for (int i = 0; i < 4; i++) {
			if (neighbors[i] != null) 
				nColors [i] = neighbors [i].color;
		}
	}


	public void generateGem() {
		color = COLORMAP [Random.Range (0, gameManager.numberOfColors())];
		Material gemMaterial = (Material)Resources.Load ("Materials/" + color);
		gemCube.GetComponent<Renderer> ().material = gemMaterial;

		gameObject.GetComponent<ParticleSystem>().GetComponent<Renderer> ().material = gemMaterial;

		isMatched = false;
		for (int i = 0; i < 4; i ++) 
			neighbors [i] = null;
		gameObject.GetComponent<Rigidbody> ().isKinematic = false;
		transform.Find ("SensorHolder").gameObject.SetActive (true);
	}

	void OnMouseDown() {
		BoardManager.onGemSelected (this);
	}

	public string getColor() {
		return color;
	}

	public void addNeighbor(Gem gem, int dir) {
		updateColors ();
		neighbors [dir] = gem;

	}

	public void removeNeighbor(Gem gem, int dir) {
		updateColors ();
		neighbors[dir] = null;
	}

	public Gem getNeighbor(int dir) {
		return neighbors [dir];
	}

	public void explode() {
		gameObject.GetComponent<ParticleSystem>().Emit (particleToEmit);
	}

	public int countNeighbors() {
		return neighbors.Length;
	}

	public bool containsNeighbor(Gem gem) {
		foreach (Gem neighbor in neighbors) {
			if (neighbor == gem) {
				return true;
			}
		}
		return false;
	}
}
