using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
public class event_0001 : cl_event {

// = = = [ CONSTRUCTORS ] = = =

    public event_0001()
    {
        event_id                    = 1;
        isPersistant                = false;        // a persistant event will instantiate himself and stay active as a modificator untill another event suppress it

        event_probability           = 200;

        event_icon_index            = 0;
        event_name                  = "Npc apparition";
        event_description           = 
        "Create a new npc in the origin place."
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

    public override cl_event InstantiateEvent(scr_place targeted_place)
	{
        cl_event instance = new event_0001();    // create an instance of himself into the region's ongoing events

        base.InstantiateEvent(targeted_place);
		return instance;		// return the reference of the instance created
    }

    public override int DefineProbability(scr_place targeted_place)
    {
        // Returns base probability if there less than *place_type generation max npc* in the place
        if (targeted_place.place_npcs.Count < targeted_place.type.max_npc) 
            { return event_probability; }
        else 
            { return event_probability / 5; }
        
    }

	public override void LaunchEvent(scr_place targeted_place, strct_local_faction targeted_faction)
	{
    // base method
        base.LaunchEvent(targeted_place, targeted_faction);

    // child method
        // generate new npc
        cl_npc new_npc;
		new_npc = GameManager.instance.GenerateNpc();
        GameManager.instance.NPCGenerateName(new_npc);

		// add to place list
		targeted_place.place_npcs.Add(new_npc);		// add the npc the place npc_list

        Debug.Log("A new npc has arrived in " + targeted_place.place_name);
        return;
	}
}
