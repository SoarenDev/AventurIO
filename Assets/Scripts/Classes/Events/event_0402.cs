using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
public class event_0402 : cl_event {

// = = = [ CONSTRUCTORS ] = = =

    public event_0402()
    {
        event_id                    = 402;
        isPersistant                = false;        // a persistant event will instantiate himself and stay active as a modificator untill another event suppress it

        event_probability           = 100;

        event_icon_index            = 0;
        event_name                  = "Place attacked by hostiles";
        event_description           = 
        "A hostile faction attacks the place. Creates a revenge quest."
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

    public  scr_place   other_place         ;

// = = =

    public override cl_event	InstantiateEvent(scr_place targeted_place)
	{
        cl_event instance = new event_0402();    // create an instance of himself into the region's ongoing events

        base.InstantiateEvent(targeted_place);
		return instance;		// return the reference of the instance created
    }

    public override bool ConditionCheck(scr_place targeted_place)
    {
        // check if region have AT LEAST ONE place with hostile faction as main faction
        foreach (var place in targeted_place.linked_region.region_places_scripts)
        {
            if (place.place_main_faction.faction.faction_id != -1)
            {
                if (place.place_main_faction.faction.faction_type.type == FactionTypeEnum.hostile && place.isLocked == false)
                {
                    return true;
                }
            }
            
        }

        // else return false
        return false;
    }

    public override void LaunchEvent(scr_place targeted_place, strct_local_faction targeted_faction)
    {
    // child method: SECOND PLACE TARGET

        // = = DRAW other_place = =
        List<scr_place> targetable_place_list = new List<scr_place>();
        scr_place target_place;

        // list all targetable places
        foreach (var place in targeted_place.linked_region.region_places_scripts)
        {
            if (place.place_main_faction.faction.faction_id != -1)
            {
                if (place.place_main_faction.faction.faction_type.type == FactionTypeEnum.hostile && place.isLocked == false)
                {
                    targetable_place_list.Add(place);
                }
            }
        }

        // draw target from list
        target_place = targetable_place_list[Random.Range(0, targetable_place_list.Count)];

        // assign target
        other_place = target_place;

        // = =

    // base method
        base.LaunchEvent(targeted_place, targeted_faction);

    // child method
        cl_npc quest_owner = SelectQuestOwner(targeted_place);

        // CREATE QUEST to linked_place
        quest_owner.npc_quests.Add(new quest_0002(quest_owner, event_origin_place, other_place));
		Debug.Log("Quest added to " + targeted_place);

        return;
    }

}
