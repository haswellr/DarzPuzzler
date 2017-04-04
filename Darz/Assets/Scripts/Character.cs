using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tiles;
using Doodads;
using Items;

public class Character : MonoBehaviour {

    public Tile location;
    [HideInInspector]
    public List<Items.Key> keys = new List<Items.Key>();

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
        if(this.location != null)
        {
            //Exit current tile, triggering 'OnExit'
            Tile[] oldTiles = this.location.GetComponents<Tile>();
            foreach (Tile tile in oldTiles)
            {
                tile.OnExit(newLocation, this);
            }
            Doodad[] oldDoodads = this.location.GetComponentsInChildren<Doodad>();
            foreach (Doodad doodad in oldDoodads)
            {
                doodad.OnExit(newLocation, this);
            }
        }
        //Enter new tile, triggering 'OnEnter'
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

    private void OnGUI()
    {
        float height = 35 + 25 * keys.Count;
        GUILayout.BeginArea(new Rect(Screen.width - 50, Screen.height - height, 50, height));
        GUILayout.Label("KEYS:");
        for (int i = 0; i < keys.Count; i++)
        {
            GUILayout.Label(keys[i].Color);
        }
        GUILayout.EndArea();
    }
}
