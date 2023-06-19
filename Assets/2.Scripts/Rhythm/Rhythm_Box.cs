using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Rhythm_Box : MonoBehaviour
{
    public GameObject destination;

    [SerializeField]
    private float flySpeed;
    [SerializeField]
    private bool breakable = false;
    [SerializeField]
    private bool ishor = false;
    [SerializeField]
    private bool isver = false;
    [SerializeField]
    private bool isLeft = false;
    [SerializeField]
    private bool isMid = false;
    [SerializeField]
    private bool isRight = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!breakable && other.tag == "BreakPoint")
            breakable = true;
        if (other.tag == "CrackPoint")
        {
            if (isLeft)
            {
                RhythmGameManager.Instance.popLeftNodeQueue();
            }
            else if(isMid)
            {
                RhythmGameManager.Instance.popMidNodeQueue();
            }
            else if (isRight)
            {
                RhythmGameManager.Instance.popRightNodeQueue();
            }
            RhythmGameManager.Instance.AddMissScore();
            RhythmGameManager.Instance.sendMissCoroutine(uiPos(gameObject));
            Destroy(gameObject);
        }
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

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination.transform.position, flySpeed * Time.deltaTime);
    }

    public bool isGoLeft()
    {
        return isLeft;
    }
    public bool isGoMid()
    {
        return isMid;
    }

    public bool isGoRight()
    {
        return isRight;
    }

    public bool isHor()
    {
        return ishor;
    }
    public bool isVer()
    {
        return isver;
    }

    public bool isBreakable()
    {
        return breakable;
    }

    public void setHorTrue()
    {
        isver = true;
    }

    public void setVerTrue()
    {
        ishor = true;
    }

    public void setLeftTrue()
    {
        isLeft = true;
    }

    public void setMidTrue()
    {
        isMid = true;
    }
     public void setRightTrue()
    {
        isRight = true;
    }

    public void setDestination(GameObject inputDestination)
    {
        destination = inputDestination;
    }

}
