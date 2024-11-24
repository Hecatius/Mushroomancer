using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    public string name;
    public bool isLocalized;

    [TextArea(3, 10)]
    public string[] sentences;

    [Header("Things to activate")]
    [SerializeField]
    public GameObject[] thingsToActivate;

    [Serializable]
    public class DialogueClearEvent : UnityEvent { }

    [FormerlySerializedAs("onClear")]
    [SerializeField]
    public DialogueClearEvent m_OnClear = new DialogueClearEvent();
}
