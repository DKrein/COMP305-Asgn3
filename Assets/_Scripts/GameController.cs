using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject enemy;
	public int enemyCount;
	public Text scoreText;
	public Text lifeText;
	public Text gameOverText;
	public Text restartText;
	public Vector3 spawnValues;

	[HideInInspector]
	public int lifes = 5;
	[HideInInspector]
	public bool gameOver;

	private int score;
	private bool restart;

	// Use this for initialization
	void Start () {

		gameOver = false;
		restart = false;
		
		gameOverText.text = "";
		restartText.text = "";

		score = 0;

		scoreText.text = "Score : " + score;
		UpdateLife ();

		StartCoroutine( SpawnEnemies ());
	}
	
	// Update is called once per frame
	void Update() {
		if (restart) {
			if (Input.GetKeyDown("r")) {
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}

	IEnumerator SpawnEnemies(){
		
		yield return new WaitForSeconds(3f);
		while(true){
			for (int i=0;i < enemyCount;i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, Random.Range (-spawnValues.z, spawnValues.z));
				Quaternion spawnRotation = Quaternion.Euler (90.0f, 0f, 0f);
				Instantiate (enemy, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(0.7f);
			}
			yield return new WaitForSeconds(1f);

			if(gameOver){
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}	
	}

	public void AddScore(int newScoreValue) {
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore() {
		scoreText.text = "Score: " + score;
	}

	public void GetHit() {
		lifes -= 1;
		UpdateLife ();
	}

	void UpdateLife() {
		string hearts = "";
		for (int i = 1; i <= lifes; i++) {
			hearts = hearts+"❤ ";
		}

		lifeText.text = "Lifes: "+hearts;
	}

	public void GameOver(){
		gameOverText.text = "Game Over";
		gameOver = true;
	}
}
