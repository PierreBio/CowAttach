using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum PanelType
{
    WAVE, 
    END
}
public class WavePanel : MonoBehaviour
{
    [SerializeField] Animator m_animator;
    [SerializeField] TextMeshPro m_waveNumber;
    [SerializeField] GameObject m_wavePanel;
    [SerializeField] GameObject m_endPanel;

    public void Display(bool _display)
    {
        //Debug.Log("TEST");
        if (_display) 
        { 
            ShowPanel(GameManager.Instance.PanelType);
            if (GameManager.Instance.PanelType == PanelType.WAVE) { 
                m_waveNumber.text = "Vague " + GameManager.Instance.WaveCount;
            }
            foreach (Renderer rend in GetComponentsInChildren<Renderer>())
            {
                rend.enabled = true;
            }
        }      
        else
        {
            foreach (Renderer rend in GetComponentsInChildren<Renderer>())
            {
                rend.enabled = false;
            }
        }
    }

    private void ShowPanel(PanelType _type)
    {
        m_wavePanel.SetActive(_type == PanelType.WAVE);
        m_endPanel.SetActive(_type == PanelType.END);
    }

    public void DisplayOn()
    {
        Display(true);
    }
    public void DisplayOff()
    {
        Display(false);
    }

    public Animator Animator
    {
        get
        {
            return m_animator;
        }
    }
}
