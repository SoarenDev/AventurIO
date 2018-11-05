using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public 	enum 	PlaceType
{
	city,
	village,
	cave,
	ruins,
	forest,
	stronghold,
	undefined
} 
public class scr_place : MonoBehaviour {

[Space(10)]
	public	so_place_type						type								;

[Space(10)][Header("Attributes")]
	public	string								place_name							;
	public	Sprite								sprite								;		
	public	int									id									;
	public	bool								canDisaster							;
	public	bool								isLocked							;		// a locked place can't be targeted by persistant events and disasters. Usually, a place is locked while a persistant event is active in it.

[Space(10)][Header("Content")]
	public	List<cl_npc>						place_npcs							= new List<cl_npc>();
	public	List<int>							place_events						= new List<int>();
	public	List<int>							place_modificators					= new List<int>();			// contient les [index] de tous les modificateurs affectant le lieu (repaire de bandit, présence d'un culte de nécromanciens, habitants faisant des rêves étranges, etc, etc..) Ces modificateurs peuvent être testés comme condition pour des events
	[Space(5)]
	public	strct_local_faction					place_main_faction					= null;
	public	List<strct_local_faction> 			place_faction_list					= new List<strct_local_faction>();			// contain the index of the faction and its influence value (power)
	[Space(5)]
	public	List<cl_quest>						place_quests						= new List<cl_quest>();
	[Space(5)]
	public	cl_npc								place_governor						;			// can be null (and it will be very often)

[Space(10)][Header("References")]
	public	scr_region							linked_region						;
	public	int									linked_level_index					;
	public	so_place_type_genPreset				generation_preset					;
	public	SpriteRenderer						spriteRenderer						;

[Space(10)][Header("DEBUG")]
	public	bool								isGenerateOnStart 					= false;

	void Awake () 
	{
		
	}

	void Start () 
	{
		// References Initialization

		// DEBUG TEST GENERATION
		if (isGenerateOnStart == true) {
			GeneratePlace();
		}
	}
	
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			Debug.Log("<b>RELAUNCH PLACE GENERETION !!!!</b>");
			GeneratePlace();
		}

	}

