using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushingWall : MonoBehaviour
{
    private Vector3 OriginPos;
    public bool goingRight;
    public float speed = 2f;
    public float waitTimer = 2f;

    // Start is called before the first frame update
    void Start()
    {
        OriginPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    private void move()
    {
        if (goingRight)
        {
            if (transform.position.x == OriginPos.x)
            {
                StartCoroutine(Wait());
                goingRight = false;
            }
            else
            {
                transform.position += Vector3.right * Time.deltaTime * speed;
            }
        }
        else
        {
            transform.position += Vector3.left * Time.deltaTime * speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            StartCoroutine(Wait());
            goingRight = !goingRight;
        }
    }

    IEnumerator Wait()
    {
        float CrushWallSpeed = speed;
        speed = 0;
        yield return new WaitForSeconds(waitTimer);
        speed = CrushWallSpeed;
    }
}
