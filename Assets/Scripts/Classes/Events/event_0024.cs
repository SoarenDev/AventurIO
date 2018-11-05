using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
public class event_0024 : cl_event {

// = = = [ CONSTRUCTORS ] = = =

    public event_0024()
    {
        event_id                    = 24;
        isPersistant                = false;        // a persistant event will instantiate himself and stay active as a modificator untill another event suppress it

        event_probability           = 160;

        event_icon_index            = 0;
        event_name                  = "Faction takes control of the place";
        event_description           = 
        "A faction with more influence that the actual main faction of the place take the control of it."
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

// = = =

    public override cl_event	InstantiateEvent(scr_place targeted_place)
	{
        cl_event instance = new event_0024();    // create an instance of himself into the region's ongoing events

        base.InstantiateEvent(targeted_place);
		return instance;		// return the reference of the instance created
    }

    public override bool ConditionCheck(scr_place targeted_place)
    {
        // place must not be locked (affects place main faction)
        if ( targeted_place.isLocked )
        {
            return false;
        }

        // place must have a faction with more influence than its main faction
        foreach (var faction_struct in targeted_place.place_faction_list)
        {
            if (faction_struct.influence > targeted_place.place_main_faction.influence)
            {
                return true;
            }
        }

        return false;
    }

	
    public override void LaunchEvent(scr_place targeted_place, strct_local_faction targeted_faction)
    {
    // base method
        base.LaunchEvent(targeted_place, targeted_faction);

    // child method
        targeted_place.UpdateMainFaction();

        Debug.Log(targeted_place.place_main_faction.faction.faction_name + " has taken control of " + targeted_place.place_name);
        return;

    }

}