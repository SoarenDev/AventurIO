using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_battle_script : MonoBehaviour 
{

// = = = [ VARIABLES DEFINITION ] = = =

[Space(10)][Header("Data")]
	public 		int 						actual_combo					;
	public		int[]						last_skill_array				;			// PAS OPTI, PEUT ETRE MIEUX GÉRÉ

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
	/// Selects the right skill to launch depending on the input and of the actual combo. Also checks if stamina != 0.
	/// </summary>
	public void SelectSkill(int[] skill_array)
	{
		// reset combo if the in-launch skill_array isn't the same as the last skill array handled by the script
		if (skill_array != last_skill_array) { ResetCombo(); }

		// launch skill if stamina != 0
		if (linked_character_data.stamina > 0)
		{
			LaunchSkill(skill_array[actual_combo]);
		}
		else 
		{ 
			Debug.Log("NO STAMINA"); 
		}

		// increment actual_combo
		IncrementCombo();
		
		// reset combo if actual combo exceeds weapon's max combo for this input
		if (actual_combo > skill_array.Length -1) { ResetCombo(); }

		// update "last_skill_array"
		last_skill_array = skill_array;

		return;
	}

	/// <summary>
	/// Launches the given skill.
	/// </summary>
	public void LaunchSkill(int skill_index)
	{
		// launch skill
		DataManager.instance.data_skill_Dict[skill_index].LaunchSkill();

		// consume stamina
		ConsumeStamina(DataManager.instance.data_skill_Dict[skill_index].stamina_cost);

		// Debug.Log("Skill launched: <b>" + skill_index + "</b>");
		return;
	}

	/// <summary>
	/// Increments actual_combo by 1.
	/// </summary>
	public void IncrementCombo()
	{
		actual_combo += 1;

		return;
	}

	/// <summary>
	/// Resets actual_combo.
	/// </summary>
	public void ResetCombo()
	{
		actual_combo = 0;

		// Debug.Log("COMBO RESET");
		return;
	}

	/// <summary>
	/// Consumes a certain amount of stamina from the character. Briefly stops its stamina regeneration if it reaches 0.
	/// </summary>
	public void ConsumeStamina(int amount)
	{
		// update character's stamina 
		linked_character_data.stamina = Mathf.Max(0, linked_character_data.stamina - amount);

		// Trigger exhaustion if stamina has reached 0
		if (linked_character_data.stamina <= 0) { Debug.Log("EXHAUSTION!"); }

		return;
	}

// = = =


}
