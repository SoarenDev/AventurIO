using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
public class quest_0003 : cl_quest {

/// = = = [ CONSTRUCTORS ] = = =

    public quest_0003(cl_npc selected_quest_owner, scr_place origin_place, scr_place other_place)
    {
        quest_id                    = 3;

        quest_target                = "";

        quest_icon_index            = 0;
        quest_name                  = "A normal lost object";
        quest_description           = 
            "An inhabitant of " + origin_place.place_name + " has lost his percious *object* while walking in " + other_place.place_name + ". He will reward the one who gets him back his *object*.";
        ;
        quest_goal_text             = "Find the *object* in " + other_place.place_name + " and bring it back to his owner in " + origin_place.place_name;
    
        quest_reward_experience     = 8;
        quest_reward_gold           = 120;
        quest_reward_reputation     = 2;

        linked_place                = origin_place;
        quest_owner                 = selected_quest_owner;

        // Quest goals
        quest_other_place    = other_place;

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

    public  scr_place           quest_other_place            ;

// = = =

    public override void    OnAcceptQuest()
    {
        base.OnAcceptQuest();

        return;
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

