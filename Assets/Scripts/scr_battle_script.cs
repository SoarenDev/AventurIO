using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_battle_script : MonoBehaviour 
{

// = = = [ VARIABLES DEFINITION ] = = =

[Space(10)][Header("Data")]
	public 		int 						actual_combo					;

[Space(10)][Header("References")]
	public 		cl_character_data 			linked_character_data			;

// = = =

// = = = [ MONOBEHAVIOR MEHODS ] = = =

	void Start () 
	{
		
	}
	
	// INPUTS SHOULD BE HANDLED IN THE "PLAYER_CONTROLLER" SCRIPT
	void Update () 
	{
		
	}

// = = =

// = = = [ CLASS METHODS ] = = =

	/// <summary>
	///
	/// </summary>
	public void LaunchSkill(int skill_index)
	{
		Debug.Log("Skill launched: <b>" + skill_index + "</b>");
		return;
	}

// = = =


}
