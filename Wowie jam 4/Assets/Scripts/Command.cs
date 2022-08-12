using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Command : MonoBehaviour
{

    public Transform ai;
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
                RotateLeft(value);
                break;
            case "right":
                RotateRight(value);
                break;
            case "wait":
                Wait(value);
                break;
        }
    }
    private void Move(float distance)
    {
        ai.Translate(Vector3.forward * distance);
    }
    private void RotateLeft(float degrees)
    {
        ai.Rotate(Vector3.up * degrees);
    }
    private void RotateRight(float degrees)
    {
        ai.Rotate(Vector3.up * degrees);
    }
    private void Wait(float seconds)
    {
        ai.GetComponent<AI>().waitTime = seconds;
    }
}
