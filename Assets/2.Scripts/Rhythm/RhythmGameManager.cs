using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhythmGameManager : MonoBehaviour
{
    [SerializeField]
    private int coolNum = 0;
    [SerializeField]
    private int goodNum = 0;
    [SerializeField]
    private int missNum = 0;

    [SerializeField]
    private Rhythm_FileSetter fileSetter;
    [SerializeField]
    private GameObject ResultUI;

    [SerializeField]
    private GameObject leftSpawnPos;
    [SerializeField]
    private GameObject middleSpawnPos;
    [SerializeField]
    private GameObject rightSpawnPos;

    [SerializeField]
    private GameObject leftSpawnGroup;
    [SerializeField]
    private GameObject middleSpawnGroup;
    [SerializeField]
    private GameObject rightSpawnGroup;

    [SerializeField]
    private GameObject NodePrefab;
    [SerializeField]
    private GameObject NodePrefabMid;

    [SerializeField]
    private float nodeInterval;

    private Queue<GameObject> leftNodeQueue;
    private Queue<GameObject> midNodeQueue;
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
        midNodeQueue = new Queue<GameObject>();
        rightNodeQueue = new Queue<GameObject>();
    }

    private void Start()
    {
        StartGame();
    }

    /***********************************************************************
    *                               public
    ***********************************************************************/
    #region .

    public GameObject popLeftNodeQueue()
    {
        if (leftNodeQueue.Count == 0)
            return null;

        return leftNodeQueue.Dequeue();
    }

    public GameObject popMidNodeQueue()
    {
        if (midNodeQueue.Count == 0)
            return null;

        return midNodeQueue.Dequeue();
    }

    public GameObject popRightNodeQueue()
    {
        if (rightNodeQueue.Count == 0)
            return null;

        return rightNodeQueue.Dequeue();
    }

    public void AddCoolScore()
    {
        coolNum+=1;
    }

    public void AddGoodScore()
    {
        goodNum+=1;
    }

    public void AddMissScore()
    {
        missNum+=1;
    }

    #endregion

    public void StartGame()
    {
        //게임시작
        List<Dictionary<string, int>> rd_dialogue = fileSetter.GetComponent<Rhythm_FileSetter>().rd_dialogue;
        string[] arr = new string[] {"Left", "Mid", "Right" };
        //코루틴으로 생성해야됨
        for(int i = 0;i <  rd_dialogue.Count;i++)
        {
            for(int j = 0;j < 3 ;j++)
            {

                if (rd_dialogue[i][arr[j]] == 0)
                    continue;

                StartCoroutine(makeNode(i, j));
            }
        }

        StartCoroutine(FinishGame(rd_dialogue.Count * nodeInterval + 3f));
        
    }

    private void InstantiateNode(int pos)
    {
        //left 0
        //middle 1
        //right 2

        GameObject curNode;

        if (pos == 0)
        {
            curNode = Instantiate(NodePrefab, leftSpawnPos.transform.position, Quaternion.identity);
            curNode.GetComponent<Rhythm_Node>().RD_Left = true;
            curNode.transform.parent = leftSpawnGroup.transform;
            leftNodeQueue.Enqueue(curNode);
        }
        else if(pos == 1)
        {
            curNode = Instantiate(NodePrefabMid, middleSpawnPos.transform.position, Quaternion.identity);
            curNode.GetComponent<Rhythm_Node>().RD_Middle = true;
            curNode.transform.parent = middleSpawnPos.transform;
            midNodeQueue.Enqueue(curNode);
        }
        else if(pos == 2)
        {
            curNode = Instantiate(NodePrefab, rightSpawnPos.transform.position, Quaternion.identity);
            curNode.GetComponent<Rhythm_Node>().RD_Left = false;
            curNode.transform.parent = rightSpawnGroup.transform;
            rightNodeQueue.Enqueue(curNode);
        }
    }

    IEnumerator makeNode(int frame, int nodePos)
    {
        yield return new WaitForSeconds(frame * nodeInterval);
        InstantiateNode(nodePos);
    }

    IEnumerator FinishGame(float frame)
    {
        yield return new WaitForSeconds(frame);
        EndGaae();
    }

    private void EndGaae()
    {
        ResultUI.SetActive(true);
        ResultUI.transform.GetChild(0).GetComponent<Text>().text = "Cool : " + coolNum;
        ResultUI.transform.GetChild(1).GetComponent<Text>().text = "Good : " + goodNum;
        ResultUI.transform.GetChild(2).GetComponent<Text>().text = "Miss : " + missNum;

        Time.timeScale = 0;
    }

}
