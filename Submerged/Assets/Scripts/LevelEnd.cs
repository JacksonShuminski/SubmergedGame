using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public string newScene;
    private AssetBundle loadedBundle;

    private void Start()
    {
        //loadedBundle = AssetBundle.LoadFromFile(newScene);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(newScene);
        }
    }
}
