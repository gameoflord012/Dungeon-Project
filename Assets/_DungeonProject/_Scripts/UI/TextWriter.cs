using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{
    [SerializeField] float timePerCharacter = 0.5f;

    public IEnumerator WaitForTyping(Text textField, string text)
    {
        textField.text = "";
        foreach (char word in text)
        {
            yield return new WaitForSeconds(timePerCharacter);
            textField.text = textField.text + word;
        }
    }
}