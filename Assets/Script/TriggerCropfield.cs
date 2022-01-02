using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCropfield : Trigger
{
    protected override void OnTriggerEnter(Collider _other)
    {
        if (_other.GetComponent<Animal>())
            m_breakableObstacle.IsDamaged(true, _other.GetComponent<Animal>());
    }
    protected override void OnTriggerExit(Collider _other)
    {
        if(_other.GetComponent<Animal>())
            m_breakableObstacle.IsDamaged(false, _other.GetComponent<Animal>());
    }
}
