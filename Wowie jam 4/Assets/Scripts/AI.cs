using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public List<Command> commands = new List<Command>();
    public GameObject runButton1, runButton2;
    int index;
    public float waitTime = 0.1f;
    
    public void SetStartPos()
    {
        transform.position = Vector3.zero;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        index = 0;
    }
    
    public IEnumerator Execute()
    {
        if (index >= commands.Count) index = 0;
        commands[index].Function();
        yield return new WaitForSeconds(waitTime);
        waitTime = 0.1f;
        index++;
        StartCoroutine(Execute());
    }
}
