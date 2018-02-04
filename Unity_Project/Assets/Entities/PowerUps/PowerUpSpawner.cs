using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour {
    public GameObject[] powerupPrefabs;
    public float width = 10f;
    public float height = 10f;
    public float depth = 10f;
    public float spawnDelay;

    private BrawlSettings brawlSettings;

    // Use this for initialization
    private void Awake()
    {
        brawlSettings = GameObject.Find("BrawlSettings").GetComponent<BrawlSettings>();
    }
    private void Start()
    {
        int itemRate = brawlSettings.GetItemRate();
        if(itemRate == 0)
        {
             spawnDelay = 30f;
        }
        else if(itemRate == 1)
        {
               spawnDelay = 15f;
        }  
        else
        {
               spawnDelay = 5f;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0f));
    }

    // Update is called once per frame
    void Update()
    {

        if (AllMembersDead())
        {
            EnemySpawn();
        }
    }
    // Function for checking if all enemies are dead
    bool AllMembersDead()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount > 0)
            {
                return false;
            }
        }

        return true;
    }

    // Enemy spawner function

    void EnemySpawn()
    {
        int powerNumber = Random.Range(0, powerupPrefabs.Length);
        Transform nextPos = NextFreePosition();
        if (nextPos)
        {
            GameObject powerUp = Instantiate(powerupPrefabs[powerNumber], nextPos.position, Quaternion.identity) as GameObject;
            powerUp.transform.parent = nextPos;
        }
        if (NextFreePosition())
        {
            Invoke("EnemySpawn", spawnDelay);
        }
    }

    // Next Free Position
    Transform NextFreePosition()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount == 0)
            {
                return childPositionGameObject;
            }
        }

        return null;
    }
}

