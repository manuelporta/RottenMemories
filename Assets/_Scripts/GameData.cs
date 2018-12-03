﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class GameData
{

    public int playerMadness=0;
    public float playerPosX = -14.437f;
    public float playerPosY = 5.742f;
    public float playerPosZ = -36.241f;

    public float musicVolume = 0.5f;
    public float fxVolume = 0.5f;

    public bool[] foodTaken = new bool[6] { false, false, false, false, false, false };
    public bool[] diaryPageTaken = new bool[4] { false, false, false, false };
    public bool makeUpTaken = false;
    public bool ladderTaken = false;
    public bool wineTaken = false;
    public bool luculoTaken = false;

    public int SceneID;
}