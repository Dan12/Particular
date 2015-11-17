using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject player;
	public GameObject seperator;

	// Use this for initialization
	void Start () {

		initObjects ();
	}

	void initObjects(){
		GameObject tempPlayer = Instantiate (player);
		GameObject tempSep = Instantiate (seperator);
		tempSep.transform.position = new Vector3 (0, -1, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
