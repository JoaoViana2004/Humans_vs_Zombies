using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_Tileset_Config : MonoBehaviour
{
    [System.Serializable]
    public struct BackgroundLayer
    {
        public Transform[] backgrounds;
        public float[] parallaxScales;
    }

    public BackgroundLayer[] backgroundLayers;
    public float smoothing = 1f;
    public Transform player;
    public float changePoint = 100f;

    private Transform cam;
    private Vector3 previousCamPosition;
    private int currentBackgroundIndex = 0;

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    private void Start()
    {
        previousCamPosition = cam.position;

        for (int i = 0; i < backgroundLayers.Length; i++)
        {
            InitializeParallaxScales(backgroundLayers[i]);
        }
    }

    private void Update()
    {
        if (player.position.x >= changePoint && currentBackgroundIndex < backgroundLayers.Length - 1)
        {
            currentBackgroundIndex++;
            ChangeBackgroundLayer(backgroundLayers[currentBackgroundIndex]);
        }

        ParallaxMove(backgroundLayers[currentBackgroundIndex]);
    }

    private void InitializeParallaxScales(BackgroundLayer layer)
    {
        layer.parallaxScales = new float[layer.backgrounds.Length];
        for (int i = 0; i < layer.backgrounds.Length; i++)
        {
            layer.parallaxScales[i] = layer.backgrounds[i].position.z * -1f;
        }
    }

    private void ChangeBackgroundLayer(BackgroundLayer layer)
    {
        for (int i = 0; i < backgroundLayers.Length; i++)
        {
            for (int j = 0; j < backgroundLayers[i].backgrounds.Length; j++)
            {
                if (i == currentBackgroundIndex)
                {
                    backgroundLayers[i].backgrounds[j].gameObject.SetActive(true);
                }
                else
                {
                    backgroundLayers[i].backgrounds[j].gameObject.SetActive(false);
                }
            }
        }
    }

    private void ParallaxMove(BackgroundLayer layer)
    {
        for (int i = 0; i < layer.backgrounds.Length; i++)
        {
            float parallax = (previousCamPosition.x - cam.position.x) * layer.parallaxScales[i];
            float backgroundTargetPosX = layer.backgrounds[i].position.x + parallax;
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, layer.backgrounds[i].position.y, layer.backgrounds[i].position.z);
            layer.backgrounds[i].position = Vector3.Lerp(layer.backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        previousCamPosition = cam.position;
    }
}