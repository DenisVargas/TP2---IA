using System.Collections.Generic;
using UnityEngine;

public class NodeTree : MonoBehaviour {
    public bool ShowAllLinks = false;
    public List<RouteNode> Childs = new List<RouteNode>();

    public void ShowLinks()
    {
        foreach (var item in Childs)
            item.viewAllConections = ShowAllLinks;
    }
}
