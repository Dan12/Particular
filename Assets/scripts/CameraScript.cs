using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position-GameObject.FindWithTag("Player").transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = GameObject.FindWithTag("Player").transform.position+offset;
	}
}
