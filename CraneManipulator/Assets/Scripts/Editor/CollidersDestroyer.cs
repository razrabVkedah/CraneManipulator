using UnityEditor;
using UnityEngine;

public class CollidersDestroyer : MonoBehaviour
{
    [MenuItem("Tools/Delete WheelColliders")]
    private static void DeleteWheelColliders()
    {
        var allObjects = Selection.GetFiltered(typeof(WheelCollider), SelectionMode.Unfiltered);
        foreach (var o in allObjects)
        {
            DestroyImmediate(o);
        }
        Debug.Log("Destroyed " + allObjects.Length + " wheel colliders");
    }
        
    [MenuItem("Tools/Delete BoxColliders")]
    private static void DeleteBoxColliders()
    {
        var allObjects = Selection.GetFiltered(typeof(BoxCollider), SelectionMode.Unfiltered);
        foreach (var o in allObjects)
        {
            DestroyImmediate(o);
        }
        Debug.Log("Destroyed " + allObjects.Length + " box colliders");
    }
        
    [MenuItem("Tools/Delete MeshColliders")]
    private static void DeleteMeshColliders()
    {
        var allObjects = Selection.GetFiltered(typeof(MeshCollider), SelectionMode.Unfiltered);
        foreach (var o in allObjects)
        {
            DestroyImmediate(o);
        }
        Debug.Log("Destroyed " + allObjects.Length + " mesh colliders");
    }
        
    [MenuItem("Tools/Delete ALL Colliders")]
    private static void DeleteAllColliders()
    {
        var allObjects = Selection.GetFiltered(typeof(Collider), SelectionMode.Unfiltered);
        foreach (var o in allObjects)
        {
            DestroyImmediate(o);
        }
        Debug.Log("Destroyed " + allObjects.Length + " colliders");
    }
}

