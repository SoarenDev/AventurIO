using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
public class quest_0000 : cl_quest {

    public quest_0000()
    {
        quest_id                    = 1;

        quest_target                = "";

        quest_icon_index            = 0;
        quest_name                  = "TEST QUEST SIMPLE";
        quest_description           = 
            "Olala quete simple non persistante"
        ;
        quest_goal_text             = "olala pas grand chose";
    
        quest_reward_gold           = 12;
        quest_reward_experience     = 15;
        quest_reward_reputation     = 3;


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

            default: Debug.LogError("Unknown outcome!"); break;
        }

        base.CompleteQuest(event_outcome);
    }

    public override void	Quest_CompletionCheck()
	{
		if (Input.GetKey(KeyCode.M)) { CompleteQuest(0); }
	}

}
