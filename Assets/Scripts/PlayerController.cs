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

[Space(10)][Header("Input axis management")]
	private Dictionary<string, bool>	axis_consumed_dict					= new Dictionary<string, bool>()
	{
		{"a", false},
		{"b", false},
		{"y", false},
		{"x", false},
		{"rb", false},
		{"rt", false},
		{"lb", false},
		{"lt", false}
	};

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
			if (Input.GetAxisRaw("AX_A") != 0 && axis_consumed_dict["a"] == false)
			{
				player_script.player_battle_script.SelectSkill(player_script.player_character_data.weapon.skill_base_index);
				axis_consumed_dict["a"] = true;
			} 
			else if (Input.GetAxisRaw("AX_A") == 0) { axis_consumed_dict["a"] = false; }

			if (Input.GetAxisRaw("AX_B") != 0 && axis_consumed_dict["b"] == false)
			{
				player_script.player_battle_script.SelectSkill(player_script.player_character_data.weapon.skill_secondary_index);
				axis_consumed_dict["b"] = true;
			}
			else if (Input.GetAxisRaw("AX_B") == 0) { axis_consumed_dict["b"] = false; }

			if (Input.GetAxisRaw("AX_Y") != 0 && axis_consumed_dict["y"] == false)
			{
				player_script.player_battle_script.SelectSkill(player_script.player_character_data.weapon.skill_special_1_index);
				axis_consumed_dict["y"] = true;
			}
			else if (Input.GetAxisRaw("AX_Y") == 0) { axis_consumed_dict["y"] = false; }

			if (Input.GetAxisRaw("AX_X") != 0 && axis_consumed_dict["x"] == false)
			{
				player_script.player_battle_script.SelectSkill(player_script.player_character_data.weapon.skill_special_2_index);
				axis_consumed_dict["x"] = true;
			}
			else if (Input.GetAxisRaw("AX_X") == 0) { axis_consumed_dict["x"] = false; }

			if (Input.GetAxisRaw("AX_RT") != 0 && axis_consumed_dict["rt"] == false)
			{
				player_script.player_battle_script.SelectSkill(player_script.player_character_data.weapon.skill_ultimate_index);
				axis_consumed_dict["rt"] = true;
			}
			else if (Input.GetAxisRaw("AX_RT") == 0) { axis_consumed_dict["rt"] = false; }
		}
			
		// = = = MOVEMENT = = =

		// check if player is not attacking
		if (player_script.player_battle_script.is_attacking == false)
		{
			if (Input.GetAxis("AX_MoveX") != 0 || Input.GetAxis("AX_MoveY") != 0)
			{
				// define direction vector
				Vector2 direction_vector = GetCharacterActionDirection();

				// move the player according to direction vector
				transform.Translate(new Vector3 (speed * direction_vector.x, speed * direction_vector.y, 0));

				if (direction_vector.x > 0)
				{
					transform.localScale = new Vector2(-localScaleX, transform.localScale.y);
					characterAnimator.SetBool("isWalkingRight?", true);
				} 
				else 
				{
					transform.localScale = new Vector2(localScaleX, transform.localScale.y);
					characterAnimator.SetBool("isWalkingRight?", true);
				}
			}
			else
			{ characterAnimator.SetBool("isWalkingRight?", false); }
		} else { characterAnimator.SetBool("isWalkingRight?", false); }

		// = = =

		// Animation

	}

// = = =



// = = = [ CLASS METHODS ] = = =

	/// <summary>
	/// Returns a normalized vector that indicates the direction of the player movement.
	/// </summary>
	public Vector2 GetCharacterActionDirection()
	{
		Vector2 direction_vector = new Vector2(0,0);

			// normalize combined axis inputs' value
			float normalize_factor;  // factor used to normalise the vector
			direction_vector.x = Input.GetAxis("AX_MoveX");
			direction_vector.y = -Input.GetAxis("AX_MoveY");
			normalize_factor = Mathf.Abs(direction_vector.x) + Mathf.Abs(direction_vector.y);
				
			// return normalized direction vector
			return (direction_vector / normalize_factor);
	}

// = = =

}
