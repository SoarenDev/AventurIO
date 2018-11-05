using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public 	enum 	FactionTypeEnum
{
	military,
	hostile,
	neutral
} 

[System.Serializable]
public class cl_faction  {

[Header("ID")]
    public  int                 faction_id			= -1;                                         // link the action to his index in the data manager's faction_list  // quite obsolete

[Space(10)][Header("Attributes")]
	public 	string 				faction_name		= "";
	public 	so_faction_type		faction_type		;
	public	List<string>		faction_ranks		= new List<string>{"default_rank"};
	[Space(5)]
	public	bool				isLocked			;		// a locked faction can't be supressed by any event. Usually, a faction involved in a persistant event will become LOCKED until the resolution of this event

[Space(10)][Header("Content")]
	public	cl_npc				faction_leader		= new cl_npc(){npc_firstname = "FN", npc_lastname = "LN"};
	public 	List<cl_npc>		faction_npcs		= new List<cl_npc>();
	public	List<int>			faction_events		= new List<int>();
	public	List<scr_place>		faction_places		= new List<scr_place>();

// =

// = = = FACTION GENERATION METHOD = = = (OVERLOADING)
// Starts with one overload method, then finish with the main method.
// Note that if you want to "lock" certain values for certain variables when generating a faction, you have to do it after calling this method, directely by targeting the faction variable

	// >>> MAIN METHOD <<< Every overload call this one!

	public	void	GenerateFaction(scr_place targeted_place)
	{
		// Attributes generation
		faction_ranks = new List<string>(faction_type.ranks_default);

		// Events binding
		faction_events = new List<int>(faction_type.events_default);

		// ReferenceIndexation
		IndexFaction();

	}

	// ::: METHOD OVERLOADS :::

		public	void	GenerateFaction(scr_place targeted_place, List<struct_factionTypeXint> type_genPreset)
		{
			// Debug.Log("\n<size=12><b>===== == FACTION GENERATION START ! == =====</b></size>");

			// TYPE GENERATION
			// Debug.Log("Generating faction type from random type generation list");
			// Debug.Log("Envoi de : " + type_genPreset);
			faction_type = RandomFactionType(type_genPreset);

			// >> main method
			GenerateFaction(targeted_place);
		}

		public	void	GenerateFaction(scr_place targeted_place, so_faction_type specified_type)
		{
			// Debug.Log("\n<size=12><b>===== == FACTION GENERATION START ! == =====</b></size>");

			// TYPE GENERATION
			// Debug.Log("Generating faction type from specified type");
			faction_type = specified_type;

			// >> main method
			GenerateFaction(targeted_place);
		}

	public void IndexFaction()		// this method add the generated npc to the reference list in the Data Manager 
	{
		// FACTION REFERENCE GENERATION
		// Debug.Log("<b>=== FACTION REFERENCE INDEXATION ! ===</b>");
		DataManager.instance.faction_list.Add(this);
		faction_id = DataManager.instance.faction_list.Count - 1;		// give the right ID of the npc in the list just after linking the new npc to this list. Don't forget the -1 because array begins in 0 while Count begins in 1
	
		// Debug.Log("<b><i>FACTION indexation SUCCESS with following result : " + faction_id + " </i></b>");
	}

// = = =

	/// <summary>
	/// Add an existing faction to a place faction list with a given amount of influence, and reference the place in the faction's faction_place list.
	/// </summary>
	public strct_local_faction AddExistingFactionToPlace(cl_faction targeted_faction, scr_place targeted_place, int new_influence)
	{
		// create a new faction_info_struct for the place
		strct_local_faction new_faction_strct = new strct_local_faction(){faction = targeted_faction, influence = new_influence};

		// add to place list ; binding
		targeted_place.place_faction_list.Add(new_faction_strct);

		// add place to faction's faction_place
		targeted_faction.faction_places.Add(targeted_place);

		return new_faction_strct;
	}

