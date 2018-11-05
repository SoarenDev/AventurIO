using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class cl_event {

// >>> [IMPORTANT] <<< 	NON-PERSISTANT EVENTS always launch from EventManager's dictionnary and create independant quests.
// >>> [IMPORTANT] <<<	PERSISTANT EVENTS create, when launching, an INDEPENDANT instance of the event which create quests which are linked to him.

[Header("Attributes")]
	public	int						event_id							;		// should also be put as a prefix in the name of the event inherited class!
	public	string					event_name							= "unbinded event";
	public	string					event_description					;
	public	int						event_probability					;
	[Space(5)]			// persistant events
	public	bool					isPersistant						;		// if true the event will instantiate and stay active in the region's ongoing event and wont disapear by himself; if false the event will only be played once
	public	bool					is_outcome_timer_active				= false;// indicate if the automatic outcome timer for the event should be running or not (ex: it should stop if the player actually)
	public	float					persistant_outcome_timer			= 15f;	// [DEBUG] timer in seconds before persistant event finish and chose an outcome by himself
	public 	float   				outcome_timer_countdown     		;
	public	int 					outcome								= 0;	// [DEBUG] indicate the outcome chosen by OutcomeEvent (arbitrary). Is overwritten in child classes
	[Space(5)]			// indexes
	public	int						event_icon_index					;		// index of the icon use when showing this event in UI 

[Space(10)][Header("Instance attributes")]
	[System.NonSerialized]
	public	scr_region				event_origin_region					;
	[System.NonSerialized]
	public	scr_place				event_origin_place					;
	[System.NonSerialized]
	public	strct_local_faction		event_origin_faction				;

// = = =

	public virtual bool ConditionCheck(scr_place targeted_place)			// function that will be overrided in child class. Check given condition for the event, and return TRUE if they are all true. A return TRUE allow the event to be selected when an event is drawn by the EventManager
	{
		return true;
	}

	public virtual bool ConditionCheck(scr_place targeted_place, strct_local_faction targeted_faction)			// function that will be overrided in child class. Check given condition for the event, and return TRUE if they are all true. A return TRUE allow the event to be selected when an event is drawn by the EventManager
	{
		return true;
	}

	/// <summary>
	/// Define the event probability from given code proper to each event. Returns the final probability. The parent version of the method simply returns the "event_probability" variable of the class.
	/// </summary>
	public virtual int DefineProbability(scr_place targeted_place)
	{
		return event_probability; 
	}

	/// <summary>
	/// Creates an instance of the event which be used to launch the event. Also bind the event to his origin place. Persistant events will also anchor to the place as "ongoing event"
	/// </summary>
	public virtual cl_event	InstantiateEvent(scr_place targeted_place)
	{
		event_origin_place = targeted_place;
		return null;
    }
	
	public virtual void LaunchEvent(scr_place targeted_place, strct_local_faction targeted_faction)
	{
		Debug.Log("<b>Event " + event_name + " launched ! </b>");
		
		event_origin_place = targeted_place;
		if(targeted_faction != null) { event_origin_faction = targeted_faction; }

		return;
	}

}
