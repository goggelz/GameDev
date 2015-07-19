using UnityEngine;
using System.Collections;

public class ObstacleScript : MonoBehaviour {
    private GameObject player;
	private PlayerScript playerScript;
	private GameManager gameManager;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
		playerScript = (PlayerScript)player.GetComponent (typeof(PlayerScript));
		gameManager = (GameManager)GameObject.Find("GameManager").GetComponent (typeof(GameManager));
	}

	void OnCollisionEnter(Collision col)
	{
		Debug.Log (this.tag);
		if (col.gameObject.CompareTag("Player")) 
		{
			switch (this.tag) 
			{
			case "Wall":
				playerScript.die();
				gameManager.playCrash(player.transform.position);
				break;
			case "Finish":
				Debug.Log ("hallo ende level");
				Application.LoadLevel(Application.loadedLevel+1);
				break;
			default:
				break;
			}
		}
	}

	void OnTriggerEnter(Collider tri)
	{
		if (tri.CompareTag("Player"))
		{

			switch (this.tag) 
			{
				case "JumpItem":
					playerScript.addJump();
					gameObject.SetActive(false);
					gameManager.playPickupJump(player.transform.position);
					break;
				case "Gravity":
					playerScript.changeGravity();
					gameObject.SetActive(false);
					gameManager.playChangeGravity(player.transform.position);
					break;
				default:
					break;
			}
	
		}

	}
}