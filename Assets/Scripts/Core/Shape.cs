using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public bool isRotate = true;

    private void move(Vector3 distance)
    {
        this.transform.position += distance;
    }

    public void moveLeft()
    {
        move(Vector2.left);
    }

    public void moveRight()
    {
        move(Vector2.right);
    }

    public void moveUp()
    {
        move(Vector2.up);
    }

    public void moveDown()
    {
        move(Vector2.down);
    }


    public void rotateRight()
    {
        if (isRotate)
            this.transform.Rotate(Vector3.forward, -90);
    }

    public void rotateLeft()
    {
        if (isRotate)
            this.transform.Rotate(Vector3.forward, 90);
    }

    private void Start()
    {
    }

}
