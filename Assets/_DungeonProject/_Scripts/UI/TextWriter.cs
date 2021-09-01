using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{
    [SerializeField] float timePerCharacter = 0.5f;

    bool isWritting;

    public IEnumerator WaitForTyping(Text textField, string text, GameObject[] inactiveOnWrite, GameObject[] activeOnWrite)
    {
        SetActiveOnWrite(inactiveOnWrite, activeOnWrite, false);
        isWritting = true;

        textField.text = "";
        foreach (char word in text)
        {
            if (!isWritting)
            {
                textField.text = text;

                SetActiveOnWrite(inactiveOnWrite, activeOnWrite, true);
                yield break;
            }

            yield return new WaitForSeconds(timePerCharacter);
            textField.text = textField.text + word;
        }

        SetActiveOnWrite(inactiveOnWrite, activeOnWrite, true);
    }

    public void Skip()
    {
        isWritting = false;
    }

    private void SetActiveOnWrite(GameObject[] inactiveOnWrite, GameObject[] activeOnWrite, bool active)
    {
        foreach (GameObject gameObject in inactiveOnWrite)
            gameObject.SetActive(active);

        foreach (GameObject gameObject in activeOnWrite)
            gameObject.SetActive(!active);
    }
}