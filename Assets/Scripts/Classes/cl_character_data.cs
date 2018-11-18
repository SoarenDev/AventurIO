using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enum_character_characteristics
{
	Strength,
	Endurance,
	Perception,
	Willpower,
	Charisma,
	Luck
}

public class cl_character_data 
{

// = = = [ VARIABLES DEFINITION ] = = =

[Space(10)][Header("Attributes")]
	public			int							health								;
	public			int							stamina								;
	public			int							experience							;
	public			int							exp_level							= 1;
    [Space(5)]
    public          int                         strength                            ;
    public          int                         endurance                           ;
    public          int                         perception                          ;
    public          int                         willpower                           ;
    public          int                         charisma                            ;
    public          int                         luck                                ;

[Space(10)][Header("Usuals")]
	public			float						shoot_cooldown						;

// = = =


// = = = [ VARIABLES PROPERTIES ] = = =

	public	int		Experience
		{
			get { return experience; }
			set { experience = value; CheckLevelUp();}
		}

// = = =


// = = = [ CLASS METHODS ] = = =

	/// <summary>
	/// Checks if the experience value is high enough to get the character to the next experience level, and if the character havn't reach the maximum level already.
	/// Method launched on all "experience" variable set.
	/// </summary>
	public	void	CheckLevelUp()
	{
		if (Experience >= DataManager.instance.exp_levelup_table[exp_level+1] && exp_level+1 < DataManager.instance.exp_levelup_table.Length)
		{
			LevelUp();
		}

		return;
	}

	/// <summary>
	/// Makes the character gain an experience level. Update all necessary variable and data.
	/// </summary>
	public	void	LevelUp()
	{
		// increments level attribute
		exp_level += 1;

		// remove experience needed for the level from actual experience
		Experience -= DataManager.instance.exp_levelup_table[exp_level];

		// re-check if character gained enough experience to gain one more level after this one
		CheckLevelUp();
		return;
	}

// = = =

}
