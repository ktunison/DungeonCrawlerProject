using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageable : Boss
{
    public GameObject bossPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.position = bossPrefab.transform.position;
        }
    }
}
