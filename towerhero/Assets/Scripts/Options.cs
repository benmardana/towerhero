using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Options {

    public const float MinDifficulty = 0.0f;

    // C# style getter and setter, used to change other attributes when difficulty is changed
    // Difficulty acts as an interface to _difficulty, the underlying variable
    private static float _difficulty = MinDifficulty;
    public static float difficulty
    {
        get
        {
            return Options._difficulty;
        }

        set
        {
            Options._difficulty = value;
            Options.enemyMovementSpeedMultiplier = (1.0f + value);
        }
    }

    // Note: Each enemy type has a unique movement speed
    public static float enemyMovementSpeedMultiplier = (1f + difficulty);

}
