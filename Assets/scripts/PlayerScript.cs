using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public Sprite stand_sprite;
	public Sprite fly_sprite;
	public Sprite[] shoot_sprite;
	private SpriteRenderer spriteRenderer;

	private bool shooting = false;
	private int shootingSqu = 0;
	public int spriteDelay = 6;
	private int curDelay = 0;
	public int holdFire = 30;

	private bool flying = false;

	private Rigidbody2D rigidBody;

	public float rbForce = 1;
	public float absVel = 4;

	//right,left,up,down
	private bool[] keys = {false,false,false,false};

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		rigidBody = GetComponent<Rigidbody2D> ();
		spriteRenderer.sprite = stand_sprite;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			keys[1] = true;
		} 
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			keys[0] = true;
		} 
		if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			keys[1] = false;
		} 
		if (Input.GetKeyUp (KeyCode.RightArrow)) {
			keys[0] = false;
		} 

		if (Input.GetKeyDown (KeyCode.Space)) {
			shooting = true;
		}

		movement ();

		shoot ();
	}

   	void movement(){
		if (keys [0]) {
			spriteRenderer.sprite = fly_sprite;
			flying = true;
			rigidBody.velocity = new Vector2 (rigidBody.velocity.x+rbForce, rigidBody.velocity.y);
			transform.rotation = Quaternion.Euler(new Vector3(0,0,-15));
		} else if (keys [1]) {
			spriteRenderer.sprite = fly_sprite;
			flying = true;
			rigidBody.velocity = new Vector2 (rigidBody.velocity.x-rbForce, rigidBody.velocity.y);
			transform.rotation = Quaternion.Euler(new Vector3(0,180,-15));
		} else {
			flying = false;
			transform.rotation = Quaternion.Euler(new Vector3(0,transform.rotation.y,0));
		}
	}

	void shoot(){
		if (shooting && !flying) {
			if(curDelay >= spriteDelay && shootingSqu != shoot_sprite.Length){
				curDelay = 0;
				spriteRenderer.sprite = shoot_sprite[shootingSqu];
				shootingSqu+=1;
			} else if(shootingSqu == shoot_sprite.Length && curDelay >= holdFire){
				curDelay = 0;
				shootingSqu = 0;
				shooting = false;
			}
			else
				curDelay+=1;
		}
		else if(!flying)
			spriteRenderer.sprite = stand_sprite;
	}
}
