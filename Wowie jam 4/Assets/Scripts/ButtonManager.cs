using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour
{
    public Transform canvas;
    [SerializeField] AI ai;

    public void Open_Close_Console(GameObject ob)
    {
        ob.SetActive(true);
        EventSystem.current.currentSelectedGameObject.GetComponentInParent<Canvas>().gameObject.SetActive(false);
    }
    public void DeleteCommand()
    {
        Destroy(ai.commands[ai.commands.Count - 1].gameObject);
        ai.commands.RemoveAt(ai.commands.Count - 1);
    }
    public void Run()
    {
        ai.SetStartPos();
        ai.index = 0;
        ai.stop = false;
        StartCoroutine(ai.Execute());
        ai.runButton1.SetActive(false);
        ai.runButton2.SetActive(false);
    }
    public void AddCommand(Command command)
    {
        Command instance = Instantiate(command, canvas);
        instance.ai = ai;
        ai.commands.Add(instance);
        instance.transform.localPosition = new Vector2(-485, 450 - 50 * ai.commands.IndexOf(instance));
    }
    public bool ifBlocking()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit hit;
        if (Physics.Raycast(ai.gameObject.transform.position, ai.gameObject.transform.TransformDirection(Vector3.forward), out hit, 2f, layerMask)) return true;
        return false;
    }
    public void Stop()
    {
        ai.stop = true;
        ai.runButton1.SetActive(true);
        ai.runButton2.SetActive(true);
    }
}
