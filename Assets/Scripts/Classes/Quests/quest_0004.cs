using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
public class quest_0004 : cl_quest {

    public quest_0004(cl_npc selected_quest_owner, scr_place origin_place, cl_npc agressor)
    {
        quest_id                    = 4;

        quest_target                = "";

        quest_icon_index            = 0;
        quest_name                  = "Law of retaliation";
        quest_description           =
            "An inhabitant of " + origin_place.place_name + " has been agressed in the street by a member of " + agressor.npc_faction.local_faction.faction.faction_name + " and seeks a deadly revenge."
        ;
        quest_goal_text             = "Kill " + agressor.npc_firstname + " " + agressor.npc_lastname + " in " + origin_place.place_name ;

        quest_reward_experience     = 18;    
        quest_reward_gold           = 150;
        quest_reward_reputation     = 3;

        linked_place                = origin_place;
        quest_owner                 = selected_quest_owner;

        // Quest goals
        quest_agressor    = agressor;

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

    public  cl_npc           quest_agressor            ;

// = = =

    public override void    OnAcceptQuest()
    {
        base.OnAcceptQuest();
    }

    public override void    CompleteQuest(int quest_outcome)   // give quest rewards, then launch linked EventOutcome
    {
        int event_outcome = 0;

        switch (quest_outcome)
        {
            case 0: 
                Debug.Log("OUTCOME 0");
                event_outcome = 0;
            break;

            default: Debug.LogError("Unknown outcome!"); break;
        }

        base.CompleteQuest(event_outcome);
    }

    public override void	Quest_CompletionCheck()
	{
		if (Input.GetKey(KeyCode.M)) { CompleteQuest(0); }
	}

}
