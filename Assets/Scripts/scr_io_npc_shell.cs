using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_io_npc_shell : scr_InteractibleObjectParentScript 
{

// = = = [ VARIABLES DEFINITION ] = = =

// = = =

// = = = [ MONOBEHAVIOR METHODS ] = = =

	void Start () 
	{

	}

// = = =

// = = = [ CLASS METHODS ] = = =
	
	public override void InRange()
	{
		// Debug.Log("Recieved IN RANGE in " + gameObject.name);
	}

	public override void Interaction()
	{
		Debug.Log("Recieved INTERACTION in " + gameObject.name);

		return;
	}

	public override void OutRange()
	{
		// Debug.Log("Recieved OUT RANGE in " + gameObject.name);

		return;
	}

// = = =


}
