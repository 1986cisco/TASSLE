using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class Test3x3 : MonoBehaviour
{


    static UdpClient udp;
    Thread thread;
    static readonly object lockObject = new object();
    string returnData = "";
    bool precessData = false;
    public GameObject[] cubes;
    public GameObject[] vorgaben; //Array für die Vorgabenwürfel

    //The Timer , Points and Stagecount
    float timeLeft = 60.0f;
    public float time = 60.0f;
    public Text timer;
    public Text points;
    public Text stage;
    public int pointsCount;
    public int stageCounter;

    void Start()
    {
        timeLeft = time;

        //cubemove = cube.GetComponent<CubeMove>();
        ColorMaster3x3 colorMaster = gameObject.GetComponent<ColorMaster3x3>();
        foreach (GameObject cube in cubes)
        {
            colorMaster.setColors(cube, false, 6, true); //Setzt das Spielfeld zu Beginn komplett weiss
        }

        newLvl();

        thread = new Thread(new ThreadStart(ThreadMethod));
        thread.Start();
    }
    
    public void newLvl()
    {
        ColorMaster3x3 colorMaster = gameObject.GetComponent<ColorMaster3x3>();
        int temp = 0;
        int correctCubes = 0;
        //Punkteabgleich:
        //10 Punkte pro richtigem Feld
        //100 Punkte extra wenn alles richtig ist
        foreach (GameObject vorgabe in vorgaben)
        {
            
            if (colorMaster.getColor(vorgabe) == colorMaster.getColor(cubes[temp]))
            {
                correctCubes++;
                pointsCount += 10;
            }
            if (correctCubes >= 9)
            {
                pointsCount += 100;
            }
            int randomNumber = UnityEngine.Random.Range(1, 7);            //Weisst den kleinen Feldern der 
            colorMaster.setColors(vorgabe, false, randomNumber, true);   //Vorgabe zufällige Farbwerte zu

            temp++;
            // HIER FEHLT NOCH: Punkteberechnung (lvlbestanden ja/nein)
        }
    }
    
    void Update()
    {
        timeLeft -= Time.deltaTime;
        timer.text = "" + Mathf.Round(timeLeft);
        points.text = "Points " + pointsCount;
        stage.text = "Stage " + stageCounter;
        if (timeLeft < 0)
        {
            resetMe();
            //pointsCount += 200;
        }

        ColorMaster3x3 colorMaster = gameObject.GetComponent<ColorMaster3x3>();

        if (precessData)
        {
            /*lock object to make sure there data is 
             *not being accessed from multiple threads at thesame time*/
            lock (lockObject)
            {
                precessData = false;

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

    void resetMe()
    {
        timeLeft = time;
        stageCounter++;
        newLvl();
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
        if (udp != null)
        {
            udp.Close();
        }
    }
}