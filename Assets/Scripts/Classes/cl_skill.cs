using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enum_skill_element
{
    none,
    fire,
    ice,
    thunder,
    water,
    blood
}

public class cl_skill 
{

// = = = [ VARIABLES DEFINITION ] = = =

[Space(10)][Header("Data")]
    public  int                     id                              ;
    [Space(5)]
    public  string                  name                            ;
    public  int                     stamina_cost                    ;
    public  int                     damage_amount                   ;
    public  enum_skill_element      element                         ;

[Space(10)][Header("Attributes")]
    public  int                     damage_collider_index           ;
    public  bool                    can_be_interrupted              ;
    public  bool                    can_interrupt                   ;

// = = =


// = = = [ CLASS METHODS ] = = =

    /// <summary>
    /// [PARENT] Launches the skill's script.
    /// </summary>
    public virtual IEnumerator LaunchSkill(scr_battle_script instigator)
    {
        yield return null;
    }

    /// <summary>
    /// [PARENT] Spawns and initializes the damage collider.
    /// </summary>
    public void SpawnDamageCollider(GameObject instigator)
    {
        GameObject damageCollider;
        scr_damageCollider damageCollider_script;

        damageCollider = Object.Instantiate(DataManager.instance.damageCollider_prefab_list[damage_collider_index], new Vector2 (instigator.transform.position.x, instigator.transform.position.y + 0.1f), Quaternion.identity);
        damageCollider_script = damageCollider.GetComponent<scr_damageCollider>();
        damageCollider_script.Initialize(instigator, this);
        // Debug.Log("Parent LaunchSkill launched!");

        return;
    }

    /// <summary>
    /// [PARENT] Forces a defined movement to the instigator. 
    /// </summary>
    public virtual void SkillMovement(scr_battle_script instigator){}

// = = =
	
}
