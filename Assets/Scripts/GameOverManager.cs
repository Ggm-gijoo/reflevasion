using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    private Text textHighScore = null;

    [SerializeField]
    private int highScore;

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("BEST\n", 0);
        textHighScore.text = string.Format("BEST\n {0}", highScore);
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene("Main");
    }
}
