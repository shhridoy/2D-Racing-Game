using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carController : MonoBehaviour {

	public float carSpeed;
	Vector3 position;
	public float maxPos = 2.5f;
	public float minPos = -2.5f;

	public uiManager ui;
	public AudioManager am;
	bool currentPlatformAndroid = false;

	Rigidbody2D rb;

	void Awake(){
		
		rb = GetComponent<Rigidbody2D> ();

        #if UNITY_ANDROID
		    currentPlatformAndroid = true;
		#else
		    currentPlatformAndroid = false;
		#endif

		am.carSound.Play();
	}

	// Use this for initialization
	void Start () {
		//ui = GetComponent<uiManager> ();
	
		position = transform.position;

		if (currentPlatformAndroid) {
			Debug.Log ("Android");
		} else {
			Debug.Log ("Windows");
		}

	}
	
	// Update is called once per frame
	void Update () {

		if (currentPlatformAndroid) {
			// android specific codes
			//TouchMove ();
			AccelerometerMove ();
		} 

		else {
			position.x += Input.GetAxis ("Horizontal") * carSpeed * Time.deltaTime;

			position.x = Mathf.Clamp (position.x, minPos, maxPos);

			transform.position = position;
		}

		position = transform.position;
		position.x = Mathf.Clamp (position.x, minPos, maxPos);
		transform.position = position;
	}

	// this method is called when two game object get collided
	void OnCollisionEnter2D (Collision2D col){
		if (col.gameObject.tag == "Enemy Car") {
			//Destroy (gameObject);
			gameObject.SetActive (false);
			ui.gameOverActivated ();
			am.carSound.Stop ();
		}
	}

	void AccelerometerMove(){
		float x = Input.acceleration.x;

		if (x < -0.1f) {
			MoveLeft ();
		} else if (x > 0.1f) {
			MoveRight ();
		} else {
			SetVelocityZero ();
		}

	}

	void TouchMove(){
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);

			float middle = Screen.width / 2;

			if (touch.position.x < middle && touch.phase == TouchPhase.Began) {
				MoveLeft ();
			} 
			else if (touch.position.x > middle && touch.phase == TouchPhase.Began) {
				MoveRight ();
			} 
		}

		else {
			SetVelocityZero ();
		}
	}

	public void MoveLeft(){
		rb.velocity = new Vector2 (-carSpeed, 0);
	}

	public void MoveRight(){
		rb.velocity = new Vector2 (carSpeed, 0);
	}

	public void SetVelocityZero(){
		rb.velocity = Vector2.zero;
	}
}
