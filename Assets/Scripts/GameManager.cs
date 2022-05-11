using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject loseUI;
    [SerializeField] private Text enemyTxt;

    [Header("Audio")]
    [SerializeField] private AudioSource audio;
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource bossmusic;
    [SerializeField] private AudioClip finishClip;

    [Header("Spawns")]
    [SerializeField] private GameObject boss;
    private Spawner spawner;
    private bool bossfight;
    public int score;
    void Start()
    {
        spawner = FindObjectOfType<Spawner>();
        enemyTxt.text = "Score: 0";
        winUI.gameObject.SetActive(false);
        loseUI.gameObject.SetActive(false);
    }

    void Update()
    {
        if (score > 10 && !bossfight)
        {
            bossfight = true;
            spawner.boss = true;
            Instantiate(boss, spawner.transform.position, Quaternion.identity);
            music.Stop();
            StartCoroutine(MusicBoss(3));

        }
    }
    public void Lose()
    {
        music.Pause();
        loseUI.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void Win()
    {
        bossmusic.Pause();
        audio.clip = finishClip;
        audio.Play();
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
        score++;
        enemyTxt.text = "Score: " + score.ToString();
    }
    public IEnumerator MusicBoss(float delay)
    {
        yield return new WaitForSeconds(delay);
        bossmusic.Play();
    }
}
