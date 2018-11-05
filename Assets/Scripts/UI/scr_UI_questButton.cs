using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_UI_questButton : MonoBehaviour {

[Space(10)][Header("Data")]
	public	cl_quest	linked_quest			;

[Space(10)][Header("References")]
	public	Button		self_button				;
	public	Text		ui_quest_name			;
	public	Text		ui_quest_reward_gold	;
	public	Text		ui_quest_reward_exp		;
	public	Text		ui_quest_reward_reput	;


// = = =

	public void Initialize () 
	{
		ui_quest_name.text 			= 	linked_quest.quest_name;
		// ui_quest_reward_gold.text 	= 	linked_quest.quest_reward_gold.ToString();
		// ui_quest_reward_exp.text 	= 	linked_quest.quest_reward_experience.ToString();
		// ui_quest_reward_reput.text 	= 	linked_quest.quest_reward_reputation.ToString(); 

		// button color
		if (linked_quest.isAccepted == false)
		{
			var new_colors = self_button.colors;
			new_colors.normalColor = new Color(1,1,1,1);
			new_colors.highlightedColor = new Color(0.85f,0.85f,0.85f,1);
			self_button.colors = new_colors;
		}
		else
		{
			var new_colors = self_button.colors;
			new_colors.normalColor = new Color(0.45f,0.65f,0.8f,1);
			new_colors.highlightedColor = new Color(0.30f,0.50f,0.65f,1);
			self_button.colors = new_colors;
		}

		return;
	}
	
}
