using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public ItemSO[] chooseFrom;
    public TextMeshProUGUI score;
    public TextMeshProUGUI highScore;
    public TextMeshProUGUI beatHighScore;
    public TextMeshProUGUI finalScore;
    public GameObject gameView;
    public GameObject pauseScreen;
    public GameObject endScreen;
    public AudioClip correct;
    public AudioClip finished;

    public static string tabWithItem;
    public static bool gameIsEnded;
    bool done;
    bool gameIsPaused;
    int highScoreInt;
    AudioSource sound;

    public RecipeManager recipeManager;
    public ItemSlot itemOutput;
    public ItemSlot objective;
    public ItemSlot holdingSlot;
    int rand;

    void Start()
    {
        tabWithItem = "";
        gameIsEnded = false;
        sound = GetComponent<AudioSource>();
        //highScoreInt = PlayerPrefs.GetInt("HighScore", 0);
        highScoreInt = SaveSystem.LoadPlayer();
        highScore.text = highScoreInt.ToString();

        rand = Random.Range(0, chooseFrom.Length - 1);
        objective.currentItem = chooseFrom[rand];
        objective.UpdateSlotData();
    }
    
    void Update()
    {
        if (itemOutput.currentItem != null && objective.currentItem != null)
        {
            if (itemOutput.currentItem == objective.currentItem)
            {
                // pick next objective item that's different from current item
                while (itemOutput.currentItem == objective.currentItem)
                {
                    rand = Random.Range(0, chooseFrom.Length);
                    objective.currentItem = chooseFrom[rand];
                }

                StartCoroutine(ScorePoint());
                sound.PlayOneShot(correct, 0.5f);
            }
        }
        if (!Timer.isRunning && gameIsEnded && !done)
        {
            StopGame();
            done = true; // to not loop the sound over again
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !gameIsPaused)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gameIsPaused)
        {
            UnpauseGame();
        }
    }

    IEnumerator ScorePoint()
    {
        yield return new WaitForSeconds(0.5f);
        objective.UpdateSlotData();
        // update score
        int scoreInt = int.Parse(score.text);
        scoreInt += RecipeManager.recipeValue;
        score.text = scoreInt.ToString();
        recipeManager.ClearAllSlots();
    }

    public static void CleanUpSelectors(Transform inventory)
    {
        for (int i = 0; i < inventory.childCount; i++)
        {
            ItemSlot item = inventory.GetChild(i).gameObject.GetComponent<ItemSlot>();
            item.itemSelector.gameObject.SetActive(false);
        }
    }
    
    public static void CheckForTab(Transform tab)
    {
        if (!tab.gameObject.activeInHierarchy)
        {
            // Debug.Log("cleaning up " + tab.name);
            CleanUpSelectors(tab);
        }
    }

    public void ClearHolding()
    {
        holdingSlot.currentItem = null;
        holdingSlot.UpdateSlotData();
    }

    void PauseGame()
    {
        Timer.isRunning = false;
        gameView.SetActive(false);
        pauseScreen.SetActive(true);
        gameIsPaused = true;
    }

    public void UnpauseGame()
    {
        Timer.isRunning = true;
        gameView.SetActive(true);
        pauseScreen.SetActive(false);
        gameIsPaused = false;
    }

    void StopGame()
    {
        gameView.SetActive(false);
        endScreen.SetActive(true);
        finalScore.text = score.text;
        sound.PlayOneShot(finished, 0.5f);
        int scoreInt = int.Parse(score.text);
        if (scoreInt > highScoreInt)
        {
            //PlayerPrefs.SetInt("HighScore", scoreInt);
            SaveSystem.SavePlayer(scoreInt);
            beatHighScore.text = "You beat your high score!";
        }
        else
        {
            beatHighScore.text = "";
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(1);
    }
}
