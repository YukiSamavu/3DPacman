using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaFluctuation : MonoBehaviour
{
    [Range(1, 255)] public int startingAlpha = 255;
    [Range(1, 254)] public int alphaMin = 128;
    [Range(2, 255)] public int alphaMax = 255;

    [Range(1, 50)] public int alphaChangeRate = 5;

    public Material material;

    private void Start()
    {
        if (material == null) return;

        material.color = new Color(material.color.r, material.color.g, material.color.b, startingAlpha);
    }

    void Update()
    {
        if (material == null) return;

        int a = (int)material.color.a + alphaChangeRate;

        if (a > alphaMax || a < alphaMin)
        {
            alphaChangeRate *= -1;
            Mathf.Clamp(a, alphaMin, alphaMax);
        }

        material.color = new Color(material.color.r, material.color.g, material.color.b, a);
    }
}
