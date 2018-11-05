using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
public class quest_1001 : cl_quest_persistant {

    public quest_1001(cl_event_persistant parent_event)
    {
        quest_id                    = 1000;
        linked_event                = parent_event;        // should alaways be NULL for non-persistant events' quests

        quest_target                = "";

        quest_icon_index            = 0;
        quest_name                  = "TEST QUEST PERSISTANT";
        quest_description           = 
            "This faction have been triggered by the test event! BEHOLD!"
        ;
        quest_goal_text             = "Persistant goal! I guess...";
    
        quest_reward_gold           = 120;
        quest_reward_experience     = 50;
        quest_reward_reputation     = 18;


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
        int event_outcome = 0;

        switch (quest_outcome)
        {
            case 0: 
                Debug.Log("OUTCOME 0");
                event_outcome = 0;
            break;
            
            case 1: 
                Debug.Log("OUTCOME 1"); 
                event_outcome = 0;
            break;

            case 2: 
                Debug.Log("OUTCOME 2");
                event_outcome = 1;
            break;

            default: Debug.LogError("Unknown outcome!"); break;
        }

        base.CompleteQuest(event_outcome);
    }

    public override void	Quest_CompletionCheck()
	{
		if (Input.GetKey(KeyCode.I)) { CompleteQuest(0); }
        if (Input.GetKey(KeyCode.O)) { CompleteQuest(1); }
        if (Input.GetKey(KeyCode.P)) { CompleteQuest(2); }
	}

}
