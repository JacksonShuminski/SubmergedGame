using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class CameraShader : MonoBehaviour
{
    public Material postMat;
    private Transform player;
    private Vector2 mouse;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (player != null)
        {
            Vector3 playerScreenPos = GetComponent<Camera>().WorldToScreenPoint(player.position);
            postMat.SetInt("_PlayerVertexX", (int)playerScreenPos.x);
            postMat.SetInt("_PlayerVertexY", (int)playerScreenPos.y);
            postMat.SetInt("_PointerX", (int)mouse.x);
            postMat.SetInt("_PointerY", (int)mouse.y);
        }
        else
        {
            postMat.SetInt("_PlayerVertexX", -1);
            postMat.SetInt("_PlayerVertexY", -1);
            postMat.SetInt("_PointerX", -1);
            postMat.SetInt("_PointerY", -1);
        }


        Graphics.Blit(source, destination, postMat);
    }


    // Start is called before the first frame update
    void Start()
    {
        mouse = Input.mousePosition;
        postMat = new Material(postMat);
    }

    // Update is called once per frame
    void Update()
    {
        mouse = Vector2.Lerp(mouse, Input.mousePosition, Time.deltaTime * 5);

        if (player == null)
            try
            {
                player = GameObject.Find("Player(Clone)").transform;
            }
            catch
            {
                print("No player!");
            }
    }
}
