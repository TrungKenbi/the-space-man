using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfect : MonoBehaviour
{
    public int refHight;
    public int PPU;

    void Start()
    {
        int PPUScale = Math.Max(Screen.height / refHight, 1);
        var cinema = GetComponent<Camera>();
        if (cinema != null)
        {
            cinema.orthographicSize = (float)Math.Round(Screen.height / (float)(PPUScale * PPU) * 0.5f, 2);
        }
    }
}