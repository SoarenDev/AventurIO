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
    public  GameObject              damage_collider                 ;
    public  bool                    can_be_interrupted              ;
    public  bool                    can_interrupt                   ;

// = = =


// = = = [ CLASS METHODS ] = = =

    /// <summary>
    /// [PARENT] Launches the skill's script.
    /// </summary>
    public virtual void LaunchSkill()
    {
        // Debug.Log("Parent LaunchSkill launched!");
        return;
    }

// = = =
	
}
