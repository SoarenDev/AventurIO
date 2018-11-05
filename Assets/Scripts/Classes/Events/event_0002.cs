using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
public class event_0002 : cl_event {

// = = = [ CONSTRUCTORS ] = = =

    public event_0002()
    {
        event_id                    = 2;
        isPersistant                = false;        // a persistant event will instantiate himself and stay active as a modificator untill another event suppress it

        event_probability           = 40;

        event_icon_index            = 0;
        event_name                  = "Npc suddent death";
        event_description           = 
        "Kill a npc no matter how old he is."
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
        cl_event instance = new event_0002();    // create an instance of himself into the region's ongoing events

        base.InstantiateEvent(targeted_place);
		return instance;		// return the reference of the instance created
    }

    public override bool ConditionCheck(scr_place targeted_place)			// function that will be overrided in child class. Check given condition for the event, and return TRUE if they are all true. A return TRUE allow the event to be selected when an event is drawn by the EventManager
	{
        // check number of npcs in the place
        if (targeted_place.place_npcs.Count <= targeted_place.type.min_npc)
        {
            return false;
        }

        // check if at least 1 npc isn't "locked"
        foreach (var npc in targeted_place.place_npcs)
        {
            if (npc.isLocked == false)
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
        List<cl_npc> targetable_npc_list = new List<cl_npc>();
        cl_npc       target;

        // list all "not locked" npcs
        foreach (var npc in targeted_place.place_npcs)
        {
            if (npc.isLocked == false) { targetable_npc_list.Add(npc); }
        }

        // draw target from list
        target = targetable_npc_list[Random.Range(0, targetable_npc_list.Count)];

        // Clear npc
        GameManager.instance.ClearNpc(target, targeted_place);

        Debug.Log("A npc died baldly in " + targeted_place.place_name);
        return;
	}
}
