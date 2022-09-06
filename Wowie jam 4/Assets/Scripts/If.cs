using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class If : MonoBehaviour
{
    public List<Command> commands = new List<Command>();
    public AI ai;
    public int index;

    public void Run()
    {
        index = 0;
        StartCoroutine(Execute());
    }
    IEnumerator Execute()
    {
        ai.ready = false;
        commands[index].Function();
        yield return new WaitForSeconds(ai.waitTime);
        ai.waitTime = 0.1f;
        index++;
        if (!ai.stop && index < commands.Count-1)
        {
            yield return new WaitUntil(() => ai.ready);
            StartCoroutine(Execute());
        }
        ai.ready = true;
    }
}
