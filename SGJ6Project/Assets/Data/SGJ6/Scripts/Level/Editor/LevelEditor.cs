using UnityEditor;
using UnityEngine;

using SGJVI.Enemies;

namespace SGJVI.Level {
    [CustomEditor(typeof(Level), true)]
	public class LevelEditor : Editor  {

        SerializedProperty enemies;
        SerializedProperty triggersLevel;

        private void OnEnable()
        {
            enemies = serializedObject.FindProperty("enemies");
            triggersLevel = serializedObject.FindProperty("triggersLevel");
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

                TriggerLevel[] triggerLevelComponent = ((Level)target).GetComponentsInChildren<TriggerLevel>(true);
                triggersLevel.arraySize = triggerLevelComponent.Length;
                for (int i = 0; i < triggerLevelComponent.Length; ++i)
                {
                    triggersLevel.GetArrayElementAtIndex(i).objectReferenceValue = triggerLevelComponent[i];
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
	}
}