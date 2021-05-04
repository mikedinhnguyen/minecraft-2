using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public SoundManager sm;
    public ItemSO[] chooseFrom;
    public TextMeshProUGUI score;
    public TextMeshProUGUI scoreDiff;
    public TextMeshProUGUI highScore;
    public TextMeshProUGUI beatHighScore;
    public TextMeshProUGUI finalScore;
    public TextMeshProUGUI itemName;
    public GameObject gameView;
    public GameObject pauseScreen;
    public GameObject endScreen;

    public static string tabWithItem;
    public static bool gameIsEnded;
    public bool practice;
    bool done;
    bool gameIsPaused;
    bool playerSolved;
    bool pass;
    int highScoreInt;

    public RecipeManager recipeManager;
    public ItemSlot itemOutput;
    public ItemSlot objective;
    public ItemSlot holdingSlot;
    int rand;

    void Start()
    {
        tabWithItem = "";
        gameIsEnded = false;
        //highScoreInt = PlayerPrefs.GetInt("HighScore", 0);
        highScoreInt = SaveSystem.LoadPlayer();
        highScore.text = highScoreInt.ToString();

        rand = Random.Range(0, chooseFrom.Length - 1);
        objective.currentItem = chooseFrom[rand];
        objective.UpdateSlotData();
        itemName.text = objective.currentItem.itemName;
        playerSolved = true;
    }
    
    void Update()
    {
        if (itemOutput.currentItem != null && objective.currentItem != null)
        {
            if (itemOutput.currentItem == objective.currentItem && playerSolved)
            {
                // pick next objective item that's different from current item
                while (itemOutput.currentItem == objective.currentItem)
                {
                    rand = Random.Range(0, chooseFrom.Length);
                    objective.currentItem = chooseFrom[rand];
                }

                StartCoroutine(ScorePoint());
                sm.PlayWinNoise();
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

    IEnumerator SolveForPlayer()
    {
        recipeManager.Solve(objective);
        yield return new WaitForSeconds(1f);
        recipeManager.ClearAllSlots();

        string currItem = objective.currentItem.itemName;

        while (objective.currentItem.itemName == currItem)
        {
            rand = Random.Range(0, chooseFrom.Length);
            objective.currentItem = chooseFrom[rand];
        }

        // deduct points equal to previous objective
        objective.UpdateSlotData();
        itemName.text = objective.currentItem.itemName;
        int scoreInt = int.Parse(score.text);
        scoreInt -= recipeManager.recipeValue;
        score.text = scoreInt.ToString();

        //scoreDiff.text = "-" + recipeManager.recipeValue.ToString();
        //Color red = new Color(209f / 255f, 47f / 255f, 44f / 255f);
        //scoreDiff.color = red;

        //scoreDiff.canvasRenderer.SetAlpha(1.0f);
        //scoreDiff.CrossFadeAlpha(0, 2, false); // fade out

        sm.PlayPassNoise();
        playerSolved = true;
    }

    IEnumerator ScorePoint()
    {
        yield return new WaitForSeconds(0.5f);
        objective.UpdateSlotData();
        itemName.text = objective.currentItem.itemName;
        // update score
        int scoreInt = int.Parse(score.text);
        scoreInt += recipeManager.recipeValue;
        score.text = scoreInt.ToString();

        //scoreDiff.text = "+" + recipeManager.recipeValue.ToString();
        //Color green = new Color(82f/255f, 189f/255f, 40f/255f);
        //scoreDiff.color = green;

        //scoreDiff.canvasRenderer.SetAlpha(1.0f);
        //scoreDiff.CrossFadeAlpha(0, 2, false); // fade out

        recipeManager.ClearAllSlots();
    }

    public void Clear()
    {
        sm.PlayClearNoise();
        recipeManager.ClearAllSlots();
    }

    public void GiveHint()
    {
        // deduct 5 points
        int scoreInt = int.Parse(score.text);
        scoreInt -= 2;
        score.text = scoreInt.ToString();

        scoreDiff.text = "-2";
        Color red = new Color(209f / 255f, 47f / 255f, 44f / 255f);
        scoreDiff.color = red;

        scoreDiff.canvasRenderer.SetAlpha(1.0f);
        scoreDiff.CrossFadeAlpha(0, 2, false); // fade out

        recipeManager.Hint(objective);
    }

    public void Pass()
    {
        // change objectives
        recipeManager.FindRecipeValue(objective);
        playerSolved = false;
        StartCoroutine(SolveForPlayer());
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

    public void PracticeMode()
    {
        practice = true;
    }

    public void PauseGame()
    {
        Timer.isRunning = false;
        gameView.SetActive(false);
        pauseScreen.SetActive(true);
        gameIsPaused = true;
    }

    public void UnpauseGame()
    {
        if (!practice)
        {
            Timer.isRunning = true;
        }
        
        gameView.SetActive(true);
        pauseScreen.SetActive(false);
        gameIsPaused = false;
      
        Color color = scoreDiff.color;
        color.a = 0;
        scoreDiff.color = color;
    }

    void StopGame()
    {
        gameView.SetActive(false);
        endScreen.SetActive(true);
        finalScore.text = score.text;
        sm.PlayFinishedNoise();
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
