using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
public class event_0000 : cl_event {

// = = = [ CONSTRUCTORS ] = = =

    public event_0000()
    {
        event_id                    = 0;
        isPersistant                = false;        // a persistant event will instantiate himself and stay active as a modificator untill another event suppress it

        event_probability           = 100;

        event_icon_index            = 0;
        event_name                  = "default test event";
        event_description           = 
        "xoxo"
        ;

        event_origin_place          = null;
        event_origin_faction        = null;

        //$param+1
        //$param+2
        //$param+3
        //$param+4
        //$param+5
        //$param+6
        //$param+7
        //$param+8
        //$param+9
        //$param+10
    }

// = = =

// = = = [ VARIABLES DEFINITION ] = = =


// = = =


    public override cl_event	InstantiateEvent(scr_place targeted_place)
	{
        cl_event instance = new event_0000();    // create an instance of himself into the region's ongoing events

        base.InstantiateEvent(targeted_place);
		return instance;		// return the reference of the instance created
    }

    public override void LaunchEvent(scr_place targeted_place, strct_local_faction targeted_faction)
    {
        // BASE LaunchEvent First
        base.LaunchEvent(targeted_place, targeted_faction);

        // then Child launchEvent
        Debug.Log(">>>>> TEST EVENT LAUNCH SUCESS!!! <<<<<");
	}

}
