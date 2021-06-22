using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    private Text textHighScore = null;
    private void Start()
    {
        Time.timeScale = 1f;
        textHighScore.text = string.Format("BEST\n{0}", PlayerPrefs.GetInt("BEST", 0));
        
    }

    public void onClickStart()
    {
        SceneManager.LoadScene("Main");
    }
}
