using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class skill_0000 : cl_skill 
{

// = = = [ CONTRUCTOR ] = = =

    public skill_0000()
    {
        id                              = 0;
        name                            = "Default skill";
        stamina_cost                    = 1;
        damage_amount                   = 1;
        element                         = enum_skill_element.none;

        damage_collider_index           = 0;
        can_be_interrupted              = false;
        can_interrupt                   = false;
    }

// = = =

// = = = [ VARIABLES DEFINITION ] = = =

// = = =


// = = = [ CLASS METHODS ] = = =

    public override IEnumerator LaunchSkill(scr_battle_script instigator)
    {
        // START SKILL
        instigator.is_attacking = true;

        // SKILL MOVEMENT
        SkillMovement(instigator);

        // SPAWN COLLIDER
        SpawnDamageCollider(instigator.gameObject);

        Debug.Log("SKILL: <b>" + name + "</b> launched!");

        // END SKILL
        instigator.is_attacking = false;
        yield return null;
    }

    public override void SkillMovement(scr_battle_script instigator)
    {
        // none
        return;
    }

// = = =
	
}
