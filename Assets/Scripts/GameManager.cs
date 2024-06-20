using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public enum SantaState
{
    MainSanta,
    ChaseMain,
    NormalAtt,
    SpecialAtt,
    Dead
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime = 100 * 60f;
    [Header("# Player Info")] 
    public Player[] players;
    public Vector2 inputVec;
    public float[] currentHealth;
    public float maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };
    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public Result uiResult;
    public GameObject enemyCleaner;

    private PlayerControls inputAction;
    
    void Awake () 
    { 
        instance = this;
        if (inputAction == null)
        {
            inputAction = new PlayerControls();
            inputAction.PlayerMovement.MoveInput.performed +=
                inputAction => inputVec = inputAction.ReadValue<Vector2>();
            inputAction.Enable();
        }
    }

    private void Start()
    {
        throw new NotImplementedException();
    }

    public void GameStart()
    {
        Array.Fill(currentHealth, maxHealth);
        
        isLive = true;
        Resume();
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;
        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();
    }
    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();
    }
    public void GameRetry()
    {
        SceneManager.LoadScene(0); 
    }
    void Update()
    {
        if (!isLive)  
            return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            GameVictory();
        }

    }

    public void GetExp()
    {
        if (!isLive)
            return;
       
        exp++;

        if (exp ==  nextExp[level])
        {
            level++;
            exp = 0;
            
        }
    }
    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isLive=true;
        Time.timeScale = 1;
    }
}
