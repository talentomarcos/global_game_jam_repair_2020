﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public static bool IsPause = false;
    public static float MinVelClamp = .1f;

    public static int LaneAmount = 3;

}

public enum Runes
{
    FIRE,
    STRENGTH,
    OCCULT,
    ENERGY
}
