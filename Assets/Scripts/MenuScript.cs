using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	private GameObject camera;

	private bool menu;
	private bool about;
	private bool readme;
	private bool level;

	private Vector3 menuPos = new Vector3(0,0,0);
	private Vector3 aboutPos = new Vector3(90,0,0);
	private Vector3 readmePos = new Vector3(0,270,0);
	private Vector3 levelPos = new Vector3(0,180,0);
	void Start(){
		camera = GameObject.Find ("Main Camera");
	}

	void Update(){
		Vector3 camRotation = camera.transform.eulerAngles;
		float camX = camera.transform.eulerAngles.x;
		float camY = camera.transform.eulerAngles.y;
	
		if (menu) {
			if (camRotation != menuPos) {
				if (camX != menuPos.x && camX < menuPos.x + 350)
					camera.transform.Rotate (-Time.deltaTime * 100, 0, 0);
				else if (camY != menuPos.y && camY < menuPos.y + 350)
					camera.transform.Rotate (0, -Time.deltaTime * 100, 0);
				else {
					Debug.Log ("Lock this menu!");
					menu = false;
					camera.transform.eulerAngles = menuPos;
				}
			}

		} else if (about) {
		
			if (camRotation != aboutPos) {
				if (camX != aboutPos.x && camX < aboutPos.x)
					camera.transform.Rotate (Time.deltaTime * 100, 0, 0);
				else {
					Debug.Log ("Lock this about!");
					about = false;
					camera.transform.eulerAngles = aboutPos;
				}
			} else {
				Debug.Log ("Lock this about!");
				about = false;
				camera.transform.eulerAngles = aboutPos;
			}

		} else if (level) {
		
			if (camRotation != levelPos) {
				if (camY != levelPos.y && camY < levelPos.y)
					camera.transform.Rotate (0, Time.deltaTime * 100, 0);
				else {
					Debug.Log ("Lock this level!");
					level = false;
					camera.transform.eulerAngles = levelPos;
				}
			}
		} else if (readme) {
		
			if (camRotation != readmePos) {
				if (camY != readmePos.y && camY < readmePos.y)
					camera.transform.Rotate (0, Time.deltaTime * 100, 0);
				else {
					Debug.Log ("Lock this readme!");
					readme = false;
					camera.transform.eulerAngles = readmePos;
				}
			} 
		}
	}

	public void ReadMeButton(){
		readme = true;
	}

	public void MenuButton(){
		menu = true;
	}

	public void AboutButton(){
		about = true;
	}

	public void ExitButton(){
		Application.Quit ();
	}

	public void SelectButton(){
		level = true;
	}

	public void GitHubButton(){
		Application.OpenURL ("http://github.com/goggelz/GameDev/");
	}

	public void LevelButton(int levelNumber){
		Application.LoadLevel (levelNumber);
	}

	public void RestartButton(){
		Application.LoadLevel (Application.loadedLevel);
	}

	public void LoadMenu(){
		Application.LoadLevel (0);
	}
}
