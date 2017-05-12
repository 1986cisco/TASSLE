using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class Test : MonoBehaviour
{


    static UdpClient udp;
    Thread thread;
    static readonly object lockObject = new object();
    string returnData = "";
    bool precessData = false;
    public GameObject[] cubes;

    void Start()
    {
        //cubemove = cube.GetComponent<CubeMove>();
        ColorMaster colorMaster = gameObject.GetComponent<ColorMaster>();
        foreach (GameObject cube in cubes)
        {
            colorMaster.setColors(cube, false, 10, true);
        }
        thread = new Thread(new ThreadStart(ThreadMethod));
        thread.Start();
    }

    void Update()
    {
        ColorMaster colorMaster = gameObject.GetComponent<ColorMaster>();

        if (precessData)
        {
            /*lock object to make sure there data is 
             *not being accessed from multiple threads at thesame time*/
            lock (lockObject)
            {
                precessData = false;
                // cube.SendMessage("Move");
                // or
                //cubemove.Move();

                //Process received data
                Debug.Log("Received: " + returnData);

                //blablubb
                int temp = int.Parse(returnData);
                colorMaster.setColors(cubes[temp], true);

                //Reset it for next read(OPTIONAL)
                returnData = "";
            }
        }
    }

    private void ThreadMethod()
    {
        udp = new UdpClient(40);
        while (true)
        {
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

            byte[] receiveBytes = udp.Receive(ref RemoteIpEndPoint);

            /*lock object to make sure there data is 
            *not being accessed from multiple threads at thesame time*/
            lock (lockObject)
            {
                returnData = Encoding.ASCII.GetString(receiveBytes);

                Debug.Log(returnData);
                if (returnData != "")
                {
                    //Done, notify the Update function
                    precessData = true;
                }
            }
        }
    }

    void OnApplicationQuit()
    {
        thread.Abort();

        if (udp != null)
        {
            udp.Close();
        }
    }


}