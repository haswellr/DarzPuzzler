using UnityEngine;
using System.Collections;
using Tiles;
using Doodads;

public class Character : MonoBehaviour {

    public Tile location;

    public void MoveTo(Tile destination)
    {
        if(destination.IsPassable(this.location, this.gameObject))
        {
            transform.position = destination.transform.position;
            EnterTile(destination);
        }
        else
        {
            Debug.Log("Can't move there!");
        }
    }

    private void EnterTile(Tile newLocation)
    {
        Tile[] tiles = newLocation.GetComponents<Tile>();
        foreach(Tile tile in tiles)
        {
            tile.OnEnter(this.location, this);
        }
        Doodad[] doodads = newLocation.GetComponentsInChildren<Doodad>();
        foreach (Doodad doodad in doodads)
        {
            doodad.OnEnter(this.location, this);
        }
        this.location = newLocation;
    }

    public void Kill()
    {
        Debug.Log("GRRRRRRAAAGHH! I died!");
        Stage stage = StageManager.instance.CurrentStage;
        stage.Lose();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(this.location.up != null)
            {
                this.MoveTo(this.location.up);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (this.location.right != null)
            {
                this.MoveTo(this.location.right);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (this.location.down != null)
            {
                this.MoveTo(this.location.down);
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (this.location.left != null)
            {
                this.MoveTo(this.location.left);
            }
        }
    }
}
