using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class Vector3Helper {

    public static Vector3 CenterOfVectors(Vector3[] vectors) {
        Vector3 sum = Vector3.zero;
        if (vectors == null || vectors.Length == 0) {
            return sum;
        }

        foreach (Vector3 vec in vectors) {
            sum += vec;
        }
        return sum / vectors.Length;
    }

    public static Vector3 getRelativePosition(Transform origin, Vector3 position) {
        Vector3 distance = position - origin.position;
        Vector3 relativePosition = Vector3.zero;
        relativePosition.x = Vector3.Dot(distance, origin.right.normalized);
        relativePosition.y = Vector3.Dot(distance, origin.up.normalized);
        relativePosition.z = Vector3.Dot(distance, origin.forward.normalized);

        return relativePosition;
    }
}


