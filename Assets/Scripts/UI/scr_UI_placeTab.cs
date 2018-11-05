using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_UI_placeTab : scr_UI_parent {

[Space(10)][Header("Data")]
	//public	static scr_place	ui_active_place		; // moved into GameManager

[Space(10)][Header("References")]
	public	Image		background						;
	public	Text		place_name						;
	public	Text		governor						;
	public	Text		main_faction					;
[Space(5)]
	public	Text		type							;
	public	Text		population						;
	public	Text		factions						;
	public	Text		events							;
[Space(5)]
	public	GameObject	ui_faction_scrollList			;
	public	GameObject	ui_faction_button_prefab		;
	public	List<GameObject> ui_faction_button_list 	= new List<GameObject>();

// = =

	// Refresh all data in the tab using ui_active_place data
	public override void RefreshTab()
	{
		place_name.text		=							GameManager.ui_active_place.place_name;
		// governor.text		=							ui_active_place.place_governor.npc_firstname + " " + ui_active_place.place_governor.npc_lastname;
		type.text			=	"Type		: " 	+ 	GameManager.ui_active_place.type.type.ToString();
		population.text		=	"Population	: " 	+ 	GameManager.ui_active_place.place_npcs.Count.ToString();
		factions.text		=	"Factions	: "		+	GameManager.ui_active_place.place_faction_list.Count.ToString();
		events.text			=	"Events 	: " 	+   GameManager.ui_active_place.place_events.Count.ToString();

		if (GameManager.ui_active_place.place_main_faction.faction.faction_name != "")
		{ main_faction.text	=							GameManager.ui_active_place.place_main_faction.faction.faction_name; }
		else
		{ main_faction.text	=	"no faction"		; }
		

	// = REFRESH UI BUTTONS =
		// Clear old UI Buttons
		while (ui_faction_button_list.Count > 0)
		{
			Destroy(ui_faction_button_list[0].gameObject);
			ui_faction_button_list.RemoveAt(0);
			// Debug.Log("1 faction tab cleared");
		}

		// Construct new UI Buttons
		foreach (var faction_struct in GameManager.ui_active_place.place_faction_list)
		{
			// draw button
			GameObject instance = Instantiate(ui_faction_button_prefab, new Vector3(0,0,0), Quaternion.identity, ui_faction_scrollList.transform);
			scr_UI_placeFactionButton instance_script = instance.GetComponent<scr_UI_placeFactionButton>();

			// add to button list
			ui_faction_button_list.Add(instance);

			// Initialize new button
			instance_script.linked_faction_struct = faction_struct;
			instance_script.Initialize();
		}

		Debug.Log("Place tab refreshed");
	}



}
