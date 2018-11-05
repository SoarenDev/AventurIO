using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_terrainCase : MonoBehaviour {

	public enum terrain_type_enum
	{
		ground,
		ocean,
		place,
		undefined
	}

	public terrain_type_enum 	terrain_type;

	public int 					linked_region;
	
}
