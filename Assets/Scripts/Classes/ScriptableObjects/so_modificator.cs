using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class so_modificator : ScriptableObject {

[Header("Generate the ID from NAME")][Tooltip("Trigger the bool to generate the proper ID following the name of the file. In order to this to work, you have to write the ID you want before of the Filename and end it with the character '_' . For example, '132_exampleFile' will generate an ID of '132'")]
	public	bool	autoGenerate_id		= false;

[Space(10)][Header("Attributes")]
	public	int		id					;		// should also be put as a prefix in the name of the ScriptableObject Instance!
	public	string	mod_name			;
	public	string	mod_description		;

	public	virtual	void ConstructEffect()
	{
	}

	public	virtual	void DestructEffect()
	{

	}

	// INSPECTOR CALLBACKS

	// This method allow you to generate an ID following the name of your file. Well... yeah... it's not really that usefull but it's fun.
	void OnValidate()
	{
		// = = = = = AUTO GENERATE ID FROM NAME = = = = =
		if (autoGenerate_id == true)
		{
			string 	new_id			= null;
			bool	keyFound		= false;

			// INTERPRETER 
			// Read every character until it finds they "_" key character
			foreach (var item in name)
			{
				if (item == '_')
				{
					keyFound = true;
				} 

				// if the key char ISN'T found, continue to interpret the actual char, else break the loop and stop the reading
				if (keyFound == false)
				{
					// if the character found is a digit, add it to the new id 
					if (char.IsDigit(item) == true)
					{
						new_id += item.ToString();
					}

				} else break;
			}

			// if the reading end and a key character as been found, create the new ID, else there are an error!
			if (keyFound == true)
			{
				if (new_id != null)
				{
					id = int.Parse(new_id);
					Debug.Log("SUCCESS: ID '<b>" + new_id + "</b>' created from name!");
				} else {
					Debug.LogWarning("ERROR: No digits found in prefix");
				}

			} else {
				Debug.LogWarning("ERROR: KEY CHARACTER '_' NOT FOUND IN FILE NAME! You need to create a prefix with digit which end with the character '_' in the file name in order to autogenerate the ID!!!");
			}

			// Reset the Generate Trigger at the end
			autoGenerate_id = false;

		}

	}

}
