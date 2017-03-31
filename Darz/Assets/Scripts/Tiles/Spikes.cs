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

        public override void OnExit(Tile destination, Character character)
        {

        }

    }
}