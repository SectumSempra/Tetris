using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    //PRIVATE
    private Board board;
    private Spawner spawner;
    private Shape activeShape;
    private float dropInterval = 1f;
    private float timeToDrop;

    void Start()
    {
        board = GameObject.FindWithTag("Board").GetComponent<Board>();
        spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();
        spawner.transform.position = Vectorf.Round(spawner.transform.position);

        activeShape = spawner.getSpawnShape().GetComponent<Shape>();
    }

    void Update()
    {
        if (!board || !spawner)
        { return; }

        if (Time.time > timeToDrop)
        {
            timeToDrop += dropInterval;
            activeShape.moveDown();
        }
    }
}
