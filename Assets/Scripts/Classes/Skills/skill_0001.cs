using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class skill_0001 : cl_skill 
{

// = = = [ CONTRUCTOR ] = = =

    public skill_0001()
    {
        id                              = 1;
        name                            = "Simple attack";
        stamina_cost                    = 5;
        damage_amount                   = 10;
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

        // get action direction
        Vector2 action_direction = instigator.GetCharacterActionDirection(enum_character_type.player);

        // SKILL MOVEMENT
        SkillMovement(instigator, action_direction);

        // SPAWN COLLIDER
        SpawnDamageCollider(instigator.gameObject, action_direction);

        Debug.Log("SKILL: <b>" + name + "</b> launched!");
        yield return new WaitForSeconds(1f);

        // END SKILL
        instigator.is_attacking = false;
        yield return null;
    }

    public override void SkillMovement(scr_battle_script instigator, Vector2 direction_vector)
    {
        instigator.transform.Translate(new Vector2(0.10f * direction_vector.x, 0.10f * direction_vector.y));
        return;
    }

// = = =
	
}
