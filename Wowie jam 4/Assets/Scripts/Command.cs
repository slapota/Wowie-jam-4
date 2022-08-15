using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command : MonoBehaviour
{

    public AI ai;
    public new string name;
    public float value;
    public string description;

    public Command(string n, float v, string d)
    {
        name = n;
        value = v;
        description = d;
    }
    public void Function()
    {
        switch (name)
        {
            case "move":
                Move(value);
                break;
            case "left":
                Rotate(value);
                break;
            case "right":
                Rotate(value);
                break;
            case "wait":
                Wait(value);
                break;
        }
    }
    private void Move(float distance)
    {
        ai.gameObject.transform.Translate(Vector3.forward * distance);
        ai.ready = true;
    }
    private void Rotate(float degrees)
    {
        ai.gameObject.transform.Rotate(Vector3.up * degrees);
        ai.ready = true;
    }
    private void Wait(float seconds)
    {
        ai.waitTime = seconds;
        ai.ready = true;
    }
}
