using System.Collections.Generic;
using UnityEngine;

public class RouteNode : MonoBehaviour {
    public List<RouteNode> Conections = new List<RouteNode>();
    public float weight = 0;

    public bool viewAllConections = false;

    private void OnDrawGizmos()
    {
        if (!viewAllConections && UnityEditor.Selection.activeGameObject == gameObject)
        {
            foreach (var Conection in Conections)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, Conection.transform.position);
            }
        }
        if (viewAllConections)
        {
            foreach (var Conection in Conections)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, Conection.transform.position);
            }
        }
    }
}
