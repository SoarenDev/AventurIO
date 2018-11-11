using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_InteractibleObjectParentScript : MonoBehaviour {
// PARENT CLASS FOR EVERY INTERACTIBLE OBJECT SCRIPT

	public	bool	is_tab_on		;		// true when the IO is linked to an active tab (allow the script to control when to close the right tab)

	void Start () 
	{
	}
	
	public virtual void InRange()
	{
		Debug.Log("Recieved IN RANGE in " + gameObject.name);
	}

	public virtual void Interaction()
	{
		Debug.Log("Recieved INTERACTION in " + gameObject.name);
	}

	public virtual void OutRange()
	{
		Debug.Log("Recieved OUT RANGE in " + gameObject.name);
	}

}
