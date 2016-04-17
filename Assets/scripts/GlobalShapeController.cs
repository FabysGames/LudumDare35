using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalShapeController : MonoBehaviour {

    public List<Rigidbody2D> shapes = new List<Rigidbody2D>();

	
    public void SetKinematic(bool isKinematic = false)
    {
        foreach(Rigidbody2D shape in this.shapes)
        {
            shape.isKinematic = isKinematic;
        }
    }

    public void HideMenus()
    {
        foreach (Rigidbody2D shape in this.shapes)
        {
            ShapeController controller = shape.gameObject.GetComponent<ShapeController>();
            controller.HideArrows();
            controller.HideMenu();
        }
    }

}
