using System.Collections.Generic;
using UnityEngine;

public class Unit_TestFloq : MonoBehaviour {
    public bool isLeader = false;
    public GameObject Leader;
    public GameObject Objective;
    public GameObject Obstacle;
    public List<GameObject> Objectives;
    public List<GameObject> Allies = new List<GameObject>();
    [Space]
    public float RotationSpeed;
    public float Velocity;
    public float AvoidanceRadius;
    public float RadioDeFloq = 2;
    [Header("Floq Variations")]
    public float separationWeight;
    public float cohetionWeight;

    private Vector3 DirToGo = Vector3.zero;
    private Vector3 LeaderDir = Vector3.zero;
    private Vector3 Avoid = Vector3.zero;
    private Vector3 Al = Vector3.zero;
    private Vector3 Co = Vector3.zero;
    private Vector3 Se = Vector3.zero;
    private Vector3 Le = Vector3.zero;

    private void Start()
    {
        if (isLeader)
            Leader = gameObject;
    }

    // Update is called once per frame
    void Update () {

        if (isLeader)
        {
            //Me muevo independientemente. Con avoidance.
            LeaderDir = (Objective.transform.position - transform.position).normalized;

            if (Vector3.Distance(transform.position,Obstacle.transform.position) < AvoidanceRadius)
            {
                Avoid = Avoidance.getAvoidance(transform.position, Obstacle.transform.position);
                LeaderDir += Avoid;
            }

            LeaderDir.y = 0;

            transform.forward = Vector3.Slerp(transform.forward, LeaderDir, RotationSpeed * Time.deltaTime);

            transform.position += transform.forward * Velocity * Time.deltaTime;
        }
        else
        {
            List<Vector3> AllyPos = new List<Vector3>();
            foreach (var Ally in Allies)
                AllyPos.Add(Ally.transform.position);

            if (Vector3.Distance(transform.position, Obstacle.transform.position) < AvoidanceRadius)
            {
                Avoid = Avoidance.getAvoidance(transform.position, Obstacle.transform.position);
                DirToGo += Avoid;
            }

            Le = (Leader.transform.position - transform.position).normalized;
//            Vector3 avoid = Avoidance.getAvoidance(transform.position,);
            Al = Floq.getAlignment(transform.position, AllyPos);
            Co = Floq.getCohesion(transform.position, AllyPos) * cohetionWeight;
            Se = Floq.getSeparation(transform.position, AllyPos, RadioDeFloq) * separationWeight;

            DirToGo = Avoid + Le + Al + Co + Se;

            DirToGo.y = 0;

            transform.forward = Vector3.Slerp(transform.forward, DirToGo, RotationSpeed * Time.deltaTime);

            transform.position += transform.forward * Velocity * Time.deltaTime;
        }


	}
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (UnityEditor.Selection.activeGameObject == gameObject)
        {
            Gizmos.DrawWireSphere(transform.position, RadioDeFloq);
            Gizmos.DrawLine(transform.position,transform.position + Le);
            Gizmos.DrawLine(transform.position, transform.position + LeaderDir);

            Gizmos.color = Color.cyan;//Avoid.
            Gizmos.DrawLine(transform.position, transform.position + Avoid);

            Gizmos.color = Color.green;//Alineacion.
            Gizmos.DrawLine(transform.position,transform.position + Al);
            Gizmos.color = Color.yellow;//Cohecion.
            Gizmos.DrawLine(transform.position, transform.position + Co);
            Gizmos.color = Color.red;//Separacion
            Gizmos.DrawLine(transform.position, transform.position + Se);
        }
    }
}
