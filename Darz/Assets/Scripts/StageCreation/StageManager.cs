using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tiles;
using Doodads;

public class StageManager : MonoBehaviour {
    public static StageManager instance = null;

    public List<TextAsset> stageFiles = new List<TextAsset>();
    //Character
    public Character character;
    //Tiles
    public Dirt dirt;
    public Water water;
    public Spikes spikes;
    public WeakGround weakGround;
    //Doodads
    public Entrance entrance;
    public Exit exit;
    public Boulder boulder;
    public FireFloor fireFloor;
    public Gate gatePink;
    public Gate gateTeal;
    public Key keyPink;
    public Key keyTeal;

    public Stage CurrentStage
    {
        get
        {
            return currentStage;
        }
    }

    private Stage currentStage = null;
    private int currentStageIndex = -1;
    private Dictionary<string, Tile> tiles;
    private Dictionary<string, Doodad> doodads;

    public List<StageCreator.StageMetadata> StageInfo {
        get
        {
            return stageInfo;
        }
    }
    private List<StageCreator.StageMetadata> stageInfo;

    public void UnloadStage()
    {
        if(currentStage != null)
        {
            Destroy(currentStage.gameObject);
        }
    }

    public void LoadStage(int index)
    {
        if (index >= 0 && index < stageFiles.Count)
        {
            UnloadStage();
            StageCreator stageCreator = new StageCreator(stageFiles[index].text, character, tiles, doodads);
            this.currentStage = stageCreator.CreateStage();
            this.currentStageIndex = index;
        }
        else
        {
            throw new System.Exception("Stage index not in range.");
        }
    }

    public void LoadNextStage()
    {
        if(currentStageIndex >= 0)
        {
            LoadStage(currentStageIndex + 1);
        }
        else
        {
            LoadStage(0);
        }
    }

    public void RestartStage()
    {
        LoadStage(currentStageIndex);
    }

    private void LoadStageDefinitions()
    {
        stageInfo = new List<StageCreator.StageMetadata>();
        foreach(TextAsset stageFile in stageFiles)
        {
            StageCreator.StageMetadata metadata = StageCreator.GetStageMetadata(stageFile.text);
            stageInfo.Add(metadata);
        }
    }

    /// <summary>
    /// Takes the assigned tile objects and assigns them to proper keys in a hash table.
    /// </summary>
    private void InitializeTiles()
    {
        tiles = new Dictionary<string, Tile>();
        tiles["o"] = dirt;
        tiles["w"] = water;
        tiles["s"] = spikes;
        tiles["k"] = weakGround;
    }

    /// <summary>
    /// Takes the assigned doodad objects and assigns them to proper keys in a hash table.
    /// </summary>
    private void InitializeDoodads()
    {
        doodads = new Dictionary<string, Doodad>();
        doodads["e"] = entrance;
        doodads["b"] = boulder;
        doodads["x"] = exit;
        doodads["f"] = fireFloor;
        doodads["g[Pink]"] = gatePink;
        doodads["g[Teal]"] = gateTeal;
        doodads["y[Pink]"] = keyPink;
        doodads["y[Teal]"] = keyTeal;
    }

    //on program start
    void Start()
    {
        LoadStageDefinitions();
        InitializeTiles();
        InitializeDoodads();
    }

    //Runs before start
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            //Enforce singleton pattern
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject.gameObject);
    }
}
