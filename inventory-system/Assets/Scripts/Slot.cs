using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class Slot : MonoBehaviour, IDropHandler
{
	public int id;
	private Inventory inv;

	void Start()
	{
		inv = GameObject.Find("Inventory").GetComponent<Inventory>();
	}

	public void OnDrop(PointerEventData eventData)
	{
		ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
		if (inv.items[id].Id == -1)
		{
			inv.items[droppedItem.slotId] = new Item();
			inv.items[id] = droppedItem.item;
			droppedItem.slotId = id;
		}
		else if(droppedItem.slotId != id)
		{
			Transform item = this.transform.GetChild(0);
			item.GetComponent<ItemData>().slotId = droppedItem.slotId;
			item.transform.SetParent(inv.slots[droppedItem.slotId].transform);
			item.transform.position = inv.slots[droppedItem.slotId].transform.position;

			droppedItem.slotId = id;
			droppedItem.transform.SetParent(this.transform);
			droppedItem.transform.position = this.transform.position;

			inv.items[droppedItem.slotId] = item.GetComponent<ItemData>().item;
			inv.items[id] = droppedItem.item;
		}
	}
}