using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
public class quest_0002 : cl_quest {

/// = = = [ CONSTRUCTORS ] = = =

    public quest_0002(scr_place origin_place, scr_place enemy_place)
    {
        quest_id                    = 2;

        quest_target                = "";

        quest_icon_index            = 0;
        quest_name                  = "Reprisals";
        quest_description           = 
            origin_place.place_name + " was recently attacked by the hostile faction controlling " + enemy_place.place_name + ". They will reward the one who avenge them.";
        ;
        quest_goal_text             = "Kill the chief of the hostiles in " + enemy_place.place_name;
    
        quest_reward_experience     = 23;
        quest_reward_gold           = 180;
        quest_reward_reputation     = 3;

        linked_place                = origin_place;

        // Quest goals
        quest_hostile_home_place    = enemy_place;

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

    public  scr_place           quest_hostile_home_place            ;

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
