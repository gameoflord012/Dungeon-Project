using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ShowHideUI : MonoBehaviour
{
    [SerializeField] KeyCode triggerKeyCode = KeyCode.Escape;
    [SerializeField] GameObject showHideObject;
    [SerializeField] Volume volume;

    void Start()
    {
        showHideObject.SetActive(false);
        volume.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(triggerKeyCode))
            Trigger();
    }

    public void Trigger()
    {
        showHideObject.SetActive(!showHideObject.activeSelf);
        volume.enabled = !volume.enabled;
        Time.timeScale = 1 - Time.timeScale;
    }
}