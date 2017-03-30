using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using Tiles;
using Doodads;
using UnityEditor.AnimatedValues;

public class StageCreatorMenu : EditorWindow
{
    //Character
    Character character = null;
    //Tiles
    Dirt dirt = null;
    Water water = null;
    Spikes spikes = null;
    //Doodads
    Entrance entrance = null;
    Exit exit = null;
    Boulder boulder = null;
    //Stage File
    string stageFilePath = null;
    //Saved Settings
    const string SETTINGS_CHARACTER = "charPath";
    const string SETTINGS_DIRT = "dirtPath";
    const string SETTINGS_WATER = "waterPath";
    const string SETTINGS_SPIKES = "spikesPath";
    const string SETTINGS_ENTRANCE = "entrancePath";
    const string SETTINGS_EXIT = "exitPath";
    const string SETTINGS_BOULDER = "boulderPath";
    //Editor UI
    bool tileGroupEnabled = true;
    bool doodadGroupEnabled = true;

    [MenuItem("Window/DARZ/Stage Creator")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(StageCreatorMenu));
    }

    void OnDisable()
    {
        //Save settings
        // Character
        if(character != null)
        {
            string charPath = AssetDatabase.GetAssetPath(character);
            EditorPrefs.SetString(SETTINGS_CHARACTER, charPath);
        }
        // Tiles
        if (dirt != null)
        {
            string dirtPath = AssetDatabase.GetAssetPath(dirt);
            EditorPrefs.SetString(SETTINGS_DIRT, dirtPath);
        }
        if (water != null)
        {
            string waterPath = AssetDatabase.GetAssetPath(water);
            EditorPrefs.SetString(SETTINGS_WATER, waterPath);
        }
        if (spikes != null)
        {
            string spikesPath = AssetDatabase.GetAssetPath(spikes);
            EditorPrefs.SetString(SETTINGS_SPIKES, spikesPath);
        }
        // Doodads
        if (entrance != null)
        {
            string entrancePath = AssetDatabase.GetAssetPath(entrance);
            EditorPrefs.SetString(SETTINGS_ENTRANCE, entrancePath);
        }
        if (exit != null)
        {
            string exitPath = AssetDatabase.GetAssetPath(exit);
            EditorPrefs.SetString(SETTINGS_EXIT, exitPath);
        }
        if (boulder != null)
        {
            string boulderPath = AssetDatabase.GetAssetPath(boulder);
            EditorPrefs.SetString(SETTINGS_BOULDER, boulderPath);
        }
    }

    void OnEnable()
    {
        //Load settings
        //  Character
        try
        {
            if (EditorPrefs.HasKey(SETTINGS_CHARACTER))
                this.character = AssetDatabase.LoadAssetAtPath(EditorPrefs.GetString(SETTINGS_CHARACTER), typeof(Character)) as Character;
        }
        catch (System.Exception) { }

        //  Tiles
        try
        {
            if (EditorPrefs.HasKey(SETTINGS_DIRT))
                this.dirt = AssetDatabase.LoadAssetAtPath(EditorPrefs.GetString(SETTINGS_DIRT), typeof(Dirt)) as Dirt;
        }
        catch (System.Exception) { }
        try
        {
            if (EditorPrefs.HasKey(SETTINGS_WATER))
                this.water = AssetDatabase.LoadAssetAtPath(EditorPrefs.GetString(SETTINGS_WATER), typeof(Water)) as Water;
        }
        catch (System.Exception) { }
        try
        {
            if (EditorPrefs.HasKey(SETTINGS_SPIKES))
                this.spikes = AssetDatabase.LoadAssetAtPath(EditorPrefs.GetString(SETTINGS_SPIKES), typeof(Spikes)) as Spikes;
        }
        catch (System.Exception) { }
        //  Doodads
        try
        {
            if (EditorPrefs.HasKey(SETTINGS_ENTRANCE))
                this.entrance = AssetDatabase.LoadAssetAtPath(EditorPrefs.GetString(SETTINGS_ENTRANCE), typeof(Entrance)) as Entrance;
        }
        catch (System.Exception) { }
        try
        {
            if (EditorPrefs.HasKey(SETTINGS_EXIT))
                this.exit = AssetDatabase.LoadAssetAtPath(EditorPrefs.GetString(SETTINGS_EXIT), typeof(Exit)) as Exit;
        }
        catch (System.Exception) { }
        try
        {
            if (EditorPrefs.HasKey(SETTINGS_BOULDER))
                this.boulder = AssetDatabase.LoadAssetAtPath(EditorPrefs.GetString(SETTINGS_BOULDER), typeof(Boulder)) as Boulder;
        }
        catch (System.Exception) { }
    }

    Dictionary<string, Tile> getStageTiles()
    {
        Dictionary<string, Tile> tiles = new Dictionary<string, Tile>();
        tiles["o"] = dirt;
        tiles["w"] = water;
        tiles["s"] = spikes;
        return (tiles);
    }

    Dictionary<string, Doodad> getStageDoodads()
    {
        Dictionary<string, Doodad> doodads = new Dictionary<string, Doodad>();
        doodads["s"] = entrance;
        doodads["b"] = boulder;
        doodads["x"] = exit;
        return (doodads);
    }

    void CreateStage(string stageDefinition)
    {
        Debug.Log("creating stage:\n" + stageDefinition);
        StageCreator stageCreator = new StageCreator(stageDefinition, character, getStageTiles(), getStageDoodads());
        stageCreator.CreateStage();
    }

    void OnGUI()
    {
        GUILayout.Label("Character", EditorStyles.boldLabel);
        character = (Character)EditorGUILayout.ObjectField("Character", character, typeof(Character), false);

        tileGroupEnabled = EditorGUILayout.ToggleLeft("Tile Menu", tileGroupEnabled);
        float showTileGroup = 0;
        if (tileGroupEnabled)
            showTileGroup = 1;
        if (EditorGUILayout.BeginFadeGroup(showTileGroup))
        {
            dirt = (Dirt)EditorGUILayout.ObjectField("Dirt", dirt, typeof(Dirt), false);
            water = (Water)EditorGUILayout.ObjectField("Water", water, typeof(Water), false);
            spikes = (Spikes)EditorGUILayout.ObjectField("Spikes", spikes, typeof(Spikes), false);
        }
        EditorGUILayout.EndFadeGroup();

        doodadGroupEnabled = EditorGUILayout.ToggleLeft("Doodad Menu", doodadGroupEnabled);
        float showDoodadGroup = 0;
        if (doodadGroupEnabled)
            showDoodadGroup = 1;
        if (EditorGUILayout.BeginFadeGroup(showDoodadGroup))
        {
            entrance = (Entrance)EditorGUILayout.ObjectField("Entrance", entrance, typeof(Entrance), false);
            exit = (Exit)EditorGUILayout.ObjectField("Exit", exit, typeof(Exit), false);
            boulder = (Boulder)EditorGUILayout.ObjectField("Boulder", boulder, typeof(Boulder), false);
        }
        EditorGUILayout.EndFadeGroup();

        if(GUILayout.Button("Select stage file"))
        {
            stageFilePath = EditorUtility.OpenFilePanel("Load stage file", "", "");
        }
        if(stageFilePath != null)
        {
            GUILayout.Label("Stage File Path: " + stageFilePath);
        }
        if (GUILayout.Button("Generate Stage"))
        {
            if(stageFilePath == null)
            {
                EditorUtility.DisplayDialog("Error", "You need to select a stage file.", "OK");
            }
            else if(dirt == null || water == null || entrance == null || exit == null || boulder == null)
            {
                EditorUtility.DisplayDialog("Error", "You must assign prefabs to all tile and doodad fields.", "OK");
            }
            else
            {
                string stageStr = System.IO.File.ReadAllText(stageFilePath);
                CreateStage(stageStr);
            }
        }
    }
}
