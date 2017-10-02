using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

	private readonly int BOARDWIDTH = 5;
	private readonly int BOARDHEIGHT = 5;

	enum PlayerStates {FirstGemSelected, None, Swapping};
	static PlayerStates currentState = PlayerStates.None;

	static Gem gem1, gem2;

	static Vector3 target1, target2;

	// Use this for initialization
	void Start () {
		onNewGameStart ();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (currentState);
		if (currentState == PlayerStates.Swapping) {
			togglePhysics (true);
			gem1.transform.position = Vector3.Lerp (gem1.transform.position, target2, Time.deltaTime * 5);
			gem2.transform.position = Vector3.Lerp (gem2.transform.position, target1, Time.deltaTime * 5);
			if (Vector3.Distance(gem1.transform.position, target2) < .01f &&
				Vector3.Distance(gem2.transform.position, target1) < .01f) {
				currentState = PlayerStates.None;
				togglePhysics (false);
				gem1 = null;
				gem2 = null;
			}
		}	
	}

	void togglePhysics(bool enable) {
		gem1.gameObject.GetComponent<Rigidbody> ().isKinematic = enable;
		gem2.gameObject.GetComponent<Rigidbody> ().isKinematic = enable;
	}

	void onNewGameStart() {
		generateGems(BOARDWIDTH, BOARDWIDTH);
	}


	void generateGems(int width, int height) {
		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				GameObject gemGameObject = Instantiate((GameObject)Resources.Load("Prefabs/Gem"), 
														new Vector3(x - width/2 ,y , 0), 
														Quaternion.identity)as GameObject;
			}
		}
	}

	public static void onGemSelected(Gem gem) {
		switch (currentState) {
		case PlayerStates.None:
			toggleRotate (gem);
			gem1 = gem;
			currentState = PlayerStates.FirstGemSelected;
			break;

		case PlayerStates.FirstGemSelected:
			toggleRotate (gem1);
			if (gem1 != gem) {
				gem2 = gem;
				currentState = PlayerStates.Swapping;
				target1 = gem1.transform.position;
				target2 = gem2.transform.position;
			} else {
				currentState = PlayerStates.None;
				gem1 = null;
			}


			break;

		case PlayerStates.Swapping:
			Debug.Log ("Swapping");
			break;
		}
	}

	static void toggleRotate(Gem gem) {
		Rotate rotate = gem.transform.Find("Cube").GetComponent<Rotate> ();
		Debug.Log (rotate.ToString());
		if (rotate.isActiveAndEnabled) {
			rotate.enabled = false;
		} else {
			rotate.enabled = true;
		}
	}

	void swapGems(Gem gem1, Gem gem2) {

		float fraction = 0;

	}

	void moveGem(Gem gem, Vector3 target) {
		float fraction = 0;

	}
}
