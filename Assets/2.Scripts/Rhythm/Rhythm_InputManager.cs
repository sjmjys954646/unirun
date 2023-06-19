using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhythm_InputManager : MonoBehaviour
{
    RhythmGameManager rhythmGameManager;

    [SerializeField]
    private float coolJudge = 0;
    [SerializeField]
    private float goodJudge = 0;

    [SerializeField]
    private GameObject leftJudgeLine;
    [SerializeField]
    private GameObject midJudgeLine;
    [SerializeField]
    private GameObject rightJudgeLine;

    private void Start()
    {
        rhythmGameManager = RhythmGameManager.Instance;
    }

    void Update()
    {
        InputProcessing();
    }

    private void InputProcessing()
    {
        GameObject pressedGameObject;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            pressedGameObject = rhythmGameManager.popLeftNodeQueue();

            if (pressedGameObject == null)
                return;

            judgePosition(pressedGameObject);
            Destroy(pressedGameObject);
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            pressedGameObject = rhythmGameManager.popMidNodeQueue();

            if (pressedGameObject == null)
                return;

            judgePosition(pressedGameObject);
            Destroy(pressedGameObject);
        }
        else if (Input.GetKeyDown(KeyCode.RightShift))
        {
            pressedGameObject = rhythmGameManager.popRightNodeQueue();

            if (pressedGameObject == null)
                return;

            judgePosition(pressedGameObject);
            Destroy(pressedGameObject);
        }
    }

    private void judgePosition(GameObject go)
    {
        if(go.GetComponent<Rhythm_Node>().RD_Middle)
        {
            if (midJudgeLine.transform.position.x - coolJudge <= go.transform.position.x &&
                 go.transform.position.x <= midJudgeLine.transform.position.x + coolJudge
                )
                rhythmGameManager.AddCoolScore();
            else if (midJudgeLine.transform.position.x - goodJudge <= go.transform.position.x &&
                 go.transform.position.x <= midJudgeLine.transform.position.x + goodJudge)
                rhythmGameManager.AddGoodScore();
            else
                rhythmGameManager.AddMissScore();

            return;
        }

        if (go.GetComponent<Rhythm_Node>().RD_Left)
        {
            if (leftJudgeLine.transform.position.y - coolJudge <= go.transform.position.y &&
                 go.transform.position.y <= leftJudgeLine.transform.position.y + coolJudge
                )
                rhythmGameManager.AddCoolScore();
            else if(leftJudgeLine.transform.position.y - goodJudge <= go.transform.position.y &&
                 go.transform.position.y <= leftJudgeLine.transform.position.y + goodJudge)
                rhythmGameManager.AddGoodScore();
            else
                rhythmGameManager.AddMissScore();
        }
        else
        {
            if (rightJudgeLine.transform.position.y - coolJudge <= go.transform.position.y &&
                 go.transform.position.y <= rightJudgeLine.transform.position.y + coolJudge
                )
                rhythmGameManager.AddCoolScore();
            else if (rightJudgeLine.transform.position.y - goodJudge <= go.transform.position.y &&
                 go.transform.position.y <= rightJudgeLine.transform.position.y + goodJudge)
                rhythmGameManager.AddGoodScore();
            else
                rhythmGameManager.AddMissScore();
        }
    }
}
