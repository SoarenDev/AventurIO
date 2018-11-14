using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
public class quest_0001 : cl_quest {

/// = = = [ CONSTRUCTORS ] = = =

    public quest_0001(cl_npc selected_quest_owner, scr_place origin_place)
    {
        quest_id                    = 1;

        quest_target                = "";

        quest_icon_index            = 0;
        quest_name                  = "Cleaning time!";
        quest_description           = 
            "Some pests recently invaded the house of a inhabitant of " + origin_place.place_name + ". He will reward you if you get rid of them.";
        ;
        quest_goal_text             = "Kill all the pests in " + origin_place.place_name;
    
        quest_reward_experience     = 9;
        quest_reward_gold           = 120;
        quest_reward_reputation     = 2;

        linked_place                = origin_place;
        quest_owner                 = selected_quest_owner;

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

    public override void    OnAcceptQuest()
    {
        base.OnAcceptQuest();
    }

    public override void    CompleteQuest(int quest_outcome)   // give quest rewards, then launch linked EventOutcome
    {
    // base method
        base.CompleteQuest(0);

        return;
    }

    public override void	Quest_CompletionCheck()
	{
		if (Input.GetKey(KeyCode.M)) { CompleteQuest(0); }
	}

}
