using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhythm_Node : MonoBehaviour
{
    [SerializeField]
    private float deletePos = -4.3f;

    public bool RD_Left;

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

            Destroy(gameObject);
        }
    }
}
