using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Color color = new Color(1f, 1f, 1f, 0.2f);

    private Shape ghostShape;
    private bool hitButtom = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DrawGhost(Shape orginalShape, Board board)
    {
        if (!ghostShape)
        {
            ghostShape = Instantiate(orginalShape, orginalShape.transform.position, orginalShape.transform.rotation) as Shape;
            ghostShape.gameObject.name = "GhostShape";
            SpriteRenderer[] allSpriteRenderers = ghostShape.GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer r in allSpriteRenderers)
            {
                r.color = color;

            }

        }
        else
        {
            ghostShape.transform.position = orginalShape.transform.position;
            ghostShape.transform.rotation = orginalShape.transform.rotation;

        }

        hitButtom = false;
        while(!hitButtom)
        {
            ghostShape.moveDown();
            if(!board.IsValidPosition(ghostShape))
            {
                ghostShape.moveUp();
                hitButtom = true;
            }
        }

    }


    public void ResetGhost()
    {
        Destroy(ghostShape.gameObject);
    }
}
