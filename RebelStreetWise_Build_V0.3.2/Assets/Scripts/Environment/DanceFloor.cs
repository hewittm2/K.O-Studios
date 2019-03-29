using UnityEngine;
using UnityEditor;

public class DanceFloor : EditorWindow
{
    GameObject panel = null;
    private GameObject danceFloor;

    int length = 10;
    int depth = 10;
    float panelGap = 1;

    [MenuItem("Window/Dance Floor")]
    public static void ShowWindow()
    {
        GetWindow<DanceFloor>("Dance Floor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Use this window to configure the Dance Floor", EditorStyles.boldLabel);
        
        panel = (GameObject)EditorGUILayout.ObjectField("Dance Floor Panel", panel, typeof(GameObject), true);

        EditorGUILayout.Space();

        GUILayout.Label("In order for this to work correctly...", EditorStyles.boldLabel);
        GUILayout.Label("LENGTH != DEPTH", EditorStyles.boldLabel);
        GUILayout.Label("DEPTH != 5x", EditorStyles.boldLabel);

        EditorGUILayout.Space();

        length = EditorGUILayout.IntSlider("Length", length, 0, 100);
        depth = EditorGUILayout.IntSlider("Depth", depth, 1, 9);
        panelGap = EditorGUILayout.Slider("Panel Gap", panelGap, 0, 10);


        if (GUILayout.Button("Boogie Down"))
        {
            danceFloor = new GameObject("Dance Floor");
            int c = 1;

            for (int l = 1; l < length + 1; l++)
            {
                for (int d = 1; d < depth + 1; d++)
                {
                    Instantiate(panel, new Vector3(l * panelGap - 1, 0, d * panelGap - 1), Quaternion.identity, danceFloor.transform);
                    if (c < panel.GetComponent<PanelV2>().colors.Count)
                    {
                        panel.GetComponent<PanelV2>().start = c;
                        c++;
                    }
                    else if (c == panel.GetComponent<PanelV2>().colors.Count)
                    {
                        panel.GetComponent<PanelV2>().start = c;
                        c = 1;
                    }
                }
            }
        }
    }
}
