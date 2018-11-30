using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_damageCollider : MonoBehaviour 
{
// = = = [ CONSTRUCTOR ] = = =

// = = =

// = = = [ VARIABLES DEFINITION ] = = =

[Space(10)][Header("Data")]
	public		GameObject				instigator						;
	[Space(5)]
	public		int						damage_value					;
	public		enum_skill_element		element							;
	public  	bool                    can_interrupt                   ;

// = = =

// = = = [ MONOBEHAVIOR METHODS ] = = =

	void Start()
	{
		Destroy(this.gameObject, 0.01f);

		return;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "npc" || other.gameObject.tag == "Player" && other.gameObject != instigator)
		{
			Debug.Log("damageCollider has sensed matching collision!");
			other.gameObject.SendMessage("TakeDamageFromCollider", this);
		}

		return;
	}

// = = =

// = = = [ CLASS METHODS ] = = =

	public void Initialize(GameObject skill_instigator, cl_skill source_skill)
	{
		instigator 		= skill_instigator.gameObject;
		damage_value 	= source_skill.damage_amount;
		element			= source_skill.element;
		can_interrupt	= source_skill.can_interrupt;

		Debug.Log("Damage collider initialized");
		return;
	}

// = = =

}
