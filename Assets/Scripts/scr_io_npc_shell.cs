using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_io_npc_shell : scr_InteractibleObjectParentScript 
{

// = = = [ VARIABLES DEFINITION ] = = =

	public			scr_npc_shell			npc_shell_script		;
	public 	static 	scr_UI_parent			UI_npcQuestsMenu		;	// obsolete as fuck
	public 	static 	scr_UI_parent			UI_npcQuestTab			;   // obsolete as fuck

// = = =

// = = = [ MONOBEHAVIOR METHODS ] = = =

	void Start () 
	{
		return;
	}

// = = =

// = = = [ CLASS METHODS ] = = =
	
	public override void InRange()
	{
		// Debug.Log("Recieved IN RANGE in " + gameObject.name);
	}

	public override void Interaction()
	{
		Debug.Log("Recieved INTERACTION in " + gameObject.name);

		if (is_tab_on == false)
		{	
			// construct the PLACE MENU if is's not already constructed
			GameManager.ui_active_npc = npc_shell_script;
			GameManager.instance.UI_Construct(GameManager.instance.UI_npcQuestsMenu);
			is_tab_on = true;		// set this script instance as the one used in open PLACE MENU
		}
		else
		{
			// quit PLACE MENU if it was actually open
			GameManager.instance.UI_Close(GameManager.UI_active_menu);
			// GameManager.instance.UI_Close(ui_place_tab);
			is_tab_on = false;
		}

		return;
	}

	public override void OutRange()
	{
		// Debug.Log("Recieved OUT RANGE in " + gameObject.name);

		// if the IO is gone out of range, close the tab if it is the active one
		if (is_tab_on)
		{	
			GameManager.instance.UI_Close(GameManager.UI_active_menu);
			// GameManager.instance.UI_Close(ui_place_tab);
			is_tab_on = false;
		}

		return;
	}

// = = =


}
