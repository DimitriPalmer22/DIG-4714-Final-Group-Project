using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    public GameObject tilePrefab; // tile
    public GameObject borderTilePrefab; // border reference in Inspector
    public GameObject obstacleTilePrefab; // reference to inspector to obstacle gameobject
    public GameObject startingPoint; // location of first tile for map generation

    // changeable map size & object amount
    public int mapSizeX = 16;
    public int mapSizeY = 16;
    public int objectDensity = 10;

    private GameObject _parentGameObject;

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        // Create a parent GameObject to hold all the tiles
        _parentGameObject = gameObject;
        
        // debug checking if Startposition GameObject is assigned
        if (startingPoint == null)
        {
            Debug.LogError("Starting point GameObject is not assigned.");
            return;
        }

        // Get starting point position from respective GameObject
        Vector3 startingPosition = startingPoint.transform.position;

        // Calculate loop boundaries based on starting position and map size
        int startX = Mathf.FloorToInt(startingPosition.x); // rounds to nearest "x" grid point
        int startY = Mathf.FloorToInt(startingPosition.y); // rounds to nearest "y" grid point
        int endX = startX + mapSizeX;
        int endY = startY + mapSizeY;

        // list storing obstacle locations
        List<Vector3> obstaclePositions = new List<Vector3>();

        /*
         loop for generating random position of obstacles withing map boundaries i.e startX endX
         Add obstacleTile to obstacle positions list
         Instantiate object at the random position
         */
        for (int i = 0; i < objectDensity; i++)
        {
            Vector3 randomPosition;
            do
            {
                randomPosition = new Vector3(Random.Range(startX, endX), Random.Range(startY, endY));
            } while (obstaclePositions.Contains(randomPosition));

            obstaclePositions.Add(randomPosition);
            var obj = Instantiate(obstacleTilePrefab, randomPosition, quaternion.identity);
            
            // Set the parent of the object to the parent GameObject
            obj.transform.SetParent(_parentGameObject.transform);
        }

        // Nested for loops for Width "x" and height "y" 
        for (int x = startX; x < endX; x++)
        {
            for (int y = startY; y < endY; y++)
            {
                Vector3 worldPosition = new Vector3(x, y, 0); // Calculate world position
                // checks for edge of map 
                if (x == startX || x == endX - 1 || y == startY || y == endY - 1)
                {
                    // instantiate border tile
                    var obj = Instantiate(borderTilePrefab, worldPosition, Quaternion.identity);
                    
                    obj.transform.SetParent(_parentGameObject.transform);
                }
                else if (!obstaclePositions.Contains(worldPosition))
                {
                    // instantiate floor tile if position is not occupied by an obstacles
                    var obj = Instantiate(tilePrefab, worldPosition, Quaternion.identity);
                    
                    obj.transform.SetParent(_parentGameObject.transform);
                }
            }
        }
    }
}