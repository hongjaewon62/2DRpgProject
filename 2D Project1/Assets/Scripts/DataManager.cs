using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


[System.Serializable]
public class NpcData
{
    public List<NpcInfo> NpcDialogueData;
}

//public class NpcData
//{
//    public int ID;
//    //public Dictionary<int, string[]> dialogueData;
//    public string dialogueData;
//}

[System.Serializable]
public class NpcInfo
{
    public int ID;
    public string dialogueData;
}
public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    private string path;

    //public NpcData npcData = new NpcData();

    //public NpcInfo npcInfo = new NpcInfo();

    public NpcData NpcDialogueData = new NpcData();

    private string dialogueFileName = "NPCDialogueData";

    private void Awake()
    {
        path = "E:\\dbslxl\\2D Project1\\Assets\\Data\\NPCData" + "\\";
    }

    private void Start()
    {
        //npcData.id = 1000;
        //npcData.dialogueData = "¹Ý°©½À´Ï´Ù.";
        //SaveData();
        LoadData();
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(NpcDialogueData);

        File.WriteAllText(path + dialogueFileName, data);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + dialogueFileName);
        NpcDialogueData = JsonUtility.FromJson<NpcData>(data);

        print(data);
        //Debug.Log(NpcDialogueData);
    }
}
