using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_InteractionController : MonoBehaviour 
{

	public	List<GameObject>	IO_in_range			= new List<GameObject>();		// interactible objects in range
	public	int					targeted_IO_index 	= -1;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "npc" || other.gameObject.tag == "place" || other.gameObject.tag == "io")
		{
			// activate UI element if there are not any IO already in range when we add one
			if (IO_in_range.Count == 0) { GameManager.instance.ui_interact_image.SetActive(true); }

			// add element
			IO_in_range.Add(other.gameObject);
			// other.gameObject.SendMessage("InRange");
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "npc" || other.gameObject.tag == "place" || other.gameObject.tag == "io")
		{
			IO_in_range.Remove(other.gameObject);
			// other.gameObject.SendMessage("OutRange");

			// desactivate UI element if there no more IO in range when we remove one
			if (IO_in_range.Count == 0) { GameManager.instance.ui_interact_image.SetActive(false); }
		}
	}

	void Start () 
	{
		
	}
	
	void Update () 
	{
		// Always select the closest IO among those in range
		if (IO_in_range.Count > 0)
		{
			targeted_IO_index = FindCloserElementIndex();

		} 
		else { targeted_IO_index = -1; }	// if there no IO in range, set target to -1

		// INTERACTION INPUT
		if (Input.GetKeyDown(KeyCode.E) && targeted_IO_index != -1)
		{
			// Debug.Log("Trigger Interaction -> index: " + targeted_IO_index);
			IO_in_range[targeted_IO_index].SendMessage("Interaction");
		}

	}

	int	FindCloserElementIndex()
	{
		int actualCloser_index = 0;
		for (int i = 1; i < IO_in_range.Count; i++)
		{
			float saved_element_distance 	= 	Mathf.Abs(IO_in_range[actualCloser_index].transform.position.x - gameObject.transform.position.x) 	+ 	Mathf.Abs(IO_in_range[actualCloser_index].transform.position.y - gameObject.transform.position.y);
			float actual_element_distance 	= 	Mathf.Abs(IO_in_range[i].transform.position.x - gameObject.transform.position.x) 					+ 	Mathf.Abs(IO_in_range[i].transform.position.y - gameObject.transform.position.y);
			
			if (actual_element_distance < saved_element_distance)
			{
				actualCloser_index = i;
			}
		}
		return actualCloser_index;
	}

	/// <summary>
	/// Instantly clears the IO list. 
	/// </summary>
	public void ClearInrangeList()
	{
		// clear the list
		IO_in_range.Clear();

		// force disable the interaction indicator
		GameManager.instance.ui_interact_image.SetActive(false);

		return;
	}
}
