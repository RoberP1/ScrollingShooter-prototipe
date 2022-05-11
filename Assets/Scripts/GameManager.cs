using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject loseUI;
    [SerializeField] private Text enemyTxt;
    [SerializeField] private AudioSource audio;
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioClip EnemyClip;
    [SerializeField] private AudioClip finishClip;

    public int enemys;
    void Start()
    {
        enemyTxt.text = "kills: 0";
        winUI.gameObject.SetActive(false);
        loseUI.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }
    public void Lose()
    {
      //  music.Pause();
        loseUI.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void Win()
    {
       // music.Pause();
       // audio.clip = finishClip;
       // audio.Play();
        winUI.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void EnemyDie()
    {
       // audio.clip = EnemyClip;
        //audio.Play();
        enemys++;
        enemyTxt.text = "Score: " + enemys.ToString();
    }
}
