using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public Shape[] allShapes = new Shape[7];
    public Transform[] queForms = new Transform[3];

    private Shape[] queShape = new Shape[3];
    private float localScale = 0.85f;

    private Shape GetRandomPrefabShape()
    {
        return allShapes[Random.Range(0, allShapes.Length - 1)];
    }

    public Shape GetSpawnShape()
    {
        if (IsArrayEmpty(queShape))
        {
            InitQue();
        }

        Shape shape = GetShapeFromQue();

        shape.transform.position = transform.position;
        shape.transform.localScale = Vector3.one;

        return shape;
    }


    private void InitQue()
    {
        for (int i = 0; i < queShape.Length; i++)
        {
            queShape[i] = null;
        }
        FilltQue();
    }


    private void FilltQue()
    {
        for (int i = 0; i < queShape.Length; i++)
        {
            if (queShape[i] == null)
            {
                queShape[i] = Instantiate(GetRandomPrefabShape(), this.transform.position, Quaternion.identity);
                queShape[i].transform.position = queForms[i].transform.position;
                queShape[i].transform.localScale = new Vector3(localScale, localScale, localScale);
            }
        }

    }

    private Shape GetShapeFromQue()
    {
        Shape firstShape = queShape[0];

        for (int i = 1; i < queShape.Length; i++)
        {
            queShape[i - 1] = queShape[i];
            queShape[i - 1].transform.position = queShape[i].transform.position;
        }

        queShape[queShape.Length - 1] = null;
        FilltQue();
        return firstShape;
    }


    private bool IsArrayEmpty<T>(T[] array)
    {
        return System.Array.TrueForAll(array, x => x == null);
    }
}