	public void	DefineLeader()
	{
		if (faction_npcs != null) { faction_leader = faction_npcs[0]; }
		else { faction_leader = null; }
	}

	public void GenerateName()		// ALWAYS GENERATE THE NAME AS LATE AS POSSIBLE, as the name interpretation need to access certain value generated after the main generation method!
	{
		// NAME GENERATION
		// Debug.Log("<b>=== FACTION NAME GENERATION ! ===</b>");

		string name_generated;

		if (faction_type.namelist != null)
		{
			name_generated = faction_type.namelist[Random.Range(0, faction_type.namelist.Count)];
			// Debug.Log("Selected name : " + name_generated);
		} else {
			name_generated = "undefined";
			Debug.LogWarning("NO NAMELIST FOUND ! Selected default name : " + name_generated);
		}

		string name_interpreted = InterpretFactionName(name_generated);		// Name codes interpretation method

		faction_name = name_interpreted;						// name assignation

		// Debug.Log("<b><i>name generation SUCCESS with following result : " + faction_name + "</i></b>");
	}

	public string InterpretFactionName(string target)
	{	
		// = INTERPRETATIONS =
		string interpreted_text = target; 
		interpreted_text = interpreted_text.Replace("$place", faction_places[0].place_name);			// interpret to the name of the origin place of the faction
		interpreted_text = interpreted_text.Replace("$leader_fn", faction_leader.npc_firstname);		// interpret to the first name of the leader of the faction
		interpreted_text = interpreted_text.Replace("$leader_ln", faction_leader.npc_lastname);			// interpret to the last name of the leader of the faction
		// =

		// Debug.Log("Interpretation method complete. New text : " + interpreted_text);
		return interpreted_text;
	}

// = TYPE RANDOM GENERATION METHOD =
    public  so_faction_type    RandomFactionType(List<struct_factionTypeXint> random_list)
    {
        // int index               = 0;                // define which element is actually tested by randomChecker
        int randomChecker       = 0;                // manage the addition of the "chance" value of each type, which allow to check within which element range the randomNumber is.
        int randomNumber        = 0;                // stock RNG number
        int totalRandomValue    = 0;                // addition of every ChancesPerType value
        so_faction_type target_factionType = null;

        // Total Random Value calculation loop
        for (int i = 0; i < random_list.Count; i++)
        {
            totalRandomValue += random_list[i].value;
            // Debug.Log("actual total random " + totalRandomValue);
        }
        // Debug.Log("FINAL TOTAL RANDOM " + totalRandomValue);

        // Random Number Draw
        randomNumber = Random.Range(1, totalRandomValue);
        // Debug.Log("RANDOM NUMBER : " + randomNumber);

        // Random value Index Checker
        for (int i = 0; i < random_list.Count; i++)
        {
            // Debug.Log("New index : " + i);
            // Debug.Log("Actual range check : index" + i + " min" + (randomChecker+1) + " max" + (randomChecker + random_list[i].value) );
            
            if (randomNumber > randomChecker && randomNumber <= randomChecker + random_list[i].value)
            {
                // Debug.Log("RANDOM NUMBER FOUND IN INDEX " + i);
                target_factionType = random_list[i].key;
                break;
            } else {
                randomChecker += random_list[i].value;
                // index += 1;
                // Debug.Log("Random number not found in index " + (i) + ".");

                // Error report if random number isn't found even in the last Check Range
                if (i < random_list.Count -1 == false)
                {
                    Debug.LogWarning("ERROR !!! RANDOM NOT FOUND IN ANY RANGE CHECK !!! OUTPUT SET TO null!");
                    // index = -1;
                    target_factionType = null;		// <<< MAYBE CHANGE TO UNDEFINED ???
                }
            }
        }

        // Reset variables

        // Return index
        // Debug.Log("End of faction type random generation method. RETURN : " + target_factionType.name);
        return target_factionType;

    }

// =

}
