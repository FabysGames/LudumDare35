using UnityEngine;
using System.Collections;

public class MenuShape : MonoBehaviour {
    public int type;
    public ShapeController controller;

    public void OnMouseDown()
    {
        this.controller.SetShapeType(this.type);
    }
}
