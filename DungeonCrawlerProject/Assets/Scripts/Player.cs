using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Vector3 PlayerOrigin;
    public float speed = 1;
    public float health = 3;
    public float lives = 3;
    public float stunTimer = 2f;

    private int KeyCount = 0;
    private int CoinCount = 0;
    public Text countText;
    public Text livesText;
    public Text gameOverText;
    public Text healthText;
    public Text keyText;
    public Text winnerText;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerOrigin = transform.position;
        gameOverText.text = "";
        winnerText.text = "";
        SetCountText();
        
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
    //UI Setup
    void SetCountText()
    {
        countText.text = "Coins:" + CoinCount.ToString();
        keyText.text = "Keys:" + KeyCount.ToString();
        livesText.text = "Lives:" + lives.ToString();
        healthText.text = "Health:" + health.ToString();
        
        if (lives <= 0)
        {
            gameOverText.text = "Game Over";
        }
        
    }
    private void respawn()
    {
        transform.position = PlayerOrigin;
        lives--;
        health = 3;

        if (lives <= 0)
        {
            this.enabled = false;
        }
        StartCoroutine(Blink());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Key")
        {
            KeyCount++;
            other.gameObject.SetActive(false);
            SetCountText();
        }

        if (other.tag == "Enemy" || other.tag == "Boss")
        {
            if (health > 1)
            {
                health--;
                StartCoroutine(Blink());
                SetCountText();
            }
            else if (health == 1)
            {
                health--;
                respawn();
                SetCountText();
            }
        }
        if (other.tag == "winnerCube")
        { 
          //other.gameObject.SetActive(false);
            Destroy(other.gameObject);
          winnerText.text = "A Winner Is You";
          SetCountText();
        }
        if (other.tag == "CrushingWall")
        {
            respawn();
            SetCountText();
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
                SetCountText();
            }
        }

        if (other.tag == "Laser")
        {
            StartCoroutine(Stun());
        }
       
        if (other.tag == "Coin")
        {
            CoinCount++;
            other.gameObject.SetActive(false);
            SetCountText();
        }
    }

    IEnumerator Blink()
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

    IEnumerator Stun()
    {
        float currentPlayerSpeed = speed;
        speed = 0;
        yield return new WaitForSeconds(stunTimer);
        speed = currentPlayerSpeed;
    }
}
