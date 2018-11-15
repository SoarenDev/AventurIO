using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

public static GameManager instance;

[Space(10)][Header("Usual Variables")]
	public			float				questCheck_refresh_delay	= 1f;

[Space(10)][Header("Data")]
	public	static 	scr_place			ui_active_place				;
	public	static	cl_npc				ui_active_npc				;
	public	static 	scr_place			INT_active_place			;			// buffer variable used, when entering in a place, to keep the reference of this place

//[Space(10)][Header("References")]

[Space(10)][Header("NPC generation")]
	public			int					age_range_min				= 16;
	public			int					age_range_max				= 64;

[Space(10)][Header("Interiors")]
	public			string				INT_scene_name				;

[Space(10)][Header("UI")]
	public 	 		GameObject			ui_interact_image			;
	public	static	scr_UI_parent		UI_active_menu				;
	[Tooltip("These references will be used as static references in the IO_script_place")]
	public			scr_UI_placeMenu	UI_placeMenu				;
	public			scr_UI_placeTab		UI_placeTab					;
	public			scr_UI_parent		UI_npcQuestsMenu			;
	public			scr_UI_parent		UI_npcQuestsTab				;
	[Space(5)][Header("Player HUD")]
	public			GameObject			ui_player_hud_parent		;
	public			Text				txt_player_gold				;

[Space(10)][Header("References")]
	public			GameObject			player_character_prefab		;
	public			player_character	player_reference			;
	public			GameObject			main_canvas_reference		;

	void Awake () {

		if (instance == null)
		{
			instance = this;
		} else {
			Destroy(this.gameObject);
		}

		

	}

	void Start () 
	{
		// Spawn player_character
		CreatePlayerCharacter(player_character_prefab, new Vector3(0,0,0) );

		// Start QuestCheck Coroutine
		StartCoroutine("QuestCheck_Coroutine");

		// Lock global objects through scenes
		DontDestroyOnLoad(this);
		DontDestroyOnLoad(main_canvas_reference);
		DontDestroyOnLoad(player_reference.gameObject);

	}


	void Update () 
	{
		txt_player_gold.text = Mathf.Round(player_reference.gold_ui_trans_value).ToString();

		// = = DEBUG: CHEATS = =
		// add gold
		if (Input.GetKeyDown(KeyCode.Keypad1)) { player_reference.Gold += 100; }
	}

	IEnumerator QuestCheck_Coroutine()
	{
		// Check Quest Conditions
		List<cl_quest> quest_list_buffer = new List<cl_quest>(player_reference.accepted_quests);
		foreach (var quest in quest_list_buffer)
		{
			quest.Quest_CompletionCheck();
		}
		// Debug.Log("QuestChecked");
		// Debug.Log(accepted_quests.Count + " quests active");

		// Loop
		yield return new WaitForSeconds(1f);
		StartCoroutine("QuestCheck_Coroutine");
	}

