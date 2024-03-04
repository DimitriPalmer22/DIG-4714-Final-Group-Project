using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public GameObject tilePrefab; //tile/floor reference in Inspector
    public GameObject borderTilePrefab;   //border reference in Inspector
    public Tilemap tilemap; //reference to tilemap
    
    public GameObject startingPoint; 
    
    //changeable map size 
    public int mapSizeX = 16; 
    public int mapSizeY = 16; 

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        //debug checking if Startposition GameObject is assigned
        if (startingPoint == null)
        {
            Debug.LogError("Starting point GameObject is not assigned.");
            return;
        }
        // list storing transform of tileposition

        // Get starting point position
        Vector3 startingPosition = startingPoint.transform.position;

        // Calculate loop boundaries based on starting position and map size
        // using Mathf.floorToInt to start on a grid cell 
        int startX = Mathf.FloorToInt(startingPosition.x);
        int startY = Mathf.FloorToInt(startingPosition.y);
        int endX = startX + mapSizeX;
        int endY = startY + mapSizeY;

        //Get center offset of tilemap
        Vector3 cellSize = tilemap.cellSize;
        Vector3 cellCenterOffset = new Vector3(cellSize.x / 2f, cellSize.y / 2f, 0f);
        

        //the loops used to create square map
        for (int x = startX; x < endX; x++)
        {
            for (int y = startY; y < endY; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);

                Vector3 worldPosition = tilemap.CellToWorld(tilePosition) + cellCenterOffset; 

                if (x == startX || x == endX - 1 || y == startY || y == endY - 1)
                {
                    //instantiate border tile
                    GameObject borderTile = Instantiate(borderTilePrefab, worldPosition, Quaternion.identity);
                    borderTile.transform.parent = tilemap.transform;
                }
                else
                {
                    //instantiate floortile
                    GameObject newTile = Instantiate(tilePrefab, worldPosition, Quaternion.identity);
                    newTile.transform.parent = tilemap.transform;
                }
            }
            
        }
    }
}
