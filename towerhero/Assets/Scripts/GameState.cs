using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameState
{

    public const int startingLives = 3;
    public const int startingWave = 1;
    public const int startingLevel = 1;

    public static int lives = startingLives;  // number of lives (remaining)	
    public static int waveNumber = 1;
    public static int levelNumber = 1;

    // Should be called when a wave or level is reloaded
    public static void ResetLives()
    {
        lives = startingLives;
    }

    // Should be called when a level is reloaded
    public static void ResetWaveNumber()
    {
        waveNumber = 1;
    }

    public static void ResetLevelNumber()
    {
        levelNumber = 1;
    }

    // Should get called when a new level is loaded
    public static void ResetGameState()
    {
        ResetLives();
        ResetWaveNumber();
        ResetLevelNumber();
        ResourceManager.ResetResources();
    }
}
