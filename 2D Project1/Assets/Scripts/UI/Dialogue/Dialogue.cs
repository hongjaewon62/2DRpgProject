using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    Dictionary<int, string[]> dialogueData;

    //int id = DataManager.instance.npcInfo.ID;

    private void Awake()
    {
        dialogueData = new Dictionary<int, string[]>();
        GenerateData();
    }

    private void GenerateData()
    {
        dialogueData.Add(1000, new string[] { "�ݰ����ϴ�.", "���� �� ������ �����Դϴ�." });
    }

    public string GetDialogue(int id, int dialogueIndex)
    {
        if(dialogueIndex == dialogueData[id].Length)
        {
            return null;
        }
        else
        {
            return dialogueData[id][dialogueIndex];
        }
    }
}
