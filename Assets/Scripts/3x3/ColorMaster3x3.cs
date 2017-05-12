using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMaster3x3 : MonoBehaviour
{

    public Material[] colors;

    public void setColors(GameObject obj, bool loop, int color = 6, bool start = false)
    {
        Renderer rend = obj.GetComponent<Renderer>();
        CubeColorChanger3x3 cubeColor = obj.GetComponent<CubeColorChanger3x3>();

        Debug.Log(color);

        if (color != 10)
        {
            rend.material = colors[color];
            cubeColor.counter = color;
        }
        else
        {
            if (cubeColor.counter == 7)
            {
                cubeColor.counter = 0;
            }
            else
            {
                if (!start)
                {
                    cubeColor.counter++;
                }
            }

            if (cubeColor.counter == 0)
            {
                rend.material = colors[0];
            }
            else if (cubeColor.counter == 1)
            {
                rend.material = colors[1];
                if (loop && !start)
                {
                    foreach (GameObject nachbar in cubeColor.nachbarn)
                    {
                        setColors(nachbar, false, 0);
                    }
                }
            }
            else if (cubeColor.counter == 2)
            {
                rend.material = colors[2];
                if (loop && !start)
                {
                    foreach (GameObject nachbar in cubeColor.nachbarn)
                    {
                        setColors(nachbar, false);
                    }
                }
            }
            else if (cubeColor.counter == 3)
            {
                rend.material = colors[3];
                if (loop && !start)
                {
                    foreach (GameObject nachbar in cubeColor.nachbarn)
                    {
                        setColors(nachbar, false, 0);
                    }
                }
            }
            else if (cubeColor.counter == 4)
            {
                rend.material = colors[4];
                if (loop && !start)
                {
                    foreach (GameObject nachbar in cubeColor.nachbarn)
                    {
                        setColors(nachbar, false, 2);
                    }
                }
            }
            else if (cubeColor.counter == 5)
            {
                rend.material = colors[5];
                if (loop && !start)
                {
                    foreach (GameObject nachbar in cubeColor.nachbarn)
                    {
                        setColors(nachbar, false, 3);
                    }
                }
            }
            else if (cubeColor.counter == 6)
            {
                rend.material = colors[6];
                if (loop && !start)
                {
                    foreach (GameObject nachbar in cubeColor.nachbarn)
                    {
                        setColors(nachbar, false, 4);
                    }
                }
            }
            else if (cubeColor.counter == 7)
            {
                rend.material = colors[7];
                if (loop && !start)
                {
                    foreach (GameObject nachbar in cubeColor.nachbarn)
                    {
                        setColors(nachbar, false, 1);
                    }
                }
            }
        }

    }
}
