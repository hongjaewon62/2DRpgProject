using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypeEffect : MonoBehaviour
{
    [SerializeField]
    private int charPerSecond;
    [SerializeField]
    private GameObject nextText;
    private string targetMsg;
    private TextMeshProUGUI msgText;
    private int index;
    private float interval;

    public bool isType;

    private void Awake()
    {
        msgText = GetComponent<TextMeshProUGUI>();
    }

    public void SetMsg(string msg)
    {
        if(isType)
        {
            msgText.text = targetMsg;
            CancelInvoke();
            TypeEffectEnd();
        }
        else
        {
            targetMsg = msg;
            TypeEffectStart();
        }
    }

    private void TypeEffectStart()
    {
        isType = true;
        msgText.text = "";
        index = 0;
        nextText.SetActive(false);

        interval = 1.0f / charPerSecond;
        Invoke("TypeEffectIng", interval);
    }

    private void TypeEffectIng()
    {
        if(msgText.text == targetMsg)
        {
            TypeEffectEnd();
            return;
        }
        msgText.text += targetMsg[index];
        index++;

        Invoke("TypeEffectIng", interval);
    }

    private void TypeEffectEnd()
    {
        isType = false;
        nextText.SetActive(true);
    }
}
