using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    [SerializeField]
    public int startingLives = 10, startingWaves = 2, timeBetweenRounds = 10;

    [SerializeField]
    public TMP_Text liveText, waveText, timerText;

    [SerializeField]
    public EnemySpawner enemySpawner;

    private float m_timer;
    private bool m_waveActive = false;
    private int m_currentWave, m_currentLives;

    public void Start()
    {
        timerText.enabled = true;
        m_timer = timeBetweenRounds;
        m_currentWave = startingWaves;
        m_currentLives = startingLives;
        SetLives(startingLives);
        SetWaveNumber(startingWaves, startingWaves);
    }

    public void Update()
    {
        DoCountdown();
    }

    private void SetWaveEnemyCount(int pEnemyNumberInWave)
    {
        timerText.text = "Enemies: " + pEnemyNumberInWave;
    }

    private void SetWaveNumber(int pMaxWaveNumbers, int pCurrentWaveNumber)
    {
        waveText.text = "Wave: " + pCurrentWaveNumber + "/" + pMaxWaveNumbers;
    }

    private void SetLives(int pLives)
    {
        if(pLives <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        liveText.text = "Lives: " + pLives;
    }

    private void SetTimerText(int pTimeLeft)
    {
        timerText.text = "Next wave in " + pTimeLeft;
    }

    private void DoCountdown()
    {
        if (!m_waveActive)
        {
            if (m_timer >= 0)
            {
                m_timer -= Time.deltaTime;
                SetTimerText((int)m_timer);
            }
            else
            {
                m_waveActive = true;
                m_timer = timeBetweenRounds;
                enemySpawner.ActivateWave();
            }
        } else
        {
            SetWaveEnemyCount(enemySpawner.GetNumberOfEnemiesInWave());
        }
    }

    //TODO: notify
    public void WaveCleared()
    {
        m_currentWave--;
        SetWaveNumber(startingWaves, m_currentWave);
    }

    //TODO: notify
    public void LostLive()
    {
        m_currentLives--;
        SetLives(m_currentLives);
    }

}
