using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhythmGameManager : MonoBehaviour
{
    public bool isRhythmGameStart = false;

    [SerializeField]
    private int coolNum = 0;
    [SerializeField]
    private int goodNum = 0;
    [SerializeField]
    private int badNum = 0;
    [SerializeField]
    private int missNum = 0;

    [SerializeField]
    private Rhythm_FileSetter fileSetter;
    [SerializeField]
    private GameObject ResultUI;

    [SerializeField]
    private GameObject spawnPos;
    [SerializeField]
    private List<GameObject> leftSpawnPos = new List<GameObject>();
    [SerializeField]
    private List<GameObject> middleSpawnPos = new List<GameObject>();
    [SerializeField]
    private List<GameObject> rightSpawnPos = new List<GameObject>();
    [SerializeField]
    private GameObject destinationPos;
    [SerializeField]
    private List<GameObject> leftDestinationPos = new List<GameObject>();
    [SerializeField]
    private List<GameObject> middleDestinationPos = new List<GameObject>();
    [SerializeField]
    private List<GameObject> rightDestinationPos = new List<GameObject>();


    [SerializeField]
    private GameObject leftSpawnGroup;
    [SerializeField]
    private GameObject middleSpawnGroup;
    [SerializeField]
    private GameObject rightSpawnGroup;

    [SerializeField]
    private List<GameObject> NodePrefab = new List<GameObject>();

    [SerializeField]
    private float nodeInterval;

    private Queue<GameObject> leftNodeQueue;
    private Queue<GameObject> midNodeQueue;
    private Queue<GameObject> rightNodeQueue;

    [SerializeField]
    private GameObject mouseCursor;


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
        Time.timeScale = 0;
        StartCoroutine(StartGame(2f));
    }


    /***********************************************************************
    *                               public
    ***********************************************************************/
    #region .

    public bool isLeftNodeQueueEmpty()
    {
        return leftNodeQueue.Count == 0;
    }

    public bool isMidNodeQueueEmpty()
    {
        return midNodeQueue.Count == 0;
    }

    public bool isRightNodeQueueEmpty()
    {
        return rightNodeQueue.Count == 0;
    }

    public GameObject showLeftNodeQueue()
    {
        return leftNodeQueue.Peek();
    }

    public GameObject showMidNodeQueue()
    {
        return midNodeQueue.Peek();
    }

    public GameObject showRightNodeQueue()
    {
        return rightNodeQueue.Peek();
    }

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

    public void AddBadScore()
    {
        badNum += 1;
    }

    public void AddMissScore()
    {
        missNum+=1;
    }


    public void StartGame()
    {
        //���ӽ���
        List<Dictionary<string, int>> rd_dialogue = fileSetter.GetComponent<Rhythm_FileSetter>().rd_dialogue;
        string[] arr = new string[] {"Left", "Mid", "Right" };
        //�ڷ�ƾ���� �����ؾߵ�
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
    #endregion

    private void allocateSpawnList()
    {
        for(int i = 0;i < spawnPos.transform.childCount ;i++)
        {
            if(i%3 == 0)
            {
                rightSpawnPos.Add(spawnPos.transform.GetChild(i).gameObject);
                rightDestinationPos.Add(destinationPos.transform.GetChild(i).gameObject);
            }
            else if(i%3 == 1)
            {
                middleSpawnPos.Add(spawnPos.transform.GetChild(i).gameObject);
                middleDestinationPos.Add(destinationPos.transform.GetChild(i).gameObject);
            }
            else if(i%3 == 2)
            {
                leftSpawnPos.Add(spawnPos.transform.GetChild(i).gameObject);
                leftDestinationPos.Add(destinationPos.transform.GetChild(i).gameObject);
            }
        }


    }

    private void InstantiateNode(int pos)
    {
        //left 0
        //middle 1
        //right 2

        GameObject curNode;

        int randPos = Random.Range(0, 3);
        int randObj = Random.Range(0, 3);

        if (pos == 0)
        {
            curNode = Instantiate(NodePrefab[randObj], leftSpawnPos[randPos].transform.position, Quaternion.identity);
            curNode.GetComponent<Rhythm_Box>().setDestination(leftDestinationPos[randPos]);
            curNode.GetComponent<Rhythm_Box>().setLeftTrue();
            if (randObj == 0)
            {
                curNode.GetComponent<Rhythm_Box>().setHorTrue();
                curNode.GetComponent<Rhythm_Box>().setVerTrue();
            }
            else if(randObj == 1)
            {
                curNode.GetComponent<Rhythm_Box>().setHorTrue();
                curNode.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if(randObj == 2)
            {
                curNode.GetComponent<Rhythm_Box>().setVerTrue();
            }

            curNode.transform.parent = leftSpawnGroup.transform;
            leftNodeQueue.Enqueue(curNode);
        }
        else if(pos == 1)
        {
            curNode = Instantiate(NodePrefab[randObj], middleSpawnPos[randPos].transform.position, Quaternion.identity);
            curNode.GetComponent<Rhythm_Box>().setDestination(middleDestinationPos[randPos]);
            curNode.GetComponent<Rhythm_Box>().setMidTrue();

            if (randObj == 0)
            {
                curNode.GetComponent<Rhythm_Box>().setHorTrue();
                curNode.GetComponent<Rhythm_Box>().setVerTrue();
            }
            else if (randObj == 1)
            {
                curNode.GetComponent<Rhythm_Box>().setHorTrue();
                curNode.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (randObj == 2)
            {
                curNode.GetComponent<Rhythm_Box>().setVerTrue();
            }

            curNode.transform.parent = middleSpawnGroup.transform;
            midNodeQueue.Enqueue(curNode);
        }
        else if(pos == 2)
        {
            curNode = Instantiate(NodePrefab[randObj], rightSpawnPos[randPos].transform.position, Quaternion.identity);
            curNode.GetComponent<Rhythm_Box>().setDestination(rightDestinationPos[randPos]);
            curNode.GetComponent<Rhythm_Box>().setRightTrue();

            if (randObj == 0)
            {
                curNode.GetComponent<Rhythm_Box>().setHorTrue();
                curNode.GetComponent<Rhythm_Box>().setVerTrue();
            }
            else if (randObj == 1)
            {
                curNode.GetComponent<Rhythm_Box>().setHorTrue();
                curNode.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (randObj == 2)
            {
                curNode.GetComponent<Rhythm_Box>().setVerTrue();
            }

            curNode.transform.parent = rightSpawnGroup.transform;
            rightNodeQueue.Enqueue(curNode);
        }
    }

    public void sendMissCoroutine(int pos)
    {
        mouseCursor.GetComponent<Rhythm_Mousecursor>().missCall(pos);
    }

    IEnumerator StartGame(float frame)
    {
        yield return new WaitForSeconds(frame);
        allocateSpawnList();
        StartGame();
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
        ResultUI.transform.GetChild(2).GetComponent<Text>().text = "Bad : " + badNum;
        ResultUI.transform.GetChild(3).GetComponent<Text>().text = "Miss : " + missNum;

        Time.timeScale = 0;
        isRhythmGameStart = false;
    }

}
