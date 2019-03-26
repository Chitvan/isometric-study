using System.Collections;
using UnityEngine;

public enum eTileType 
{
	Dirt,
	Grass,
	Stone,
	Wood
}

public class TileInfo : MonoBehaviour 
{
	public eTileType TileType { get; private set; }
	public int TilePosX { get; private set; }
	public int TilePosY { get; private set; }
	public bool IsEmpty { get; set; }


	public void SetInfo(eTileType type, int x, int y)
	{
		TileType = type;
		TilePosX = x;
		TilePosY = y;
		IsEmpty = true;
	}

	public string GetId()
	{
		return GetIdWithPosition(TilePosX, TilePosY);
	}

	public static string GetIdWithPosition(int x, int y)
	{
		return string.Format("{0}_{1}", x, y);
	}

		
}
