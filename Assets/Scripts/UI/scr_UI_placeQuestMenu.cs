using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class scr_UI_placeQuestMenu : scr_UI_parent {

[Space(10)][Header("Data")]
	//public	static scr_place	ui_active_place		; // moved into GameManager
	private	int					menu_index				= 0;
	public cl_quest				selected_quest			;

[Space(10)][Header("References")]
	public	Image				background				;
	public	Text				place_name				;
	public	Button				quest_button_prefab		;
	[Space(5)]
	public	List<Button>		bt_quests				= new List<Button>();

[Space(10)][Header("Locals")]
	private	bool				consumeInput_menuMove	= true;
	private	bool				consumeInput_menuAction	= true;

// = = =

// UTILISER onClick.Invoke() POUR GERER LE CLIC DE BOUTON VIA INDEXER (controle manette)

	void Start()
	{
		
	}

	void Update()
	{
		// Move menu input
		if (Input.GetAxis("MenuMove") != 0)		
		{
			if (consumeInput_menuMove == false)			// check if axis input hasn't been consumed already
			{
				if (Input.GetAxis("MenuMove") > 0) { Menu_MoveIndex(1); }
				if (Input.GetAxis("MenuMove") < 0) { Menu_MoveIndex(-1); }
				consumeInput_menuMove = true;
			}
		} 
		else { consumeInput_menuMove = false; }

		// Menu accept input
		if (Input.GetAxis("MenuAction") > 0)
		{
			if (consumeInput_menuAction == false)		// check if axis input hasn't been consumed already
			{
				Menu_Action();
				menu_index = 0;
				consumeInput_menuAction = true;
			}
		} 
		else { consumeInput_menuAction = false; }

	}

	// Refresh all data in the tab using ui_active_place data
	public override void RefreshTab()
	{
		menu_index = 0;

	// = REFRESH UI BUTTONS =

		// Clear old UI Buttons
		while (bt_quests.Count > 0)
		{
			Destroy(bt_quests[0].gameObject);
			bt_quests.RemoveAt(0);
			// Debug.Log("1 Quest button cleared");
		}

		// Construct new UI Buttons
		foreach (var quest in GameManager.ui_active_place.place_quests)
		{
			// draw button
			Button instance = Instantiate(quest_button_prefab, new Vector3(0,0,0), Quaternion.identity, this.transform);
			scr_UI_questButton instance_script = instance.GetComponent<scr_UI_questButton>();

			// add to button list
			bt_quests.Add(instance);

			// Initialize new button
			instance_script.linked_quest = quest;
			instance_script.Initialize();
		}

		// Binding click events
		foreach (var item in bt_quests)
		{
			item.onClick.AddListener(BT_QuestClick);
		}

	// =

		Debug.Log("Place Quest menu refreshed");
	}

// = = = MENU CONTROL = = =	

	public void		Menu_MoveIndex(int value)		
	{
		if 		(menu_index + value < 0) { menu_index = bt_quests.Count - 1; }						// move to last index
		else if (menu_index + value > bt_quests.Count - 1) { menu_index = 0; }		// move to first index
		else	{ menu_index += value; }											// normal movement
		
		Debug.Log("New index = " + menu_index);
	}

	public void		Menu_Action()
	{
		bt_quests[menu_index].onClick.Invoke(); 	
	}

// = = = BUTTON EVENTS = = =

	/// <summary>
	/// Method launched when one of the "Quest" button is pressed. Open the linked quest tab.
	/// </summary>
	public void		BT_QuestClick()			// X
	{
		Debug.Log("QUEST TAB METHOD");

		// set the selected quest active in UI
		selected_quest = EventSystem.current.currentSelectedGameObject.GetComponent<scr_UI_questButton>().linked_quest;
		scr_UI_placeQuestTab.ui_active_quest = selected_quest;

		// change menu
		GameManager.instance.UI_Close(GameManager.UI_active_menu);
		GameManager.instance.UI_Construct(scr_ioScript_place.UI_placeQuestTab);
	}

}
