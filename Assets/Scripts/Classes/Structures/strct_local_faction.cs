using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class strct_local_faction {

// This structure allow to keep a faction index information along with differents info such as an influence value, usefull to keep track of it's power in a place

	public cl_faction 	faction		;
	public int			influence	;
	public int			local_size	;

}
