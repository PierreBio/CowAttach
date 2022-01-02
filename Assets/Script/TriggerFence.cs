using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TriggerFence : Trigger
{
    protected override void OnTriggerEnter(Collider _other)
    {
        if (_other.GetComponent<Animal>())
        {
            m_breakableObstacle.IsDamaged(true, _other.GetComponent<Animal>());
            /*if (_other.GetComponent<NavMeshAgent>())
            {
                Debug.Log("TEST");

                _other.GetComponent<NavMeshAgent>().isStopped = true;
            }*/
        }
    }
    protected override void OnTriggerExit(Collider _other)
    {
        if (_other.GetComponent<Animal>())
        {
            m_breakableObstacle.IsDamaged(false, _other.GetComponent<Animal>());
           /* if (_other.GetComponent<NavMeshAgent>())
            {
                _other.GetComponent<NavMeshAgent>().isStopped = false;
            }*/
        }
    }
}
