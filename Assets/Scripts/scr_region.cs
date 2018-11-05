using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class scr_region : MonoBehaviour {

[Header("Attributes")]
	public	string							region_name							;

[Space(10)][Header("Content")]
	public	scr_place						region_capital_script				;
	public	List<scr_place>					region_places_scripts				= new List<scr_place>();
	public	int								region_population					;
	public	List<int>						region_modificators					= new List<int>();
	public	List<cl_event_persistant>		region_ongoingEvents				= new List<cl_event_persistant>();			// contain INSTANCES of events

//[Space(10)][Header("References")]

[Space(10)][Header("Usual variables")]
	public	float							timeBeforeNextEvent					= 3f;

// 

	void Start()
	{
		// // Initialize place->region link
		for (int i = 0; i < region_places_scripts.Count; i++)
		{
			region_places_scripts[i].linked_region = this;
		}

		// First launch resfresh methods
		RefreshRegion();
		ResetEventTimer();
	}

	// Update is mandatory to use the regions to compute the launching of events
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha0))
		{
			RefreshRegion();
		}

		// = = DEBUG: UPDATE PERSISTANT EVENTS = =
		List<cl_event_persistant> buffer_events_list = new List<cl_event_persistant>(region_ongoingEvents);	// buffer to keep the event list unchanged even if some of them should be destroyed (to avoid foreach loop itme list changing size while running )
		foreach (var item in buffer_events_list)
		{
			if (item.isPersistant == true)
			{
				item.CountdownOutcome();
			}
		}

		// = = DEBUG : Spawn Event immediately = =
		if (Input.GetKeyDown(KeyCode.P))
		{
			timeBeforeNextEvent = 0.1f;
		}

		// = = = EVENT LAUNCHER = = =
		if (timeBeforeNextEvent <= 0)
		{
			Debug.Log("\n<size=12><b>= = = TIMER BEFORE NEXT EVENT ENDED ! LAUNCHING EVENT NOW ! = = = </b></size>");

			ResetEventTimer();

			// Select then call an event
			struct_event_indexXowner selected_event;
			selected_event = SelectEvent();
			EventManager.instance.CallEvent(selected_event);
		}
		else
		{
			timeBeforeNextEvent -= Time.deltaTime;
		}


	}

	public void RefreshRegion() 		// called every time something change in a place or in the region to update the informations stocked in this script
	{
		Debug.Log("<b>= = = REFRESH REGION LAUNCHED = = =</b>");
		int new_population = 0;

		for (int i = 0; i < region_places_scripts.Count; i++)
		{
			new_population += region_places_scripts[i].place_npcs.Count;

			region_population = new_population;
			// Debug.Log("added " + region_places_scripts[i].place_npcs.Count + " population from " + region_places_scripts[i].place_name);
		}

		// Debug.Log("Population update : now " + region_population);

		Debug.Log("<b>= = = REFRESH REGION SUCCESS = = =</b>");
	}

	public struct_event_indexXowner SelectEvent()
	{
		// Debug.Log("<b>= = = BEGINNING SELECT EVENT METHOD = = = </b>");
		List<struct_event_indexXowner> possibleEvents = new List<struct_event_indexXowner>();			// list containing the events which conditions are valid and which could be selected by the method. It keep these event under a struct linking the event with the place which contain it's instance

		// check every place of the region
		// Debug.Log(region_places_scripts.Count + " places found");
		for (int place = 0; place < region_places_scripts.Count; place++)		
		{
			scr_place actualPlace = region_places_scripts[place];
			// Debug.Log("Checking place " + place + " (" + actualPlace.place_name + ")");
			
			// check every  [place_event] of actual place (not linked to any faction)
			// Debug.Log( actualPlace.place_events.Count + " events found");
			for (int evnt = 0; evnt < region_places_scripts[place].place_events.Count; evnt++)
			{
				int actualEvent;
				actualEvent = actualPlace.place_events[evnt];	// recuperate index of actual event
				// Debug.Log("Checking event " + evnt + " (index:" + actualEvent + ")");

				bool isValid = false;
				isValid = EventManager.instance.event_Dict[actualEvent].ConditionCheck(actualPlace);	// test the conditions of the actual event (with his method "ConditionCheck which returns a bool)
				// Debug.Log("Condition check returns " + isValid);

				if (isValid == true)		// if the check return true, create an event:index/owner struct to contain the information and it's origin place, and add it to the list of possible events (in which we will draw 1 event), else do nothing
				{
					struct_event_indexXowner new_event_struct = new struct_event_indexXowner(){event_index = actualEvent, event_place = actualPlace};		// no event_faction specified here, so it will be the default null index (-1)
					possibleEvents.Add(new_event_struct);
					// Debug.Log("New event structure : index:" + new_event_struct.event_index + "; place:" + new_event_struct.event_place + " succefully added to the list of possible events");
				}
			}

			// check every faction of actual place
			// Debug.Log(region_places_scripts[place].place_faction_list.Count + " factions found");
			for (int faction = 0; faction < region_places_scripts[place].place_faction_list.Count; faction++)
			{
				strct_local_faction actualFaction;
				actualFaction = region_places_scripts[place].place_faction_list[faction];
				// Debug.LogWarning("Checking faction " + faction + " (" + actualFaction.faction.faction_name + ")");

				// check every  [faction_event] of actual faction (linked to the faction)
				// Debug.Log( actualFaction.faction.faction_events.Count + " events found");
				for (int evnt = 0; evnt < actualFaction.faction.faction_events.Count; evnt++)
				{
					int actualEvent;
					actualEvent = actualFaction.faction.faction_events[evnt];	// recuperate index of actual event
					// Debug.Log("Checking event " + evnt + " (index:" + actualEvent + ")");

					bool isValid = false;
					isValid = EventManager.instance.event_Dict[actualEvent].ConditionCheck(actualPlace, actualFaction);	// test the conditions of the actual event (with his method "ConditionCheck which returns a bool)
					// Debug.Log("Condition check returns " + isValid);

					if (isValid == true)		// if the check return true, create an event:index/owner struct to contain the information and it's origin place, and add it to the list of possible events (in which we will draw 1 event), else do nothing
					{
						struct_event_indexXowner new_event_struct = new struct_event_indexXowner(){event_index = actualEvent, event_place = actualPlace, event_faction = actualFaction};		// we specify the faction from which the event is launched
						possibleEvents.Add(new_event_struct);
						// Debug.Log("New event structure : index:" + new_event_struct.event_index + "; place:" + new_event_struct.event_place + "; faction:" + new_event_struct.event_faction.faction_name + " succefully added to the list of possible events");
					}
				}
			}
		}

		// Random choice of the event among the possible event list
		struct_event_indexXowner selectedEvent = new struct_event_indexXowner();
		// Debug.Log("random draw of 1 event among " + possibleEvents.Count);
		selectedEvent = RandomEvent(possibleEvents);

		// End of the method
		// Debug.Log("<b><i>End of Event Selection Method with following result : index:" + selectedEvent.event_index + "</i></b>");
		return selectedEvent;
	}

