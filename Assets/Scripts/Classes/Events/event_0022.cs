using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
public class event_0022 : cl_event {

// = = = [ CONSTRUCTORS ] = = =

    public event_0022()
    {
        event_id                    = 22;
        isPersistant                = false;        // a persistant event will instantiate himself and stay active as a modificator untill another event suppress it

        event_probability           = 140;

        event_icon_index            = 0;
        event_name                  = "Faction gains influence";
        event_description           = 
        "Add some influence to one of the place's factions"
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

    private  int         influence_gain_min          = 1;
    private  int         influence_gain_max          = 3;

// = = =

    public override cl_event	InstantiateEvent(scr_place targeted_place)
	{
        cl_event instance = new event_0022();    // create an instance of himself into the region's ongoing events

        base.InstantiateEvent(targeted_place);
		return instance;		// return the reference of the instance created
    }


    public override bool ConditionCheck(scr_place targeted_place)
    {
        // place must have at least one faction
        if (targeted_place.place_faction_list.Count == 0)
        {
            return false;
        }

        return true;
    }

	
    public override void LaunchEvent(scr_place targeted_place, strct_local_faction targeted_faction)
    {
    // base method
        base.LaunchEvent(targeted_place, targeted_faction);

    // child method
        List<strct_local_faction> targetable_faction_structs    = new List<strct_local_faction>();
        strct_local_faction       target_faction_struct        ;

        // list all targatable factions
        foreach (var faction_struct in targeted_place.place_faction_list)
        {
            targetable_faction_structs.Add(faction_struct);
        }

        // Draw one faction struct from list
        target_faction_struct = targeted_place.place_faction_list[Random.Range(0, targeted_place.place_faction_list.Count)];

        // Add random amount of influence
        int influence_gain = Random.Range(influence_gain_min, influence_gain_max+1);
        target_faction_struct.influence += influence_gain;

        Debug.Log(target_faction_struct.faction.faction_name + " has gained " + influence_gain + " influence in " + targeted_place.place_name);
        return;
    }

}
