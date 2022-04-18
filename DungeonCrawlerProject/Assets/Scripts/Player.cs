using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Vector3 PlayerOrigin;
    public float speed = 3;
    public float health = 3;
    public float lives = 3;

    private int KeyCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        PlayerOrigin = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move();
    }

    void move()
    {
        Vector3 addPosition = Vector3.zero;

        //left
        if (Input.GetKey("a"))
        {
            addPosition += Vector3.left * Time.deltaTime * speed;
        }
        //right
        if (Input.GetKey("d"))
        {
            addPosition += Vector3.right * Time.deltaTime * speed;
        }
        //up
        if (Input.GetKey("w"))
        {
            addPosition += new Vector3(0, 0, 1) * Time.deltaTime * speed;
        }
        //down
        if (Input.GetKey("s"))
        {
            addPosition += new Vector3(0, 0, -1) * Time.deltaTime * speed;
        }

        GetComponent<Transform>().position += addPosition;
    }

    private void respawn()
    {
        transform.position = PlayerOrigin;
        lives--;

        if (lives <= 0)
        {
            this.enabled = false;
        }
        StartCoroutine(Blink());
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Key")
        {
            KeyCount++;
            other.gameObject.SetActive(false);
        }

        if (other.tag == "Enemy" || other.tag == "Boss")
        {
            if (health > 0)
            {
                health--;
                StartCoroutine(Blink());
            }
            else
            {
                respawn();
            }
        }

        if (other.tag == "switchScene")
        {
            SceneSwitch.instance.switchScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (other.tag == "Door")
        {
            if (KeyCount >= other.gameObject.GetComponent<Door>().keysNeeded)
            {
                KeyCount -= other.gameObject.GetComponent<Door>().keysNeeded;
                other.gameObject.SetActive(false);
            }
        }
    }
}
