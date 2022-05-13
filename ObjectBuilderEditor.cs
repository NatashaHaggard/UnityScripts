using UnityEngine;
using UnityEditor;

public class ObjectBuilderEditor : Editor
{
    [CustomEditor(typeof(ObjectBuilderScript))]

    public class ObjectBuilder : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            ObjectBuilderScript ObjectBuilderScript = (ObjectBuilderScript)target;
            if (GUILayout.Button("Spawn"))
            {
                ObjectBuilderScript.BuildObjects();
            }
        }
    }

}
