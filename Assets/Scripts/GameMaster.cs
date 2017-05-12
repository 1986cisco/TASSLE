using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class GameMaster : MonoBehaviour {

    static UdpClient udp;
    Thread thread;
    static readonly object lockObject = new object();
    string returnData = "";
    bool precessData = false;

    public GameObject[] cubes;


    // Use this for initialization
    void Start () {
        ColorMaster colorMaster = gameObject.GetComponent<ColorMaster>();
        /*foreach (GameObject cube in cubes)
        {
            colorMaster.setColors(cube, false, 10, true);
        }
        */
        thread = new Thread(new ThreadStart(ThreadMethod));
        thread.Start();
    }
	
	// Update is called once per frame
	void Update () {
        ColorMaster colorMaster = gameObject.GetComponent<ColorMaster>();
        
        if (precessData)
        {
            lock (lockObject)
            {
                precessData = false;

                Debug.Log("Received: " + returnData);

                returnData = "";
            }
        }
        /*
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            colorMaster.setColors(cubes[0], true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            colorMaster.setColors(cubes[1], true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            colorMaster.setColors(cubes[2], true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            colorMaster.setColors(cubes[3], true);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            colorMaster.setColors(cubes[4], true);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            colorMaster.setColors(cubes[5], true);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            colorMaster.setColors(cubes[6], true);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            colorMaster.setColors(cubes[7], true);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            colorMaster.setColors(cubes[8], true);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            colorMaster.setColors(cubes[9], true);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            colorMaster.setColors(cubes[10], true);
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            colorMaster.setColors(cubes[11], true);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            colorMaster.setColors(cubes[12], true);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            colorMaster.setColors(cubes[13], true);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            colorMaster.setColors(cubes[14], true);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            colorMaster.setColors(cubes[15], true);
        }
        */
        
    }

    private void ThreadMethod()
    {
        udp = new UdpClient(40);
        while (true)
        {
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

            byte[] receiveBytes = udp.Receive(ref RemoteIpEndPoint);

            lock (lockObject)
            {
                returnData = Encoding.ASCII.GetString(receiveBytes);

                if(returnData == "1\n")
                {
                    precessData = true;
                }
            }
        }
    }
}
