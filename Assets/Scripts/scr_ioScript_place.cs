using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_ioScript_place : scr_InteractibleObjectParentScript 
{

// = = = [ VARIABLES DEFINITION ] = = =

	public 			scr_place				linked_place_script		;
	public 	static 	scr_UI_placeMenu		UI_place_menu			;
	public 	static 	scr_UI_placeTab			UI_place_tab			;

// = = =

// = = = [ MONOBEHAVIOR METHODS ] = = =

	void Start () 
	{
		linked_place_script = gameObject.GetComponent<scr_place>();

		// binding
		UI_place_menu 		= GameManager.instance.UI_placeMenu;
		UI_place_tab 		= GameManager.instance.UI_placeTab;

		// Debug.Log("bind place menu: " + UI_place_menu);
	}

// = = =

// = = = [ CLASS METHODS ] = = =
	
	public override void InRange()
	{
		// Debug.Log("Recieved IN RANGE in " + gameObject.name);
	}

	public override void Interaction()
	{
		// Debug.Log("Recieved INTERACTION in " + gameObject.name);

		if (is_tab_on == false)
		{	
			// construct the PLACE MENU if is's not already constructed
			GameManager.ui_active_place = linked_place_script;
			GameManager.instance.UI_Construct(UI_place_menu);
			is_tab_on = true;		// set this script instance as the one used in open PLACE MENU
		}
		else
		{
			// quit PLACE MENU if it was actually open
			GameManager.instance.UI_Close(GameManager.UI_active_menu);
			// GameManager.instance.UI_Close(ui_place_tab);
			is_tab_on = false;
		}

	}

	public override void OutRange()
	{
		// Debug.Log("Recieved OUT RANGE in " + gameObject.name);

		// if the place instance gone out of range was the one used in the PLACE MENU UI, close the tab
		if (is_tab_on)
		{	
			GameManager.instance.UI_Close(GameManager.UI_active_menu);
			// GameManager.instance.UI_Close(ui_place_tab);
			is_tab_on = false;
		}
		
	}

// = = =

}
