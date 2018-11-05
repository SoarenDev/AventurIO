using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class cl_quest 
{

[Header("Attributes")]
	public	int						quest_id							;
	public	string					quest_name							= "noname";
	public	string					quest_description					;
	public	string					quest_goal_text						= "undefined goal";
	public	int						quest_icon_index					;
	public	float					quest_timeout_delay					= 600f;
	[Space(5)]
	public	int						quest_level							= 1;	// used to define some difficulty parameters depending on quest level, such as base level of spawned enemies, etc.

[Header("Data")]
	public	bool					isAccepted							;
	public	string					quest_target						;

[Header("Rewards")]
	public	int						quest_reward_experience				;
	public	int						quest_reward_gold					;
	public	int						quest_reward_reputation				;

[Header("References")]
	[System.NonSerialized]
	public	cl_event_persistant		linked_event						;		// only for persistant events' quests. One shot event quests' will have a NULL linked_event
	[System.NonSerialized]
	public	strct_local_faction		linked_local_faction				;
	[System.NonSerialized]
	public	scr_place				linked_place						;		// mainly for basic quests

// = = =

	/// <summary>
 	/// Called when the player accepts the quest and registers it in his active quests.
 	/// </summary>
	public virtual void		OnAcceptQuest()
	{
		isAccepted = true;
		GameManager.instance.player_reference.accepted_quests.Add(this);

		Debug.Log("<b>Quest " + quest_name + " accepted ! </b>");
		return;
	}

	/// <summary>
 	/// Called when the quest is ended by the player. Launches DeleteQuest() and RewardQuest().
 	/// </summary>
	public virtual void		CompleteQuest(int outcome_in)
	{
		RewardQuest();
		DeleteQuest();

		return;
	}

	/// <summary>
 	/// Called when the quest is automaticaly ended because it haven't been accept by the player after a certain delay. Launches DeleteQuest() but doesn't reward the player.
	/// In the case of a persistant event, an outcome is defined in the child class override method.
 	/// </summary>
	public virtual void		TimeoutQuest(int outcome_in)
	{
		DeleteQuest();

		return;
	}

	/// <summary>
	///	Deletes the quest and all its references.
	/// </summary>
	public virtual 	void 	DeleteQuest()
	{
		if (GameManager.instance.player_reference.accepted_quests.Remove(this) == true) { GameManager.instance.player_reference.accepted_quests.Remove(this); }
		if (linked_place.place_quests.Remove(this) == true) { linked_place.place_quests.Remove(this); }

		Debug.Log("Quest deleted");
		return;
	}

	/// <summary>
	///	Gives the quest's rewards to the player.
	/// </summary>
	public virtual 	void 	RewardQuest()
	{
		GameManager.instance.player_reference.Experience += quest_reward_experience;
		GameManager.instance.player_reference.Gold += quest_reward_gold;

		Debug.Log("Quest rewards given to player");
		return;
	}

	/// <summary>
 	/// Called EVERY SECOND (via Coroutine) when the quest is accepted. Checks the end quest conditions. Calls CompleteQuest() with a given arbitrary index when certain condition are met.
	/// Try to use as few as code as possible in this method. 
	/// Note that the .base method will NEVER be called.
 	/// </summary>
	public virtual void		Quest_CompletionCheck()
	{
		
		return;
	}

}
