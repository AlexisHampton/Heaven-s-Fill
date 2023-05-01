using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public bool canMove;
    public float speed = 3f;
    public string noEvidenceText;
    public string noPersonText;


    // Update is called once per frame
    void Update()
    {
        if (canMove)
            Move();
    }

    private void Move()
    {
        float xDelta = Input.GetAxis("Horizontal");
        float yDelta = Input.GetAxis("Vertical");

        Vector3 newPos = new Vector3(xDelta, yDelta, 0);

        transform.Translate(newPos * Time.deltaTime * speed);
    }
}
