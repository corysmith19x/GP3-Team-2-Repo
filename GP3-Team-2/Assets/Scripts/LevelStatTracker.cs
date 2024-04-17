using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class LevelStatTracker : MonoBehaviour
{
    public int gruntsKilled = 0;
    public int bossCaptured = 0;
    private float levelStartTime;
    private float levelCompletionTime;
    private float damageTaken;

    public GameObject victoryPanel;

    public static LevelStatTracker instance;

    //log
    private List<LevelRunData> levelRuns = new List<LevelRunData>();

    public TextMeshProUGUI levelNameText;
    public TextMeshProUGUI completionTimeText;
    public TextMeshProUGUI gruntsKilledText;
    public TextMeshProUGUI damageTakenText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetStats();
    }

    public void StartLevel()
    {
        levelStartTime = Time.time;
    }

    public void Grunts()
    {
        gruntsKilled++;
        CheckVictory();
    }

    public void Boss()
    {
        bossCaptured++;
        levelCompletionTime = Time.time - levelStartTime;
        string levelName = SceneManager.GetActiveScene().name;
        LogRun(levelName);
        CheckVictory();
    }

    public void DamageTaken(float amount)
    {
        amount = -1 * amount;
        damageTaken += amount;
    }

    private void CheckVictory()
    {
        if (bossCaptured == 1)
        {
            victoryPanel.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            LevelRunData lastRunData = levelRuns[levelRuns.Count - 1];
            levelNameText.text = " " + lastRunData.levelName;
            completionTimeText.text = "Completion Time: " + lastRunData.completionTime.ToString("F2") + " seconds";
            gruntsKilledText.text = "Enemies Killed: " + lastRunData.gruntsKilled.ToString();
            damageTakenText.text = "Damage Taken: " + lastRunData.damageTaken.ToString("F0");
        }
    }

    private void ResetStats()
    {
        levelStartTime = 0f;
        levelCompletionTime = 0f;
        damageTaken = 0f;
        gruntsKilled = 0;
        bossCaptured = 0;
    }

    private void LogRun(string levelName)
    {
        LevelRunData runData = new LevelRunData(levelName, levelCompletionTime, gruntsKilled, damageTaken);
        levelRuns.Add(runData);
    }

    public List<LevelRunData> GetLevelRuns()
    {
        return levelRuns;
    }

    [Serializable]
    public class LevelRunData
    {
        public string levelName;
        public float completionTime;
        public int gruntsKilled;
        public float damageTaken;

        public LevelRunData(string levelName, float completionTime, int gruntsKilled, float damageTaken)
        {
            this.levelName = levelName;
            this.completionTime = completionTime;
            this.gruntsKilled = gruntsKilled;
            this.damageTaken = damageTaken;
        }
    }
}
