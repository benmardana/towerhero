public static class ResourceManager {

    public const int StartingResources = 200;
    public const int EnemyKilledResources = 5;
    public const int PurpleCostResources = 50;
    public const int RedCostResources = 100;
    public const double PercentageResourcesReturned = 0.8;

    public static int resources = StartingResources;

    public static void EnemyIsKilled()
    {
        resources += EnemyKilledResources;
    }

    public static void TurretBuilt(string turretType)
    {
        if (turretType == "Red")
        {
            resources -= RedCostResources;
        }
        else if (turretType == "Purple")
        {
            resources -= PurpleCostResources;
        }
    }

    public static void ReturnResources(string turretType)
    {
        if (turretType == "Red")
        {
            resources += (int)(RedCostResources * PercentageResourcesReturned);
        }

        else if (turretType == "Purple")
        {
            resources += (int)(PurpleCostResources * PercentageResourcesReturned);
        }
    }

    public static void ResetResources()
    {
        resources = StartingResources;
    }
}
