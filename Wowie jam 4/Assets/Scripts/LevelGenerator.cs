using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject cube, cubeParent;
    public Objective objective;
    public ButtonManager bm;
    public int quantity;
    public int spread;
    public int minX, minZ, maxX, maxZ;
    public int chance;
    public AI ai;
    public float startSpace;
    int index;
    Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }
    public void ManualGenerator()
    {
        transform.position = startPos;
        index = 0;
        ai.SetStartPos();
        StartCoroutine(Generate());
    }
    IEnumerator Generate()
    {
        int distance = Random.Range(spread - 5, spread + 5);
        if(Random.Range(-chance, chance) == 0)
        {
            Vector3 dir = new Vector3(0, Random.Range(1, 4) * 90, 0);
            transform.rotation = Quaternion.Euler(dir) * transform.rotation;
        }
        transform.position += transform.forward * distance;
        if (transform.position.x > maxX || transform.position.x < minX || transform.position.z > maxZ || transform.position.z < minZ)
        {
            transform.position -= transform.forward * distance * 2;
            transform.rotation = Quaternion.Euler(Vector3.up * 180) * transform.rotation;
        }
        GameObject instance = Instantiate(cube, transform.position, Quaternion.identity, cubeParent.transform);
        Scale(instance);
        Check(instance);
        if (index < Random.Range(quantity - 10, quantity + 10))
        {
            index++;
            StartCoroutine(Generate());
        }
        else
        {
            GenerateObjective();
        }
        yield return null;
    }
    void GenerateObjective()
    {
        transform.position = new Vector3(Random.Range(-49f, 49f), -2, Random.Range(-49f, 49f));
        Objective instance = Instantiate(objective, transform.position, Quaternion.identity);
        instance.bm = bm;
        instance.levelGenerator = this;
    }
    public void RandomGenerator()
    {
        for (int i = 0; i < Random.Range(quantity - 10, quantity + 10); i++)
        {
            GameObject instance = Instantiate(cube, new Vector3(Random.Range(-50f, 50f), -1.5f, Random.Range(-50f, 50f)), Quaternion.identity, cubeParent.transform);
            Scale(instance);
            Check(instance);
        }
    }
    void Scale(GameObject go)
    {
        go.transform.localScale = new Vector3(Random.Range(1f, 10f), Random.Range(1f, 10f), Random.Range(1f, 10f));
    }
    void Check(GameObject cube)
    {
        if(Vector3.Distance(ai.transform.position, cube.transform.position) < startSpace)
        {
            Destroy(cube);
        }
    }
}
