using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private UnitProfile localProfile;
    private GameObject playerObj; 
    private NavMeshAgent agent;
    private Input_Main inputsys;



    private void Awake()
    {
        localProfile = this.GetComponent<UnitProfile>();
        playerObj = localProfile.gameObject;
        agent = playerObj.GetComponent<NavMeshAgent>();
        agent.speed = localProfile.movementSpeed;
        inputsys = new Input_Main();
        inputsys.Main.Enable();
        inputsys.Main.SetDesination.performed += MovementOrder;
        inputsys.Main.SetDesination.canceled += MovementOrder;
    }

    private void Update()
    {
        
    }

    private void MovementOrder(InputAction.CallbackContext input)
    {
        Raycast(Mouse.current.position.ReadValue() ,out RaycastHit hitInfo,out bool isHit);
        if(isHit)
        agent.SetDestination(hitInfo.point);
        print("saaas");
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



