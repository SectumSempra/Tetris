using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public Transform[] allShapes = new Transform[7];


    private Transform getRandomPrefabShape()
    {
        return allShapes[Random.Range(0, allShapes.Length - 1)];
    }

    public Transform getSpawnShape()
    {
        Transform randomPrefabShape = getRandomPrefabShape();
        Transform newShap = Instantiate(randomPrefabShape, this.transform.position, Quaternion.identity);
        return newShap;
    }


}
