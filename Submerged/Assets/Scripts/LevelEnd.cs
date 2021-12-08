using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour
{
    public string newScene;
    public float wait;
    private GameObject scoreUI;
    private GameObject popUp;
    private bool doneLevel;
    private float timer;

    private void Start()
    {
        scoreUI = GameObject.Find("Score UI");
        popUp = GameObject.Find("Level Complete UI");
        popUp.SetActive(false);
        doneLevel = false;
        timer = 0;
    }

    private void Update()
    {

        if (doneLevel) 
        {
            if (Input.GetMouseButtonDown(0) && timer >= wait)
                SceneManager.LoadScene(newScene);
            timer += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !doneLevel)
        {
            LevelPopup();
        }
    }

    void LevelPopup()
    {
        float score = scoreUI.GetComponentInChildren<Score>().timer;
        scoreUI.SetActive(false);
        popUp.SetActive(true);
        string message = "Level Complete\nScore: " + score.ToString("n2");
        popUp.GetComponentInChildren<Text>().text = message;
        doneLevel = true;
    }
}
