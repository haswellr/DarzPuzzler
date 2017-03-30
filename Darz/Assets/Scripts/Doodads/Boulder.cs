using UnityEngine;
using System.Collections;
using System;
using Tiles;

namespace Doodads
{
    public class Boulder : Doodad
    {
        public override void OnEnter(Tile origin, Character character)
        {
            Debug.Log("Bonk! You hit a boulder. You dumb bitch.");
            Tile tile = this.GetComponentInParent<Tile>();
            Tile nextTile = null;
            if(origin == tile.up)
            {
                nextTile = tile.down;
            }
            else if(origin == tile.right)
            {
                nextTile = tile.left;
            }
            else if(origin == tile.down)
            {
                nextTile = tile.up;
            }
            else if(origin == tile.left)
            {
                nextTile = tile.right;
            }
            if(nextTile != null)
            {
                //Push boulder

                //IF(nextTile.isPassable()){
                //move boulder over
                nextTile.AddDoodad(this);
                //}
            }
            
        }
    }
}
