using UnityEditor;

[CustomEditor(typeof(MoveEnemy))]
public class EnemyDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MoveEnemy moveEnemy = (MoveEnemy)target;
        HealthBar healthBar = moveEnemy.gameObject.GetComponentInChildren<HealthBar>();
        healthBar.maxHealth = EditorGUILayout.FloatField("Max Health", healthBar.maxHealth);
        healthBar.currentHealth = EditorGUILayout.Slider("Current Health", healthBar.currentHealth, 0, healthBar.maxHealth);
        DrawDefaultInspector();
    }
}

