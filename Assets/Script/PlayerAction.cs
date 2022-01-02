using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    private const float m_maxDistance = float.PositiveInfinity;
    public GameObject pointer;
    [SerializeField] private Transform m_gunEnd;
    [SerializeField] private GameObject m_shotPrefab;

    [SerializeField] private AudioSource m_source;
    
    // Update is called once per frame
    void Update()
    {
        if (Google.XR.Cardboard.Api.IsTriggerPressed || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {     
        SoundManager.m_instance.PlaySoundFX(m_source, SoundManager.SoundFX.Shots);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, m_maxDistance))
        {
            //Debug.Log("HIT = " + hit.transform.gameObject.name);
            GameObject laser = GameObject.Instantiate(m_shotPrefab, m_gunEnd.position, transform.rotation) as GameObject;
            laser.GetComponent<ShotBehavior>().setTarget(hit.point, hit.transform.gameObject);
            GameObject.Destroy(laser, 2f);
        }
    }
    
}
