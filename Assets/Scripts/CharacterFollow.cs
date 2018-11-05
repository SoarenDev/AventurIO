using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFollow : MonoBehaviour {

	public 	GameObject 		player;

	public	Vector3			offset;

	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		offset = transform.position - player.transform.position;
	}
	
	void Update () 
	{
		transform.position = player.transform.position + offset;
	
	}

}
