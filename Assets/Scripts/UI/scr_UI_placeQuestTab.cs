using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_UI_placeQuestTab : scr_UI_parent {

[Space(10)][Header("Data")]
	//public	static scr_place	ui_active_place		; // moved into GameManager

[Space(10)][Header("References")]
	public	static	cl_quest		ui_active_quest				;
[Space(5)]
	public			Image			background					;
	public			Image			ui_quest_icon				;
	public			Text			ui_quest_name				;
	public			Text			ui_quest_description		;
	public			Button			bt_accept_quest				;
	public			Text			txt_accept_quest			;
	public			Text			ui_quest_goal				;
[Space(5)]
	public			Text			ui_quest_reward_gold		;
	public			Text			ui_quest_reward_exp			;
	public			Text			ui_quest_reward_rep			;

// = =

	void Start()
	{
		// Binding click events
		bt_accept_quest.onClick.AddListener(BT_AcceptQuest);
	}

	// Refresh all data in the tab using ui_active_place data
	public override void RefreshTab()
	{
		ui_quest_name.text				=				ui_active_quest.quest_name;
		ui_quest_description.text		=				ui_active_quest.quest_description;
		// ui_quest_icon.sprite				=				GameManager.dict_icons[ui_active_quest.quest_icon_index];
		ui_quest_goal.text				=				"- " + ui_active_quest.quest_goal_text;

		ui_quest_reward_gold.text		=				ui_active_quest.quest_reward_gold.ToString();
		ui_quest_reward_exp.text		=				ui_active_quest.quest_reward_experience.ToString();	
		ui_quest_reward_rep.text		=				ui_active_quest.quest_reward_reputation.ToString();		

		if (ui_active_quest.isAccepted == false) { txt_accept_quest.text = "Accept quest"; bt_accept_quest.interactable = true; }
		else { txt_accept_quest.text = "Accepted"; bt_accept_quest.interactable = false; }

		Debug.Log("Quest tab refreshed");

		return;
	}

	/// <summary>
	/// Method launched when the "Accept quest" button is pressed.
	/// </summary>
	public void		BT_AcceptQuest()			// 1
	{
		Debug.Log("ACCEPT QUEST BT");

		ui_active_quest.OnAcceptQuest();

		// update menu
		RefreshTab();

		return;
	}

}
