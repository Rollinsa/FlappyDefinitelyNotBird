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
    [SerializeField] public float spawnDistance;
    [SerializeField] public float minDistanceBetween;
    [SerializeField] public float maxDistanceBetween;
    [SerializeField] public GameObject notBird;
    public List<GameObject> spawnedObjects;

    private float maxSpawnLocation;
    private float minSpawnLocation;

    private bool nextIsFloor;

    private GameObject prevWall;
    private Queue<GameObject> upcomingWalls;
    private int wallsPassed;

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

        upcomingWalls = new Queue<GameObject>();

        // First Wall
        createWall(startingX);
    }

    // Update is called once per frame
    void Update()
    {
        // Create next wall once we're close enough to prev wall
        // Creating this next wall should set a new prev wall, so the next check won't pass and will wait until we're closer as well
        if ((prevWall.transform.position.x - notBird.transform.position.x) < spawnDistance)
        {
            // TODO: distance between should really be based off of the height of each one
            createWall(prevWall.transform.position.x + Random.Range(minDistanceBetween, maxDistanceBetween));
        }

        checkPassedWall();

        destroyWalls();
    }

    GameObject createWall(float xPos)
    {
        GameObject clonedWall = getWallClone(xPos);
        clonedWall.SetActive(true);
        prevWall = clonedWall;
        upcomingWalls.Enqueue(clonedWall);
        spawnedObjects.Add(clonedWall);
        return clonedWall;
    }

    GameObject getWallClone(float xPos)
    {
        GameObject clonedWall = Instantiate(objectToSpawn, transform.position, transform.rotation);
        // TODO: Height randomization should depend on the previous wall, so that they overlap at least a little bit every time, but not too much
        float randomHeight = Random.Range(minHeight, maxHeight);
        clonedWall.transform.localScale = new Vector3(objectToSpawn.transform.localScale.x, randomHeight, 1);
        if (nextIsFloor)
        {
            clonedWall.transform.position = new Vector3(xPos, getYForFloorWall(clonedWall), -1);
        }
        else
        {
            clonedWall.transform.position = new Vector3(xPos, getYForCeilingWall(clonedWall), -1);
        }
        nextIsFloor = !nextIsFloor;
        return clonedWall;
    }

    // Get the Y location for the wall if it's coming from the floor
    float getYForFloorWall(GameObject wall)
    {
        return minSpawnLocation + (wall.transform.localScale.y / 2);
    }

    // Get the Y location for the wall if it's coming from the ceiling
    float getYForCeilingWall(GameObject wall)
    {
        return maxSpawnLocation - (wall.transform.localScale.y / 2);
    }

    void checkPassedWall()
    {
        if (upcomingWalls.Peek().transform.position.x < notBird.transform.position.x)
        {
            upcomingWalls.Dequeue();
            wallsPassed++;
        }
    }

    public int getWallsPassed()
    {
        return wallsPassed;
    }

    void destroyWalls()
    {
        // TODO: Destroy walls that are far enough away that they don't matter anymore

    }
}
