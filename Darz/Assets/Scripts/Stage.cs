using UnityEngine;
using System.Collections;

public class Stage : MonoBehaviour {
    public int number;
    public string title;

	public void Win()
    {
        Debug.Log("Congratulations! You beat Stage " + number + ": " + title + ". Heading to next stage...");
        StageManager.instance.LoadNextStage();
    }

    public void Lose()
    {
        Debug.Log("You lost! Restarting stage...");
        StageManager.instance.RestartStage();
    }
}
