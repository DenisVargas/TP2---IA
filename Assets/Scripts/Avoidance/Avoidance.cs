using System.Collections.Generic;
using UnityEngine;

public static class Avoidance{
    /// <summary>
    /// Retorna un vector unitario de avoidance entre los objetivos.
    /// </summary>
    /// <param name="Origin">Vector posicion de origen del objeto A.</param>
    /// <param name="Destiny">Vector posicion de origen del objeto B.</param>
    /// <returns>Vector3.Avoidance</returns>
    public static Vector3 getAvoidance(Vector3 Origin, Vector3 Destiny)
    {
        return (Origin - Destiny).normalized;
    }
    /// <summary>
    /// Retorna un vector de avoidance entre los objetivos.
    /// </summary>
    /// <param name="Origin">Vector posicion de origen del objeto A.</param>
    /// <param name="Destiny">Vector posicion de origen del objeto B.</param>
    /// <param name="Magnitude">Magnitud del vector resultante.</param>
    /// <returns>Vector3.Avoidance</returns>
    public static Vector3 getAvoidance(Vector3 Origin, Vector3 Destiny, float Magnitude)
    {
        var avoid = (Origin - Destiny).normalized;
        avoid *= Magnitude;
        return avoid;
    }
    /// <summary>
    /// Retorna un vector de avoidance entre los objetivos.
    /// </summary>
    /// <param name="Origin">Vector posicion de origen del objeto A.</param>
    /// <param name="Objectives">Lista de vectores posicion de los objetos a evitar.</param>
    /// <returns>Vector3.Avoidance</returns>
    public static Vector3 getAvoidance(Vector3 Origin, List<Vector3> Objectives)
    {
        Vector3 avoid = Vector3.zero;
        foreach (var Destiny in Objectives)
        {
            if (avoid == Vector3.zero)
                avoid = (Origin - Destiny);
            else
                avoid += (Origin - Destiny);
        }
        avoid /= Objectives.Count; //Sacamos el promedio.
        avoid.Normalize();//Normalizamos.
        return avoid;
    }
    /// <summary>
    /// Retorna un vector de avoidance entre los objetivos.
    /// </summary>
    /// <param name="Origin">Vector posicion de origen del objeto A.</param>
    /// <param name="Objectives">Lista de vectores posicion de los objetos a evitar.</param>
    /// <param name="Magnitude">Magnitud del vector resultante.</param>
    /// <returns>Vector3.Avoidance</returns>
    public static Vector3 getAvoidance(Vector3 Origin, List<Vector3> Objectives, float Magnitude)
    {
        Vector3 avoid = Vector3.zero;
        foreach (var Destiny in Objectives)
        {
            if (avoid == Vector3.zero)
                avoid = (Origin - Destiny);
            else
                avoid += (Origin - Destiny);
        }
        avoid /= Objectives.Count; //Sacamos el promedio.
        avoid.Normalize();//Normalizamos.
        avoid *= Magnitude;//Le damos una magnitud.
        return avoid;
    }
}
