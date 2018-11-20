using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Script that contains and centralize a lot of informations and data about player character, such as life, stamina, attributes...
public class player_character : MonoBehaviour 
{

// = = = [ VARIABLES DEFINITION ] = = =

[Space(10)][Header("Data")]
	public	 		List<cl_quest>				accepted_quests						= new List<cl_quest>();
	public			cl_character_data			player_character_data				;
	
[Space(10)][Header("Attributes")]
	public			int							gold								;

[Space(10)][Header("Usuals")]
	public			float						gold_ui_trans_value 				;

[Space(10)][Header("References")]
	public			PlayerController			player_controller					;
	public			scr_InteractionController	interaction_controller				;
	public			scr_battle_script			player_battle_script				;	

// = = =

// = = = [ VARIABLES PROPERTIES ] = = =

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

	void Start () 
	{
		// bind player's character data to player's battle script
		player_battle_script.linked_character_data = player_character_data;
	}
	
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.KeypadPlus)) {player_character_data.Experience += 9; }
	}

// = = =

// = = = [ CLASS METHODS ] = = =

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
