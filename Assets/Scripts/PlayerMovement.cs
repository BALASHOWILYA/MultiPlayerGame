using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : NetworkBehaviour
{

    [SerializeField] private NavMeshAgent agent = null;

    private Camera mainCamera;

    #region Server
    [Command]
    private void CmdMove(Vector3 position)
    {
        // if it is not valid position then return
        if (!NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas)) { return; }

        agent.SetDestination(hit.position);
    }
    #endregion

    #region Client

    public override void OnStartAuthority()
    {
        mainCamera = Camera.main;
    }

    //ClientCallback prevents the server from running this Update
    [ClientCallback]
    private void Update()
    {
        // make sure it belongs to us 
        if (!hasAuthority){ return; }

        // make sure the right mouse button
        if(!Input.GetMouseButtonDown(1)) { return; }

        //go grab where our cursor is
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        //check in the scene where we hit if we didn't hit anywhere in the scene return
        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) { return; }

        CmdMove(hit.point);
    }

    #endregion
}
