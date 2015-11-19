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

	private float flyYPos = 0.5f;

	private float flyYVel = 1f;

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
			rigidBody.isKinematic = true;
			if(!flying)
				flyYPos = transform.position.y+0.5f;
			spriteRenderer.sprite = fly_sprite;
			flying = true;
			rigidBody.velocity = new Vector2 (rigidBody.velocity.x+rbForce, flyYVel);
			transform.rotation = Quaternion.Euler(new Vector3(0,0,-15));
		} else if (keys [1]) {
			rigidBody.isKinematic = true;
			if(!flying)
				flyYPos = transform.position.y+0.5f;
			spriteRenderer.sprite = fly_sprite;
			flying = true;
			rigidBody.velocity = new Vector2 (rigidBody.velocity.x-rbForce, flyYVel);
			transform.rotation = Quaternion.Euler(new Vector3(0,180,-15));
		} else {
			rigidBody.isKinematic = false;
			transform.rotation = Quaternion.Euler(new Vector3(0,transform.rotation.eulerAngles.y,0));
		}

		if (flying && transform.position.y > flyYPos) {
			transform.position = new Vector3 (transform.position.x, flyYPos, transform.position.z);
			flyYVel = 0f;
		} else {
			flyYVel = 1f;
		}
		
		if (Mathf.Abs (rigidBody.velocity.x) > absVel) {
			rigidBody.velocity = new Vector2(absVel*Mathf.Sign(rigidBody.velocity.x), rigidBody.velocity.y);
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.name == "Seperator(Clone)") {
			flying = false;
			rigidBody.velocity = new Vector2 (0,rigidBody.velocity.y);
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
