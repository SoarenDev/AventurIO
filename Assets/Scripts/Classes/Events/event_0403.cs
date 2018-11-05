using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
public class event_0403 : cl_event {

// = = = [ CONSTRUCTORS ] = = =

    public event_0403()
    {
        event_id                    = 403;
        isPersistant                = false;        // a persistant event will instantiate himself and stay active as a modificator untill another event suppress it

        event_probability           = 80;

        event_icon_index            = 0;
        event_name                  = "Object lost in a place";
        event_description           = 
        "A inhabitant of the place lose one of his objects in another place."
        ;

        event_origin_place          = null;
        event_origin_faction        = null;

        //$param+1
        //$param+2
        //$param+3
        //$param+4
        //$param+5
        //$param+6
        //$param+7
        //$param+8
        //$param+9
        //$param+10
    }

// = = =

// = = = [ VARIABLES DEFINITION ] = = =

    public  scr_place   other_place         ;

// = = =

    public override cl_event	InstantiateEvent(scr_place targeted_place)
	{
        cl_event instance = new event_0403();    // create an instance of himself into the region's ongoing events

        base.InstantiateEvent(targeted_place);
		return instance;		// return the reference of the instance created
    }

	
    public override void LaunchEvent(scr_place targeted_place, strct_local_faction targeted_faction)
    {
    // child method: SECOND PLACE TARGET

        // = = DRAW other_place; no condition = =
        scr_place target_place;

        // draw target DIRECTLY from region's place_list
        target_place = targeted_place.linked_region.region_places_scripts[Random.Range(0, targeted_place.linked_region.region_places_scripts.Count)];

        // assign target
        other_place = target_place;

        // = =

    // base method
        base.LaunchEvent(targeted_place, targeted_faction);

        // CREATE QUEST to linked_place
        targeted_place.place_quests.Add(new quest_0003(event_origin_place, other_place));
		Debug.Log("Quest added to " + targeted_place);

        return;
    }

}
