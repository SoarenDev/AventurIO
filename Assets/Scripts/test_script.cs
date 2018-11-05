using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class test_script : MonoBehaviour {

	public	DataManager dataManager;
	public	cl_npc 	test_npc = new cl_npc();
	public	test_npc_strct	test_strct;

	void Start()
	{
		dataManager.npc_list.Add(test_npc);

		test_npc.npc_age = 66;
		test_strct = new test_npc_strct(){strct_npc = test_npc};

		test_npc.npc_age = 99;

		Debug.Log("local npc age: " + test_npc.npc_age);
		Debug.Log("structure npc age: " + test_strct.strct_npc.npc_age);
	}
}
