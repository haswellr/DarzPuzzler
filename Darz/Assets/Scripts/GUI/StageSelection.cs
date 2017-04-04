using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameGUI
{
    public class StageSelection : MonoBehaviour
    {

        private StageManager stageManager;

        // Use this for initialization
        void Start()
        {
            stageManager = Object.FindObjectOfType<StageManager>();
        }

        private void OnGUI()
        {
            GUILayout.Label("Select a Stage");
            for (int i = 0; i < stageManager.StageInfo.Count; i++)
            {
                string buttonLabel = stageManager.StageInfo[i].number + ". " + stageManager.StageInfo[i].title;
                if (stageManager.CurrentStage != null && stageManager.CurrentStage.number == stageManager.StageInfo[i].number)
                    buttonLabel = "* " + buttonLabel;
                if (GUILayout.Button(buttonLabel))
                {
                    stageManager.LoadStage(i);
                }
            }
        }
    }
}
