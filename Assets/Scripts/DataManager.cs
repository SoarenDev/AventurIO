using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {

public static DataManager instance;

//[Space(10)][Header("REFERENCES")]

// [Space(10)][Header("GLOBAL")]

[Space(10)][Header("REGIONS")]
	public	List<GameObject>						region_list					;

[Space(10)][Header("PLACES")]
	public	so_modificator_place[]					place_modificatorList		;				// only here to generate the Dictionary on start
	public	Dictionary<int, so_modificator_place> 	place_modificatorDict 		= new 	Dictionary<int, so_modificator_place>();
	public	so_place_type_genPreset[]				place_generationPreset		;
	public	so_namelist[]							place_nameList				;
	[Space(5)]
	public	List<so_place_type>						place_typeList				;
	public	Dictionary<int, so_place_type>			place_typeDict				= new Dictionary<int, so_place_type>();

[Space(10)][Header("FACTIONS")]
	public	List<cl_faction>						faction_list				;
	[Space(5)]
	public	List<so_faction_type>					faction_typeList			;
	public	Dictionary<int,so_faction_type>			faction_typeDict			= new Dictionary<int,so_faction_type>();

[Space(10)][Header("NPCS")]
	public 	List<cl_npc> 							npc_list 					; /*= new List<cl_npc>()*/
	public	so_npc_situation[]						npc_situationList			;
	[Space(5)]
	public 	so_namelist[]							npc_male_nameList			;					// the position in the array is there in prevision of the use of a variable (like "Culture" for exemple) witch would make vary which "version" of the given type will be used, depending of this variable
	public 	so_namelist[]							npc_female_nameList			;
	public 	so_namelist[]							npc_family_nameList			;

[Space(10)][Header("RPG Data")]
	public	int[]									exp_levelup_table			= new int[100];		// contain the needed exp for a character to reach next exp_level for each level. The array's index correspond the concerned experience level

	void Awake () {

		if (instance == null)
		{
			instance = this;
		} else {
			Destroy(this);
		}

	}

	void Start () {

	// = = GENERATE DICTIONARIES = =

		// place_modificator
		foreach (var item in place_modificatorList)
		{ if (item != null) { place_modificatorDict.Add(item.id, item); } }
		Debug.Log("place modificators dictionary created with " + place_modificatorDict.Count + " references!");
		// Debug.Log("Test dictionnary reference name : reference" + "0" + " = " + place_modificatorDict[0].name);

		// place_type
		foreach (var item in place_typeList)
		{ if (item != null) { place_typeDict.Add(item.id, item); } }
		Debug.Log("place types dictionary created with " + place_typeDict.Count + " references!");

		// faction_type
		foreach (var item in faction_typeList)
		{ if (item != null) { faction_typeDict.Add(item.id, item); } }
		Debug.Log("faction types dictionary created with " + faction_typeDict.Count + " references!");

	// = =

	}
	
	void Update () {
		
	}

	public void Check(){
		Debug.Log("DataManager checked");
	}
}
