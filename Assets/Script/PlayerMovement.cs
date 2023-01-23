using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private UnitProfile localProfile;
    private GameObject playerObj; //實際的移動對象
    private NavMeshAgent agent;


    private void Awake()
    {
        localProfile = this.GetComponent<UnitProfile>();
        playerObj = localProfile.gameObject;
        agent = playerObj.GetComponent<NavMeshAgent>();
        agent.speed = localProfile.movementSpeed;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
            MovementOrder(Input.mousePosition);
    }

    private void MovementOrder(Vector3 inputV3)
    {
        Raycast(inputV3,out RaycastHit hitInfo,out bool isHit);
        if(isHit)
        agent.SetDestination(hitInfo.point);
    }

    private void Raycast(Vector3 inputV3,out RaycastHit hitInfo,out bool isHit)
    {
        Ray ray = Camera.main.ScreenPointToRay(inputV3);
        Physics.Raycast(ray,out hitInfo , 1000f, 1 << LayerMask.NameToLayer("Ground"), QueryTriggerInteraction.Ignore);

        if (hitInfo.collider.tag != "Ground")
            isHit = false;
                else isHit = true;
    }
}
