using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Math : MonoBehaviour {
    public static Vector2 PosRot(Vector2 pos, float ang) {
        return PosRot(pos, Vector2.zero, ang);
    }

    public static Vector2 PosRot(Vector2 pos, Vector2 center, float ang) {
        float rad = ang * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);
        float x = pos.x - center.x;
        float y = pos.y - center.y;
        return new Vector2(x * cos - y * sin + center.x, x * sin + y * cos + center.y);
    }

    public static float VecToAng(Vector2 VecIn) {
        if (VecIn.x > 0)
            return 360 - Mathf.Atan2(VecIn.x, VecIn.y) * Mathf.Rad2Deg;

        return Mathf.Atan2(VecIn.x, VecIn.y) * Mathf.Rad2Deg * -1;
    }

    public static float Decimalsave(float num, int place) {
        if (place < 0) return 0;
        float ten = Mathf.Pow(10, place);
        return (int)(num * ten) / ten;
    }

    public static Vector2 Decimalsave(Vector2 vec, int place) {
        return new Vector2(Decimalsave(vec.x, place), Decimalsave(vec.y, place));
    }

    public static Vector2 MouseToThisPos(Transform tf) {
        Vector3 mouseLocalPos = tf.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10)));

        return (Vector2)mouseLocalPos;
    }

    public static float MoveToWards(float current, float target, float maxDistence) {
        float vec = target - current;
        if (Mathf.Abs(vec) > maxDistence)
            return vec > 0 ? maxDistence : vec < 0 ? -maxDistence : 0;
        return vec;
    }

    public static Vector2 MoveToWards(Vector2 current, Vector2 target, float maxDistence) {
        Vector2 vec = target - current;
        if (vec.magnitude > maxDistence)
            return vec.normalized * maxDistence;
        return vec;
    }

    public static bool turnLR(float baseAngle, float targetAngle) {
        baseAngle = AngTo180(baseAngle);
        targetAngle = AngTo180(targetAngle);
        float value = targetAngle - baseAngle;
        return Mathf.Abs(value) < 180 ? value < 0 : value > 0;
    }

    public static float addAngle(float baseAngle, float addAngle) {
        baseAngle = AngTo180(baseAngle);
        addAngle = AngTo180(addAngle);
        float angle = baseAngle + addAngle;
        if (angle > 180) {
            return -360 + angle;
        }
        if (angle < -180) {
            return 360 + angle;
        }
        return angle;
    }

    public static float missAngle(float angle1, float angle2) {
        angle1 = AngTo180(angle1);
        angle2 = AngTo180(angle2);
        float missAngle = Mathf.Abs(angle1 - angle2);
        return missAngle > 180 ? 360 - missAngle : missAngle;
    }

    public static float AngTo180(float angle) {
        while (angle > 360) {
            angle -= 360;
        }
        while (angle < -360) {
            angle += 360;
        }
        return angle > 180 ? angle - 360 : angle;
    }

    public static Vector2 BezierPoint3(float t, Vector2 p0, Vector2 p1, Vector2 p2){
        t=Mathf.Clamp01(t);
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector2 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;
        return p;
    }
//    public static void ChangAllLayerInChild(GameObject obj, int layer) {
//        obj.layer = layer;
//        int childCount = obj.transform.childCount;
//        if (childCount > 0)
//            for (int i = 0; i < childCount; i++) {
//                GameObject child = obj.transform.GetChild(i).gameObject;
//                child.layer = layer;
//                if (child.transform.childCount > 0)
//                    ChangAllLayerInChild(child, layer);
//            }
//    }
//
//    public static bool IsInSeeView(Transform looker, Transform targetPos, float seeRange, float seeAng, int? obs) {
//        Vector2 cur = looker.position;
//        Vector2 tag = targetPos.position;
//        Vector2 vec = tag - cur;
//        if (missAngle(looker.eulerAngles.z, VecToAng(vec)) > seeAng / 2)
//            return false;
//        if (vec.magnitude > seeRange)
//            return false;
//        if (obs != null && Physics2D.Raycast(cur, vec, vec.magnitude, (int)obs))
//            return false;
//
//        return true;
//    }
//
//    public static float getSimilarAng(float ang, int pice) {
//        float piceAng = 360 / pice;
//        float maxAng = 0;
//        while (missAngle(maxAng, ang) > piceAng) {
//            maxAng = addAngle(maxAng, piceAng);
//        }
//        float minAng = maxAng - piceAng;
//        float maxError = missAngle(maxAng, maxAng);
//        float minError = missAngle(maxAng, minAng);
//        return maxError > minError ? minAng : maxAng;
//    }
//
//    public static void CreateExplode(Transform pos, float ang, float radius, int attachMask, int obsMask, float force) {
//        Vector3 explosionPos = pos.position;
//        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, radius, attachMask);
//        foreach (var hit in colliders) {
//            if (!hit || !IsInSeeView(pos, hit.transform, radius, ang, obsMask)) continue;
//            Vector2 baseVec = hit.transform.position - explosionPos;
//            Vector2 vec = baseVec.normalized * (1 - baseVec.magnitude / radius) * force;
//
//            Rigidbody2D rb = hit.attachedRigidbody;
//            if (rb == null) continue;
//            EnityCtrl entity = rb.GetComponent<EnityCtrl>();
//            if (entity != null)
//                entity.AddDamgae(vec);
//            else
//                rb.AddForce(vec * rb.mass);
//        }
//    }
}