// = = = = = GENERATION METHODS = = = = =

	public void GeneratePlace()
	{
		// Debug.Log("\n<size=12><b>===== == PLACE GENERATION START ! == =====</b></size>");

	// TYPE GENERATION
		// Debug.Log("<b>=== PLACE TYPE GENERATION ! ===</b>");
		type = generation_preset.RandomType();	// recuperation of the index from the type generation method
		spriteRenderer.sprite = type.sprite;					// APPLY SPRITE
		spriteRenderer.color = type.sprite_color;													
		// Debug.Log("<b><i>type generation SUCCESS with following result : " + type.name.ToString() + " (" + type.type.ToString() + ")</i></b>");

	// NPCs GENERATION
		// Debug.Log("<b>=== PLACE NPCS GENERATION ! ===</b>");
		cl_npc new_npc;

		// Debug.Log("Range of npcs to generate > (min:" + type.min_npc + " max:" + type.max_npc + ")");
		int nbr_of_npcs = Random.Range(type.min_npc, type.max_npc+1);		// +1 because random range max is Exclusive with int
		// Debug.Log("Amount of npcs to generate: " +  nbr_of_npcs);
		

		for (int i = 0; i < nbr_of_npcs; i++)
		{
			// generate
			new_npc = GameManager.instance.GenerateNpc();

			// add to place list
			place_npcs.Add(new_npc);		// add the npc the place npc_list

		}

		// Debug.Log("<b><i>npcs generation SUCCESS with following result : " + place_npcs.Count + "</i></b>");

	// FACTIONS GENERATION
		// Debug.Log("<b>=== PLACE FACTIONS GENERATION ! ===</b>");

		// Debug.Log("Range of faction to generate > (min:" + type.min_faction + " max:" + type.max_faction + ")");
		int nbr_of_factions = Random.Range(type.min_faction, type.max_faction+1);	
		// Debug.Log("Amount of faction to generate: " +  nbr_of_factions);

		if (nbr_of_factions != 0)
		{
		// FACTION GENERATION LOOP
			for (int i = 0; i < nbr_of_factions; i++)
			{
				cl_faction new_faction = new cl_faction();

				// generate
				// Debug.Log("Envoi de : " + type.faction_genList_default);
				new_faction.GenerateFaction(this, type.faction_genList_default);

				// define base influence
				int min_influence = new_faction.faction_type.min_default_influence;
				int max_influence = new_faction.faction_type.max_default_influence;
				int default_influence = Random.Range(min_influence, max_influence+1);
				// Debug.Log("Default influence: " +  default_influence);

				// convert faction to faction_info struct
				strct_local_faction new_faction_strct = new strct_local_faction(){faction = new_faction, influence = default_influence, local_size = 0};
				// Debug.Log("new faction generated > name: " + new_faction_strct.faction.faction_name + " influence: " + new_faction_strct.influence);

				// add to place list ; binding
				place_faction_list.Add(new_faction_strct);
				new_faction.faction_places.Add(this);		// add this place to the faction's places list

			}

			// TOTAL NUMBER OF ASSIGN NPCs to a faction		// p-e migrer ça dans le cl_faction directement
			int numberOfNpc_to_assign;
			numberOfNpc_to_assign = Mathf.FloorToInt(place_npcs.Count * type.part_of_npcs_in_faction);	// calculation of number of npc from the total number and the type part of npcs in faction
			// Debug.Log("npcs to assign to a faction in " + name + " : " + numberOfNpc_to_assign);

			// assign 1 npc to every faction 
			foreach (var faction_struct in place_faction_list)	
			{
				RecruitNpcInFaction(faction_struct);
				numberOfNpc_to_assign -= 1;
			}

			// assign other npcs to random weightened faction
			while (numberOfNpc_to_assign > 0)
			{
				// draw random faction
				int target_index = RandomFactionFromInfluence(place_faction_list);

				// add random npc to drawn faction
				RecruitNpcInFaction(place_faction_list[target_index]);
				
				// decrement loop
				numberOfNpc_to_assign -= 1;
			}
		
			// Define each faction leader
			foreach (var faction_struct in place_faction_list)
			{
				faction_struct.faction.DefineLeader();
			}

			// update main faction
			UpdateMainFaction();
			
			
		} 
		// else { Debug.Log("NO FACTION TO GENERATE"); }

		// Debug.Log("<b><i>factions generation SUCCESS with following result : " + (place_faction_list.Count-1) + "</i></b>");	

		
	// GENERATE EVENTS
		// Debug.Log("<b>=== PLACE EVENT GENERATION ! ===</b>");
		place_events = new List<int>(type.events_default);

		// Debug.Log("<b><i>events generation SUCCESS with following result : " + (place_events.Count-1) + " events generated</i></b>");	

	// GENERATE LEVEL


	// NAME GENERATION

		// NPCs
		foreach (var item in place_npcs) { GameManager.instance.NPCGenerateName(item); }

		// PLACE (this)
		PlaceGenerateName();

		// FACTIONS
		foreach (var item in place_faction_list) { item.faction.GenerateName(); }

	// END
		// Debug.Log("\n<size=12><b>>>>>> >> PLACE GENERATION SUCCESS !!! << <<<<< </b></size>");
	}
	
	public void PlaceGenerateName()
	{
		// Debug.Log("<b>=== PLACE NAME GENERATION ! ===</b>");

		List<string> name_list = type.name_list;
		string name_generated;

		if (name_list != null)
		{
			name_generated = name_list[Random.Range(0, name_list.Count)];
			// Debug.Log("Selected name : " + name_generated);
		} else {
			name_generated = "undefined";
			// Debug.LogWarning("NO NAMELIST FOUND ! Selected default name : " + name_generated);
		}

		string name_interpreted = InterpretPlaceName(name_generated);		// Name codes interpretation method

		place_name = name_interpreted;										// name assignation
		// name = place_name = name_interpreted;								// inspector name assignation

		// Debug.Log("<b><i>place name generation SUCCESS with following result : " + place_name + "</i></b>");
	}

	public string InterpretPlaceName(string target)
	{	
		// = INTERPRETATIONS =
		string interpreted_text = target; 
		interpreted_text = interpreted_text.Replace("$npc_fn", place_npcs[Random.Range(0, place_npcs.Count)].npc_firstname);	// replace $npc_fn by the first name of one of the npcs inside the place (reference get in DataManager)
		interpreted_text = interpreted_text.Replace("$npc_ln", place_npcs[Random.Range(0, place_npcs.Count)].npc_lastname);		// replace $npc_ln by the last name of one of the npcs inside the place (reference get in DataManager)
		// =

		// Debug.Log("Interpretation method complete. New text : " + interpreted_text);
		return interpreted_text;
	}

