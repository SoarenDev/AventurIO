using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// // This structure allow to keep a faction index information along with differents info such as an influence value, usefull to keep track of it's power in a place
// [System.Serializable]
// public struct struct_local_faction	// scrplace
// {
// 	public cl_faction 	faction		;
// 	public int			influence	;
// 	public int			local_size	;
// }

public enum enum_character_type
{
	player,
	IA
}

[System.Serializable]
public struct struct_event_indexXowner { // event manager

	// This structure allow to keep a event index information ( int ) linked together with the place where it come from ( scr_place ) and the faction concerned by the event (there can be none, in this case event_faction = -1)
	public	int					event_index;
	public	scr_place			event_place;
	public	strct_local_faction	event_faction;
}

[System.Serializable]
public struct struct_factionTypeXint { // cl faction

	public	so_faction_type		key;
	public	int					value;

}

public struct struct_faction_memberInfos { // cl_npc

	// Contain all informations linking a character (npc or the player) to a faction (his rank for exemple)
	public	strct_local_faction	local_faction;

	public	int						rank;

}

public class StructuresInitializer : MonoBehaviour 
{

}
