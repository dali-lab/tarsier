using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SelectAllOfTag : ScriptableWizard {
    string tagName = "ExampleTag";

    [MenuItem("Example/Select All of Tag...")]
    static void SelectAllOfTagWizard()
    {
        ScriptableWizard.DisplayWizard(
            "Mesh",
            typeof(SelectAllOfTag),
            "Make Selection");
    }

    void OnWizardCreate()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag(tagName);
        Selection.objects = gos;
    }
}
