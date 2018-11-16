using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_UI_npcQuestTab : scr_UI_parent {

[Space(10)][Header("References")]
	public	static	cl_quest		ui_active_quest				;
[Space(5)]
	public			Image			background					;
	public			Image			ui_quest_icon				;
	public			Text			ui_quest_name				;
	public			Text			ui_quest_description		;

	public			Button			bt_accept_quest				;
	public			Text			txt_accept_quest			;
	public			Button			bt_abandon_quest			;
	public			Text			txt_abandon_quest			;
	public			Button			bt_validate_quest			;
	public			Text			txt_validate_quest			;

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
		bt_abandon_quest.onClick.AddListener(BT_AbandonQuest);
		bt_validate_quest.onClick.AddListener(BT_ValidateQuest);
	}

	// Refresh all data in the tab using ui_active_quest data
	public override void RefreshTab()
	{
		ui_quest_name.text				=				ui_active_quest.quest_name;
		ui_quest_description.text		=				ui_active_quest.quest_description;
		// ui_quest_icon.sprite				=				GameManager.dict_icons[ui_active_quest.quest_icon_index];
		ui_quest_goal.text				=				"- " + ui_active_quest.quest_goal_text;

		ui_quest_reward_gold.text		=				ui_active_quest.quest_reward_gold.ToString();
		ui_quest_reward_exp.text		=				ui_active_quest.quest_reward_experience.ToString();	
		ui_quest_reward_rep.text		=				ui_active_quest.quest_reward_reputation.ToString();		

		// Accept quest button ini
		if (ui_active_quest.quest_state == enum_quest_state.Available) { txt_accept_quest.text = "Accept quest"; bt_accept_quest.interactable = true; }
		else { txt_accept_quest.text = "Accepted"; bt_accept_quest.interactable = false; }

		// Abandon quest button ini
		if (ui_active_quest.quest_state == enum_quest_state.Ongoing) { txt_abandon_quest.text = "Abandon quest"; bt_abandon_quest.interactable = true; }
		else { txt_abandon_quest.text = "Abandon"; bt_abandon_quest.interactable = false; }

		// Validate quest button ini [!!!!!! DEBUG: VALIDATE BUTTON BECOMES AVAILABLE IN ONGOING STATE !!!!!!! ]
		if (ui_active_quest.quest_state == enum_quest_state.Ongoing) { txt_validate_quest.text = "Complete quest"; bt_validate_quest.interactable = true; }
		else { txt_validate_quest.text = "Not completed"; bt_validate_quest.interactable = false; }

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

		// update npc_shell overhead sprite
		GameManager.ui_active_npc.UpdateQuestOverheadSprite();

		// update menu
		RefreshTab();

		return;
	}

	/// <summary>
	/// Method launched when the "Abandon quest" button is pressed.
	/// </summary>
	public void		BT_AbandonQuest()			// 1
	{
		Debug.Log("ACCEPT QUEST BT");

		ui_active_quest.DeleteQuest();

		// update npc_shell overhead sprite
		GameManager.ui_active_npc.UpdateQuestOverheadSprite();

		// close tab
		GameManager.instance.UI_Close(GameManager.UI_active_menu);

		return;
	}

	/// <summary>
	/// Method launched when the "Complete quest" button is pressed.
	/// </summary>
	public void		BT_ValidateQuest()			// 1
	{
		Debug.Log("ACCEPT QUEST BT");

		ui_active_quest.CompleteQuest(0);

		// update npc_shell overhead sprite
		GameManager.ui_active_npc.UpdateQuestOverheadSprite();

		// close tab
		GameManager.instance.UI_Close(GameManager.UI_active_menu);

		return;
	}

}
