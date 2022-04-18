using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageable : Boss
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            DamageBoss();
            Destroy(this.gameObject);
        }
    }
}
