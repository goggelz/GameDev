using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour {

	public GameObject plate;
    public GameObject wall;
    public GameObject tower;
    public GameObject jump;
    public GameObject ramp;

	private List<GameObject> plates = new List<GameObject> ();
    private List<GameObject> obstacles = new List<GameObject>();

    private GameObject player;

    private float frontEnd;
	private float leftEnd;
	private float rightEnd;

	private float xScale;
	private float zScale;

	private Vector3 platePosition;
	private GameObject currentPlate;

	private GameObject frontPlate;
	private GameObject leftPlate;
	private GameObject rightPlate;
	private GameObject frontLeftPlate;
	private GameObject frontRightPlate;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
 
		xScale = plate.transform.localScale.x;
		zScale = plate.transform.localScale.z;

		currentPlate = (GameObject)Instantiate (plate, plate.transform.position, plate.transform.rotation);
		plates.Add (currentPlate);

		positionUpdate ();
		frontPlate = (GameObject)Instantiate (plate, new Vector3(platePosition.x, platePosition.y, frontEnd), new Quaternion(0,0,0,0));
		plates.Add (frontPlate);
		leftPlate = (GameObject)Instantiate (plate, new Vector3(leftEnd, platePosition.y, platePosition.z), new Quaternion(0,0,0,0));
		plates.Add (leftPlate);
		rightPlate = (GameObject)Instantiate (plate, new Vector3(rightEnd, platePosition.y, platePosition.z), new Quaternion(0,0,0,0));
		plates.Add (rightPlate);

		frontLeftPlate = (GameObject)Instantiate (plate, new Vector3(leftEnd, platePosition.y, frontEnd), new Quaternion(0,0,0,0));
		plates.Add (frontLeftPlate);
		frontRightPlate = (GameObject)Instantiate (plate, new Vector3(rightEnd, platePosition.y, frontEnd), new Quaternion(0,0,0,0));
		plates.Add (frontRightPlate);
	}
	
	// Update is called once per frame
	void Update () {
		GameObject tempPlate;
		Ray ray = new Ray (player.transform.position, -player.transform.up);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit)) {
			GameObject obj = hit.collider.gameObject;
			if(obj == frontPlate || obj == frontRightPlate || obj == frontLeftPlate) {
				Debug.Log ("Front Plate");
				currentPlate = frontPlate;
				leftPlate = frontLeftPlate;
				rightPlate = frontRightPlate;

                Vector3 front = new Vector3(platePosition.x, platePosition.y, frontEnd);
                Vector3 frontRight = new Vector3(rightEnd, platePosition.y, frontEnd);
                Vector3 frontLeft = new Vector3(leftEnd, platePosition.y, frontEnd);

				positionUpdate();
				frontPlate = (GameObject)Instantiate (plate, front, new Quaternion (0, 0, 0, 0));
				plates.Add (frontPlate);
                createObstacle(front);

				frontLeftPlate = (GameObject)Instantiate (plate, frontLeft, new Quaternion (0, 0, 0, 0));
				plates.Add(frontLeftPlate);
                createObstacle(frontLeft);

                frontRightPlate = (GameObject)Instantiate (plate, frontRight, new Quaternion (0, 0, 0, 0));
				plates.Add (frontRightPlate);
                createObstacle(frontRight);

                clearList ();
                clearObstacles();
			}
			else if (hit.collider.gameObject == leftPlate) {
				Debug.Log ("Left Plate");
				
				rightPlate = currentPlate;
				currentPlate = leftPlate;
				frontRightPlate = frontPlate;
				frontPlate = frontLeftPlate;
                
                Vector3 left = new Vector3(leftEnd, platePosition.y, platePosition.z);
                Vector3 frontLeft = new Vector3(leftEnd, platePosition.y, frontEnd);
				positionUpdate();
				leftPlate = (GameObject)Instantiate (plate, left, new Quaternion (0, 0, 0, 0));
				plates.Add (leftPlate);
                createObstacle(left);

				frontLeftPlate = (GameObject)Instantiate (plate, frontLeft, new Quaternion(0,0,0,0));
				plates.Add (frontLeftPlate);
                createObstacle(frontLeft);
			}
			else if(hit.collider.gameObject == rightPlate) {
				Debug.Log ("Right Plate");

				leftPlate = currentPlate;
				currentPlate = rightPlate;
				frontLeftPlate = frontPlate;
				frontPlate = frontRightPlate;

                Vector3 right = new Vector3(rightEnd, platePosition.y, platePosition.z);
                Vector3 frontRight = new Vector3(rightEnd, platePosition.y, frontEnd);

				positionUpdate();
				frontRightPlate = (GameObject)Instantiate (plate, frontRight, new Quaternion(0,0,0,0));
				plates.Add (frontRightPlate);
                createObstacle(frontRight);

				rightPlate = (GameObject)Instantiate (plate, right, new Quaternion (0, 0, 0, 0));
				plates.Add (rightPlate);
                createObstacle(right);
			}
		}
	}

	void positionUpdate(){

		platePosition = currentPlate.transform.position;
		
		frontEnd = platePosition.z + zScale;
        leftEnd = platePosition.x - Math.Abs(xScale);
		rightEnd = platePosition.x + xScale;
	}

    void clearList()
    {
        List<GameObject> plateList = new List<GameObject> { currentPlate, frontPlate, leftPlate, rightPlate, frontLeftPlate, frontRightPlate };
        foreach (GameObject plate in plates)
        {
            if (!plateList.Contains(plate))
                Destroy(plate);
        }
    }
    void clearObstacles()
    {
        foreach (GameObject obstacle in obstacles)
        {
            Destroy(obstacle);
        }
    }

    void createObstacle(Vector3 position) {
        int obstacles = UnityEngine.Random.Range(1, 3);
        for (int i = 0; i < obstacles; i++)
        {
            GameObject obs = null;
            int obstacle = UnityEngine.Random.Range(0,3);
            Vector3 random = new Vector3(UnityEngine.Random.Range(-10f,10f), 0, UnityEngine.Random.Range(-10f,10f));
            switch (obstacle)
            {
                case 0: obs = (GameObject)Instantiate(wall, position + random, Quaternion.Euler(UnityEngine.Random.Range(0f, 360f), 0, UnityEngine.Random.Range(0f, 360f)));
                    break;
                case 1: obs = (GameObject)Instantiate(jump, position + random, Quaternion.Euler(UnityEngine.Random.Range(0f, 360f), 0, UnityEngine.Random.Range(0f, 360f)));
                    break;
                case 2: obs = (GameObject)Instantiate(tower, position + random, Quaternion.Euler(UnityEngine.Random.Range(0f, 360f), 0, UnityEngine.Random.Range(0f, 360f)));
                    break;
                case 3: obs = (GameObject)Instantiate(ramp, position + random, Quaternion.Euler(UnityEngine.Random.Range(0f, 360f), 0, UnityEngine.Random.Range(0f, 360f)));
                    break;
            }
            this.obstacles.Add(obs);
        }
    }
}
