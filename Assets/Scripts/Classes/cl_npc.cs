using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class cl_npc {

public 	enum 	npcRaceEnum
{
    Aboriginal,
    Colon

} 

[Header("ID")]
    public  int                                 npc_id;                                         // link the npc to his index in the data manager's npc_list

[Space(10)][Header("Data")]
    public  bool                                isDead              ;
    public  bool                                killed_by_player    ;                           // set to true if the charcter is considered as killed by the player (used for quest completion check)

[Space(10)][Header("Attributes")]
    public  string                              npc_firstname       = "undefined";
    public  string                              npc_lastname        = "undefined";
    public  int                                 npc_age             = 30;                       // for now, everyone gain 1 year together // may not be used
    public  int                                 npc_gender          = 0;                        // 0 = male, 1 = female
    public  npcRaceEnum                         npc_race            = npcRaceEnum.Aboriginal;        // for now use an enum, may use a global indexed reference in the future 
    public  so_npc_situation                    npc_situation       ;                           // contain differents data for npc behavior. These data will be processed in a behavior script in the obj_npc (not here!)
    public  struct_faction_memberInfos          npc_faction         ; 
    [Space(5)]
    public  bool                                isLocked            ;                           // if "isBusy" = true, the npc can't be selected by events that will, for exemple, kill or move him.

// [Space(10)][Header("References")]
    // public  so_namelist         npc_namelist        ;       

//

    /// <summary>
    /// Clears NPC's faction and rank informations.
    /// </summary>
    public void ClearFaction()
    {
        // clear npc from faction npc list
        DataManager.instance.faction_list[npc_faction.local_faction.faction.faction_id].faction_npcs.Remove(this);

        // clear faction from npc
        npc_faction.local_faction = null;
        npc_faction.rank = 0;

        return;
    }

}
