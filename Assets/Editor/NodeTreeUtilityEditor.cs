using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class NodeTreeUtilityEditor : EditorWindow {
    public GameObject Node;
    public GameObject NodeTree;
    float Filas = 0;
    float Columnas = 0;
    float distancia = 1;
    float distOff = 0.5f;
    int NodeLayer = 0;
    
    [MenuItem("Nodes/CreateNodeGrid")]
    public static void OpenWindow()
    {
        var MainWindow = GetWindow(typeof(NodeTreeUtilityEditor));
        MainWindow.Show();
    }

    private void OnGUI()
    {
        Node = (GameObject)EditorGUILayout.ObjectField("Nodo a dibujar: ",Node,typeof(GameObject),false);
        NodeTree = (GameObject)EditorGUILayout.ObjectField("Objeto Padre: ", NodeTree, typeof(GameObject), true);
        Filas = EditorGUILayout.FloatField("Numero de filas", Filas);
        Columnas = EditorGUILayout.FloatField("Numero de columnas", Columnas);
        distancia = EditorGUILayout.FloatField("Distancia entre nodos: ", distancia);
        distOff = EditorGUILayout.FloatField("Offset de distancia: ", distOff);
        NodeLayer = EditorGUILayout.IntField("Node Layer Index: ", NodeLayer);
        if (GUILayout.Button("Crear!"))
        {
            if (Node != null && NodeTree != null)
                if (Filas != 0 && Columnas != 0)
                    CreateNodeGrid(Filas, Columnas, distancia);
            else
                MonoBehaviour.print("No asignaste el nodo o el contenedor salame.");
        }
    }

    private void CreateNodeGrid(float Fila, float Columna, float DistanciaEntreNodos)
    {
        List<RouteNode> Nodes = new List<RouteNode>();
        const float Y = 0;//Altura.
        float X = 0;//Largo.
        float Z = 0;//Ancho.
        for (int i = 0; i < Fila; i++)
        {
            for (int j = 0; j < Columna; j++)
            {
                Vector3 newpos = new Vector3(X,Y,Z);
                GameObject A = Instantiate(Node,newpos,Quaternion.identity,NodeTree.transform);
                var comp = NodeTree.GetComponent<NodeTree>();
                comp.Childs.Add(A.GetComponent<RouteNode>());
                Nodes.Add(A.GetComponent<RouteNode>());
                Z += DistanciaEntreNodos;
            }
            Z = 0;
            X += DistanciaEntreNodos;
        }

        foreach (var Node in Nodes)
        {
            Collider[] finded = Physics.OverlapSphere(Node.transform.position, DistanciaEntreNodos + 1);
            foreach (var item in finded)
            {
                if (item.gameObject.GetComponent<RouteNode>() != Node && !Node.Conections.Contains(item.gameObject.GetComponent<RouteNode>()))
                    Node.Conections.Add(item.gameObject.GetComponent<RouteNode>());
            }
        }
    }
}
