using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public AudioClip crash;
	public AudioClip bump;
	public AudioClip changeGravity;
	public AudioClip jump;
	public AudioClip pickupJump;
	public AudioClip select;
    private PlayerScript player;

    public void Start()
    {
        player = (PlayerScript)GameObject.Find("Player").GetComponent(typeof(PlayerScript));
    }

    public void Update()
    {
        if (!player.getAlive())
        {
            if (Input.GetButton("Jump"))
            {
                player.revive();
                Application.LoadLevel(Application.loadedLevel);
            }
			else if (Input.GetKeyDown(KeyCode.Escape)){
				Application.LoadLevel(0);
			}
            else if (Input.GetKeyDown("1"))
            {
                player.revive();
                Application.LoadLevel(1);
            }
            else if (Input.GetKeyDown("2"))
            {
                player.revive();
                Application.LoadLevel(2);
            }
            else if (Input.GetKeyDown("3"))
            {
                player.revive();
                Application.LoadLevel(3);
            }
            else if (Input.GetKeyDown("4"))
            {
                player.revive();
                Application.LoadLevel(4);
            }
            else if (Input.GetKeyDown("5"))
            {
                player.revive();
                Application.LoadLevel(5);
            }
        }

        if (Input.GetKeyDown (KeyCode.Escape))
        {
            player.die();
        }
    }

	public void playCrash(Vector3 position){
		AudioSource.PlayClipAtPoint (crash, transform.position);
	}

	public void playBump(Vector3 position){
		AudioSource.PlayClipAtPoint (bump, transform.position);
	}

	public void playChangeGravity(Vector3 position){
		AudioSource.PlayClipAtPoint (changeGravity, transform.position);
	}

	public void playJump(Vector3 position){
		AudioSource.PlayClipAtPoint (jump, transform.position);
	}

	public void playPickupJump(Vector3 position){
		AudioSource.PlayClipAtPoint (pickupJump, transform.position);
	}

	public void playSelect(Vector3 position){
		AudioSource.PlayClipAtPoint (select, transform.position);
	}

}
