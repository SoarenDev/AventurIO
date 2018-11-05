using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_UI_placeFactionButton : MonoBehaviour {

[Space(10)][Header("Data")]
	public	strct_local_faction	linked_faction_struct		;

[Space(10)][Header("References")]
	public	Text					ui_faction_name				;
	public	Text					ui_faction_influence		;
	public	Text					ui_faction_size				;

// = = =

	public void Initialize () 
	{
		ui_faction_name.text 			= 	linked_faction_struct.faction.faction_name;
		ui_faction_influence.text 		= 	linked_faction_struct.influence.ToString();
		ui_faction_size.text 			= 	linked_faction_struct.local_size.ToString();
	}

}
