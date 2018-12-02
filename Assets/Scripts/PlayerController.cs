using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enum_controller_mode
{
	worldmap,
	interior,
	menu
}

public class PlayerController : MonoBehaviour 
{

// = = = [ VARIABLES DEFINITION ] = = =

[Space(10)][Header("Data")]
	public	enum_controller_mode		controller_mode						;
	public 	float						speed								;
	private	float						localScaleX							;

[Space(10)][Header("References")]
	public	player_character			player_script						;
	public	Animator					characterAnimator					;
	public 	Animation					walkAnimation						;

// = = =

// = = = [ MONOBEHAVIOR MEHODS ] = = =

	void Start () 
	{
		localScaleX = transform.localScale.x;
	}
	
	void Update () 
	{
		// = = = BATTLE = = =

		// check if player is not attacking
		if (player_script.player_battle_script.is_attacking == false)
		{
			// check battle input recieved
			if (Input.GetKeyDown(KeyCode.T))
			{
				player_script.player_battle_script.SelectSkill(player_script.player_character_data.weapon.skill_base_index);
			} 
			else if (Input.GetKeyDown(KeyCode.Y))
			{
				player_script.player_battle_script.SelectSkill(player_script.player_character_data.weapon.skill_secondary_index);
			}
			else if (Input.GetKeyDown(KeyCode.U))
			{
				player_script.player_battle_script.SelectSkill(player_script.player_character_data.weapon.skill_special_1_index);
			}
			else if (Input.GetKeyDown(KeyCode.I))
			{
				player_script.player_battle_script.SelectSkill(player_script.player_character_data.weapon.skill_special_2_index);
			}
			else if (Input.GetKeyDown(KeyCode.O))
			{
				player_script.player_battle_script.SelectSkill(player_script.player_character_data.weapon.skill_ultimate_index);
			}
		}
			

		// = = = MOVEMENT = = =

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

		// = = =

		// Animation

	}

// = = =



// = = = [ CLASS METHODS ] = = =

// = = =

}
