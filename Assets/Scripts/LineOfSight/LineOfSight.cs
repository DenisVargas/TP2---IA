using System.Collections.Generic;
using UnityEngine;

public static class LineOfSight {
    //public GameObject target; Este es el objeto.
    //public float viewAngle; Esto lo calculamos.
    //public float viewDistance; Esto lo pedimos como parametro.
    //public bool _targetInSight; Exportamos esto.
    public static int ObstaclesLayer = 1;
    public static int ObjectivesLayer = 2;

    //Linea de vision Simple.

    /// <summary>
    /// Dados dos objetos, calcula si el objetivo esta dentro de la linea de vision del primero.
    /// </summary>
    /// <param name="Origin">Transform del objeto de origen.</param>
    /// <param name="Target">posición del objeto de destino.</param>
    /// <param name="SightDistance">Distancia maxima de nuestra linea de vision.</param>
    /// <returns>Verdadero si el objeto es visible.</returns>
    public static bool TargetInSight(Transform Origin, Vector3 Target, float SightDistance)
    {
        bool _targetInSight = false;
        Vector3 _dirToTarget = (Target - Origin.position).normalized;
        float _angleToTarget = Vector3.Angle(Origin.forward, _dirToTarget);
        float _distanceToTarget = Vector3.Distance(Origin.position, Target);

        //Si entra en el angulo y en el rango de vision 
        if (_angleToTarget <= SightDistance && _distanceToTarget <= SightDistance)
        {
            RaycastHit rch;
            bool obstaclesBetween = false;

            //Se hace un chequeo de colisiones
            if (Physics.Raycast(Origin.position, _dirToTarget, out rch, _distanceToTarget))
                if (rch.collider.gameObject.layer == ObstaclesLayer)
                    obstaclesBetween = true;

            if (!obstaclesBetween)
                _targetInSight = true;
            else
                _targetInSight = false;
        }
        else
            _targetInSight = false;

        return _targetInSight;
    }
    /// <summary>
    /// Dados dos objetos, calcula si el objetivo esta dentro de la linea de vision del primero.
    /// </summary>
    /// <param name="Origin">Vector posicion del objeto de origen.</param>
    /// <param name="OriginDirection">Vector direccion del objeto de origen.</param>
    /// <param name="Target">Vector porsicion del objetivo.</param>
    /// <param name="SightDistance">Distancia maxima de nuestra linea de vision.</param>
    /// <returns>Verdadero si el objeto es visible.</returns>
    public static bool TargetInSight(Vector3 Origin,Vector3 OriginDirection, Vector3 Target, float SightDistance)
    {
        bool _targetInSight = false;
        Vector3 _dirToTarget = (Target - Origin).normalized;
        float _angleToTarget = Vector3.Angle(OriginDirection, _dirToTarget);
        float _distanceToTarget = Vector3.Distance(Origin,Target);

        //Si entra en el angulo y en el rango de vision 
        if (_angleToTarget <= SightDistance && _distanceToTarget <= SightDistance)
        {
            RaycastHit rch;
            bool obstaclesBetween = false;

            //Se hace un chequeo de colisiones
            if (Physics.Raycast(Origin, _dirToTarget, out rch, _distanceToTarget))
                if (rch.collider.gameObject.layer == ObstaclesLayer)
                    obstaclesBetween = true;

            if (!obstaclesBetween)
                _targetInSight = true;
            else
                _targetInSight = false;
        }
        else
            _targetInSight = false;

        return _targetInSight;
    }
    /// <summary>
    /// Dados: Un objeto, la direccion, la distancia y el angulo hacia un objetivo, calcula si el objetivo esta visible.
    /// </summary>
    /// <param name="Origin">Vector posicion del objeto de origen.</param>
    /// <param name="DirToTarget">Vector direccion al objetivo.</param>
    /// <param name="angleToTarget">Angulo al objetivo.</param>
    /// <param name="DistanceToTarget">Distancia al objetivo.</param>
    /// <param name="SightDistance">Distancia maxima de nuestra linea de vision.</param>
    /// <returns>Verdadero si el objeto es visible.</returns>
    public static bool TargetInSight(Vector3 Origin, Vector3 DirToTarget,float angleToTarget,float DistanceToTarget, float SightDistance)
    {
        bool _targetInSight = false;
        if (angleToTarget <= SightDistance && DistanceToTarget <= SightDistance)
        {
            RaycastHit Hit;
            bool obstaclesBetween = false;

            //Se hace un chequeo de colisiones
            if (Physics.Raycast(Origin, DirToTarget, out Hit, DistanceToTarget))
                if (Hit.collider.gameObject.layer == ObstaclesLayer)
                    obstaclesBetween = true;

            if (!obstaclesBetween)
                _targetInSight = true;
            else
                _targetInSight = false;
        }
        else
            _targetInSight = false;

        return _targetInSight;
    }

