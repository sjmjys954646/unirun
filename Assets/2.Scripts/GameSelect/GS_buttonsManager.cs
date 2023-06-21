using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GS_buttonsManager : MonoBehaviour
{
    public GameObject GS_Buttons;
    // Start is called before the first frame update
    void Start()
    {
        //싱글톤에 Don't Destroy 사용시 eventlistner 사용하여 할당
        //그냥 각 씬마다 씬매니저 prefab 사용하기로함

        //for(int i = 0; i< GS_Buttons.transform.childCount;i++)
        //{
        //    GS_Buttons.transform.GetChild(i).GetComponent<mapIndex>().index = i + 2;
        //}

        //for (int i = 0; i < GS_Buttons.transform.childCount; i++)
        //{
        //    var Go = GS_Buttons.transform.GetChild(i);
        //    Go.GetComponent<Button>().onClick.AddListener(delegate { Setbtn(Go.GetComponent<mapIndex>().index); });
        //}
    }

    void Setbtn(int idx)
    {
        ScenarioManager.Instance.MoveScene(idx);
    }
    
}
