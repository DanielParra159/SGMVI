using UnityEditor;
using UnityEngine;

using SGJVI.Enemies;

namespace SGJVI.Level {
    [CustomEditor(typeof(Level), true)]
	public class LevelEditor : Editor  {

        SerializedProperty enemies;

        private void OnEnable()
        {
            enemies = serializedObject.FindProperty("enemies");
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Build Object"))
            {
                Enemy[] enemiesComponent = ((Level)target).GetComponentsInChildren<Enemy>(true);
                enemies.arraySize = enemiesComponent.Length;
                for (int i = 0; i < enemiesComponent.Length; ++i)
                {
                    enemies.GetArrayElementAtIndex(i).objectReferenceValue = enemiesComponent[i];
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
	}
}