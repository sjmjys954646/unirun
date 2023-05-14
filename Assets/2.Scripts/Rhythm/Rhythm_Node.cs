using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhythm_Node : MonoBehaviour
{
    [SerializeField]
    private float deletePos = -4.3f;

    public bool RD_Left;
    public bool RD_Middle = false;

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < deletePos)
        {
            if(RD_Left)
            {
                RhythmGameManager.Instance.popLeftNodeQueue();
            }
            else
            {
                RhythmGameManager.Instance.popRightNodeQueue();
            }

            RhythmGameManager.Instance.AddMissScore();
            Destroy(gameObject);
        }

        if(RD_Middle && transform.position.x < deletePos)
        {
            RhythmGameManager.Instance.popMidNodeQueue();
            RhythmGameManager.Instance.AddMissScore();
            Destroy(gameObject);
        }
    }
}
