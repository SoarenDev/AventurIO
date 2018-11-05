using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
public class event_0021 : cl_event {

// = = = [ CONSTRUCTORS ] = = =

    public event_0021()
    {
        event_id                    = 21;
        isPersistant                = false;        // a persistant event will instantiate himself and stay active as a modificator untill another event suppress it

        event_probability           = 80;

        event_icon_index            = 0;
        event_name                  = "Faction disparition";
        event_description           = 
        "Remove one of the place's factions with low influence"
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
        cl_event instance = new event_0021();    // create an instance of himself into the region's ongoing events

        base.InstantiateEvent(targeted_place);
		return instance;		// return the reference of the instance created
    }

    public override bool ConditionCheck(scr_place targeted_place)
    {
        // check number of factions in the place
        if ( targeted_place.place_faction_list.Count <= 2 )
        {
            return false;
        }

        // check if place has a faction with less than 5 influence and which is not "locked"
        foreach (var faction_struct in targeted_place.place_faction_list)
        {
            if (faction_struct.faction.isLocked == false && faction_struct.influence < 5)
            {
                return true;
            }
        }

        return false;
    }
	
    public override void LaunchEvent(scr_place targeted_place, strct_local_faction targeted_faction)     // NOTE: The faction stay referenced in DataManager, event if it have no linked place left!
    {
    // base method
        base.LaunchEvent(targeted_place, targeted_faction);

    // child method
        List<strct_local_faction> targetable_faction_structs    = new List<strct_local_faction>();
        strct_local_faction       target_faction_struct        ;

        // list all targatable factions
        foreach (var faction_struct in targeted_place.place_faction_list)
        {
            if (faction_struct.faction.isLocked == false && faction_struct.influence < 5)
            {
                targetable_faction_structs.Add(faction_struct);
            }
        }

        // draw one faction from targatable list
        target_faction_struct = targetable_faction_structs[Random.Range(0, targetable_faction_structs.Count)];

        // clear faction reference from place npcs
        foreach (var npc in targeted_place.place_npcs)
        {
            if (npc.npc_faction.local_faction.faction == target_faction_struct.faction) { npc.ClearFaction(); }
        }

        // clear faction from place
        targeted_place.place_faction_list.Remove(target_faction_struct);

        // update place main faction if removed faction was his main faction
        if (targeted_place.place_main_faction.faction == target_faction_struct.faction)
        {
            targeted_place.UpdateMainFaction();
        }

        // clear place from faction_places list
        target_faction_struct.faction.faction_places.Remove(targeted_place);

        Debug.Log("Due to his too low influence, " + target_faction_struct.faction.faction_name + " has dissolved in " + targeted_place.place_name + "...");
        return;
    }

}