// = = = NPC GENERATION METHOD = = = (OVERLOADING)
// Starts by Main method, then finish with one overload method.
// Note that if you want to "lock" certain values for certain variables when generating a npc, you have to

	// >>> MAIN METHOD <<< Every overload call this one!

	/// <summary>
	/// Generate one npc with a random, or given situation, and possibly a specific random class
	/// If you dont specify a class_genPreset in parameters, the default class_genPreset of the generated npc_situation will be used)
	/// </summary>
	public cl_npc GenerateNpc()		
	{
		// Debug.Log("\n<size=12><b>===== == NPC GENERATION START ! == =====</b></size>");
		cl_npc new_npc = new cl_npc();

		// ATTRIBUTES GENERATION
		// Debug.Log("<b>=== NPC ATTRIBUTES GENERATION ! ===</b>");
		new_npc.npc_gender = Random.Range(0, 2);
		// Debug.Log("Gender selected : " + new_npc.npc_gender);

		new_npc.npc_age = Random.Range(age_range_min, age_range_max + 1);
		// Debug.Log("Age selected : " + new_npc.npc_age);

		// reference in data manager
		new_npc = IndexNpc(new_npc);

		// END OF BASE METHOD
		// Debug.Log("<b>=== NPC BASE GENERATION METHOD COMPLETE! Now processing possible method overloads ===</b>");
		return new_npc;
	}

	// ::: METHOD OVERLOADS :::

		public cl_npc GenerateNpc(so_npc_situation_genPreset situation_genPreset)
		{
			cl_npc new_npc;
			new_npc = GenerateNpc();
			new_npc = IndexNpc(new_npc);

			// Debug.Log("NPC generated with random situation");
			return new_npc;
		}

		public cl_npc GenerateNpc(so_npc_situation_genPreset situation_genPreset, so_npc_class_genPreset class_genPreset)
		{
			cl_npc new_npc;
			new_npc = GenerateNpc();
			new_npc = IndexNpc(new_npc);

			// Debug.Log("NPC generated with random situation and special class");
			return new_npc;
		}

		public cl_npc GenerateNpc(so_npc_situation situation)
		{
			cl_npc new_npc;
			new_npc = GenerateNpc();
			new_npc = IndexNpc(new_npc);

			// Debug.Log("NPC generated with given situation");
			return new_npc;
		}

		public cl_npc GenerateNpc(so_npc_situation situation, so_npc_class_genPreset class_genPreset)
		{
			cl_npc new_npc;
			new_npc = GenerateNpc();
			new_npc = IndexNpc(new_npc);

			// Debug.Log("NPC generated with given situation, special class");
			return new_npc;
		}

	public cl_npc IndexNpc(cl_npc targetNpc)		// this method add the generated npc to the reference list in the Data Manager 
	{
		cl_npc new_npc = targetNpc;

		// NPC REFERENCE GENERATION
		// Debug.Log("<b>=== NPC REFERENCE INDEXATION ! ===</b>");
		DataManager.instance.npc_list.Add(new_npc);
		new_npc.npc_id = DataManager.instance.npc_list.Count - 1;		// give the right ID of the npc in the list just after linking the new npc to this list. Don't forget the -1 because array begins in 0 while Count begins in 1
	
		// Debug.Log("<b><i>NPC indexation SUCCESS with following result : " + new_npc.npc_id + " </i></b>");
		return new_npc;
	}

	public	void	NPCGenerateName(cl_npc targetNpc)		// ALWAYS GENERATE THE NAME AS LATE AS POSSIBLE, as the name interpretation need to access certain value generated after the main generation method!
	{
		// NAME GENERATION
		// Debug.Log("<b>=== NPC NAME GENERATION ! ===</b>");
		so_namelist namelist_firstname;
		so_namelist namelist_lastname;

		switch (targetNpc.npc_gender)
		{
			case 0:	
			{
				namelist_firstname = DataManager.instance.npc_male_nameList[0];
				// Debug.Log("gender = male ");
				break;
			}

			case 1:
			{
				namelist_firstname = DataManager.instance.npc_female_nameList[0];
				// Debug.Log("gender = female ");
				break;
			}

			default: 
			{
				namelist_firstname = DataManager.instance.npc_male_nameList[0];
				// Debug.Log("gender = undefined; name will be picked in male namelist.");
				break;
			}
		} 
		namelist_lastname = DataManager.instance.npc_family_nameList[0];

		string firstname_generated;
		string lastname_generated;
		firstname_generated = namelist_firstname.main_names[Random.Range(0, namelist_firstname.main_names.Count)];
		lastname_generated 	= namelist_lastname.main_names[Random.Range(0, namelist_lastname.main_names.Count)];

		// name assignation
		targetNpc.npc_firstname = firstname_generated;
		targetNpc.npc_lastname 	= lastname_generated;										

		// Debug.Log("<b><i>name generation SUCCESS with following result : " + firstname_generated + " " + lastname_generated + " </i></b>");	
		return;
	}

	/// <summary>
	/// Clears the targeted npc and supress it from the differents databases. 
	/// </summary>
	public void ClearNpc(cl_npc npc, scr_place place)		
	{
		// >> security: stop method if target is locked
		if (npc.isLocked == true) { Debug.LogError("ClearNpc METHOD TARGETS A LOCKED NPC! This could break some quests and events!"); return; }

		// clear from place npc list
		place.place_npcs.Remove(npc);

		// clear from faction list
		if (npc.npc_faction.local_faction != null)
		{
			// decrements local_faction local size
			npc.npc_faction.local_faction.local_size -= 1;

			// remove from faction list
			npc.npc_faction.local_faction.faction.faction_npcs.Remove(npc);
		}

		// clear from DataManager [WARNING doing this will cause a offset between npcs' ID and their position in DataManager npc_list! So don't use it for now.]
		// DataManager.instance.npc_list.Remove(npc);

		Debug.Log("NPC CLEARED FROM GAME BUT NOT FROM DATA!");
		return;
	}

