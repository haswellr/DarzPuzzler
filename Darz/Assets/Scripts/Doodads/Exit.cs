using UnityEngine;
using System.Collections;
using System;
using Tiles;

namespace Doodads
{
    public class Exit : Doodad
    {
        public override void OnEnter(Tile origin, Character character)
        {
            Debug.Log("entering exit tile");
            Stage stage = StageManager.instance.CurrentStage;
            stage.Win();
        }
    }
}
