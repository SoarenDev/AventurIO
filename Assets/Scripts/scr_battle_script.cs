using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_battle_script : MonoBehaviour 
{

// = = = [ VARIABLES DEFINITION ] = = =

[Space(10)][Header("Data")]
	public 		int 						actual_combo					;
	public		int[]						last_skill_array				;			// PAS OPTI, PEUT ETRE MIEUX GÉRÉ
	[Space(5)]
	public		float						health							= 30;
	public		float						stamina							= 30;

[Space(10)][Header("Usuals")]
	public		float						shoot_cooldown					;
	public		bool						is_exhausted					;
	public		bool						is_attacking					;
	public		bool						is_interruptible				;
	public		IEnumerator					ongoing_skill_coroutine			;

[Space(10)][Header("References")]
	public 		cl_character_data 			linked_character_data			;
	public		Rigidbody2D					rigidbody_reference				;
	private		Coroutine					exhaustion_coroutine			;

// = = =

// = = = [ VARIABLES PROPERTIES ] = = =

	public	float	Health
	{
		get { return health; }
		set 
		{ 
			if (value > 0) { health = Mathf.Min(value, linked_character_data.max_health); }
			else { health = 0; Debug.LogWarning("DEAD DEAD DEAD!!!"); }
		}
	}

	public	float	Stamina
	{
		get { return stamina; }
		set 
		{ 
			if (value > 0) { stamina = Mathf.Min(value, linked_character_data.max_stamina); }
			else { stamina = 0; TriggerExhaustion(1f); Debug.LogWarning("EXHAUSTION!!!"); }
		}
	}

// = = =

// = = = [ MONOBEHAVIOR MEHODS ] = = =

	void Start () 
	{

		// initialize character vitals
		Health = linked_character_data.max_health;
		Stamina = linked_character_data.max_stamina;
		
	}
	
	// INPUTS SHOULD BE HANDLED IN THE "PLAYER_CONTROLLER" SCRIPT
	void Update () 
	{
		// = = Stamina regeneration = =
		if (is_exhausted == false && is_attacking == false && Stamina < linked_character_data.max_stamina)
		{
			Stamina += (linked_character_data.stamina_regen * Time.deltaTime);
		}
		// = =
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
		if (Stamina > 0)
		{
			LaunchSkill(skill_array[actual_combo]);
		}
		else 
		{ 
			Debug.Log("NO STAMINA"); 
			return;
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
		// DataManager.instance.data_skill_Dict[skill_index].LaunchSkill(this.gameObject);
		ongoing_skill_coroutine = DataManager.instance.data_skill_Dict[skill_index].LaunchSkill(this);
		StartCoroutine(ongoing_skill_coroutine);

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
		Stamina = Mathf.Max(0, Stamina - amount);

		return;
	}

	/// <summary>
	/// Sets the character as exhausted, briefly stopping its stamina regeneration. Triggering this method while the coroutine is still running will reset the exhaustion time to 0, and will only take into account the NEW DURATION!
	/// </summary>
	public	void TriggerExhaustion(float duration)
	{
		if (exhaustion_coroutine != null) { StopCoroutine(exhaustion_coroutine); }
		exhaustion_coroutine = StartCoroutine("ExhaustionCoroutine", 1f);
		return;
	}

	/// <summary>
	/// Exhaustion coroutine. Sets the character as exhausted for a given time.
	/// </summary>
	public IEnumerator ExhaustionCoroutine(float duration)
	{
		is_exhausted = true;

		yield return new WaitForSeconds(duration);

		is_exhausted = false;
		yield return null;
	}

	/// <summary>
	/// Applies damage from a given damageCollider.
	/// </summary>
	void TakeDamageFromCollider(scr_damageCollider damageCollider)
	{
		Health -= damageCollider.damage_value;
		Debug.Log("HIT TAKEN! <b>" + damageCollider.damage_value + " damages taken!</b>");
		return;
	}

	/// <summary>
	/// Returns a normalized vector that indicates the direction of a action.
	/// Note that launching this method with a player "chara_type" will get the Movement Axis value, while launching it with a "IA" chara_type will launch the "GetTargetDirection" method.
	/// </summary>
	public Vector2 GetCharacterActionDirection(enum_character_type chara_type)
	{
		Vector2 direction_vector = new Vector2(0,-1);

		switch (chara_type)
		{
			case enum_character_type.player:

				// check if player has specify a direction
				if (Input.GetAxis("AX_MoveX") + Input.GetAxis("AX_MoveY") != 0)
				{
					// normalize combined axis inputs' value
					float normalize_factor;  // factor used to normalise the vector
					direction_vector.x = Input.GetAxis("AX_MoveX");
					direction_vector.y = -Input.GetAxis("AX_MoveY");
					normalize_factor = Mathf.Abs(direction_vector.x) + Mathf.Abs(direction_vector.y);
					
					// return normalized direction vector
					return (direction_vector / normalize_factor);
				}
				else
				{
					// return default direction
					return new Vector2(0,1);
				}

			case enum_character_type.IA:
				
			default: 
				Debug.LogError("GetCharacterActionDirection returns with error: Undefined parameter!"); 
				return new Vector2(0,0);
		}
	}

	/// <summary>
	/// Interrupts the ongoing skill if its "can_be_interrupted" is true.
	/// </summary>
	public void InterruptAction()
	{
		StopCoroutine(ongoing_skill_coroutine);
		is_attacking = false;
		
		Debug.Log("INTERRUPTED");
		return;
	}

// = = =


}
