using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Test3x3))]
public class CountDown3x3 : MonoBehaviour
{

    public Test3x3 thisTest;
    void Start()
    {
        // instantiate a copy of the script class and call the non-static method
        
    }

    float timeLeft = 5.0f;
    public Text text;

    void Update()
    {
        timeLeft -= Time.deltaTime;
        text.text = "Time:" + Mathf.Round(timeLeft);
        if (timeLeft < 0)
        {
            resetMe();
        }
    }

    void resetMe()
    {
        thisTest = new Test3x3();
        thisTest.newLvl();
        timeLeft = 3.0f;
        thisTest.newLvl();
    }
}
