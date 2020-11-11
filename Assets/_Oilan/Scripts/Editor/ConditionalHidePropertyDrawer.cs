using UnityEditor;
using UnityEngine;
using System;
namespace Temirlan
{
    [CustomPropertyDrawer(typeof(ConditionallyHideAttribute))]
    public class ConditionalHidePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ConditionallyHideAttribute condHAtt = (ConditionallyHideAttribute)attribute;

            if (condHAtt._type == typeof(GameType)) {
                GameType _type = GetConditionalHideAttributeResult<GameType>(condHAtt, property);
                bool wasEnabled = GUI.enabled;
                GUI.enabled = true;
                if (condHAtt.gameType == _type) {
                    EditorGUI.PropertyField(position, property, label, true);
                }
                GUI.enabled = wasEnabled;
            }else if(condHAtt._type == typeof(Type)) {
                Type _type = GetConditionalHideAttributeResult<Type>(condHAtt, property);
                bool wasEnabled = GUI.enabled;
                GUI.enabled = true;
                if (condHAtt.type == _type) {
                    EditorGUI.PropertyField(position, property, label, true);
                }
                GUI.enabled = wasEnabled;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ConditionallyHideAttribute condHAtt = (ConditionallyHideAttribute)attribute;
            if(condHAtt._type == typeof(GameType)) {

                GameType _type = GetConditionalHideAttributeResult<GameType>(condHAtt, property);

                if (condHAtt.gameType == _type) {
                    return EditorGUI.GetPropertyHeight(property, label);
                } else
                    return -EditorGUIUtility.standardVerticalSpacing;
            } else if (condHAtt._type == typeof(Type)) {
                Type _type = GetConditionalHideAttributeResult<Type>(condHAtt, property);

                if (condHAtt.type == _type) {
                    return EditorGUI.GetPropertyHeight(property, label);
                } else
                    return -EditorGUIUtility.standardVerticalSpacing;
            }
            return -EditorGUIUtility.standardVerticalSpacing;
        }


        private T GetConditionalHideAttributeResult<T>(ConditionallyHideAttribute condHAtt, SerializedProperty property) where T : IConvertible
        {
            string propertyPath = property.propertyPath;
            string conditionPath = propertyPath.Replace(property.name, condHAtt.ConditionalSourceField);
            SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);
            if(sourcePropertyValue == null)
                Debug.LogWarning("Attempting to use a ConditionalHideAttribute but no matching SourcePropertyValue found in object: " + condHAtt.ConditionalSourceField);
            return (T)Convert.ChangeType((T)(object)(sourcePropertyValue.enumValueIndex), typeof(T));
        }
    }
}
