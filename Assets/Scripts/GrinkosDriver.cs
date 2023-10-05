using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrinkosDriver : PathController
{
    protected override void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //toggle if any key is pressed
            isWalking = !isWalking;
            animator.SetBool("IsWalking", isWalking);
        }
        if (Input.GetMouseButtonDown(1))
        {
            //trigger die when right click
            animator.SetBool("IsDead", true);
        }
        if (isWalking)
        {
            rotateTowardsTarget();
            moveForward();
        }   
    }
}
