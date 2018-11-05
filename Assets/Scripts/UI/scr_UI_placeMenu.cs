using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_UI_placeMenu : scr_UI_parent {

[Space(10)][Header("Data")]
	//public	static scr_place	ui_active_place		; // moved into GameManager
	private	int					menu_index				;

[Space(10)][Header("References")]
	public	Image				background				;
	public	Text				place_name				;
	public	Button				bt_enter_place			;
	public	Button				bt_place_tab			;
	public	Button				bt_quests				;		// DEBUG

[Space(10)][Header("Locals")]
	private	bool				consumeInput_menuMove	= true;
	private	bool				consumeInput_menuAction	= true;

// = = =

// UTILISER onClick.Invoke() POUR GERER LE CLIC DE BOUTON VIA INDEXER (controle manette)

	void Start()
	{
		// Binding click events
		bt_enter_place.onClick.AddListener(BT_EnterPlace);
		bt_place_tab.onClick.AddListener(BT_PlaceTab);
		bt_quests.onClick.AddListener(BT_Quests);
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
		place_name.text	= GameManager.ui_active_place.place_name;
		menu_index = 0;
		// Debug.Log("Place menu refreshed");
	}

// = = = MENU CONTROL = = =	
	// WARNING HERE ! INDEX CONTROL USE ARBITARY VALUES !!! YOU HAVE TO CHANGE THE VALUE MANUALLY IF ADDING/REMOVING NEW MENU BUTTONS 

	public void		Menu_MoveIndex(int value)		
	{
		if 		(menu_index + value < 0) { menu_index = 2; }	// move to last index
		else if (menu_index + value > 2) { menu_index = 0; }	// move to first index
		else	{ menu_index += value; }						// normal movement
		
		Debug.Log("New index = " + menu_index);
	}

	public void		Menu_Action()
	{
		switch (menu_index)
		{
			case 0 : bt_enter_place.onClick.Invoke(); 	break;

			case 1 : bt_place_tab.onClick.Invoke(); 	break;

			case 2 : bt_quests.onClick.Invoke(); 		break;

			default: Debug.LogError("MENU ACTION SWITCH INDEX OUT OF RANGE"); break;
		}
	}

// = = = BUTTON EVENTS = = =

	/// <summary>
	/// Method launched when the "Enter place" button is pressed.
	/// </summary>
	public void		BT_EnterPlace()			// 0
	{
		Debug.Log("ENTER PLACE METHOD");
	}

	/// <summary>
	/// Method launched when the "Place informations" button is pressed.
	/// </summary>
	public void		BT_PlaceTab()			// 1
	{
		Debug.Log("PLACE TAB METHOD");

		GameManager.instance.UI_Close(GameManager.UI_active_menu);
		GameManager.instance.UI_Construct(scr_ioScript_place.UI_place_tab);
	}

	/// <summary>
	/// Method launched when the "Available quests" button is pressed.
	/// </summary>
	public void		BT_Quests()				// 2
	{
		Debug.Log("QUESTS METHOD");
		GameManager.instance.UI_Close(GameManager.UI_active_menu);
		GameManager.instance.UI_Construct(scr_ioScript_place.UI_placeQuestsMenu);
	}

}
