using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_npc_shell : MonoBehaviour 
{

// = = = [ VARIABLES DEFINITION ] = = =

[Space(10)][Header("Data")]
	public		cl_npc					linked_npc					;		// contains the npc class, the "soul" of the shell

[Space(10)][Header("References")]
	public		SpriteRenderer			npc_overhead_renderer		;
	public		Sprite					quest_available_sprite		;
	public		Sprite					quest_validable_sprite		;

// = = =


// = = = [ MONOBEHAVIOR METHODS ] = = =

	void Start()
	{
		return;
	}

// = = =


// = = = [ CLASS METHODS ] = = =

	/// <summary>
	/// Sets a given sprite and color for the npc_shell's overhead object, and displays it.
	/// </summary>
	public void DrawOverheadSprite(Sprite sprite, Color color)
	{
		npc_overhead_renderer.sprite = sprite;
		npc_overhead_renderer.color = color;

		npc_overhead_renderer.gameObject.SetActive(true);
		return;
	}

	/// <summary>
	/// Hides the npc_shell's overhead object.
	/// </summary>
	public void ClearOverheadSprite()
	{
		npc_overhead_renderer.gameObject.SetActive(false);
		return;
	}

	/// <summary>
	/// Updates the npc_shell's overhead object according to quest indicator.
	/// </summary>
	public void UpdateQuestOverheadSprite()
	{
		// Display validable quest indicator (higher priority)
		if ( linked_npc.SearchQuestState(enum_quest_state.Complete) == true ) { DrawOverheadSprite(quest_validable_sprite, new Color(0.45f, 0.75f, 0, 1)); }
		// Display available quest indicator
		else if ( linked_npc.SearchQuestState(enum_quest_state.Available) == true ) { DrawOverheadSprite(quest_available_sprite, new Color(1, 0.8f, 0, 1)); }
		// Display ongoing quest indicator (lower priority)
		else if ( linked_npc.SearchQuestState(enum_quest_state.Ongoing) == true ) { DrawOverheadSprite(quest_validable_sprite, new Color(0.9f, 0, 0, 1)); }

		return;
	}
	
// = = =

}
