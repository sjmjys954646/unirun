using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Rhythm_Mousecursor : MonoBehaviour
{
    RhythmGameManager rhythmGameManager;
    [SerializeField]
    private Vector3 downPoint;
    [SerializeField]
    private Vector3 upPoint;
    [SerializeField]
    private float coolJudge = 0;
    [SerializeField]
    private float goodJudge = 0;
    [SerializeField]
    private GameObject coolJudgeLIne;
    [SerializeField]
    private GameObject goodJudgeLine;

    [SerializeField]
    private GameObject guideTxt;


    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private Vector3 mousePos;
    [SerializeField]
    private Vector3 startPos;
    [SerializeField]
    private Vector3 endPos;

    [SerializeField]
    private GameObject JUSTICE;

    private void Start()
    {
        rhythmGameManager = RhythmGameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(!rhythmGameManager.isRhythmGameStart)
        {
            if (Input.GetMouseButtonDown(0))
            {
                downPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            }

            if (Input.GetMouseButtonUp(0))
            {
                upPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
                rhythmGameManager.isRhythmGameStart = true;
                guideTxt.SetActive(false);
                Time.timeScale = 1;
            }
            lineRenderProcessing();
        }
        else
        {
            InputSystemProcessing();
            lineRenderProcessing();
        }
    }

    private void lineRenderProcessing()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (lineRenderer == null)
                createLine();
            mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            lineRenderer.SetPosition(0, mousePos);
            lineRenderer.SetPosition(1, mousePos);
            startPos = mousePos;
        }
        else if(Input.GetMouseButtonUp(0) && lineRenderer)
        {
            if(lineRenderer)
            {
                mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
                lineRenderer.SetPosition(1, mousePos);
                endPos = mousePos;
                lineRenderer = null;
            }
        }
        else if(Input.GetMouseButton(0))
        {
            if(lineRenderer)
            {
                mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
                lineRenderer.SetPosition(1, mousePos);
            }
        }
    }

    private void InputSystemProcessing()
    {
        if (Input.GetMouseButtonDown(0))
        {
            downPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        }

        if (Input.GetMouseButtonUp(0))
        {
            upPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            

            bool isHorizontalCut = isHorizontal();
            GameObject cutHubo = null;
            if (downPoint.x < -2.5)
            {
                if (rhythmGameManager.isLeftNodeQueueEmpty())
                    return;

                if (!rhythmGameManager.showLeftNodeQueue().GetComponent<Rhythm_Box>().isBreakable())
                    return;

               
                if ((isHorizontalCut && rhythmGameManager.showLeftNodeQueue().GetComponent<Rhythm_Box>().isHor())
                   ||
                   (!isHorizontalCut && rhythmGameManager.showLeftNodeQueue().GetComponent<Rhythm_Box>().isVer())
                   )
                {
                    cutHubo = rhythmGameManager.popLeftNodeQueue();
                    judgePosition(cutHubo, uiPos(cutHubo));
                    cutHubo.GetComponent<Rhythm_Box>().makeParticle();
                    Destroy(cutHubo);
                }

            }
            else if (-2.5 <= downPoint.x && downPoint.x <= 2)
            {
                if (rhythmGameManager.isMidNodeQueueEmpty())
                    return;

                if (!rhythmGameManager.showMidNodeQueue().GetComponent<Rhythm_Box>().isBreakable())
                    return;

                if ((isHorizontalCut && rhythmGameManager.showMidNodeQueue().GetComponent<Rhythm_Box>().isHor())
                   ||
                   (!isHorizontalCut && rhythmGameManager.showMidNodeQueue().GetComponent<Rhythm_Box>().isVer())
                   )
                {
                    cutHubo = rhythmGameManager.popMidNodeQueue();
                    judgePosition(cutHubo, uiPos(cutHubo));
                    cutHubo.GetComponent<Rhythm_Box>().makeParticle();
                    Destroy(cutHubo);
                }
            }
            else if (2 <= downPoint.x)
            {
                if (rhythmGameManager.isRightNodeQueueEmpty())
                    return;

                if (!rhythmGameManager.showRightNodeQueue().GetComponent<Rhythm_Box>().isBreakable())
                    return;

                if ((isHorizontalCut && rhythmGameManager.showRightNodeQueue().GetComponent<Rhythm_Box>().isHor())
                  ||
                  (!isHorizontalCut && rhythmGameManager.showRightNodeQueue().GetComponent<Rhythm_Box>().isVer())
                  )
                {
                    cutHubo = rhythmGameManager.popRightNodeQueue();
                    judgePosition(cutHubo, uiPos(cutHubo));
                    cutHubo.GetComponent<Rhythm_Box>().makeParticle();
                    Destroy(cutHubo);
                }

                    
            }

        }
    }

    public void missCall(int pos)
    {
        StartCoroutine(FadeIn(JUSTICE.transform.GetChild(pos).GetChild(3).gameObject, 1f));
    }

    private int uiPos(GameObject go)
    {
        if (go.GetComponent<Rhythm_Box>().isGoLeft())
            return 0;
        if (go.GetComponent<Rhythm_Box>().isGoMid())
            return 1;
        if (go.GetComponent<Rhythm_Box>().isGoRight())
            return 2;
        return 0;
    }

    private void judgePosition(GameObject go, int position)
    {
        float bojung = 0.5f;

        if (coolJudgeLIne.transform.position.z + bojung - coolJudge <= go.transform.position.z &&
                 go.transform.position.z <= coolJudgeLIne.transform.position.z + bojung + coolJudge
                )
        {
            rhythmGameManager.AddCoolScore();
            StartCoroutine(FadeIn(JUSTICE.transform.GetChild(uiPos(go)).GetChild(0).gameObject,1f));
        }
        else if (goodJudgeLine.transform.position.z + bojung - goodJudge <= go.transform.position.z &&
             go.transform.position.z <= goodJudgeLine.transform.position.x + bojung + goodJudge)
        {
            rhythmGameManager.AddGoodScore();
            StartCoroutine(FadeIn(JUSTICE.transform.GetChild(uiPos(go)).GetChild(1).gameObject, 1f));
        }
        else
        {
            rhythmGameManager.AddBadScore();
            StartCoroutine(FadeIn(JUSTICE.transform.GetChild(uiPos(go)).GetChild(2).gameObject, 1f));
        }

    }

    // 세로일때 true
    // 가로일때 false
    public bool isHorizontal()
    {
        Vector3 vec = upPoint - downPoint;
        float vecHor = Mathf.Abs(Vector3.Dot(vec, Vector3.right));
        float vecRow = Mathf.Abs(Vector3.Dot(vec, Vector3.up));

        if (vecHor >= vecRow)
            return true;
        else
            return false;
    }

    private void createLine()
    {
        lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
        lineRenderer.SetVertexCount(2);
        lineRenderer.SetWidth(0.1f, 0.1f);
        lineRenderer.SetColors(Color.green, Color.green);
        lineRenderer.useWorldSpace = true;
        Destroy(lineRenderer.gameObject, 1f);
    }

    public IEnumerator FadeIn(GameObject go, float time)
    {
        TMP_Text text = go.GetComponent<TMP_Text>();
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / time));
            yield return null;
        }
        StartCoroutine(FadeOut(go, time));
    }

    public IEnumerator FadeOut(GameObject go, float time)
    {
        TMP_Text text = go.GetComponent<TMP_Text>();
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / time));
            yield return null;
        }
    }
}
