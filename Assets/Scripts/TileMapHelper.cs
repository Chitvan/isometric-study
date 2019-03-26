using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******** 
 * Class which stores map tiles
*********/

public class TileMapHelper : MonoBehaviour 
{
	private Dictionary<string, TileInfo> _map;

	public void InitMap()
	{
		_map = new Dictionary<string, TileInfo> ();
	}

	public void AddTile(string tileId, TileInfo tile)
	{
		if (!_map.ContainsKey (tileId)) 
		{
			_map.Add (tileId, tile);
		} 
		else 
		{
			Debug.Log ("TileMapHelper:AddTile:: Attempt to add existing tile in map");
		}
	}

	public void AddItemToTile(string searchTileId, GameObject item) 
	{
		if (_map.ContainsKey (searchTileId)) 
		{
			TileInfo tileInfo = _map [searchTileId];
			int xPos = tileInfo.TilePosX;
			int yPos = tileInfo.TilePosY;


			if (_map.ContainsKey(TileInfo.GetIdWithPosition(xPos + 1, yPos)) ) 
			{
				TileInfo rightNeighbor = _map [TileInfo.GetIdWithPosition(xPos + 1, yPos)];
				if (rightNeighbor.IsEmpty && tileInfo.TileType == rightNeighbor.TileType) 
				{
					CreateItem (item, tileInfo, rightNeighbor, true);
					return;
				}
			}

			if (_map.ContainsKey(TileInfo.GetIdWithPosition(xPos, yPos+1))) 
			{
				TileInfo topNeighbor = _map [TileInfo.GetIdWithPosition(xPos, yPos+1)];
				if (topNeighbor.IsEmpty && tileInfo.TileType == topNeighbor.TileType) 
				{
					CreateItem (item, topNeighbor, tileInfo, false);
					return;
				}
			}

			if (_map.ContainsKey(TileInfo.GetIdWithPosition(xPos - 1, yPos))) 
			{
				TileInfo leftNeighbor = _map [TileInfo.GetIdWithPosition(xPos - 1, yPos)];
				if (leftNeighbor.IsEmpty && tileInfo.TileType == leftNeighbor.TileType) 
				{
					CreateItem (item, leftNeighbor, tileInfo, true);
					return;
				}
			}

			if (_map.ContainsKey(TileInfo.GetIdWithPosition(xPos, yPos-1))) 
			{
				TileInfo bottomNeighbor = _map [TileInfo.GetIdWithPosition(xPos, yPos-1)];
				if (bottomNeighbor.IsEmpty && tileInfo.TileType == bottomNeighbor.TileType) 
				{
					CreateItem (item, tileInfo, bottomNeighbor, false);
					return;
				}
			}
		}
		else 
		{
			Debug.Log ("TileMapHelper:AddItemToTile:: Map doesnt contain searched Tile");
		}	
	}

	void CreateItem(GameObject itemToInstantiate, TileInfo parent, TileInfo neighbor, bool isHorizontal)
	{
		GameObject itemGameObject = GameObject.Instantiate (itemToInstantiate);
		if (itemGameObject != null)
		{
			itemGameObject.transform.SetParent(parent.transform);
			itemGameObject.transform.localPosition = Vector3.zero;
			AlignItem alignItem = itemGameObject.GetComponent<AlignItem>();
			if (alignItem != null)
			{
				alignItem.ShouldAlignHorizontally(isHorizontal);
			}

			parent.IsEmpty = false;
			neighbor.IsEmpty = false;
		}
	}
		
}
