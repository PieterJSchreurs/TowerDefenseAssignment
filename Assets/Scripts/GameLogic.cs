using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    [SerializeField]
    public int startingLives = 10, timeBetweenRounds = 10;

    [SerializeField]
    public Text liveText, waveText, timerText;

    [SerializeField]
    public EnemySpawner enemySpawner;

    private float m_timer;
    private bool m_waveActive = false;
    private int m_currentWave, m_currentLives, m_amountOfWaves;

    public void Start()
    {
        m_amountOfWaves = enemySpawner.GetNumberOfWaves();
        timerText.enabled = true;
        m_timer = timeBetweenRounds;
        m_currentWave = m_amountOfWaves;
        m_currentLives = startingLives;
        SetLives(startingLives);
        SetWaveNumber(m_amountOfWaves, m_amountOfWaves);
    }

    public void Update()
    {
        DoCountdown();
    }
    
    //TODO: notify
    public void UpdateEnemyCount(int pEnemiesLeftToSpawn)
    {
        SetWaveEnemyCount(pEnemiesLeftToSpawn);
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
            SceneManager.LoadScene("GameOverScene");
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
                SetWaveEnemyCount(enemySpawner.GetNumberOfEnemiesInWave());
            }
        } 
    }

    //TODO: notify
    public void WaveCleared()
    {
        m_currentWave--;
        if(m_currentWave <= 0)
        {
            SceneManager.LoadScene("VictoryScene");
        }
        SetWaveNumber(m_amountOfWaves, m_currentWave);
        m_waveActive = false;
        m_timer = timeBetweenRounds;
    }

    //TODO: notify
    public void LostLive()
    {
        m_currentLives--;
        SetLives(m_currentLives);
    }

}
