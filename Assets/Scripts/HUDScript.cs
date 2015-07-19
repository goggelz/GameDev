using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour {
	private GameObject gameOver;
	private PlayerScript player;
	private Text jumps;

	// Use this for initialization
	void Start () {
		player = (PlayerScript)GameObject.Find("Player").GetComponent(typeof(PlayerScript));
		jumps = (Text)GameObject.Find ("Jumps").GetComponent (typeof(Text));
		gameOver = GameObject.Find ("GameOver");
		gameOver.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (!player.getAlive ()) {
			gameOver.SetActive (true);


		}
		jumps.text = "Jumps: " + player.getJumps ().ToString();
	}
}
