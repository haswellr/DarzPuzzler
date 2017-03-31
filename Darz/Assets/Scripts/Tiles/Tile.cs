using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Doodads;

namespace Tiles
{
    public abstract class Tile : MonoBehaviour
    {
        public Tile up, right, left, down;

        public abstract void OnEnter(Tile origin, Character character);

        public virtual void AddDoodad(Doodad doodad)
        {
            Vector3 localPosition = doodad.transform.localPosition;
            Vector3 localScale = doodad.transform.localScale;
            doodad.transform.SetParent(this.transform);
            doodad.transform.localPosition = localPosition;
            doodad.transform.localScale = localScale;
        }

        public void Replace(Tile replacement)
        {
            GameObject newObj = Instantiate(replacement.gameObject, this.transform.position, this.transform.rotation, this.transform.parent);
            Tile tile = newObj.GetComponent<Tile>();
            tile.up = this.up;
            if(tile.up != null)
            {
                tile.up.down = tile;
            }
            tile.right = this.right;
            if(tile.right != null)
            {
                tile.right.left = tile;
            }
            tile.down = this.down;
            if(tile.down != null)
            {
                tile.down.up = tile;
            }
            tile.left = this.left;
            if(tile.left != null)
            {
                tile.left.right = tile;
            }
            Destroy(this);
        }

        public virtual bool IsPassable(Tile moveOrigin, GameObject movingObject)
        {
            Doodad[] doodads = this.gameObject.GetComponentsInChildren<Doodad>();
            foreach(Doodad doodad in doodads)
            {
                if(!doodad.IsTilePassable(moveOrigin, movingObject)){
                    return false;
                }
            }
            return true;
        }

    }

}