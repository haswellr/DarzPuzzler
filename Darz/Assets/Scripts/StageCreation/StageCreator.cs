using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tiles;
using Doodads;

public class StageCreator {

    private string stageDefinition;
    private Dictionary<string, Tiles.Tile> tiles;
    private Dictionary<string, Doodads.Doodad> doodads;
    private Character character;

	public StageCreator(string stageDefinition, Character character, Dictionary<string, Tiles.Tile> tiles, Dictionary<string, Doodads.Doodad> doodads)
    {
        this.stageDefinition = stageDefinition;
        this.character = character;
        this.tiles = tiles;
        this.doodads = doodads;
    }

    private void PrintStageDefinition()
    {
        Debug.Log("expected stage file:\nSTAGE_NUMBER\nSTAGE_NAME\nTILE(DOODAD,DOODAD,...),TILE(DOODAD,DOODAD,...),...\nTILE(DOODAD,DOODAD,...),TILE(DOODAD,DOODAD,...),...");
    }

    public struct StageMetadata
    {
        public int number;
        public string title;
    }

    public static StageMetadata GetStageMetadata(string stageDefinition)
    {
        string[] linesArr = stageDefinition.Split(new string[] { "\r\n", "\n" }, System.StringSplitOptions.None);
        StageMetadata metadata = new StageMetadata();
        //Parse stage number and name
        metadata.number = System.Int32.Parse(linesArr[0]);
        metadata.title = linesArr[1];
        return metadata;
    }

    public Stage CreateStage()
    {
        PrintStageDefinition();
        //Create stage object which will be the parent for everything else
        GameObject stageObj = new GameObject("Stage");
        Stage stage = stageObj.AddComponent<Stage>();
        //Create character
        Character newCharacter = GameObject.Instantiate(character);
        newCharacter.transform.SetParent(stage.transform);
        //Read stage from definition
        string[] linesArr = stageDefinition.Split(new string[] { "\r\n", "\n" }, System.StringSplitOptions.None);
        List<string> lines = new List<string>(linesArr);
        //Parse stage metadata
        StageMetadata metadata = GetStageMetadata(stageDefinition);
        stage.number = metadata.number;
        stage.title = metadata.title;
        //Remove metadata from file
        lines.RemoveRange(0, 2);
        //Parse stage file for tiles and doodads
        float x = 0;
        List<List<Tile>> createdTiles = new List<List<Tile>>();
        for(int i = 0; i < lines.Count; i++)
        {
            List<Tile> currentRowTiles = new List<Tile>();
            createdTiles.Add(currentRowTiles);
            float nextX = 0;
            float z = 0;
            string line = lines[i];
            //Debug.Log("Looking at line: " + line);
            string[] elements = line.Split(new string[] { "," }, System.StringSplitOptions.None);
            for(int j = 0; j < elements.Length; j++)
            {
                string element = elements[j];
                //Debug.Log("Looking at element: " + element);
                int doodadIndex = element.IndexOf("(");
                string tileStr;
                string[] doodadStrs = new string[0];
                if (doodadIndex != -1)
                {
                    string doodadStr = element.Substring(doodadIndex + 1, element.Length - doodadIndex - 2);
                    //Debug.Log("DoodadStr: " + doodadStr);
                    doodadStrs = doodadStr.Split(new string[] { "," }, System.StringSplitOptions.None);

                    tileStr = element.Substring(0, doodadIndex);
                    //Debug.Log("TileStr: " + tileStr);
                }
                else
                {
                    tileStr = element;
                    //Debug.Log("TileStr: " + tileStr);
                }
                //Create Tile
                Debug.Log("tile str: " + tileStr);
                Tile tile = GameObject.Instantiate(tiles[tileStr]);
                tile.transform.position = new Vector3(x, 0, z);
                nextX = tile.transform.position.x + tile.GetComponent<Renderer>().bounds.size.x;
                z = tile.transform.position.z + tile.GetComponent<Renderer>().bounds.size.z;
                tile.transform.SetParent(stage.transform);
                currentRowTiles.Add(tile);
                //Set tile neighbors
                if (i > 0)
                {
                    tile.up = createdTiles[i - 1][j];
                    tile.up.down = tile;
                }
                if(j > 0)
                {
                    tile.left = createdTiles[i][j - 1];
                    tile.left.right = tile;
                }
                //Create Doodads
                List<Doodad> doodadObjects = new List<Doodad>();
                for(var k = 0; k < doodadStrs.Length; k++)
                {
                    Doodad doodad = GameObject.Instantiate(doodads[doodadStrs[k]]);
                    tile.AddDoodad(doodad);
                    doodadObjects.Add(doodad);
                }
            }
            x = nextX;
        }
        return stage;
    }
}
