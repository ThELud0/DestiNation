using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStateResources
{
    public static bool mouseButtonHeldDown = false;
    public static bool mouseButtonReleased = false;
    public static bool trainStationSelected = false;
    public static bool humanReached = false;
    public static int currentTrainStationId = 0;
    public static int currentRailOrderId = 0;

    public static bool zAxisFixed = false;
    public static bool xAxisFixed = false;

    public static int currentFixedX = 0;
    public static int currentFixedZ = 0;

    public static int currentX = 0;
    public static int currentZ = 0;
    public static int previousX = 0;
    public static int previousZ = 0;

    public static int currentIdGeneration = 0;

    public static int trainstationDestinyType;
    public static Vector3 trainstationPosition;
}
