using UnityEngine;
using UnityEditor;

public class LevelEditorWindow : EditorWindow
{
    int selectedAction = 0;

    [MenuItem("Window/Level Editor")]
    static void Init()
    {
        LevelEditorWindow window = (LevelEditorWindow)EditorWindow.GetWindow(typeof(LevelEditorWindow));
        window.Show();
    }

    void OnGUI()
    {
        float width = position.width - 5;
        float height = 30;
        string[] actionLabels = new string[] { "X", "Create Spot", "Delete Spot" };
        selectedAction = GUILayout.SelectionGrid(selectedAction, actionLabels, 3, GUILayout.Width(width), GUILayout.Height(height));
    }

    void OnEnable()
    {
        SceneView.duringSceneGui -= OnScene;
        SceneView.duringSceneGui += OnScene;
    }


    void OnScene(SceneView sceneview)
    {
        Event e = Event.current;
        if (e.type == EventType.MouseUp)
        {
            Ray r = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, Camera.current.pixelHeight - e.mousePosition.y));
            
            if (selectedAction == 1)
            {
                RaycastHit2D hitInfo2D = Physics2D.GetRayIntersection(r, Mathf.Infinity, 1 << LayerMask.NameToLayer("Background"));
                if (hitInfo2D)
                {
                    GameObject spotPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Openspot.prefab");
                    GameObject spot = (GameObject)PrefabUtility.InstantiatePrefab(spotPrefab);
                    spot.transform.position = hitInfo2D.point;
                    Undo.RegisterCreatedObjectUndo(spot, "Create spot");
                }

            }
            else if (selectedAction == 2)
            {
                RaycastHit2D hitInfo2D = Physics2D.GetRayIntersection(r, Mathf.Infinity, 1 << LayerMask.NameToLayer("Spots"));
                if (hitInfo2D)
                {
                    GameObject selectedOpenSpot = hitInfo2D.collider.gameObject;
                    Undo.DestroyObjectImmediate(selectedOpenSpot);
                }

            }
        }
    }



}
