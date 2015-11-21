using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class EnemyScript : MonoBehaviour
{
	
	public enum FSMState
	{
		None,
		Patrol,
		Look,
		Chase,
		Attack,
		Dead,
		Run
	}
	
	public float speed; //Enemy Speed
	public bool isAgressive; //Chase player

	private GameObject objPlayer;

	
	protected void Initialize()
	{
		objPlayer = GameObject.FindGameObjectWithTag("Player");

	}
	
	// Use this for initialization
	void Start()
	{
		Initialize();
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		ExecuteChaseState();
	}
		
	protected void ExecuteChaseState()
	{
		if (objPlayer != null) {
			Quaternion targetRotation = Quaternion.LookRotation (objPlayer.transform.position - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * 5.0f);
			transform.Translate (Vector3.forward * Time.deltaTime * speed);
		}
	}

	
}
