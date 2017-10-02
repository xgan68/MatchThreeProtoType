using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

	private readonly int BOARDWIDTH = 5;
	private readonly int BOARDHEIGHT = 5;

	public static BoardManager instance;

	enum PlayerStates {FirstGemSelected, None, Swapping};
	static PlayerStates currentState = PlayerStates.None;

	static Gem gem1, gem2;

	static Vector3 target1, target2;

	static bool gem1OnPosition, gem2OnPosition;

	// Use this for initialization
	void Start () {
		instance = this;
		onNewGameStart ();
		gem1OnPosition = false;
		gem2OnPosition = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentState == PlayerStates.Swapping) {
			if (gem1.onPosition && gem2.onPosition) {
				Debug.Log ("afasdf");
				resetGems ();
			}
		}	
	}

	float startTime;
	void moveGem(Gem gem, Vector3 start, Vector3 target) {
		Vector3 center = (start + target) * 0.5f;
		center -= new Vector3 (0, 0, -0.1f);
		Vector3 riseRel = start - center;
		Vector3 setRel = target - center;
		float frac = (Time.time - startTime) / 2f;
		gem.transform.position = Vector3.Slerp (riseRel, setRel, frac);
		gem.transform.position += center;
	}

	static void togglePhysics(bool enable) {
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
				target2 = new Vector3(Mathf.Round(gem1.transform.position.x), 
									Mathf.Round(gem1.transform.position.y), 0);
				target1 = new Vector3(Mathf.Round(gem2.transform.position.x), 
									Mathf.Round(gem2.transform.position.y), 0);
				swapGems(gem1, gem2);
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

	static void swapGems(Gem gem1, Gem gem2) {
		togglePhysics (true);

		instance.StartCoroutine(moveGem(gem1, target1));
		instance.StartCoroutine(moveGem(gem2, target2));

	}

	void resetGems() {
		currentState = PlayerStates.None;
		togglePhysics (false);
		gem1.onPosition = false;
		gem2.onPosition = false;
		gem1 = null;
		gem2 = null;
	}


	static IEnumerator moveGem(Gem gem, Vector3 target) {
		float remainingDistance = Vector3.Distance (gem.transform.position, target);

		while (remainingDistance > 0.01f) {
			gem.transform.position = Vector3.Lerp (gem.transform.position, target, Time.deltaTime * 5);
			remainingDistance = Vector3.Distance (gem.transform.position, target);

			yield return null;
		}
		gem.onPosition = true;;
	}
}
