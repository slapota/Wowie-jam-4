using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public List<Command> commands = new List<Command>();
    public GameObject runButton1, runButton2;
    public int index;
    public float waitTime = 0.1f;
    public bool stop;
    public bool ready = false;
    public GameObject canvas1, canvas2;

    private void Start()
    {
        canvas1.SetActive(false);
        canvas2.SetActive(true);
    }
    public void SetStartPos()
    {
        transform.position = Vector3.zero;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        index = 0;
    }
    public void Run()
    {
        StartCoroutine(Execute());
    }
    
    IEnumerator Execute()
    {
        ready = false;
        if (index >= commands.Count) index = 0;
        commands[index].Function();
        yield return new WaitForSeconds(waitTime);
        waitTime = 0.1f;
        index++;
        if (!stop)
        {
            yield return new WaitUntil(() => ready);
            StartCoroutine(Execute());
        }
        
    }
    public void Restart()
    {

    }
}
