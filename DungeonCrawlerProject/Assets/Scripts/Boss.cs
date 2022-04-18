using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float Speed;
    public float BossHealth = 4;
    private Vector3 Direction;
    public Vector3 location;

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
        location = transform.position;
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

        if (other.tag == "bossDamageable")
        {
            if (BossHealth == 1)
            {
                BossHealth--;
                Destroy(this.gameObject);
            }
            else
            {
                BossHealth--;
                Speed += 2;
                StartCoroutine(Blink());
            }
            Destroy(other.gameObject);
        }
    }

    public IEnumerator Blink()
    {
        for (int index = 0; index < 30; index++)
        {
            if (index % 2 == 0)
            {
                GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                GetComponent<MeshRenderer>().enabled = true;
            }
            yield return new WaitForSeconds(.1f);
        }

        GetComponent<MeshRenderer>().enabled = true;
    }
}
