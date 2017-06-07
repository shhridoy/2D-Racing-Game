using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiManager : MonoBehaviour {

	public Button[] buttons;
	bool gameOver;
	public Text scoreText;
	public AudioManager am;
	int score;

	// Use this for initialization
	void Start () {
		gameOver = false;
		score = 0;
		InvokeRepeating ("scoreUpdate", 1.0f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = "Score: " + score;
	}

	void scoreUpdate(){
		if (gameOver == false) {
			score += 1;
		}

	}

	public void gameOverActivated(){
		gameOver = true;
		foreach (Button button in buttons) {
			button.gameObject.SetActive(true);
		}
	}

	public void Play(){
		Application.LoadLevel ("level1");
	}

	public void Pause(){
		if (Time.timeScale == 1) {
			Time.timeScale = 0;
			am.carSound.Stop ();
		} 
		else if (Time.timeScale == 0) {
			Time.timeScale = 1;
			am.carSound.Play ();
		}
	}

	/*public void Replay(){
		Application.LoadLevel (Application.loadedLevel);
	}*/

	public void Menu(){
		Application.LoadLevel ("menuScene");
	}

	public void Exit(){
		Application.Quit ();
	}
}