    //Linea de vision Multiple.

    /// <summary>
    /// Dados la posicion de un objeto, una lista de objetivos y una distancia maxima, devuelve todos los objetos que son visibles.
    /// </summary>
    /// <param name="Origin">Posicion del objeto de origen.</param>
    /// <param name="Objetivos">Lista de "Colliders" de los objetivos.</param>
    /// <param name="SightDistance">Distancia maxima de nuestra linea de vision.</param>
    /// <returns>Lista de Objetos Visibles.</returns>
    public static List<Transform> TargetsInSight(Vector3 Origin, Collider[] Objetivos, float SightDistance)
    {
        List<Transform> NonVisibleTargets = new List<Transform>();//Objetos no visibles.
        List<Transform> VisibleTargets = new List<Transform>();//Objetos visibles.

        for (int i = 0; i < Objetivos.Length; i++)
        {
            Transform CurrentTarget = Objetivos[i].transform;//El objetivo actual.
            Vector3 TargetPos = CurrentTarget.position;//Posicion del objetivo.
            Vector3 _dirToTarget = (TargetPos - Origin).normalized;//Dirección
            float _distanceToTarget = Vector3.Distance(Origin, TargetPos) + 10;//Distancia.
            float _angleToTarget = Vector3.Angle(Origin, _dirToTarget);//angulo.

            if (_angleToTarget <= SightDistance && _distanceToTarget <= SightDistance)
            {
                RaycastHit Hit;
                if (Physics.Raycast(Origin, _dirToTarget, out Hit, _distanceToTarget))
                {
                    int HitLayer = Hit.collider.gameObject.layer;
                    if (HitLayer == ObstaclesLayer)//Si choque contra un obstaculo.
                        if (!NonVisibleTargets.Contains(CurrentTarget))
                            NonVisibleTargets.Add(CurrentTarget);

                    if (HitLayer == ObjectivesLayer)//Si choque contra un Objetivo.
                        if (!VisibleTargets.Contains(CurrentTarget))
                            VisibleTargets.Add(CurrentTarget);
                }
            }
        }

        return VisibleTargets;//Retorno la lista de objetos visibles.
    }
    /// <summary>
    /// Dados la posicion de un objeto, una lista de objetivos y una distancia maxima, devuelve todos los objetos que son visibles.
    /// </summary>
    /// <param name="Origin">Posicion del objeto de origen.</param>
    /// <param name="Objetivos">Lista de "Colliders" de los objetivos.</param>
    /// <param name="DistanceOffset">Distancia extra que se sumará a la Distancia.</param>
    /// <param name="SightDistance">Distancia maxima de nuestra linea de vision.</param>
    /// <returns>Lista de Objetos Visibles.</returns>
    public static List<Transform> TargetsInSight(Vector3 Origin, Collider[] Objetivos,float DistanceOffset, float SightDistance)
    {
        List<Transform> NonVisibleTargets = new List<Transform>();//Objetos no visibles.
        List<Transform> VisibleTargets = new List<Transform>();//Objetos visibles.

        for (int i = 0; i < Objetivos.Length; i++)
        {
            Transform CurrentTarget = Objetivos[i].transform;//El objetivo actual.
            Vector3 TargetPos = CurrentTarget.position;//Posicion del objetivo.
            Vector3 _dirToTarget = (TargetPos - Origin).normalized;//Dirección
            float _distanceToTarget = Vector3.Distance(Origin, TargetPos) + DistanceOffset;//Distancia.
            float _angleToTarget = Vector3.Angle(Origin, _dirToTarget);//angulo.

            if (_angleToTarget <= SightDistance && _distanceToTarget <= SightDistance)
            {
                RaycastHit Hit;
                if (Physics.Raycast(Origin, _dirToTarget, out Hit, _distanceToTarget))
                {
                    int HitLayer = Hit.collider.gameObject.layer;
                    if (HitLayer == ObstaclesLayer)//Si choque contra un obstaculo.
                        if (!NonVisibleTargets.Contains(CurrentTarget))
                            NonVisibleTargets.Add(CurrentTarget);

                    if (HitLayer == ObjectivesLayer)//Si choque contra un Objetivo.
                        if (!VisibleTargets.Contains(CurrentTarget))
                            VisibleTargets.Add(CurrentTarget);
                }
            }
        }

        return VisibleTargets;//Retorno la lista de objetos visibles.
    }
}
