using UnityEngine;
using System.Collections;
using System;

namespace Tiles
{
    public class Spikes : Tile
    {

        public override void OnEnter(Tile origin, Character character)
        {
            character.Kill();
        }

    }
}