using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Wave))]
public class WavePropertyDrawer : PropertyDrawer
{
    private const int spriteHeight = 50;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(position, property, label, true);
        if (property.isExpanded)
        {
            SerializedProperty enemyPrefabProperty = property.FindPropertyRelative("enemyPrefab");
            GameObject enemyPrefab = (GameObject)enemyPrefabProperty.objectReferenceValue;
            if (enemyPrefab != null)
            {
                SpriteRenderer enemySprite = enemyPrefab.GetComponentInChildren<SpriteRenderer>();
                int previousIndentLevel = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 2;
                Rect indentedRect = EditorGUI.IndentedRect(position);
                float fieldHeight = base.GetPropertyHeight(property, label) + 2;
                Vector3 enemySize = enemySprite.bounds.size;
                Rect texturePosition = new Rect(indentedRect.x, indentedRect.y + fieldHeight * 4, enemySize.x / enemySize.y * spriteHeight, spriteHeight);
                EditorGUI.DropShadowLabel(texturePosition, new GUIContent(enemySprite.sprite.texture));
                EditorGUI.indentLevel = previousIndentLevel;
            }
        }

    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty enemyPrefabProperty = property.FindPropertyRelative("enemyPrefab");
        GameObject enemyPrefab = (GameObject)enemyPrefabProperty.objectReferenceValue;
        if (property.isExpanded && enemyPrefab != null)
        {
            return EditorGUI.GetPropertyHeight(property) + spriteHeight;
        }
        else
        {
            return EditorGUI.GetPropertyHeight(property);
        }
    }


}
