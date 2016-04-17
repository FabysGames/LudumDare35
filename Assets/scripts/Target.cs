using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {
    public int type;


	void OnTriggerEnter2D(Collider2D other)
    {
        ShapeController controller = other.gameObject.GetComponent<ShapeController>();

        if(controller.type == this.type)
        {
            controller.OnCollisionEnter2D();
            controller.globalController.shapes.Remove(controller.rigidbody2D);

            controller.gameController.AddDoneShape(this.type);

            GameObject.Destroy(other.gameObject);
        }
    }
}
