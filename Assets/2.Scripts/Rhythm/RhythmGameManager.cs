using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmGameManager : MonoBehaviour
{
    [SerializeField]
    private int coolNum = 0;
    [SerializeField]
    private int goodNum = 0;
    [SerializeField]
    private int missNum = 0;

    [SerializeField]
    private float coolJudge = 0;
    [SerializeField]
    private float goodJudge = 0;
    [SerializeField]
    private float missJudge = 0;

    [SerializeField]
    private GameObject leftJudgeLine;
    [SerializeField]
    private GameObject rightJudgeLine;

    private Queue<GameObject> leftNodeQueue;
    private Queue<GameObject> rightNodeQueue;


    /***********************************************************************
    *                               SingleTon
    ***********************************************************************/
    #region .
    private static RhythmGameManager instance = null;

    public static RhythmGameManager Instance
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

        leftNodeQueue = new Queue<GameObject>();
        rightNodeQueue = new Queue<GameObject>();
    }

    public void popLeftNodeQueue()
    {
        if (leftNodeQueue.Count == 0)
            return;

        leftNodeQueue.Dequeue();
    }

    public void popRightNodeQueue()
    {
        if (rightNodeQueue.Count == 0)
            return;

        rightNodeQueue.Dequeue();
    }

}
