using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;

#endif

[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class ButtonAttribute : PropertyAttribute
{

}

[CustomPropertyDrawer(typeof(ButtonAttribute))]
public class ButtonAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        base.OnGUI(position, property, label);
    }
}
[CustomEditor(typeof(MonoBehaviour), true)]
public class ButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var methods = target.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        foreach (var method in methods)
        {
            var bMethod = method.GetCustomAttribute(typeof(ButtonAttribute), true);
            if (bMethod != null)
            {
                if (GUILayout.Button(method.Name))
                {
                    method.Invoke(target, null);
                }
            }
        }

        DrawDefaultInspector();
    }
}
[CustomEditor(typeof(MonoBehaviour), true)]
public class CuonglhButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var mTarget = (MonoBehaviour)target;
        var methodInfors = mTarget.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (var methodInfor in methodInfors)
        {
            var buttonEditor = methodInfor.GetCustomAttribute(typeof(ButtonEditor), true);
            if (buttonEditor != null)
            {
                if (GUILayout.Button(methodInfor.Name))
                {
                    methodInfor.Invoke((object)mTarget, null);
                }
            }
        }
    }
}
public class Btn_gui : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