// = RandomEvent pick Method

	public struct_event_indexXowner RandomEvent(List<struct_event_indexXowner> random_list)
	{
		
		// int index               = 0;                // define which element is actually tested by randomChecker
        int randomChecker       = 0;                // manage the addition of the "chance" value of each type, which allow to check within which element range the randomNumber is.
        int randomNumber        = 0;                // stock RNG number
        int totalRandomValue    = 0;                // addition of every ChancesPerType value
        struct_event_indexXowner picked_eventStrct = new struct_event_indexXowner();

        // Total Random Value calculation loop
        for (int i = 0; i < random_list.Count; i++)
        {
			struct_event_indexXowner actualEventStrct = random_list[i];												// actual event structure being checked
			cl_event targetedEvent_Reference = EventManager.instance.event_Dict[actualEventStrct.event_index];		// recuperate the correct event in the Event Manager from the index of the actual event checked
            totalRandomValue += targetedEvent_Reference.DefineProbability(actualEventStrct.event_place);											// add the probability value from the checked event to the random calculator
            // Debug.Log("actual total random " + totalRandomValue);
        }
        // Debug.Log("FINAL TOTAL RANDOM " + totalRandomValue);

        // Random Number Draw
        randomNumber = Random.Range(1, totalRandomValue);
        // Debug.Log("RANDOM NUMBER : " + randomNumber);

        // Random value Index Checker
        for (int i = 0; i < random_list.Count; i++)
        {
			struct_event_indexXowner actualEventStrct = random_list[i];												// actual event structure being checked
			cl_event targetedEvent_Reference = EventManager.instance.event_Dict[actualEventStrct.event_index];		// recuperates the correct event in the Event Manager from the index of the actual event checked
			int actualEvent_probability = targetedEvent_Reference.DefineProbability(actualEventStrct.event_place) ;								// calculs and stores the event's probability from his DefineProbability method

            // Debug.Log("New index : " + i);
            // Debug.Log("Actual range check : index" + i + " min" + (randomChecker+1) + " max" + (randomChecker + actualEvent_probability) );
            
            if (randomNumber > randomChecker && randomNumber <= randomChecker + actualEvent_probability)
            {
                // Debug.Log("RANDOM NUMBER FOUND IN INDEX " + i);
                picked_eventStrct = actualEventStrct;
                break;
            } else {
                randomChecker += actualEvent_probability;
                // index += 1;
                // Debug.Log("Random number not found in index " + (i) + ".");

                // Error report if random number isn't found even in the last Check Range
                if (i < random_list.Count -1 == false)
                {
                    Debug.LogError("ERROR !!! RANDOM NOT FOUND IN ANY RANGE CHECK !!! OUTPUT SET TO -1!");
                    // index = -1;
                    picked_eventStrct.event_index = -1;
                }
            }
		}

		// Test success
		if (picked_eventStrct.event_index != -1)
		{
			// Debug.Log("<i>Random draw ended with following result : index:" + picked_eventStrct.event_index + "</i>");
		}
		else 
		{
			Debug.LogError("<b>RANDOM EVENT DRAW METHOD FAILED !!! PICKED EVENT IS NULL</b>");
		}
    
	// Return Index
	return picked_eventStrct;
	}

	private void ResetEventTimer()
	{
		// Reset next event launching timer
		timeBeforeNextEvent = Random.Range(10, 30);

		Debug.Log("Next Event Timer reset");
		return;
	}

}
