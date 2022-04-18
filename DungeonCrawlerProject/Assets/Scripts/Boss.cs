using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float Speed;
    private float BossHealth = 4;
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

    public void DamageBoss()
    {
        if (BossHealth <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            BossHealth--;
            StartCoroutine(Blink());
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
