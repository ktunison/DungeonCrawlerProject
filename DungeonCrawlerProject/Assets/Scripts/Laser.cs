using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [Header("Projectile Variables")]
    public float speed;
    public bool goingLeft;

    // Update is called once per frame
    void Update()
    {
        //will move laser based on bool value
        if (goingLeft == true)
        {
            transform.position += speed * Vector3.left * Time.deltaTime;
        }
        else
        {
            transform.position += speed * Vector3.right * Time.deltaTime;
        }
    }
}
