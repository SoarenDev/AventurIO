using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlaceType", menuName = "Place/Type", order = 1)]
public class so_place_type : ScriptableObject {

[Space(10)][Header("GLOBAL EVENTS AUTOMATED MANAGEMENT")][Tooltip("TOOGLE THESE BOOLEANS TO UPDATE EVENT LIST WITH 'GLOBAL PLACE EVENTS'")]
	public	bool						clear_global_events					;
	public	bool						add_global_events					;

[Space(10)][Header("Data")]
	public	int							id									;
[Space(10)]
	public	PlaceType					type								;

[Space(10)][Header("Attributes")]
	public	Sprite						sprite								;
	public	Color						sprite_color						;
	[Space(5)]
	public	int							min_npc								;
	public	int							max_npc								;
	[Space(5)]
	// public	so_namelist					namelist_default					; // OBSOLETE, list of possible names is now handled directely by the place_type
	public	List<string>				name_list							= new List<string>();
	public	string						level_default						;  	// temp

[Space(10)][Header("Events")]
	public	static List<int>			global_place_events					= new List<int>
	{
		// global place evolution events
		1,2,3,4,5,20,21,22,23,24
	};
	public	List<int>					events_default						= new List<int>();

[Space(10)][Header("Faction generation ")][Range(0,1)]
	public	float						part_of_npcs_in_faction  		    ;	//[0 ~ 1]	// define which percent of generated npcs will be instantly linked to a faction (faction will be random weighted by factions influence)
	public	int							min_faction							;	// minimal amount of faction generated
	[Tooltip("Should NEVER be higher than min_npc")]
	public	int							max_faction							;	// maximal (...)

[Space(10)][Header("Faction random list ")]
	public 	List<struct_factionTypeXint> faction_genList_default			;	// gen preset which define which types of faction can appear in generation and their random weight

// = = =

// /*

	void OnValidate()
	{
		if (clear_global_events == true)
		{
			RemoveOldGlobalEvents();
		}

		// ADD all "global_place_events" from actual event list
		if (add_global_events == true)
		{
			AddNewGlobalEvents();
		}

		return;
	}

	/// <summary>
	/// REMOVE all "global_place_events" from actual event list. 
	/// </summary>
	void RemoveOldGlobalEvents()
	{
		foreach (var evnt in global_place_events)
		{
			events_default.Remove(evnt);
		}

		// Debug.Log("<b>>>> GLOBAL PLACE EVENTS REMOVED <<<</b>");
		clear_global_events = false;

		return;
	}

	/// <summary>
	/// ADD all newly added "global_place_events" from global_place_events list to actual event list
	/// </summary>
	void AddNewGlobalEvents()
	{
		foreach (var evnt in global_place_events)
		{
			events_default.Add(evnt);
		}

		// Debug.Log("<b>>>> GLOBAL PLACE EVENTS ADDED <<<</b>");
		add_global_events = false;

		return;
	}

// */

}
