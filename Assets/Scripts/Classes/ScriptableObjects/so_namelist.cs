using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Namelist", menuName = "Namelist", order = 1)]
public class so_namelist : ScriptableObject  {

    public List<string> main_names          = new List<string>(){"undefined"};
    public List<string> secondary_names     = new List<string>(){"undefined"};      // for family name, or other name pool
    public List<string> third_names         = new List<string>(){"undefined"};      // for aliases, pesudonym...

}
