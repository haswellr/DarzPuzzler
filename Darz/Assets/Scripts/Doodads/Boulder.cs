using UnityEngine;
using System.Collections;
using System;
using Tiles;

namespace Doodads
{
    public class Boulder : Doodad
    {
        /// <summary>
        /// Gets the tile that this boulder will be pushed into if the boulder is pushed from a specified origin.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        private Tile GetTileTargetOfPush(Tile origin)
        {
            Tile tile = this.GetComponentInParent<Tile>();
            Tile nextTile = null;
            if (origin == tile.up)
            {
                nextTile = tile.down;
            }
            else if (origin == tile.right)
            {
                nextTile = tile.left;
            }
            else if (origin == tile.down)
            {
                nextTile = tile.up;
            }
            else if (origin == tile.left)
            {
                nextTile = tile.right;
            }
            return nextTile;
        }

        private bool CanBePushed(Tile pushOrigin)
        {
            Tile nextTile = GetTileTargetOfPush(pushOrigin);
            return (nextTile != null && nextTile.IsPassable(pushOrigin, this.gameObject));
        }

        public override bool IsTilePassable(Tile moveOrigin, GameObject movingObject)
        {
            if(movingObject.GetComponent<Boulder>() != null)
            {
                //other boulders can't move onto this tile
                return false;
            }
            else if(movingObject.GetComponent<Character>() != null && !CanBePushed(moveOrigin))
            {
                //characters can't move in if the boulder is not pushable
                return false;
            }
            else
            {
                return true;
            }
        }

        public override void OnEnter(Tile origin, Character character)
        {
            Tile nextTile = GetTileTargetOfPush(origin);
            if (CanBePushed(origin))
            {
                //Push boulder
                nextTile.AddDoodad(this);
            }
        }

        public override void OnExit(Tile destination, Character character)
        {
            
        }
    }
}
