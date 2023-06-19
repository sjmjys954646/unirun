using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
public class CSVAssetReader
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public static List<Dictionary<string, object>> Read(TextAsset file) // string -> TextAsset  ���� ����. ���� ��ũ�� �б�
    {
        var list = new List<Dictionary<string, object>>();
        //TextAsset data = Resources.Load(file) as TextAsset �� ����.file ��ü�� TextAsset�̱� ����

        var lines = Regex.Split(file.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
    }
}

public class Rhythm_FileSetter : MonoBehaviour
{
    [SerializeField]
    private TextAsset chaeBo;

    private List<Dictionary<string, object>> data;

    public List<Dictionary<string, int>> rd_dialogue;

    private void Awake()
    {
        DialogueTextLoad();
        makeDialogue();
    }


    public void DialogueTextLoad()
    {
        chaeBo.text.Substring(0, chaeBo.text.Length - 1);
        data = CSVAssetReader.Read(chaeBo);
    }

    public void makeDialogue()
    {
        rd_dialogue = new List<Dictionary<string, int>>();

        int finalNum = Convert.ToInt32(data[data.Count - 1]["Frame"].ToString());

        for (int i = 0; i < finalNum + 1; i++)
        {
            Dictionary<string, int> p = new Dictionary<string, int>();
            rd_dialogue.Add(p);
        }

        for (int i = 0; i < data.Count; i++)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>() {
                { "Left", data[i]["Left"].GetType() != i.GetType() ? 0 : 1 },
                { "Mid", data[i]["Mid"].GetType() != i.GetType()  ? 0 : 1 },
                { "Right", data[i]["Right"].GetType() != i.GetType() ? 0 : 1 },
            };

            rd_dialogue[i] = dic;
        }

        //Debug.Log(rd_dialogue[0]["Left"]);
        //Debug.Log(rd_dialogue[0]["Mid"]);
        //Debug.Log(rd_dialogue[0]["Right"]);
    }

}
