﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailController : Controller
{
    // Update is called once per frame
    void Update()
    {
        if (pawn != null)
        {
            //move pawn forward
            pawn.Move(-pawn.transform.right);
        }
    }
}
