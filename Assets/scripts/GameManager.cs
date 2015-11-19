using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject player;
	public GameObject seperator;
	public GameObject wall;

	public float halfSepWidth = 1;
	public float sepWidth = 10;
	public float height = 10f;
	public float sepDiv = 2.5f;
	public float wallHeight = 10f;
	private float sepDivStep;

	// Use this for initialization
	void Awake () {
		sepDivStep = height / sepDiv;
		initObjects ();
	}

	void initObjects(){
		GameObject tempPlayer = Instantiate (player);
		tempPlayer.transform.position = Vector3.zero;

		float startY = -1;
		for (int r = 0; r < sepDivStep; r++) {
			float startX = -halfSepWidth * sepWidth;
			for (int i = 0; i < halfSepWidth*2; i++) {
				GameObject tempSep = Instantiate (seperator);
				tempSep.transform.position = new Vector3 (startX, startY, 0);	
				startX += sepWidth;
			}
			startY += sepDiv;
		}

		GameObject tempWall = Instantiate (wall);
		tempWall.transform.position = new Vector3 (-halfSepWidth * sepWidth - sepWidth/2, -1+wallHeight/2, 0);	
		tempWall = Instantiate (wall);
		tempWall.transform.position = new Vector3 (halfSepWidth * sepWidth - sepWidth/2, -1+wallHeight/2, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
