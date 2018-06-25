using System.Collections.Generic;
using UnityEngine;

public static class Floq{

    //Calcular Cohecion.

    /// <summary>
    /// Retorna el vector de cohecion entre dos objetos.
    /// </summary>
    /// <param name="Origin">Posicion del objeto de origen.</param>
    /// <param name="Destiny">Posicion del objeto de destino.</param>
    /// <returns>Vector3.Coh</returns>
    public static Vector3 getCohesion(Vector3 Origin, Vector3 Destiny)
    {
        return Destiny - Origin;
    }
    /// <summary>
    /// Retorna el vector de cohecion promedio entre un objeto y varios objetivos.
    /// </summary>
    /// <param name="Origin">Posicion del objeto de origen.</param>
    /// <param name="Objetives">Lista de posiciones de origen de los objetivos.</param>
    /// <returns>Vector3.Coh</returns>
    public static Vector3 getCohesion(Vector3 Origin, List<Vector3> Objetives)
    {
        Vector3 coh = new Vector3();
        foreach (var Destiny in Objetives)
            coh += Destiny - Origin;
        return coh /= Objetives.Count;
    }

    //Calcular Separacion.

    /// <summary>
    /// Calcula el vector Separacion entre un objeto y un objeto "amigo".
    /// </summary>
    /// <param name="Origin">Objeto de origen.</param>
    /// <param name="Friend">Objeto "Amigo".</param>
    /// <param name="DesiredSep">Distancia de separacion deseada.</param>
    /// <returns>Vector3.Sep</returns>
    public static Vector3 getSeparation(Vector3 Origin, Vector3 Friend, float DesiredSep)
    {
        Vector3 Sep = Origin - Friend;
        float mag = DesiredSep - Sep.magnitude;
        Sep.Normalize();
        Sep *= mag;
        return Sep;
    }
    /// <summary>
    /// Calcula el vector Separacion promedio entre un objeto y una lista de "amigos".
    /// </summary>
    /// <param name="Origin">Objeto de origen.</param>
    /// <param name="Friends">Lista de "Amigos".</param>
    /// <param name="DesiredSep">Distancia de separacion deseada.</param>
    /// <returns>Vector3.Sep</returns>
    public static Vector3 getSeparation(Vector3 Origin, List<Vector3> Friends,float DesiredSep)
    {
        Vector3 Sep = new Vector3();
        foreach (var item in Friends)
        {
            Vector3 ItemSep = Origin - item;
            float mag = DesiredSep - ItemSep.magnitude;
            ItemSep.Normalize();
            ItemSep *= mag;
            Sep += ItemSep;
        }
        return Sep /= Friends.Count;
    }

    //Calcular Alineacion.

    /// <summary>
    /// Calcula el vector Alineacion entre un objeto y una lista de "Amigos".
    /// </summary>
    /// <param name="Origin">Objeto de origen.</param>
    /// <param name="Friends">Lista de "amigos".</param>
    /// <returns>Vector3.Alig</returns>
    public static Vector3 getAlignment(Vector3 Origin, List<Vector3> Friends)
    {
        Vector3 Alig = new Vector3();
        foreach (var Forwards in Friends)
            Alig += Forwards;
        Alig /= Friends.Count;
        return Alig;
    }
    /// <summary>
    /// Calcula el vector Alineacion entre un objeto y una lista de "Amigos".
    /// </summary>
    /// <param name="Origin">Objeto de origen.</param>
    /// <param name="Friends">Lista de "amigos".</param>
    /// <returns>Vector3.Alig</returns>
    public static Vector3 getAlignment(Vector3 Origin, List<Transform> Friends)
    {
        Vector3 Alignment = new Vector3();
        foreach (var Transform in Friends)
            Alignment += Transform.forward;
        Alignment /= Friends.Count;
        return Alignment;
    }
}
