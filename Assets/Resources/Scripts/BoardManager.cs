using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

	private readonly int BOARDWIDTH = 5;
	private readonly int BOARDHEIGHT = 5;

	// Use this for initialization
	void Start () {
		onNewGameStart ();
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
