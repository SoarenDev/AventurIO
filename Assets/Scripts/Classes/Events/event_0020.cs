using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
public class event_0020 : cl_event {

// = = = [ CONSTRUCTORS ] = = =

    public event_0020()
    {
        event_id                    = 20;
        isPersistant                = false;        // a persistant event will instantiate himself and stay active as a modificator untill another event suppress it

        event_probability           = 80;

        event_icon_index            = 0;
        event_name                  = "Faction apparition";
        event_description           = 
        "Create a new faction in the place"
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
        cl_event instance = new event_0020();    // create an instance of himself into the region's ongoing events

        base.InstantiateEvent(targeted_place);
		return instance;		// return the reference of the instance created
    }

    public override bool ConditionCheck(scr_place targeted_place)
    {
        // check number of factions in the place
        if ( targeted_place.place_faction_list.Count >= Mathf.Floor((targeted_place.place_npcs.Count)/3) )
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

        // if check method arrives here without return, return:
        return false;
    }
	
    public override void LaunchEvent(scr_place targeted_place, strct_local_faction targeted_faction)
    {
    // base method
        base.LaunchEvent(targeted_place, targeted_faction);

    // child method
        List<cl_npc> targetable_npc_list = new List<cl_npc>();
        cl_npc       target;
        cl_faction   new_faction = new cl_faction();

        // = = FACTION = =
		// generate
		new_faction.GenerateFaction(targeted_place, targeted_place.type.faction_genList_default);

		// define base influence
		int default_influence = 5;

		// convert faction to faction_info struct
		strct_local_faction new_faction_strct = new strct_local_faction(){faction = new_faction, influence = default_influence};

		// add to place list ; binding
		targeted_place.place_faction_list.Add(new_faction_strct);
		new_faction.faction_places.Add(targeted_place);		// add this place to the faction's places list

        // = = NPC = =
        // list all targatable npcs
        foreach (var npc in targeted_place.place_npcs)
        {
            if (npc.isLocked == false && npc.npc_faction.local_faction == null) { targetable_npc_list.Add(npc); }
        }

        // draw target from list
        target = targetable_npc_list[Random.Range(0, targetable_npc_list.Count)];
        Debug.Log("target id: " + target.npc_id);

        // Recruit first npc (leader)
        targeted_place.RecruitNpcInFaction(new_faction_strct, target);

        // = = FACTION = =
        // Define Leader
        new_faction_strct.faction.DefineLeader();

        // generate name
        new_faction_strct.faction.GenerateName();

        // set the new faction as place's main faction if it's the first faction of the place
        if (targeted_place.place_faction_list.Count == 1) { new_faction_strct = targeted_place.place_main_faction; }

        Debug.Log("new faction: " + new_faction_strct.faction.faction_name + " has been formed in " + targeted_place.place_name);
        return;
    }

}
