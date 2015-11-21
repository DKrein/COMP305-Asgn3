/*
 * Create by Douglas Krein
 * Description: Script to destroy enemies when colide with other object
 * 
 * 
*/
using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	private GameController gameController;

	void Start() {
		//Find the game Controller and set to a variable
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
	}

	void OnTriggerEnter(Collider other) 
	{
		//Instantiate(explosion, transform.position, transform.rotation);
		if (other.tag == "Enemy") {
			//Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GetHit();
			//destroy enemy
			Destroy(other.gameObject);

			//If player have damage 0 it finish the game 
			if (gameController.lifes <=0){
				//Call the gameover function
				gameController.GameOver();
			}
		}
	} 
}
