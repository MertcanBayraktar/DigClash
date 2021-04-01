using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavMeshController : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;
	NavMeshPath path;
    public GameObject raycastObject;
    void Start()
    {
        path = new NavMeshPath();   
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (GameMaster.instance._gameState == GameMaster.gameState.play)
        {
            Vector3 fwd = raycastObject.transform.TransformDirection(Vector3.forward);
            Debug.DrawRay(raycastObject.transform.position, fwd * 50, Color.green);
            Ray _ray = new Ray(raycastObject.transform.position, fwd);
            if (Physics.Raycast(_ray, out RaycastHit hitDeform))
            {
                if (hitDeform.transform.gameObject.tag == "DeformableMesh")
                {
                    hitDeform.transform.GetComponent<MeshDeformer>().Deform(hitDeform.point, 1.0f, 0.1f, -1.0f, -0.1f, hitDeform.normal);
                }
            }
            if (Input.GetMouseButton(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    agent.SetDestination(hit.point);

                }
            }
        }
    }
}
