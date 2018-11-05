using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FactionType", menuName = "Faction/Type", order = 1)]
public class so_faction_type : ScriptableObject {

[Space(10)]
	public	int									id			;
[Space(10)]
	public 	FactionTypeEnum						type		;

[Space(10)][Header("Attributes")]
	public	Sprite								ui_icon								;
	[Space(5)]
	// public	so_namelist							namelist_default					;
	public	List<string>						namelist							= new List<string>{"default_name"};
	public	string[]							ranks_default						= new string[1]{"default_rank"};
	public	int[]								events_default						;

	[Space(5)]
	public	int									min_default_influence				;
	public	int									max_default_influence				;

}
