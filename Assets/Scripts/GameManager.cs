using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text lapText, countdownText, gameResultText;
    [SerializeField] Slider progressSlider;
    [SerializeField] GameObject panelGameOver;
    [SerializeField] AudioClip countdownClip, startClip;
    [SerializeField] Color green, red;
    [SerializeField] int maxCountdown = 3;

    public string LapText
    {
        get { return lapText.text; }
        set { lapText.text = value; }
    }

    public float ProgressSliderValue
    {
        get { return progressSlider.value; }
        set { progressSlider.value = value; }
    }

    public float ProgressSliderMaxValue
    {
        get { return progressSlider.maxValue; }
        set { progressSlider.maxValue = value; }
    }

    void Start()
    {
        StartCoroutine(StartLevel());
    }

    IEnumerator StartLevel()
    {
        Time.timeScale = 0;
        int countdown = maxCountdown + 1;
        countdownText.color = red;
        while (countdown > 1)
        {
            countdown--;
            countdownText.text = countdown.ToString();
            AudioSource.PlayClipAtPoint(countdownClip, Camera.main.transform.position);
            yield return new WaitForSecondsRealtime(1f);
        }
        countdownText.color = green;
        countdownText.text = "�����!";
        Time.timeScale = 1;
        AudioSource.PlayClipAtPoint(startClip, Camera.main.transform.position);
        yield return new WaitForSecondsRealtime(0.5f);
        countdownText.text = "";
    }

    public void GameOver(string winner)
    {
        Time.timeScale = 0;
        AudioSource.PlayClipAtPoint(startClip, Camera.main.transform.position);
        panelGameOver.SetActive(true);
        gameResultText.text = $"{winner} �������!";
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
