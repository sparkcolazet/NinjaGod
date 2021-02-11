using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderRigging : MonoBehaviour
{
    public GameObject oppositeLeg;

    public GameObject target;
    public GameObject body;
    public GameObject groundTarget;
    public GameObject tip;
    public LayerMask ground;
    public float speed;
    public float bodyPosX; 
    public float bodyPosZ; 

    private Vector3 targetPos;
    [HideInInspector]
    public bool isGrounded = true;

    private void Start()
    {
        targetPos = target.transform.position;
    }

    private void LateUpdate()
    {
        target.transform.position = targetPos;
        
        Vector3 raycastOrigin = new Vector3(body.transform.position.x + bodyPosX, targetPos.y, body.transform.position.z + bodyPosZ);

        Ray down = new Ray(raycastOrigin, -transform.up);
        Physics.Raycast(down, out var hit, 10f, ground);
        groundTarget.transform.position = hit.point;

        float distance = Vector3.Distance(groundTarget.transform.position, tip.transform.position);

        float step = speed * Time.deltaTime;
        Vector3 neededPos = new Vector3(groundTarget.transform.position.x, groundTarget.transform.position.y + 2f, groundTarget.transform.position.z);

        if ((distance >= 1.5f || !isGrounded) && oppositeLeg.GetComponent<SpiderRigging>().isGrounded)
        {
            isGrounded = false;
            targetPos = Vector3.MoveTowards(targetPos, neededPos, step);
        }
        if (targetPos == neededPos)
        {
            isGrounded = true;
        }
        body.transform.position = new Vector3(body.transform.position.x, LegsInfo.avgLegsY + 1.5f, body.transform.position.z);
    }
}