// = = =

// = = = PLACE INTERIOR MANAGEMENT = = =

	/// <summary>
	/// Sets the targeted place as the static INT_active_place, and load the interior scene.
	/// <param name="target_place"> place which interior will be load </param>
	/// </summary>
	public	void	INT_EnterPlace(scr_place target_place)
	{
		Debug.Log("Entering " + target_place.name + " ...");
		// set the static variable
		INT_active_place = target_place;

		// close UI tabs
		UI_Close(UI_active_menu);

		// empty player in-range interaction list
		player_reference.interaction_controller.ClearInrangeList();

		// open scene
		SceneManager.LoadScene(INT_scene_name);

		return;
	}

	/// <summary>
	/// Moves the player back to the worldmap scene.
	/// </summary>
	public	void	INT_LeavePlace()
	{
		Debug.Log("Leaving the place...");

		// set the static variable
		INT_active_place = null;

		// close UI tabs
		UI_Close(UI_active_menu);

		// empty player in-range interaction list
		player_reference.interaction_controller.ClearInrangeList();

		// open scene
		SceneManager.LoadScene("00_WorldMap");

		return;
	}

// = = =

// = = = UI METHODS = = =
// Regroups all UI-related methods, which will be called by other scripts

	// = = MENUS = =
	public	void	UI_Construct(scr_UI_parent ui_target)
	{
		UI_active_menu = ui_target;

		ui_target.RefreshTab();

		// LayoutRebuilder.ForceRebuildLayoutImmediate(ui_target.GetComponent<RectTransform>());
		ui_target.gameObject.SetActive(true);
	}

	public	void	UI_Close(scr_UI_parent ui_target)
	{
		ui_target.gameObject.SetActive(false);
	}
	// = =

	// = = PLAYER HUD = =

	/// <summary>
	/// Visually interpolates the gold value shown in the Player HUD from its last value to his new value. Note that if final gold value is modified while the transition is still running, the transition time will be reset.
	/// <param name="transition_time"> time in seconds to make the transition </param>
	/// </summary>
	public IEnumerator GoldUiTransition(float transition_time)
	{
		// while trans_value hasn't reach new Gold value, interpolate it and yield the method
		while ( Mathf.RoundToInt(player_reference.gold_ui_trans_value) != player_reference.Gold )
		{
			player_reference.gold_ui_trans_value = Mathf.Lerp( player_reference.gold_ui_trans_value, player_reference.Gold, (Time.deltaTime / transition_time) );
			yield return null;
		}
		
		// when trans_value has reach the "true" value and dont need to interpolate anymore, set the trans_value = the "true" value (just in case), and stop the coroutine
		player_reference.gold_ui_trans_value = player_reference.Gold;
		Debug.Log("END GOLD TRANSITION");
		yield break;
	}
	// = =

// = = =

// = = = OTHER METHOS = = =
	/// <summary>
	/// Spawns a player character game object from a prefab.
	/// <param name="prefab"> player character prefab to instantiate </param>
	/// </summary>
	public void CreatePlayerCharacter(GameObject prefab, Vector3 position)
	{
		// Instantiate player_character from prefab
		GameObject instance;
		instance = Instantiate(prefab, position, Quaternion.identity);

		// Set player_character reference
		player_reference = instance.GetComponent<player_character>();

		Debug.Log("Player spawned");
		return;
	}

}
