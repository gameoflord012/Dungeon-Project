using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideUI : MonoBehaviour
{
    [SerializeField] KeyCode triggerKeyCode = KeyCode.Escape;
    [SerializeField] GameObject showHideObject;

    void Start()
    {
        showHideObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(triggerKeyCode))
            showHideObject.SetActive(!showHideObject.activeSelf);
    }
}