using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    public TextMeshProUGUI highScore;

    public void Start()
    {
        GrabHighScore();
    }

    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void LowerTransparency(Image image)
    {
        Color alpha = image.color;
        alpha.a = 0.5f;
        image.color = alpha;
    }

    public void NormalTransparency(Image image)
    {
        Color alpha = image.color;
        alpha.a = 1f;
        image.color = alpha;
    }

    public void GrabHighScore()
    {
        int highScoreInt = SaveSystem.LoadPlayer();
        highScore.text = highScoreInt.ToString();
    }

    public void DeletePlayerData()
    {
        //PlayerPrefs.DeleteKey("HighScore");
        SaveSystem.DeletePlayer();
    }
}
