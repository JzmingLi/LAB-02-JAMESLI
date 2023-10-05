using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    [SerializeField]
    public PathManager pathManager;

    List<Waypoint> thePath;
    Waypoint target;

    public float MoveSpeed;
    public float RotateSpeed;

    public Animator animator;
    protected bool isWalking;

    public bool collisionDetecting;

    //Before first frame update
    protected void Start()
    {
        isWalking = false;
        animator.SetBool("IsWalking", isWalking);
        thePath = pathManager.GetPath();
        if (thePath != null && thePath.Count > 0)
        {
            target = thePath[0];
        }
    }

    protected void rotateTowardsTarget()
    {
        float stepSize = RotateSpeed * Time.deltaTime;

        Vector3 targetDir = target.pos - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, stepSize, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }
    protected void moveForward()
    {
        float stepSize = Time.deltaTime * MoveSpeed;
        float distanceToTarget = Vector3.Distance(transform.forward, target.pos);
        if(distanceToTarget < stepSize)
        {
            //we will overshoot the targer,
            //so we should so something smarter here
            return;
        }
        //take a step forward
        Vector3 moveDir = Vector3.forward;
        transform.Translate(moveDir * stepSize);
    }

    protected virtual void Update()
    {
        if (Input.anyKeyDown)
        {
            //toggle if any key is pressed
            isWalking = !isWalking;
            animator.SetBool("IsWalking", isWalking);
        }
        if(isWalking)
        {
            rotateTowardsTarget();
            moveForward();
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        //switch to next target
        target = pathManager.GetNextTarget();
    }

    protected void OnCollisionEnter(Collision collision)
    {
        Debug.Log("IM TOUCHING SOMETHING");
        if (collisionDetecting)
        {
            isWalking = false;
            animator.SetBool("IsWalking", isWalking);
        }
    }
}
