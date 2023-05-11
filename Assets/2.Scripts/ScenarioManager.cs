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
            MoveScene(2);
        }
        else
        {
            MoveScene(1);
        }
    }

    public void MoveScene(int index)
    {
        if(index >= sceneName.Count)
        {
            Debug.Log("Not maked Yet");
            return;
        }

        SceneManager.LoadScene(sceneName[index]);
    }

    public void RestartScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void ReturnToSelectScene()
    {
        SceneManager.LoadScene(sceneName[1]);
    }

    public void ReturnToIntroScene()
    {
        SceneManager.LoadScene(sceneName[0]);
    }
}
