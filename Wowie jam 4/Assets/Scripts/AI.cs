using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public List<Command> commands = new List<Command>();
    public GameObject runButton;
    public Transform canvas;
    int index;
    public float waitTime = 0.1f;


    public void Run()
    {
        transform.position = Vector3.zero;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        index = 0;
        StartCoroutine(Execute());
        runButton.SetActive(false);
    }
    public void AddCommand(Command command)
    {
        Command instance = Instantiate(command, canvas);
        instance.ai = transform;
        commands.Add(instance);
        instance.transform.localPosition = new Vector2(-485, 450 - 50*commands.IndexOf(instance));
    }
    public void DeleteCommand()
    {
        Destroy(commands[commands.Count - 1].gameObject);
        commands.RemoveAt(commands.Count-1);
    }
    IEnumerator Execute()
    {
        commands[index].Function();
        yield return new WaitForSeconds(waitTime);
        waitTime = 0.1f;
        if(index< commands.Count - 1)
        {
            index++;
            StartCoroutine(Execute());
        }
        else
        {
            runButton.SetActive(true);
        }
    }
}
