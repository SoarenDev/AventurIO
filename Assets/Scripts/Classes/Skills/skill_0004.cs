using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class skill_0004 : cl_skill 
{

// = = = [ CONTRUCTOR ] = = =

    public skill_0004()
    {
        id                              = 4;
        name                            = "Ultimate attack";
        stamina_cost                    = 20;
        damage_amount                   = 40;
        element                         = enum_skill_element.none;

        damage_collider_index           = 0;
        can_be_interrupted              = false;
        can_interrupt                   = false;
    }

// = = =

// = = = [ VARIABLES DEFINITION ] = = =

// = = =


// = = = [ CLASS METHODS ] = = =

    public override IEnumerator LaunchSkill(GameObject instigator)
    {
        SpawnDamageCollider(instigator);

        Debug.Log("SKILL: <b>" + name + "</b> launched!");

        yield return null;
    }

// = = =
	
}
