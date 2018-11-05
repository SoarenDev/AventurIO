using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "npcSituation", menuName = "NPCs/Situation", order = 1)]
public class so_npc_situation : ScriptableObject {

public 	enum 	situationType
{
	ruler,
	solider,
	adventurer,
	merchant,
	villager,
	bandit,
	undefined
} 

	public 	situationType 				situation;					// maybe not used // indicate the *parent situation* of the class which will be use as a reference in code (ex: King, Jarl, President and Khan are all derived from the situation "ruler")

	public	so_place_type_genPreset		generation_preset;
	public	so_namelist					namelist;


	// faire en sorte que la "situation" contienne une **cl_chance_preset**, contenant une LIST avec des ut_KeyValue, et qui permet de définir une liste de classe parmi lesquelles sera tiré la classe d'un personnage généré avec cette situation.

}
