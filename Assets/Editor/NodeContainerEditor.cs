using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NodeTree))]
public class NodeContainerEditor : Editor {

    private NodeTree _nodeTree;
    private void OnEnable()
    {
        _nodeTree = (NodeTree)target;
    }
    public override void OnInspectorGUI()
    {
        EditorGUI.BeginDisabledGroup(_nodeTree.Childs.Count > 0 ? false: true);
        if (GUILayout.Button("Show Links ON/Off"))
        {
            if (_nodeTree.ShowAllLinks == true)
                _nodeTree.ShowAllLinks = false;
            else
                _nodeTree.ShowAllLinks = true;
            _nodeTree.ShowLinks();
        }
        EditorGUI.EndDisabledGroup();
    }
}
