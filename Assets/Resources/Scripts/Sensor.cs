using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour {

	enum Directions {up, down, left, right};

	// Use this for initialization
	Gem gem;
	[SerializeField]
	private int dir;

	void Start () {
		gem = transform.parent.parent.GetComponent<Gem> ();
	}

	void OnTriggerStay(Collider other) {
		if (other.tag == "Gem") {
			gem.addNeighbor (other.GetComponent<Gem> (), dir);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Gem") {
			gem.removeNeighbor (other.GetComponent<Gem> (), dir);
		}
	}
}
