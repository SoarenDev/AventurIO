using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public 	float		speed;
	private	float		localScaleX;
	public	Animator	characterAnimator;
	public 	Animation	walkAnimation;

	void Start () 
	{
		localScaleX = transform.localScale.x;
	}
	
	void Update () 
	{
		// Movement
		if (Input.GetKey(KeyCode.RightArrow))
		{
			transform.Translate(new Vector3 (1 * speed, 0, 0));
			transform.localScale = new Vector2(-localScaleX, transform.localScale.y);
			characterAnimator.SetBool("isWalkingRight?", true);
		} else {
			characterAnimator.SetBool("isWalkingRight?", false);
		}
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			transform.Translate(new Vector3 (-1 * speed, 0, 0));
			characterAnimator.SetBool("isWalkingLeft?", true);
			transform.localScale = new Vector2(localScaleX, transform.localScale.y);
		} else {
			characterAnimator.SetBool("isWalkingLeft?", false);
		}
		if (Input.GetKey(KeyCode.UpArrow))
		{
			transform.Translate(new Vector3 (0, 1 * speed * 0.75f, 0));
			characterAnimator.SetBool("isWalkingUp?", true);
		} else {
			characterAnimator.SetBool("isWalkingUp?", false);
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			transform.Translate(new Vector3 (0, -1 * speed * 0.75f, 0));
			characterAnimator.SetBool("isWalkingDown?", true);
		} else {
			characterAnimator.SetBool("isWalkingDown?", false);
		}

		// Animation

	}

}
