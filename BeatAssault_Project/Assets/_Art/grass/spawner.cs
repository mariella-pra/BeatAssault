using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{

    public GameObject grassPrefab; // Grass prefab to be spawned
    public int numberOfGrasses = 10; // Number of grass objects to spawn
    public Vector2 xRange = new Vector2(-5f, 5f); // Range for random X positions along the mesh
    public Vector2 zRange = new Vector2(-5f, 5f); // Range for random Z positions along the mesh
     Vector2 Range = new Vector2(-1.2f, 1.2f); // Range for random X positions along the mesh
    // Vector2 zRange2 = new Vector2(-1.5f, 1.5f);
    public LayerMask terrainLayer; // The layer the terrain mesh belongs to (used for raycasting)

    private void Start()
    {
        // Check if the grassPrefab and terrainLayer are set
        if (grassPrefab == null)
        {
            Debug.LogError("Grass prefab is not assigned!");
            return;
        }
        if (terrainLayer == 0)
        {
            Debug.LogError("Terrain Layer is not assigned!");
            return;
        }

        SpawnGrassOnMesh();
    }

    void SpawnGrassOnMesh()
    {
        for (int i = 0; i < numberOfGrasses; i++)
        {
            // Randomize X and Z positions within the defined range
            float xPosition = Random.Range(xRange.x, xRange.y);
            float zPosition = Random.Range(zRange.x, zRange.y);

            // Cast a ray from above the surface to the mesh
            Vector3 rayOrigin = new Vector3(transform.position.x + xPosition, transform.position.y, transform.position.z + zPosition);
            RaycastHit hit;

            if (Physics.Raycast(rayOrigin, Vector3.down, out hit, Mathf.Infinity, terrainLayer))
            {
                // Instantiate the grass at the hit point on the mesh
                Vector3 spawnPosition = hit.point;
                RandomPos(spawnPosition);
                RandomPos(spawnPosition);
                RandomPos(spawnPosition);
                RandomPos(spawnPosition);
                RandomPos(spawnPosition);

                //Instantiate(grassPrefab, new Vector3(spawnPosition.x + 0.35f, spawnPosition.y + 0.1f, spawnPosition.z + 0.3f), Quaternion.EulerAngles(0, Random.Range(0, 360), 0), transform);
                i += 4;
            }
            else
            {
                i--;
            }
        }
        void RandomPos(Vector3 spawnPosition)
        {
            float xPosition = Random.Range(Range.x, Range.y);
            float zPosition = Random.Range(Range.x, Range.y);
            Instantiate(grassPrefab, new Vector3(spawnPosition.x + xPosition, spawnPosition.y + 0.1f, spawnPosition.z + zPosition), Quaternion.EulerAngles(0, Random.Range(0, 360), 0), transform);


        }
    }
}
