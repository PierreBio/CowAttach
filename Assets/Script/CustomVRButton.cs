using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomVRButton : MonoBehaviour
{
    BoxCollider m_boxCollider;
    Button m_button;
    Animator m_animator;
     // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_button = GetComponent<Button>();
        m_boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Targeted()
    {
        m_animator.SetTrigger("Push");
    }

    public void ButtonPushed()
    {
        m_button.onClick.Invoke();
    }
}
