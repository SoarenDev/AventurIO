using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enum_weaponType
{
    sword,
    axe,
    hammer,
    spear
}

[CreateAssetMenu(fileName = "so_weapon", menuName = "Weapon", order = 1)]
public class so_weapon : ScriptableObject 
{

// = = = [ VARIABLES DEFINITION ] = = =

[Space(10)][Header("Data")]
    public  enum_weaponType         type                                ;    
    public  int                     price                               ;

[Space(10)][Header("Skills")]
    public  int[]                   skill_base_index                    ;
    public  int[]                   skill_secondary_index               ;
    public  int[]                   skill_special_1_index               ;
    public  int[]                   skill_special_2_index               ;
    public  int[]                   skill_ultimate_index                ;

// = = =


// = = = [ CLASS METHODS ] = = =

// = = =


}
