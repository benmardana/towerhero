public static class GameState
{
    public const int StartingLives = 10;
    public const int StartingWave = 1;
    public const int FinalWave = 5;
    public const int StartingLevel = 1;     // Starting level number - NOT called from MainMenuScene
    public const int FinalLevel = 3;

    public static int lives = StartingLives;  // number of lives (remaining), per level	
    public static int waveNumber = StartingWave;
    public static int levelNumber = StartingLevel;

    // Should be called when a wave or level is reloaded
    public static void ResetLives()
    {
        lives = StartingLives;
    }

    // Should be called when a level is reloaded
    public static void ResetWaveNumber()
    {
        waveNumber = StartingWave;
    }

    public static void ResetLevelNumber()
    {
        levelNumber = StartingLevel;
    }

    // Should get called when a new level is loaded
    public static void SetNextLevelState()
    {
        ResetLives();
        ResetWaveNumber();
        ResourceManager.ResetResources();

        levelNumber++;
        if (levelNumber > FinalLevel) {
            // TODO - Ensure that last level is loaded
        }
    }

    public static void ResetGameState()
    {
        ResetLives();
        ResetWaveNumber();
        ResetLevelNumber();
        ResourceManager.ResetResources();
    }
}
