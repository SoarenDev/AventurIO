using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class cl_event_persistant : cl_event {

    public cl_event_persistant()
    {
        isPersistant                = true;        // a persistant event will instantiate himself and stay active as a modificator untill another event suppress it
    }

// = = = [ VARIABLES DEFINITION ] = = =

    public List<cl_quest>   linked_quests_list      = new List<cl_quest>();

// = = =

    public override cl_event	InstantiateEvent(scr_place targeted_place)
	{
        base.InstantiateEvent(targeted_place);
		return null;		// return the reference of the instance created
    }

    public virtual void	AnchorPersistant(scr_place targeted_place)
	{
		cl_event_persistant downcasted_instance = (cl_event_persistant)this;    // create an instance of himself into the region's ongoing events
		List<cl_event_persistant> target = targeted_place.linked_region.region_ongoingEvents;	// target the "ongoing event" list of the region containg the origin place of the event

		target.Add(downcasted_instance);					

		return;
    }

    public override void LaunchEvent(scr_place targeted_place, strct_local_faction targeted_faction)
    {
        base.LaunchEvent(targeted_place, targeted_faction);

        // anchor the persistant event to the place
        AnchorPersistant(targeted_place);

        // >
        event_origin_region = targeted_place.linked_region;
    }

    public virtual void OutcomeEvent(int event_outcome)
    {
        EndEvent();
    }

// = = = PERSISTANT EVENTS METHODS = = =

    public void CountdownOutcome()     // CALLED ON EVERY FRAME IN SCR_REGION
    {
        // DEBUG - décompte outcome timer
        if (is_outcome_timer_active)
        {
            if(outcome_timer_countdown <= 0)
            {
                EndEvent();
                outcome_timer_countdown = persistant_outcome_timer;     // reset countdown
            } 
            else 
            { outcome_timer_countdown -= Time.deltaTime; }
			
			// Debug.Log("cd= " + outcome_timer_countdown);
        }
    }

	public virtual void EndEvent()		// end persistant event
	{
		Debug.Log("Ending persistant event");
		DeleteAllEventQuests();
		DeleteEventInstance(event_origin_region.region_ongoingEvents);
	}

	public virtual void	DeleteEventInstance(List<cl_event_persistant> targeted_list)		// ONLY FOR PERSISTANT EVENTS !!!
	{
		targeted_list.Remove(this);
	}

// = = Quests = =

	/// <summary>
	///	For persistant events. Delete all quests linked to this event, and all their references.
	/// </summary>
	public void DeleteAllEventQuests()
	{
		foreach (var quest in linked_quests_list)
		{
			GameManager.instance.player_reference.accepted_quests.Remove(quest);
			event_origin_place.place_quests.Remove(quest);
			Debug.Log("All event quest cleared");
		}
	}


}
