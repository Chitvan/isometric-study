using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceItem : MonoBehaviour 
{
	public GameObject currentItem ;

	void Update () 
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			Vector3 mouseClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Collider2D tileCollider = Physics2D.OverlapPoint(mouseClickPos);
			if (tileCollider) 
			{
				if (tileCollider.gameObject.GetComponent<TileInfo>() != null) 
				{
					TileInfo selectedTile = tileCollider.GetComponent<TileInfo> ();
					if (selectedTile.IsEmpty && selectedTile.TileType == eTileType.Wood && currentItem!= null) 
					{
						TileMapHelper tileMapHelper = GetComponent<TileMapHelper> ();
						tileMapHelper.AddItemToTile(selectedTile.GetId(), currentItem);
					}
				}
			}
		}		
	}
}
