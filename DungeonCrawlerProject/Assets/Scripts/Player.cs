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
        Vector3 up = new Vector3(0, 0, 1);
        Vector3 down = new Vector3(0, 0, -1);

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
            addPosition += up * Time.deltaTime * speed;
        }
        if (Input.GetKey("s"))
        {
            addPosition += down * Time.deltaTime * speed;
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
