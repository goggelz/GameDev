using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour 
{
    public float speed;
	public float hoverHeight;
	public float jumpForce;
	public float maxFallDownSpeed;
	public long points;
	private Vector3 jumpVector;
	private bool alive = true;
	private int jumps;
	private float gravity = 10f;
    private float powerInput;
    private float turnInput;
    private Rigidbody rb;
    void Start () 
    {
        rb = GetComponent <Rigidbody>();
		rb.useGravity = true;
		Physics.gravity = new Vector3(0f, -gravity, 0f);
		this.tag = "Player";
		foreach (Transform child in transform) {
			child.tag = "Player";
		}
		jumpVector = new Vector3 (0f, jumpForce, 0f);
    }

    void Update () 
    {
		turnInput = Input.GetAxis ("Horizontal");
		if(powerInput <0.5)
			powerInput = powerInput + Time.fixedDeltaTime/100;
        //powerInput = Input.GetAxis("Vertical");
        if (alive) {
			if (Input.GetButtonDown ("Jump")) {
				jump ();
			}
		}
	}

    void FixedUpdate()
    {
		if (alive) {
			Ray ray = new Ray (transform.position, -transform.up);

			RaycastHit hit;
			float factor = Mathf.Abs (gravity) / gravity;

			if (Physics.Raycast (ray, out hit, hoverHeight)) {
				float relHeight = hit.distance;
				float hoverForce = (gravity + (hoverHeight - relHeight)) / relHeight;
                if (rb.velocity.y * factor < 1)
                {
					rb.AddForce (new Vector3 (0, hoverForce, 0));
				}
			} else {

				if (rb.velocity.y * factor > 0 || rb.velocity.y * factor > -maxFallDownSpeed) {
					rb.useGravity = true;
				} else {
					rb.useGravity = false;
				}
			}
		//	Debug.Log(powerInput);
			rb.AddRelativeForce (0f, 0f, powerInput * speed);
			rb.AddRelativeForce (turnInput * speed, 0f, 0f);

		}



    }

	public void changeGravity(){
	//	hoverHeight = -hoverHeight;
		gravity = -gravity;
		Physics.gravity = new Vector3(0, -gravity, 0);
		rb.transform.RotateAround (rb.transform.position, Vector3.forward, 180);
	}

	public void die(){
		this.alive = false;
		rb.velocity = Vector3.zero;
	}

	public void addJump(){
		jumps++;	
	}

	public int getJumps(){
		return jumps;
	}

	public void jump(){
		if (jumps > 0) {
			rb.useGravity = true;
			rb.AddForce(jumpVector,ForceMode.Impulse);
			jumps--;
		}
	}

    public bool getAlive()
    {
        return alive;
    }

    public void revive()
    {
        alive = true;
    }
}