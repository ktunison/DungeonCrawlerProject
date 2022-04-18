using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpawner : MonoBehaviour
{
    [Header("Projectile Variables")]
    public bool goingLeft;

    [Header("Spawner Variables")]
    public GameObject projectilePrefab;
    public float timeBetweenShots;
    public float startDelay;

    // Start is called before the first frame update
    void Start()
    {
        //repeatedly spawns laser through spawnProjectile
        InvokeRepeating("SpawnProjectile", startDelay, timeBetweenShots);
    }

    //grabs laser prefab so the spawner can launch them out and changes lasers bool value
    public void SpawnProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);

        if (projectile.GetComponent<Laser>())
        {
            projectile.GetComponent<Laser>().goingLeft = goingLeft;
        }
    }
}
