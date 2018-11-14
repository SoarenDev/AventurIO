using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public class event_1000 : cl_event_persistant {

    public event_1000()
    {
        event_id                    = 1000;
        isPersistant                = true;        // a persistant event will instantiate himself and stay active as a modificator untill another event suppress it

        event_probability           = 30;

        event_icon_index            = 0;
        event_name                  = "Faction test event";
        event_description           = 
        "This faction have been triggered by the test event! BEHOLD!"
        ;

        event_origin_place          = null;
        event_origin_faction        = null;

        is_outcome_timer_active		= true;
        persistant_outcome_timer	= 30f;
        outcome_timer_countdown     = persistant_outcome_timer;

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

    public override cl_event	InstantiateEvent(scr_place targeted_place)
	{
        cl_event instance = new event_1000();    // create an instance of himself into the region's ongoing events

        base.InstantiateEvent(targeted_place);
		return instance;		// return the reference of the instance created
    }

    public override void LaunchEvent(scr_place targeted_place, strct_local_faction targeted_faction)
    {
    // base method
        base.LaunchEvent(targeted_place, targeted_faction);

    // child method

        cl_npc quest_owner = SelectQuestOwner(targeted_place);

        // CREATE QUEST to linked_place
        quest_1000 new_quest = new quest_1000(quest_owner, this);
        targeted_place.place_quests.Add(new_quest);

        // add to event linked quests list
        linked_quests_list.Add(new_quest);

		Debug.Log("Quest added to " + targeted_place);

    }

    public override void OutcomeEvent(int event_outcome)
    {
        switch (event_outcome)
        {
            case 0: 
                Debug.Log("LÉ GANTI Y ON GAGNÉ");
            break;

            case 1: 
                Debug.Log("LÉ MÉCHAN Y ON GAGNÉ");
            break;

            default: Debug.LogError("Unknown outcome!"); break;
        }

        base.OutcomeEvent(event_outcome);
    }

    public override void EndEvent()
    {  
        base.EndEvent();
    }

}
