using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;

public class ShapeController : MonoBehaviour {

    public Sprite typeUnknown;
    public Sprite type1;
    public Sprite type2;


    public GameObject menuUnknown;
    public GameObject menu1;
    public GameObject menu2;
    public GameObject arrow;

    public Vector2 moveDirection = Vector2.zero;

    public int type = 0;
    private SpriteRenderer spriteRenderer;
    public Rigidbody2D rigidbody2D;
    public GlobalShapeController globalController;

    private bool menuVisible = false;
    private bool arrowsVisible = false;
    private List<GameObject> menuObjects = new List<GameObject>();
    private List<GameObject> arrowObjects = new List<GameObject>();

    public GameController gameController;
    private AudioSource audio; 

    void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.rigidbody2D = GetComponent<Rigidbody2D>();
        this.globalController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GlobalShapeController>();
        this.globalController.shapes.Add(this.rigidbody2D);
        this.gameController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameController>();
        this.audio = GetComponent<AudioSource>();
        
    }

    void FixedUpdate()
    {
        if(this.moveDirection == Vector2.zero)
        {
            return;
        }

        Vector3 dir = new Vector3(this.moveDirection.x, this.moveDirection.y);
        this.rigidbody2D.MovePosition(this.transform.position + dir * 3.5f * Time.deltaTime);

    }

    public void OnCollisionEnter2D()
    {
        Vector3 dir = new Vector3(this.moveDirection.x, this.moveDirection.y);

        this.moveDirection = Vector2.zero;
        this.rigidbody2D.velocity = Vector2.zero;
        this.rigidbody2D.isKinematic = true;

        this.globalController.SetKinematic(true);

        this.transform.position = new Vector3((float)Math.Floor(transform.position.x) + 0.5f, (float)Math.Floor(transform.position.y) + 0.5f);
    }

    
    void OnMouseOver()
    {
        if(!this.rigidbody2D.isKinematic)
        {
            return;
        }

        // menu on right click
        if (Input.GetMouseButtonDown(1))
        {
            this.audio.Play();
            this.ToggleMenu();
        }

        // arrows on left
        if (Input.GetMouseButtonDown(0))
        {
            this.audio.Play();
            this.ToggleArrows();
        }
    }

    public void SetShapeType(int type)
    {
        this.type = type;
        this.gameController.AddShapeshift();

        switch (type)
        {
            case 0:
                this.spriteRenderer.sprite = this.typeUnknown;
                break;
            case 1:
                this.spriteRenderer.sprite = this.type1;
                break;
            case 2:
                this.spriteRenderer.sprite = this.type2;
                break;
        }

        this.audio.Play();

        this.HideMenu();
    }

    public void SetMoveDirection(Vector2 direction)
    {
        this.gameController.AddMove();

        this.globalController.SetKinematic();
        this.globalController.HideMenus();
        //this.rigidbody2D.isKinematic = false;
        this.moveDirection = direction;
        // this.rigidbody2D.mass = 1;

        this.audio.Play();

        this.HideArrows();
        this.HideMenu();
    }

    private void ToggleArrows()
    {
        if (this.arrowsVisible)
        {
            this.HideArrows();
            return;
        }

        if (this.menuVisible)
        {
            this.HideMenu();
        }

  

        this.globalController.HideMenus();

        this.arrowsVisible = true;

        //right
        GameObject obj = GameObject.Instantiate(this.arrow, new Vector3(this.transform.position.x + 0.45f, this.transform.position.y, -1), Quaternion.identity) as GameObject;
        Arrow arrow = obj.GetComponent<Arrow>();
        arrow.direction = Vector2.right;
        arrow.controller = this;

     //   obj.transform.parent = this.transform;
        this.arrowObjects.Add(obj);

        // bottom
        obj = GameObject.Instantiate(this.arrow, new Vector3(this.transform.position.x, this.transform.position.y - 0.45f, -1), Quaternion.Euler(0, 0, -90)) as GameObject;
        arrow = obj.GetComponent<Arrow>();
        arrow.direction = Vector2.down;
        arrow.controller = this;

      //  obj.transform.parent = this.transform;
        this.arrowObjects.Add(obj);

        // left
        obj = GameObject.Instantiate(this.arrow, new Vector3(this.transform.position.x - 0.45f, this.transform.position.y, -1), Quaternion.Euler(0, 0, 180)) as GameObject;
        arrow = obj.GetComponent<Arrow>();
        arrow.direction = Vector2.left;
        arrow.controller = this;

     //   obj.transform.parent = this.transform;
        this.arrowObjects.Add(obj);


        // top
        obj = GameObject.Instantiate(this.arrow, new Vector3(this.transform.position.x, this.transform.position.y + 0.44f, -1), Quaternion.Euler(0, 0, 90)) as GameObject;
        arrow = obj.GetComponent<Arrow>();
        arrow.direction = Vector2.up;
        arrow.controller = this;

    //    obj.transform.parent = this.transform;
        this.arrowObjects.Add(obj);
    }


    public void HideMenu()
    {
        foreach (GameObject menuObject in this.menuObjects)
        {
            GameObject.Destroy(menuObject);
           // menuObject.SetActive(false);
        }

        this.menuVisible = false;
    }


    public void HideArrows()
    {
        foreach (GameObject arrow in this.arrowObjects)
        {
            GameObject.Destroy(arrow);
           // arrow.SetActive(false);
        }

        this.arrowsVisible = false;
    }



    private void ToggleMenu()
    {
        if (this.menuVisible)
        {
            this.HideMenu();
            return;
        }

        if(this.arrowsVisible)
        {
            this.HideArrows();
        }



        this.globalController.HideMenus();

        this.menuVisible = true;

        // instantiate
        GameObject obj = GameObject.Instantiate(this.menuUnknown, new Vector3(this.transform.position.x + 0.71f, this.transform.position.y, -1), Quaternion.identity) as GameObject;
        MenuShape shape = obj.GetComponent<MenuShape>();
        shape.controller = this;

       // obj.transform.parent = this.transform;
        this.menuObjects.Add(obj);

        obj = GameObject.Instantiate(this.menu1, new Vector3(this.transform.position.x + 0.71f + 0.5f, this.transform.position.y, -1), Quaternion.identity) as GameObject;
        shape = obj.GetComponent<MenuShape>();
        shape.controller = this;

      //  obj.transform.parent = this.transform;
        this.menuObjects.Add(obj);

        obj = GameObject.Instantiate(this.menu2, new Vector3(this.transform.position.x + 0.71f + 0.5f * 2, this.transform.position.y, -1), Quaternion.identity) as GameObject;
        shape = obj.GetComponent<MenuShape>();
        shape.controller = this;

      //  obj.transform.parent = this.transform;
        this.menuObjects.Add(obj);
    }
}
