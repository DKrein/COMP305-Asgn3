using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;

public class Shoot : MonoBehaviour {

	public GameObject explosion;
	public AudioSource bulletFireSound;

	// PRIVATE INSTANCE VARIABLES 
	private int _currentImpact = 0;
	private int _maxImpacts = 5; 
	private Transform _transform;

	private bool _shooting = false;

	private GameController gameController;

	// Use this for initialization
	void Start () {
		// reference to the gameObject's transform component
		this._transform = gameObject.GetComponent<Transform> ();

		//Find the game Controller and set to a variable
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
		// play muzzle flash and shoot impact when left-mouse button is pressed
		if (CrossPlatformInputManager.GetButtonDown ("Fire1") && !gameController.gameOver ) {
			this._shooting = true;
			this.bulletFireSound.Play();
		}
	}

	// Physics effects
	void FixedUpdate() {
		if (this._shooting) {
			this._shooting = false;

			RaycastHit hit;

			Debug.DrawRay(this._transform.position, this._transform.forward);
			if(Physics.Raycast(this._transform.position, this._transform.forward, out hit, 50f)) {

				if(hit.transform.tag == "Enemy") {
					Destroy(hit.transform.gameObject);
					Instantiate(this.explosion);
					this.explosion.transform.position = hit.point;
					gameController.AddScore (20);

				}

				// ensure that you don't go out of bounds of the object pool
				if(++this._currentImpact >= this._maxImpacts) {
					this._currentImpact = 0;
				}
				
			}
		}
	}
}
