using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Platform_GameManager : MonoBehaviour
{
    public int Platform_Score = 0;
    public GameObject player;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text guideText;
    [SerializeField]
    private bool gameStart = false;


    /***********************************************************************
    *                               SingleTon
    ***********************************************************************/
    #region .
    private static Platform_GameManager instance = null;

    public static Platform_GameManager Instance
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

    private void Start()
    {
        if(player == null)
        {
            player = GameObject.Find("Player");
        }

        Time.timeScale = 0;
    }

    private void Update()
    {
        if(player.GetComponent<Platform_Player>().Platform_isDead)
        {
            StartCoroutine(Wait());   
        }

        if(Input.GetKeyDown(KeyCode.Space) && !gameStart)
        {
            GameStart();
        }
    }

    public void addScore(int score)
    {
        Platform_Score+= score;
        scoreText.text = "Score : " + Platform_Score;
    }

    private void GameStart()
    {
        gameStart = true;
        Time.timeScale = 1;
        guideText.gameObject.SetActive(false);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0;
    }
}

