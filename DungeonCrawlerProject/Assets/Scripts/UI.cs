using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]
    private Text Health;
    [SerializeField]
    private Text Keys;
    [SerializeField]
    private Text Lives;


    // Start is called before the first frame update
    void Start()
    {
        Health.text = GetComponent<Player>().health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
