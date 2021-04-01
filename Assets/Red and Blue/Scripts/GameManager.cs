using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Material pinkPlatform;
    public Material bluePlatform;
    public Material neutralPlatform;

    GameObject player1;
    GameObject player2;

    public bool isPlayer1OnFinish = false;
    public bool isPlayer2OnFinish = false;

    public Animator heart;
    public Animator levelTranstion;

    public bool isLevelStarted = false;
    public bool levelFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        isLevelStarted = true;
    }
    private void Update()
    {
        if (isPlayer1OnFinish && isPlayer2OnFinish)
            LevelFinish();
    }

    public void LevelFinish()
    {
        if (!levelFinished)
        {
            levelFinished = true;
            heart.enabled = true;
            StartCoroutine(BeginTransitionToNextLevel());
            
        }
    }
    IEnumerator BeginTransitionToNextLevel()
    {
        yield return new WaitForSeconds(2);
        levelTranstion.SetTrigger("SceneChange");
        StartCoroutine(LoadNextLevel());
    }
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(2);
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextLevel);
    }
}
