using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class CameraShader : MonoBehaviour
{
    public Material postMat;
    public float lightingScale = 1;
    private Transform player;
    private Vector2 mouse;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        postMat.SetFloat("_LevelScale", lightingScale);
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

        try
        {
            Vector3 start = GameObject.Find("Start Node").transform.position;
            Vector3 end = GameObject.Find("End Node").transform.position;
            start = GetComponent<Camera>().WorldToScreenPoint(start);
            postMat.SetInt("_StartX", (int)start.x);
            postMat.SetInt("_StartY", (int)start.y);
            end = GetComponent<Camera>().WorldToScreenPoint(end);
            postMat.SetInt("_EndX", (int)end.x);
            postMat.SetInt("_EndY", (int)end.y);
        }
        catch
        {
            postMat.SetInt("_StartX", -1);
            postMat.SetInt("_StartY", -1);
            postMat.SetInt("_EndX", -1);
            postMat.SetInt("_EndY", -1);

        }

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
                //print("No player!");
            }
    }
}
