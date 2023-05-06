using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenarioManager : MonoBehaviour
{
    [SerializeField]
    List<string> sceneName = new List<string>();


    /***********************************************************************
    *                               SingleTon
    ***********************************************************************/
    #region .
    private static ScenarioManager instance = null;

    public static ScenarioManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    #endregion

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void FirstTry()
    {
        if(GameManager.Instance.firstTry)
        {
            MoveScene(1);
        }
        else
        {
            MoveScene(0);
        }
    }

    public void MoveScene(int index)
    {
        SceneManager.LoadScene(sceneName[index]);
    }
}
