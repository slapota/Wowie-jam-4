using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public LevelGenerator levelGenerator;
    public ButtonManager bm;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform == levelGenerator.ai)
        {
            bm.Stop();
            levelGenerator.ai.points++;
            levelGenerator.ManualGenerator();
        }
    }
}
