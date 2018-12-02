using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class skill_0002 : cl_skill 
{

// = = = [ CONTRUCTOR ] = = =

    public skill_0002()
    {
        id                              = 2;
        name                            = "Large impact";
        stamina_cost                    = 10;
        damage_amount                   = 15;
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
        yield return new WaitForSeconds(2f);

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
