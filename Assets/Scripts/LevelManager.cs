using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public SoundManager sm;
    public ItemSO[] chooseFrom;
    public Sprite[] mobIcons;
    public Image endMobIcon;
    public Sprite[] baseTexts;
    public Image baseText;
    public TextMeshProUGUI score;
    public TextMeshProUGUI scoreDiff;
    public TextMeshProUGUI highScore;
    public TextMeshProUGUI beatHighScore;
    public TextMeshProUGUI finalScore;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI timerText;
    public GameObject gameView;
    public GameObject pauseScreen;
    public GameObject endScreen;
    public GameObject timerOptions;

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
    public ObjectiveCheck objCheck;
    int rand;

    void Awake()
    {
        tabWithItem = "";
        gameIsEnded = false;
        //highScoreInt = PlayerPrefs.GetInt("HighScore", 0);
        highScoreInt = SaveSystem.LoadPlayer();
        highScore.text = highScoreInt.ToString();

        rand = Random.Range(0, chooseFrom.Length - 1);
        objective.currentItem = chooseFrom[rand];
        objective.UpdateSlotData();

        objCheck.CheckForBaseItem();

        baseText.sprite = baseTexts[1];

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
        if (Input.GetKeyDown(KeyCode.Escape) && !gameIsPaused && CountdownTimer.canPause)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gameIsPaused && CountdownTimer.canPause)
        {
            UnpauseGame();
        }
    }

    IEnumerator SolveForPlayer()
    {
        sm.PlayClickNoise();
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
        if (scoreInt < 0)
        {
            scoreInt = 0;
        }
        score.text = scoreInt.ToString();

        objCheck.CheckForBaseItem();

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

        objCheck.CheckForBaseItem();

        recipeManager.ClearAllSlots();
    }

    public void Debug()
    {
        int scoreInt = 180;
        score.text = scoreInt.ToString();
    }

    public void Clear()
    {
        sm.PlayClearNoise();
        recipeManager.ClearAllSlots();
    }

    public void GiveHint()
    {
        sm.PlayHintNoise();
        // deduct 5 points
        int scoreInt = int.Parse(score.text);
        scoreInt -= 2;
        if (scoreInt < 0)
        {
            scoreInt = 0;
        }
        score.text = scoreInt.ToString();

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
        baseText.sprite = baseTexts[0];
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
        //sm.PlayFinishedNoise();
        int scoreInt = int.Parse(score.text);
        if (scoreInt > highScoreInt)
        {
            //PlayerPrefs.SetInt("HighScore", scoreInt);
            highScoreInt = scoreInt;
            SaveSystem.SavePlayer(scoreInt);
            beatHighScore.text = "You beat your high score!";
        }
        else
        {
            beatHighScore.text = "";
        }

        int mobNum = 0;

        //if (scoreInt >= 0 && scoreInt <= 19)
        //{
        //    endMobIcon.sprite = mobIcons[0]; // pufferfish
        //}
        //else 
        if (scoreInt >= 20 && scoreInt <= 39)
        {
            mobNum = 1; // bee
        }
        else if (scoreInt >= 40 && scoreInt <= 59)
        {
            mobNum = 2; // chicken
        }
        else if (scoreInt >= 60 && scoreInt <= 79)
        {
            mobNum = 3; // pig
        }
        else if (scoreInt >= 80 && scoreInt <= 99)
        {
            mobNum = 4; // sheep
        }
        else if (scoreInt >= 100 && scoreInt <= 119)
        {
            mobNum = 5; // cow
        }
        else if (scoreInt >= 120 && scoreInt <= 139)
        {
            mobNum = 6; // spider
        }
        else if (scoreInt >= 140 && scoreInt <= 159)
        {
            mobNum = 7; // zombie
        }
        else if (scoreInt >= 160 && scoreInt <= 179)
        {
            mobNum = 8; // skeleton
        }
        else if (scoreInt >= 180 && scoreInt <= 199)
        {
            mobNum = 9; // creeper
        }
        else if (scoreInt >= 200 && scoreInt <= 219)
        {
            mobNum = 10; // piglin
        }
        else if (scoreInt >= 220 && scoreInt <= 239)
        {
            mobNum = 11; // enderman
        }
        else if (scoreInt >= 240 && scoreInt <= 259)
        {
            mobNum = 12; // iron golem
        }
        else if (scoreInt >= 260 && scoreInt <= 279)
        {
            mobNum = 13; // ender dragon
        }
        else if (scoreInt >= 280)
        {
            mobNum = 14; // steve
        }

        endMobIcon.sprite = mobIcons[mobNum];
        sm.PlayMobNoise(mobNum);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ReloadGame()
    {
        //SceneManager.LoadScene(1);
        CountdownTimer.canPause = false;
        practice = false;
        baseText.sprite = baseTexts[1];
        ClearHolding();
        recipeManager.ClearAllSlots();
        timerOptions.SetActive(true);
        timerText.text = ""; 
        Color gray = new Color(77f / 255f, 77f / 255f, 77f / 255f);
        timerText.color = gray;

        tabWithItem = "";
        score.text = "0";
        done = false;
        gameIsEnded = false;
        gameView.SetActive(true);
        endScreen.SetActive(false);

        rand = Random.Range(0, chooseFrom.Length - 1);
        objective.currentItem = chooseFrom[rand];
        objective.UpdateSlotData();

        objCheck.CheckForBaseItem();

        itemName.text = objective.currentItem.itemName;
        playerSolved = true;
    }
}
