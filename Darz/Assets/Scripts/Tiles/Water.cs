using UnityEngine;
using System.Collections;
using System;
using Doodads;

namespace Tiles
{
    public class Water : Tile
    {
        public Tile dirtyWaterTile;

        public override void OnEnter(Tile origin, Character character)
        {
            character.Kill();
        }

        public override void AddDoodad(Doodad doodad)
        {
            base.AddDoodad(doodad);

            if(doodad.GetType() == typeof(Boulder))
            {
                //boulder pushed into water, so replace water with dirty water
                this.Replace(dirtyWaterTile);
                Destroy(doodad.gameObject);
            }
        }

        public override void OnExit(Tile destination, Character character)
        {

        }

    }
}