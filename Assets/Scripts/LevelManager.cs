using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public ItemSO[] chooseFrom;
    public Text score;
    public TextMeshProUGUI finalScore;
    public GameObject gameView;
    public GameObject endScreen;
    public AudioClip correct;
    public AudioClip finished;

    bool gameIsEnded;
    AudioSource sound;

    RecipeManager rm;
    ItemSlot itemOutput;
    ItemSlot objective;
    int rand;
    // Start is called before the first frame update
    void Start()
    {
        rm = GameObject.Find("CraftingTable").GetComponent<RecipeManager>();
        itemOutput = GameObject.Find("HoldingSlot").GetComponent<ItemSlot>();
        objective = GameObject.Find("ObjectiveSlot").GetComponent<ItemSlot>();
        
        gameIsEnded = false;
        sound = GetComponent<AudioSource>();

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
                
                objective.UpdateSlotData();
                // score point
                ScorePoint();
                rm.ClearAllSlots();
                sound.clip = correct;
                sound.Play();
            }
        }
        if (!Timer.isRunning && !gameIsEnded)
        {
            StopGame();
            gameIsEnded = true;
        }
    }

    void ScorePoint()
    {
        // update score
        int scoreInt = int.Parse(score.text);
        scoreInt += RecipeManager.recipeValue;
        score.text = scoreInt.ToString();
    }

    void StopGame()
    {
        gameView.SetActive(false);
        endScreen.SetActive(true);
        finalScore.text = score.text;
        sound.clip = finished;
        sound.Play();
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
