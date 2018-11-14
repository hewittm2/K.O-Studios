using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemFetch : MonoBehaviour {

    EventSystem m_EventSystem;
    GameObject firstSelected;

    void Start()
    {
        firstSelected = m_EventSystem.firstSelectedGameObject;
    }
    void OnEnable()
    {
        m_EventSystem = EventSystem.current;
    }

    void Update()
    {
        if(m_EventSystem.currentSelectedGameObject == null)
        {
            m_EventSystem.SetSelectedGameObject(firstSelected);
        }
    }
}
