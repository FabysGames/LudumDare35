using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {
    public ShapeController controller;
    public Vector2 direction;

    void OnMouseDown()
    {
        this.controller.SetMoveDirection(this.direction);
    }
}