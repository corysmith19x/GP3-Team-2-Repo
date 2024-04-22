using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class LevelStatTracker : MonoBehaviour
{
    public int gruntsKilled { get; private set; }
    public int bossCaptured { get; private set; }
    private float damageTaken;

    public GameObject victoryPanel;
    public GameObject closeBtn;

    public TextMeshProUGUI levelNameText;
    public TextMeshProUGUI completionTimeText;
    public TextMeshProUGUI gruntsKilledText;
    public TextMeshProUGUI damageTakenText;

    public static LevelStatTracker instance;

    private void Awake()
    {
        instance = this;
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
        ResetStats();
    }

    public void Grunts()
    {
        gruntsKilled++;
        CheckVictory();
    }

    public void Boss()
    {
        bossCaptured++;
        CheckVictory();
    }

    public void DamageTaken(float amount)
    {
        damageTaken += amount;
    }

    private void CheckVictory()
    {
        if (bossCaptured == 1)
        {
            if (victoryPanel != null)
            {
                victoryPanel.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                EventSystem.current.SetSelectedGameObject(closeBtn);
            }
            else
            {
                Debug.LogError("Victory panel is not assigned!");
                return;
            }

            if (levelNameText != null)
            {
                levelNameText.text = "Level: " + SceneManager.GetActiveScene().name;
            }
            else
            {
                Debug.LogError("Level name text is not assigned!");
                return;
            }

            if (completionTimeText != null && gruntsKilledText != null && damageTakenText != null)
            {
                completionTimeText.text = "Completion Time: " + (Time.timeSinceLevelLoad).ToString("F2") + " seconds";
                gruntsKilledText.text = "Enemies Killed: " + gruntsKilled;
                damageTakenText.text = "Damage Taken: " + damageTaken.ToString("F0");
            }
            else
            {
                Debug.LogError("One or more text components are not assigned!");
                return;
            }
        }
    }

    private void ResetStats()
    {
        gruntsKilled = 0;
        bossCaptured = 0;
        damageTaken = 0f;
    }
}
