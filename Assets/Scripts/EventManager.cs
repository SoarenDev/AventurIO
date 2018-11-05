using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

public static EventManager instance;		
	
[Space(10)][Header("CONTENT")]
	// EVENTS
	private	List<cl_event>				event_list					= new List<cl_event>		// only here to generate the Dictionary on start
	{
		new event_0000(),
		new event_0001(),
		new event_0002(),
		new event_0003(),
		new event_0004(),
		new event_0005(),
		new event_0020(),
		new event_0021(),
		new event_0022(),
		new event_0023(),
		new event_0024(),
		new event_0401(),
		new event_0402(),
		new event_0403(),
		new event_0404(),
		new event_1000(),
		new event_1001(),
		new event_1002(),
		new event_1003(),
	};
	public	Dictionary<int, cl_event> 	event_Dict 		= new 	Dictionary<int, cl_event>();

	void Awake () 
	{
		if (instance == null)
		{
			instance = this;
		} else {
			Destroy(this);
		}
	}

	void Start () 
	{

	// = = GENERATE DICTIONARIES = =
		
		// events
		foreach (var item in event_list)
		{ if (item != null) { event_Dict.Add(item.event_id, item); } }
		Debug.Log("event dictionary created with " + event_Dict.Count + " references!");

	// = =
		
	}
	
	void Update () 
	{
	}

	/// <summary>
	/// Launch the event associated with the given index. If this event is a PERSISTANT event, it is Downcasted to a cl_event_persistant, and then instantiate itself.
	/// </summary>
	public void CallEvent(struct_event_indexXowner event_strcture)
	{
		if (event_Dict[event_strcture.event_index].isPersistant == false)	// if the event is persistant, we Instatiate it and then launch it from the instance, else we just launch it from the reference
		{
			cl_event source_event;
			cl_event instance;

			source_event = event_Dict[event_strcture.event_index];
			instance = source_event.InstantiateEvent(event_strcture.event_place);			// Instantiate the source event (from Dictionary) to a NEW inherited class cl_event
			instance.LaunchEvent(event_strcture.event_place, event_strcture.event_faction);	// Launch the newly Instantiate event
		}
		else
		{
			cl_event source_event;
			cl_event instance;
			// cl_event_persistant downcasted_instance;

			source_event = event_Dict[event_strcture.event_index];
			instance = source_event.InstantiateEvent(event_strcture.event_place);			// Instantiate the downcasted source event (from Dictionary) to a NEW inherited class cl_event
		 	// downcasted_instance = (cl_event_persistant)instance;
			// downcasted_instance.AnchorPersistant(event_strcture.event_place);
			instance.LaunchEvent(event_strcture.event_place, event_strcture.event_faction);	// Launch the newly Instantiate event

		}
	}
}
