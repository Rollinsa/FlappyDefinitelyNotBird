using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] public GameObject objectToSpawn;
    [SerializeField] public GameObject floor;
    [SerializeField] public GameObject ceiling;
    [SerializeField] public float maxHeight;
    [SerializeField] public float minHeight;
    [SerializeField] public float startingX;
    public GameObject[] spawnedObjects;

    private float maxSpawnLocation;
    private float minSpawnLocation;

    private bool nextIsFloor;

    private GameObject prevWall;

    void Awake()
    {
        // maxSpawnLocation = ceiling.transform.position.y - (ceiling.GetComponent<RectTransform>().rect.width / 2);
        // Using x on the scale for the ceiling because it's just a wall rotated 90 degrees
        maxSpawnLocation = ceiling.transform.position.y - (ceiling.transform.localScale.x / 2);
        minSpawnLocation = floor.transform.position.y + (floor.transform.localScale.y / 2);
        print("max spawn location");
        print(maxSpawnLocation);
        print("min spawn location");
        print(minSpawnLocation);

        // First Wall
        createWall(startingX);
    }

    // Update is called once per frame
    void Update()
    {
        if (false)
        {
            Instantiate(objectToSpawn, transform.position, transform.rotation);
        }
    }

    GameObject createWall(float xPos)
    {
        GameObject clonedWall = getWallClone(xPos);
        clonedWall.SetActive(true);
        prevWall = clonedWall;
        return clonedWall;
    }

    GameObject getWallClone(float xPos)
    {
        GameObject clonedWall = Instantiate(objectToSpawn, transform.position, transform.rotation);
        float randomHeight = Random.Range(minHeight, maxHeight);
        clonedWall.transform.localScale = new Vector3(objectToSpawn.transform.localScale.x, randomHeight, 1);
        clonedWall.transform.position = new Vector3(xPos, getYForWall(clonedWall), -1);
        return clonedWall;
    }

    // Get the Y location for the wall
    float getYForWall(GameObject wall)
    {
        return maxSpawnLocation - (wall.transform.localScale.y / 2);
    }
}
