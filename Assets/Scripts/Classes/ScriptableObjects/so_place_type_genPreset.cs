using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
[CreateAssetMenu(fileName = "PlaceType_GenPreset", menuName = "Place/Type_genPreset", order = 2)]
public class so_place_type_genPreset : ScriptableObject  {

[Space(10)]
    public  strct_placeTypeXint[]              ChancesPerType;
[Space(5)]
    public  so_place_type                      errorType;
    // public  int                             type_city_chance;
    // public  int                             type_village_chance;
    // public  int                             type_cave_chance;
    // public  int                             type_ruins_chance;
    // public  int                             type_forest_chance;
    // public  int                             type_stronghold_chance;


    // TYPE GENERATION METHOD
    public  so_place_type    RandomType()
    {
        // int index               = 0;         // define which element is actually tested by randomChecker
        int randomChecker       = 0;           // manage the addition of the "chance" value of each type, which allow to check within which element range the randomNumber is. (It represent the lower value of the range to check for each index)
        int randomNumber        = 0;           // stock RNG number
        int totalRandomValue    = 0;           // addition of every ChancesPerType value
        so_place_type target_placeType = null;

        // Total Random Value calculation loop
        for (int i = 0; i < ChancesPerType.Length; i++)
        {
            totalRandomValue += ChancesPerType[i].value;
            // Debug.Log("actual total random " + totalRandomValue);
        }
        // Debug.Log("FINAL TOTAL RANDOM " + totalRandomValue);

        // Random Number Draw
        randomNumber = Random.Range(1, totalRandomValue);
        // Debug.Log("RANDOM NUMBER : " + randomNumber);

        // Random value Index Checker
        for (int i = 0; i < ChancesPerType.Length; i++)
        {
            // Debug.Log("New index : " + i);
            // Debug.Log("Actual range check : index" + i + " min" + (randomChecker+1) + " max" + (randomChecker + ChancesPerType[i].value) );
            
            if (randomNumber > randomChecker && randomNumber <= randomChecker + ChancesPerType[i].value)
            {
                // Debug.Log("RANDOM NUMBER FOUND IN INDEX " + i);
                target_placeType = ChancesPerType[i].key;
                break;
            } else {
                randomChecker += ChancesPerType[i].value;
                // index += 1;
                // Debug.Log("Random number not found in index " + (i) + ".");

                // Error report if random number isn't found even in the last Check Range
                if (i < ChancesPerType.Length -1 == false)
                {
                    Debug.LogWarning("ERROR !!! RANDOM NOT FOUND IN ANY RANGE CHECK !!! OUTPUT SET TO undefined!");
                    // index = -1;
                    target_placeType = errorType;
                }
            }
        }

        // Reset variables

        // Return index
        // Debug.Log("End of type random generation method. RETURN : " + target_placeType.name);
        return target_placeType;

    }

}
