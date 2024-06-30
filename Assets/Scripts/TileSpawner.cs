using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    DungeonManager dungman;

    void Awake()
    {
        dungman = FindObjectOfType<DungeonManager>();
        GameObject goFloor = Instantiate(dungman.floorPrefab, transform.position, Quaternion.identity) as GameObject;
        goFloor.name = dungman.floorPrefab.name;
        goFloor.transform.SetParent(dungman.transform);
        if (transform.position.x > dungman.maxX)
        {
            dungman.maxX = transform.position.x;
        }
        if (transform.position.x < dungman.minX)
        {
            dungman.minX = transform.position.x;
        }
        if (transform.position.y > dungman.maxY)
        {
            dungman.maxY = transform.position.y;
        }
        if (transform.position.y < dungman.minY)
        {
            dungman.minY = transform.position.y; // Исправлено
        }
    }

    void Start()
    {
        LayerMask envMask = LayerMask.GetMask("Wall", "Floor");
        Vector2 hitSize = Vector2.one * 0.8f;
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector2 targetPos = new Vector2(transform.position.x + x, transform.position.y + y);
                Collider2D hit = Physics2D.OverlapBox(targetPos, hitSize, 0, envMask);
                if (!hit)
                {
                    GameObject goWall = Instantiate(dungman.wallPrefab, targetPos, Quaternion.identity) as GameObject;
                    goWall.name = dungman.wallPrefab.name;
                    goWall.transform.SetParent(dungman.transform);
                }
            }
        }
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(transform.position, Vector3.one);
    }
}
