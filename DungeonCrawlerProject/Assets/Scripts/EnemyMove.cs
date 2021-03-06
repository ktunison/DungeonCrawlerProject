using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private bool goingLeft;
    public float speed = 2;
    private Vector3 OriginPos;
    private Vector3 movedLocation;
    public float unitsToMove = 5f;

    // Start is called before the first frame update
    void Start()
    {
        OriginPos = this.transform.position;
        movedLocation = this.transform.position;
        movedLocation.x += unitsToMove;
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    private void move()
    {
        if (goingLeft)
        {
            if (transform.position.x <= OriginPos.x)
            {
                goingLeft = false;
            }
            else
            {
                transform.position += Vector3.left * Time.deltaTime * speed;
            }
        }
        else
        {
            if (transform.position.x >= movedLocation.x)
            {
                goingLeft = true;
            }
            else
            {
                transform.position += Vector3.right * Time.deltaTime * speed;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            if (goingLeft == true)
            {
                goingLeft = false;
            }
            else
            {
                goingLeft = true;
            }
        }
    }
}
