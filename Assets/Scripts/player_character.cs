using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Script that contains and centralize a lot of informations and data about player character, such as life, stamina, attributes...
public class player_character : MonoBehaviour {

// = = = [ VARIABLES DEFINITION ] = = =

[Space(10)][Header("Data")]
	public	 		List<cl_quest>				accepted_quests						= new List<cl_quest>();
	
[Space(10)][Header("Attributes")]
	public			int							health								;
	public			int							stamina								;
	public			int							experience							;
	public			int							gold								;
		
	[Space(5)]
	public			int							exp_level							= 1;

[Space(10)][Header("Usuals")]
	public			float						shoot_cooldown						;
	public			float						gold_ui_trans_value 				;

[Space(10)][Header("References")]
	public			PlayerController			player_controller					;
	public			scr_InteractionController	interaction_controller				;

// = = =

// = = = [ VARIABLES PROPERTIES ] = = =

	public	int		Experience
		{
			get { return experience; }
			set { experience = value; CheckLevelUp();}
		}

	public	int		Gold
		{
			get { return gold; }
			set { 
				gold = value; 
				GameManager.instance.StopCoroutine("GoldUiTransition");
				GameManager.instance.StartCoroutine("GoldUiTransition", 0.45f);
			}
		}
		
// = = =

// = = = [ MONOBEHAVIOR METHODS ] = = =

	/* void Start () 
	{
		
	}*/
	
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.KeypadPlus)) {Experience += 9; }
	}

// = = =

// = = = [ CLASS METHODS ] = = =

	/// <summary>
	/// Checks if the experience value is high enough to get the character to the next experience level, and if the character havn't reach the maximum level already.
	/// Method launched on all "experience" variable set.
	/// </summary>
	public	void	CheckLevelUp()
	{
		if (Experience >= DataManager.instance.exp_levelup_table[exp_level+1] && exp_level+1 < DataManager.instance.exp_levelup_table.Length)
		{
			LevelUp();
		}

		return;
	}

	/// <summary>
	/// Makes the character gain an experience level. Update all necessary variable and data.
	/// </summary>
	public	void	LevelUp()
	{
		// increments level attribute
		exp_level += 1;

		// remove experience needed for the level from actual experience
		Experience -= DataManager.instance.exp_levelup_table[exp_level];

		// re-check if character gained enough experience to gain one more level after this one
		CheckLevelUp();
		return;
	}

	/// <summary>
	/// Instantly moves the player at the given world position.
	/// </summary>
	public	void	SpawnPlayer(Vector3 spawn_position)
	{
		transform.SetPositionAndRotation(spawn_position, Quaternion.identity);

		return;
	}

// = = =

}
