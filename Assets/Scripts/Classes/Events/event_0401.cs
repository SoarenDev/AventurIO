using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
public class event_0401 : cl_event {

// = = = [ CONSTRUCTORS ] = = =

    public event_0401()
    {
        event_id                    = 401;
        isPersistant                = false;        // a persistant event will instantiate himself and stay active as a modificator untill another event suppress it

        event_probability           = 140;

        event_icon_index            = 0;
        event_name                  = "Pests invasion";
        event_description           = 
        "Pests invade the place. Creates quest_0001."
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
        cl_event instance = new event_0401();    // create an instance of himself into the region's ongoing events

        base.InstantiateEvent(targeted_place);
		return instance;		// return the reference of the instance created
    }

    public override bool ConditionCheck(scr_place targeted_place)
    {
        // no conditions: always true
        return true;
    }
	
    public override void LaunchEvent(scr_place targeted_place, strct_local_faction targeted_faction)
    {
    // base method
        base.LaunchEvent(targeted_place, targeted_faction);

    // child method
        cl_npc quest_owner = SelectQuestOwner(targeted_place);

        // CREATE QUEST in linked_place
        quest_owner.npc_quests.Add(new quest_0001(quest_owner, event_origin_place));
		Debug.Log("Quest added to " + targeted_place);
        
        return;
    }

}
