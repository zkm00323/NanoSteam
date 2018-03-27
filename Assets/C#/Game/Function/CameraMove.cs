using System.Collections;
using System.Collections.Generic;
using UnityEngine;
       
public class CameraMove : MonoBehaviour {
    public float SmoothTime = 5f;
    void Update() {
        Vector2 pos = Vector2.Lerp(transform.position, GameCtrl.Player.transform.position, SmoothTime * Time.deltaTime);
        transform.position = new Vector3(pos.x,pos.y,-10);
    }
}