// = = = = =

	// =
	public  int   RandomFactionFromInfluence(List<strct_local_faction> faction_list)
    {
        // int index               = 0;                // define which element is actually tested by randomChecker
        int randomChecker       = 0;                // manage the addition of the "chance" value of each type, which allow to check within which element range the randomNumber is. Here, "influence" is the value controlling the random
        int randomNumber        = 0;                // stock RNG number
        int totalRandomValue    = 0;                // addition of every ChancesPerType value
        int target_index 		= -1;

        // Total Random Value calculation loop
        for (int i = 0; i < faction_list.Count; i++)
        {
            totalRandomValue += faction_list[i].influence;
            // Debug.Log("actual total random " + totalRandomValue);
        }
        // Debug.Log("FINAL TOTAL RANDOM " + totalRandomValue);

        // Random Number Draw
        randomNumber = Random.Range(1, totalRandomValue);
        // Debug.Log("RANDOM NUMBER : " + randomNumber);

        // Random value Index Checker
        for (int i = 0; i < faction_list.Count; i++)
        {
            // Debug.Log("New index : " + i);
            // Debug.Log("Actual range check : index" + i + " min" + (randomChecker+1) + " max" + (randomChecker + faction_list[i].influence) );
            
            if (randomNumber > randomChecker && randomNumber <= randomChecker + faction_list[i].influence)
            {
                // Debug.Log("RANDOM NUMBER FOUND IN INDEX " + i);
                target_index = i;
                break;
            } else {
                randomChecker += faction_list[i].influence;
                // index += 1;
                // Debug.Log("Random number not found in index " + (i) + ".");

                // Error report if random number isn't found even in the last Check Range
                if (i < faction_list.Count -1 == false)
                {
                    Debug.LogWarning("ERROR !!! RANDOM NOT FOUND IN ANY RANGE CHECK !!! OUTPUT SET TO index -1!");
                    // index = -1;
                    target_index = -1;		// <<< MAYBE CHANGE TO UNDEFINED ???
                }
            }
        }

        // Reset variables

        // Return index
        // Debug.Log("End of random faction drom influence selection method. RETURN INDEX : " + target_index);
        return target_index;

    }
	// =

	/// <summary>
	/// Check all place's factions, and set the one with the higher Influence as "Main faction" of the place. If there are no faction in the place, main faction will be set as "null".
	/// </summary>
	public void UpdateMainFaction()
	{
		// security: verify that faction list isn't null when trying to update the main faction
		if (place_faction_list.Count == 0){ place_main_faction.faction = null; return; }
		
		// select the faction with the higher influence by checking every one of them
		strct_local_faction selected_faction_struct = place_faction_list[0];
		foreach (var faction_struct in place_faction_list)
		{
			if (faction_struct.influence > selected_faction_struct.influence)
			{
				selected_faction_struct = faction_struct;
			}
		}

		// update place main faction 
		place_main_faction = selected_faction_struct;

		return;
	}

	/// <summary>
	/// Recruit a random or a defined npc and add it to the given faction. Random npc is draw among those who haven't already a faction.
	/// </summary>
	public void RecruitNpcInFaction(strct_local_faction targeted_faction_struct)
	{
		// define available npc for draw
		// Debug.Log("recruit npc");
		List<cl_npc> available_npc_list = new List<cl_npc>();

		for (int i = 0; i < place_npcs.Count; i++)		// we add to the available npc list every npc in the place who haven't any faction linked
		{
			if (place_npcs[i].npc_faction.local_faction == null)
			{
				available_npc_list.Add(place_npcs[i]);
			}
		}

		// draw random npc from list
		if (available_npc_list.Count == 0) { Debug.LogWarning("TRYING TO RECUIT NPC IN FACTION BUT THERE ARE NO NPC WITHOUT FACTION! (or there are in fact 0 npc to assign?) TERMINATE METHOD"); return; }
		int drawn_npc_index = Random.Range(0, available_npc_list.Count);

		// add npc to faction
		targeted_faction_struct.faction.faction_npcs.Add(available_npc_list[drawn_npc_index]);

		// increments the local_faction local_size
		targeted_faction_struct.local_size += 1;	

		// add faction to npc
		struct_faction_memberInfos new_member_strct = new struct_faction_memberInfos(){local_faction = targeted_faction_struct, rank = 1};
		available_npc_list[drawn_npc_index].npc_faction = new_member_strct;
	}

	/// <summary>
	/// Add the given npc to the given faction.
	/// </summary>
	public void RecruitNpcInFaction(strct_local_faction targeted_faction_struct, cl_npc targeted_npc)
	{
		// add npc to faction
		targeted_faction_struct.faction.faction_npcs.Add(targeted_npc);

		// increments the local_faction local_size
		targeted_faction_struct.local_size += 1;

		// add faction to npc
		struct_faction_memberInfos new_member_strct = new struct_faction_memberInfos(){local_faction = targeted_faction_struct, rank = 1};
		targeted_npc.npc_faction = new_member_strct;
	}
	
}
