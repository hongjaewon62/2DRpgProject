using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DialogueManager : MonoBehaviour //IPointerDownHandler
{
    [SerializeField]
    private GameObject dialoguePanel;
    [SerializeField]
    private Dialogue dialogue;
    [SerializeField]
    private TypeEffect dialogueEffect;

    public GameObject scanObject;

    public bool isAction;

    private int dialogueIndex;

    public void ScanAction(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjectData objectData = scanObject.GetComponent<ObjectData>();
        DialogueImport(objectData.id, objectData.isNPC);

        dialoguePanel.SetActive(isAction);
    }

    private void DialogueImport(int id, bool isNPC)
    {

        string dialogueData = "";
        if(dialogueEffect.isType)
        {
            dialogueEffect.SetMsg("");
            return;
        }
        else
        {
            dialogueData = dialogue.GetDialogue(id, dialogueIndex);
        }

        if(dialogueData == null)
        {
            isAction = false;
            dialogueIndex = 0;
            return;
        }

        isAction = true;

        if (isNPC)
        {
            dialoguePanel.SetActive(isAction);
            dialogueEffect.SetMsg(dialogueData);
        }
        else
        {
            dialoguePanel.SetActive(isAction);
            dialogueEffect.SetMsg(dialogueData);
        }

        dialogueIndex++;
    }
}
