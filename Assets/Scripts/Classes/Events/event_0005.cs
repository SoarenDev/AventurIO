using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
public class event_0005 : cl_event {

// = = = [ CONSTRUCTORS ] = = =

    public event_0005()
    {
        event_id                    = 5;
        isPersistant                = false;        // a persistant event will instantiate himself and stay active as a modificator untill another event suppress it

        event_probability           = 120;

        event_icon_index            = 0;
        event_name                  = "Npc joins faction";
        event_description           = 
        "Make a npc without faction joining one and give it some influence"
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
        cl_event instance = new event_0005();    // create an instance of himself into the region's ongoing events

        base.InstantiateEvent(targeted_place);
		return instance;		// return the reference of the instance created
    }

    public override bool ConditionCheck(scr_place targeted_place)			// function that will be overrided in child class. Check given condition for the event, and return TRUE if they are all true. A return TRUE allow the event to be selected when an event is drawn by the EventManager
	{
        // place must have at least one faction
        if (targeted_place.place_faction_list.Count == 0)
        {
            return false;
        }

        // check if at least 1 npc: isn't "locked" and has no faction
        foreach (var npc in targeted_place.place_npcs)
        {
            if (npc.isLocked == false && npc.npc_faction.local_faction == null)
            {
                return true;
            }
        }

        // if method arrives here without having return true, then conditions aren't fulfilled
		return false;
	}

	
    public override void LaunchEvent(scr_place targeted_place, strct_local_faction targeted_faction)
    {
    // base method
        base.LaunchEvent(targeted_place, targeted_faction);

    // child method
        List<cl_npc>            targetable_npc_list = new List<cl_npc>();
        cl_npc                  target;
        strct_local_faction    faction_to_join;

        // list all targatable npcs
        foreach (var npc in targeted_place.place_npcs)
        {
            if (npc.isLocked == false && npc.npc_faction.local_faction == null) { targetable_npc_list.Add(npc); }
        }

        // draw target from list
        target = targetable_npc_list[Random.Range(0, targetable_npc_list.Count)];
        Debug.Log("target id: " + target.npc_id);

        // draw faction to join
        faction_to_join = targeted_place.place_faction_list[Random.Range(0, targeted_place.place_faction_list.Count)];

        // launch recruit to faction method
        targeted_place.RecruitNpcInFaction(faction_to_join, target);

        // gives influence to the joined faction
        faction_to_join.influence += Random.Range(1,4);  //[1,3]

        Debug.Log(target.npc_firstname + " " + target.npc_lastname + " of " + targeted_place.place_name + " has joined " + faction_to_join.faction.faction_name);
        return;
    }

}
