using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceManager {

    public const int StartingResources = 200;
    public const int EnemyKilledResources = 5;
    public const int TurretBuiltResources = 50;
    public const double PercentageResourcesReturned = 0.8;

    public static int resources = StartingResources;

    // TODO(Adam) In future this will take an enemy type and determine amt.
    public static void EnemyIsKilled()
    {
        resources += EnemyKilledResources;
    }

    // TODO(Adam) - This will also take a turret type.
    public static void TurretBuilt()
    {
        resources -= TurretBuiltResources;
    }

    // TODO(Adam) - In future this should take a bridge or turret type and dynamically calculate a percentage (~80%)? to return.
    public static void ReturnResources()
    {
        resources += (int) (TurretBuiltResources * PercentageResourcesReturned);
    }

    public static void ResetResources()
    {
        resources = StartingResources;
    }
}
