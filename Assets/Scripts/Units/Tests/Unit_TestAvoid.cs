using UnityEngine;

[ExecuteInEditMode]
public class Unit_TestAvoid : MonoBehaviour {
    public GameObject Obstacle;

    Vector3 avoidance = Vector3.zero;
	
	// Update is called once per frame
	void Update () {

        avoidance = Avoidance.getAvoidance(transform.position,Obstacle.transform.position);
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (avoidance != Vector3.zero)
            Gizmos.DrawLine(transform.position,Obstacle.transform.position);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,transform.position + avoidance);
    }
}
