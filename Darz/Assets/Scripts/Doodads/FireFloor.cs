using UnityEngine;
using System.Collections;
using System;
using Tiles;

namespace Doodads
{
    public class FireFloor : Doodad
    {
        public ParticleSystem fire;

        private ParticleSystem fireEmitter = null;
        private bool fireOn = false;

        private void ToggleFire()
        {
            fireOn = !fireOn;
            if (fireOn)
            {
                //Create fire emitter (TODO: we shouldn't have to create and destroy this every time. I did it this way because Unity's garbage collector was cleaning up our emitters for some reason.)
                fireEmitter = Instantiate(fire);
                Vector3 localPosition = fireEmitter.transform.localPosition;
                Vector3 localScale = fireEmitter.transform.localScale;
                fireEmitter.transform.SetParent(this.transform);
                fireEmitter.transform.localPosition = localPosition;
                fireEmitter.transform.localScale = localScale;

                Character[] characters = FindObjectsOfType<Character>();
                Tile tile = this.gameObject.GetComponentInParent<Tile>();
                for(int i = 0; i < characters.Length; i++)
                {
                    if(characters[i].location == tile)
                    {
                        characters[i].Kill();
                    }
                }
            }
            else
            {
                Destroy(fireEmitter.gameObject);
            }
        }

        private void Start()
        {
            InvokeRepeating("ToggleFire", 3f, 3f);
        }

        public override void OnEnter(Tile origin, Character character)
        {
            if (fireOn)
            {
                character.Kill();
            }
        }

        public override void OnExit(Tile destination, Character character)
        {

        }
    }
}
