using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameLogic : MonoBehaviour
{
    [SerializeField]
    public int startingLives = 10, startingWaves = 5;

    [SerializeField]
    public TMP_Text liveText, waveText;


    public void Start()
    {
        
    }

    private void SetWaveNumber(int pMaxWaveNumbers, int pCurrentWaveNumber)
    {
        waveText.text = "Wave: " + pCurrentWaveNumber + "/" + pMaxWaveNumbers;
    }

    private void SetLives(int pLives)
    {
        liveText.text = "Lives: " + pLives;
    }

}
