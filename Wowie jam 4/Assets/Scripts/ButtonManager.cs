using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Transform canvas;
    [SerializeField] AI ai;
    public bool ifList;
    public GameObject exitButton;
    public int index;
    bool hide;

    private void Start()
    {
        exitButton.SetActive(false);
    }
    public void Open_Close_Console(GameObject ob)
    {
        canvas.localScale = (hide) ? Vector3.zero : Vector3.one;
        hide = !hide;
        ob.SetActive(true);
        EventSystem.current.currentSelectedGameObject.GetComponentInParent<Canvas>().gameObject.SetActive(false);
    }
    public void DeleteCommand()
    {
        if (ifList && ai.commands.Last().If.commands.Count > 0)
        {
            Destroy(ai.commands.Last().If.commands.Last().gameObject);
            ai.commands.Last().If.commands.RemoveAt(ai.commands.Last().If.commands.Count - 1);
        }
        else
        {
            Exit();
            Destroy(ai.commands.Last().gameObject);
            ai.commands.RemoveAt(ai.commands.Count - 1);
        }
        index--;
        if (ai.commands.Last().name == "if") ifList = true;
    }
    public void Run()
    {
        ai.SetStartPos();
        ai.index = 0;
        ai.stop = false;
        ai.Run();
        ai.runButton1.SetActive(false);
        ai.runButton2.SetActive(false);
    }
    public void AddCommand(Command command)
    {
        if (command.name == "if")
        {
            exitButton.SetActive(true);
            if (ifList) return;
        }
        Command instance = Instantiate(command, canvas);
        instance.ai = ai;
        if (!ifList)
        {
            ai.commands.Add(instance);
            instance.transform.localPosition = new Vector2(-485, 450 - 50 * index);
        }
        else
        {
            ai.commands[ai.commands.Count-1].If.commands.Add(instance);
            ai.commands[ai.commands.Count - 1].If.ai = ai;
            instance.transform.localPosition = new Vector2(-420, 450 - 50 * index);
        }
        if (command.name == "if") ifList = true;
        index++;
    }
    public void Stop()
    {
        ai.stop = true;
        ai.runButton1.SetActive(true);
        ai.runButton2.SetActive(true);
    }
    public void Exit()
    {
        ifList = false;
        exitButton.SetActive(false);
    }

    public void Delete()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("block");
        foreach (GameObject block in blocks)
        {
            Destroy(block);
        }
    }
}
