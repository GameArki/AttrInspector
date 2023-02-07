using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GameArki.AttrInspector.Editors {

    public class ATMainEditor : Editor {

        // Fields

        // Properties

        // Methods

        // Slider
        List<ATSliderAttribute> sliders;

        void OnEnable() {
            
            sliders = new List<ATSliderAttribute>();

            // Init And Cache:
            // - Fields
            // - Properties
            // - Methods
            FieldInfo[] fields = target.GetType().GetFields();
            for (int i = 0; i < fields.Length; i += 1) {
                var field = fields[i];
                var attrs = field.GetCustomAttributes(true);
                foreach (var attr in attrs) {
                    if (attr is ATSliderAttribute) {
                        var tar = attr as ATSliderAttribute;
                        tar.belongField = field;
                        sliders.Add(tar);
                    }
                }
            }

        }

        void OnDisable() {
            // Clean Up:
            // - Fields
            // - Properties
            // - Methods

            sliders?.Clear();
        }

        public override void OnInspectorGUI() {
            const BindingFlags FLAGS = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            foreach (var slider in sliders) {
                object sliderValueObj = slider.belongField.GetValue(target);
                if (sliderValueObj != null) {
                    float sliderValue = (float)sliderValueObj;
                    float min = (float)slider.belongField.DeclaringType.GetField(slider.MinName, FLAGS).GetValue(target);
                    float max = (float)slider.belongField.DeclaringType.GetField(slider.MaxName, FLAGS).GetValue(target);
                    float newValue = EditorGUILayout.Slider(slider.belongField.Name, sliderValue, min, max);
                    if (newValue != sliderValue) {
                        slider.belongField.SetValue(target, newValue);
                    }
                }
            }
        }

    }

    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoBehaviour), editorForChildClasses: true, isFallback = true)]
    public class ATMonoBehaviourEditor : ATMainEditor { }

    [CanEditMultipleObjects]
    [CustomEditor(typeof(ScriptableObject), editorForChildClasses: true, isFallback = true)]
    public class ATScriptableObjectEditor : ATMainEditor { }

}