using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    public void reset()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        GetComponent<Text>().text = "Score: " + timer.ToString("n2");
    }
}
