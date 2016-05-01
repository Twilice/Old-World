using UnityEngine;
using System.Linq;
using System.Collections;

public static class Helper
{
    /// <summary>
    /// <para>This will get the distance between two transitions using 3d space</para>
    /// </summary>
    /// <param name="source">The transform of the main object</param>
    /// <param name="target">The transform of the target object</param>
    /// <returns>A float indicating the range</returns>
    public static float GetObjectDist(this Transform source, Transform target)
    {
        return Vector3.Distance(source.position, target.position);
    }

    /// <summary>
    /// <para>This will get the distance between two transitions using only the horizontal plane</para>
    /// </summary>
    /// <param name="source">The transform of the main object</param>
    /// <param name="target">The transform of the target object</param>
    /// <returns>A float indicating the range</returns>
    public static float GetObjectDistH(this Transform source, Transform target)
    {
        return Vector2.Distance(new Vector2(source.position.x, source.position.z), new Vector2(target.position.x, target.position.z));
    }

    /// <summary>
    /// <para>Atempts to recursively look for a child object with a specific name</para>
    /// </summary>
    /// <param name="gameObject">The Parent GameObject</param>
    /// <param name="name">Name of the child that is to be found</param>
    /// <returns>Returns the GameObject with the specified name (including child objects)</returns>
    public static GameObject FindChildObject(this GameObject gameObject, string name)
    {
        if (gameObject != null)
        {
            Transform[] children = gameObject.GetComponentsInChildren<Transform>(true);
            for (int i = 0; i < children.Length; i++)
            {
                if (children[i].name == name)
                    return children[i].gameObject;
            }
        }
        else
            Debug.LogWarning("Tried to find child in NULL parent");
        return null;
    }
}
