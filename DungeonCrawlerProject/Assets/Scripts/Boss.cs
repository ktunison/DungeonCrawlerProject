using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float Speed;
    private float BossHealth = 3;
    private Vector3 Direction;

    // Start is called before the first frame update
    void Start()
    {
        Direction.x = Random.Range(-1f, 1f);
        Direction.z = Random.Range(-1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        moveBoss();
    }

    private void moveBoss()
    {
        transform.position += Direction * Time.deltaTime * Speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "VerticalWall")
        {
            Direction.x *= -1;
        }

        if (other.tag == "HorizontalWall")
        {
            Direction.z *= -1;
        }
    }
}
