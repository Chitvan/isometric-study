using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class TileMapGenerator : MonoBehaviour 
{
	private string _jsonFilePath = "Data/data.json";
	private Vector3 _isometricRotation = new Vector3 (60, 0, 45);

	public GameObject tilePrefab;
	public Sprite[] tilesSprites;

	void Start () 
	{
		if (tilePrefab!=null) 
		{
			GenerateMap ();
		}
	}
	
	void GenerateMap()
	{
		string path = Path.Combine (Application.streamingAssetsPath, _jsonFilePath);
		if (File.Exists (path)) 
		{
			string fileData = File.ReadAllText (path);
			TerrainInfo terrain = JsonConvert.DeserializeObject<TerrainInfo> (fileData);
			if (terrain != null) 
			{
				TileMapHelper tileMapHelper = GetComponent<TileMapHelper> (); 
				tileMapHelper.InitMap ();

				for (int i = 0; i < terrain.TerrainGrid.GetLength (0); i++) 
				{
					for (int j = 0; j < terrain.TerrainGrid [i].Length; j++) 
					{
						TileData tile = terrain.TerrainGrid [i] [j];
						GameObject tileObject = GameObject.Instantiate (tilePrefab, 
																		new Vector3 (i, j, 0), 
																		tilePrefab.transform.rotation);
							               
						SpriteRenderer spriteRenderer = tileObject.GetComponent<SpriteRenderer>();
						if (spriteRenderer != null && tilesSprites.Length>tile.TileType) 
						{
							spriteRenderer.sprite = tilesSprites [tile.TileType];
						}
						
						tileObject.transform.SetParent (this.transform);

						TileInfo tileInfo = tileObject.AddComponent<TileInfo>();
						tileInfo.SetInfo ((eTileType)tile.TileType, i, j);
						tileObject.name = tileInfo.GetId ();

						// add tile to local tilemap
						tileMapHelper.AddTile (tileInfo.GetId (), tileInfo);
					}
				}

				//apply isometric rotation to map tiles
				this.transform.rotation = Quaternion.Euler (_isometricRotation);
			}
		} 
		else 
		{
			Debug.Log ("TileMapGenerator:GenerateMap Cant load json file");
		}		
	}


}
