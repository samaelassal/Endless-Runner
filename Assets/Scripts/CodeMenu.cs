using UnityEngine;

public class PauseResume : MonoBehaviour
{
    public Texture2D pauseIcon; // assign a small icon in Inspector
    private bool isPaused = false;
    private int iconSize = 60; // size of the icon

    private Texture2D normalTexture;
    private Texture2D hoverTexture;

    void Awake()
    {
        // Create colored textures (no GUI calls here)
        normalTexture = new Texture2D(1, 1);
        normalTexture.SetPixel(0, 0, new Color(0.1f, 0.5f, 0.8f, 1f)); // blue
        normalTexture.Apply();

        hoverTexture = new Texture2D(1, 1);
        hoverTexture.SetPixel(0, 0, new Color(0.1f, 0.7f, 1f, 1f)); // lighter blue
        hoverTexture.Apply();
    }

    void Start()
    {
        Time.timeScale = 1f; // game starts running
    }

    void OnGUI()
    {
        // Create GUIStyle inside OnGUI (safe)
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.normal.background = normalTexture;
        buttonStyle.hover.background = hoverTexture;
        buttonStyle.fontSize = 20;
        buttonStyle.alignment = TextAnchor.MiddleCenter;
        buttonStyle.normal.textColor = Color.white;
        buttonStyle.hover.textColor = Color.yellow;
        buttonStyle.border = new RectOffset(8, 8, 8, 8);

        // Draw pause/resume button top-left
        int x = 10;
        int y = 10;

        if (pauseIcon != null)
        {
            if (GUI.Button(new Rect(x, y, iconSize, iconSize), pauseIcon, buttonStyle))
                TogglePause();
        }
        else
        {
            if (GUI.Button(new Rect(x, y, iconSize, iconSize), "||", buttonStyle))
                TogglePause();
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
    }
}