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

        if (Input.GetKey("a"))
        {
            addPosition += Vector3.left * Time.deltaTime * speed;
        }
        if (Input.GetKey("d"))
        {
            addPosition += Vector3.right * Time.deltaTime * speed;
        }
        if (Input.GetKey("w"))
        {
            addPosition += Vector3.up * Time.deltaTime * speed;
        }
        if (Input.GetKey("s"))
        {
            addPosition += Vector3.down * Time.deltaTime * speed;
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (health > 0)
            {
                health--;
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
    }
}
