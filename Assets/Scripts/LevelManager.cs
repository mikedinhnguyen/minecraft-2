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

    public static bool gameIsEnded;
    bool done;
    bool gameIsPaused;
    int highScoreInt;
    AudioSource sound;

    RecipeManager rm;
    ItemSlot itemOutput;
    ItemSlot objective;
    int rand;
    // Start is called before the first frame update
    void Start()
    {
        rm = GameObject.Find("CraftingTable").GetComponent<RecipeManager>();
        itemOutput = GameObject.Find("OutputSlot").GetComponent<ItemSlot>();
        objective = GameObject.Find("ObjectiveSlot").GetComponent<ItemSlot>();

        gameIsEnded = false;
        sound = GetComponent<AudioSource>();
        highScoreInt = PlayerPrefs.GetInt("HighScore", 0);
        highScore.text = highScoreInt.ToString();

        rand = Random.Range(0, chooseFrom.Length - 1);
        objective.currentItem = chooseFrom[rand];
        objective.UpdateSlotData();
    }

    // Update is called once per frame
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
        rm.ClearAllSlots();
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
            PlayerPrefs.SetInt("HighScore", scoreInt);
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
