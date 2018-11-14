using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
public class event_0404 : cl_event {

// = = = [ CONSTRUCTORS ] = = =

    public event_0404()
    {
        event_id                    = 404;
        isPersistant                = false;        // a persistant event will instantiate himself and stay active as a modificator untill another event suppress it

        event_probability           = 120;

        event_icon_index            = 0;
        event_name                  = "Agression in the place";
        event_description           = 
        "An inhabitant of the place is attacked by members of a hostile faction from the same place."
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

    private     cl_npc      agressor;

// = = =

    public override cl_event	InstantiateEvent(scr_place targeted_place)
	{
        cl_event instance = new event_0404();    // create an instance of himself into the region's ongoing events

        base.InstantiateEvent(targeted_place);
		return instance;		// return the reference of the instance created
    }

    public override bool ConditionCheck(scr_place targeted_place, strct_local_faction targeted_faction)
    {
        // check if place have at least one npc who isn't member of the event's origin faction and have a npc member of this faction
        List<cl_npc> npc_check_list = new List<cl_npc>();

        // = = FIRST CONDITION = =
        foreach (var npc in targeted_place.place_npcs)
        {
            if (npc.isLocked == false && npc.npc_faction.local_faction != targeted_faction)     // <<< THIS IS A PLACE EVENT, TARGETED FACTION IS EMPTY !!!!!!!!!!!!!
            {
                npc_check_list.Add(npc);
            }        
        }

        // if check list is empty, condition isn't fulfilled
        if (npc_check_list.Count == 0) { return false; }

        // = =

        // = = SECOND CONDITION = =
        npc_check_list = new List<cl_npc>();  // reset check list
        foreach (var npc in targeted_place.place_npcs)
        {
            if (npc.isLocked == false && npc.npc_faction.local_faction == targeted_faction)
            {
                npc_check_list.Add(npc);
            }        
        }

        // if check list is empty, condition isn't fulfilled
        if (npc_check_list.Count == 0) { return false; }

        // = =
        
        // if method arrives here, conditions have been fulfilled
        return true;
    }

    // CONDITION: NPC isn't locked && NPC is not in the same faction as the agressor
    public override cl_npc SelectQuestOwner(scr_place targeted_place)
    {
        List<cl_npc> matching_npcs = new List<cl_npc>();
		cl_npc selected_npc;

		// list all npcs matching the condition
		foreach (var npc in targeted_place.place_npcs)
		{
			if (npc.isLocked == false && npc.npc_faction.local_faction != agressor.npc_faction.local_faction) { matching_npcs.Add(npc); }
		}

		// draw random npc from matching list
		selected_npc = matching_npcs[Random.Range(0, matching_npcs.Count)];

		return selected_npc;
    }
	
    public override void LaunchEvent(scr_place targeted_place, strct_local_faction targeted_faction)
    {
        // assign agressor
        agressor = DrawAgressor(targeted_place, targeted_faction);
        targeted_faction = agressor.npc_faction.local_faction;
    
    // base method
        base.LaunchEvent(targeted_place, targeted_faction);

    // child method

        cl_npc quest_owner = SelectQuestOwner(targeted_place);
        
        // CREATE QUEST to linked_place
        quest_owner.npc_quests.Add(new quest_0004(quest_owner, event_origin_place, agressor));

		Debug.Log("An agression had happened in " + targeted_place);
        return;
    }

    cl_npc DrawAgressor(scr_place targeted_place, strct_local_faction targeted_faction)
    {
        List<cl_npc> targetable_npcs_list = new List<cl_npc>();
        cl_npc target_npc;

        // list all targatable npcs
        foreach (var npc in targeted_place.place_npcs)
        {
            if (npc.isLocked == false && npc.npc_faction.local_faction == targeted_faction)
            {
                targetable_npcs_list.Add(npc);
            }
        }

        // draw target targetable list
        target_npc = targetable_npcs_list[Random.Range(0, targetable_npcs_list.Count)];

        return target_npc;
    }

}
