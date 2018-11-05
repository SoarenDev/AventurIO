using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
public class cl_quest_persistant : cl_quest {

// = = =

    /// <summary>
 	/// Called when the player accept the quest and register it in his active quests.
 	/// </summary>
    public override void    OnAcceptQuest()
    {
        base.OnAcceptQuest();
    }

    /// <summary>
 	/// PERSISTANT QUEST VERSION. Called when the quest end. Launches the Outcome() of the linked event, passing it a given outcome.
 	/// </summary>
    public override void    CompleteQuest(int event_outcome)   // give quest rewards, then launch linked EventOutcome
    {
    // overrided method

        // Transmit Outcome to the linked_event
        linked_event.OutcomeEvent(event_outcome);

    // base method
        base.CompleteQuest(event_outcome);

        Debug.Log("<b>Quest " + quest_name + " completed! </b>");
        return;
    }

}
