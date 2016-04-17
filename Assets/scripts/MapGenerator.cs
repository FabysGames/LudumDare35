using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{
    public float startX = -6.5f;
    public float startY = 3.5f;
    public int sizeX = 15;
    public int sizeY = 8;

    public GameObject wall;
    public GameObject corner;
    public GameObject floor;
    public GameObject mapContainer;
    public GameObject unknownShape;

    public GameObject target1;
    public GameObject target2;

    public GameController gameController;
    private GameData gameData;
    


 


    void Start() {
        this.gameData = GameData.GetInstance();

        Map mapObject = this.gameData.GetCurrentMap();
        List<int> map = mapObject.layout;

        this.gameController.SetMinCount(mapObject.minType1, mapObject.minType2);


        int k = 0;
        for(int x = 0; x < this.sizeX; x++)
        {
            for(int y = 0; y < this.sizeY; y++)
            {
                float mx = x + this.startX;
                float my = this.startY - y;

                int i = k + this.sizeX * y;
                int mapValue = map[i];

                if(mapValue == 0)
                {
                    continue;
                }

                
                /* Map Layout */
                GameObject obj = GameObject.Instantiate(this.floor, new Vector3(mx, my), Quaternion.identity) as GameObject;
                obj.transform.parent = this.mapContainer.transform;

                // left
                int indexLeft = i - 1;
                bool leftBorder = false;
                if (k == 0 || map[indexLeft] == 0)
                {
                    leftBorder = true;
                    obj = GameObject.Instantiate(this.wall, new Vector3(mx - 0.344f, my), Quaternion.Euler(0, 0, -90)) as GameObject;
                    obj.transform.parent = this.mapContainer.transform;
                }

                // right
                int indexRight = i + 1;
                bool rightBorder = false;
                if (k == this.sizeX - 1 || map[indexRight] == 0)
                {
                    rightBorder = true;
                    obj = GameObject.Instantiate(this.wall, new Vector3(mx + 0.344f, my), Quaternion.Euler(0, 0, 90)) as GameObject;
                    obj.transform.parent = this.mapContainer.transform;
                }
                    
                // top
                int indexTop = i - this.sizeX;
                bool topBorder = false;
                if (indexTop < 0 || map[indexTop] == 0)
                {
                    topBorder = true;
                    obj = GameObject.Instantiate(this.wall, new Vector3(mx, my + 0.344f), Quaternion.Euler(0, 0, 180)) as GameObject;
                    obj.transform.parent = this.mapContainer.transform;
                }

                // bottom
                bool bottomBorder = false;
                int indexBottom = i + this.sizeX;
                if (indexBottom > map.Count - 1 || map[indexBottom] == 0)
                {
                    bottomBorder = true;
                    obj = GameObject.Instantiate(this.wall, new Vector3(mx, my - 0.344f), Quaternion.identity) as GameObject;
                    obj.transform.parent = this.mapContainer.transform;
                }

                

                // corner bottom right
                int indexCBottomR = i + this.sizeX + 1;
                if (bottomBorder && rightBorder)
                {
                    obj = GameObject.Instantiate(this.corner, new Vector3(mx + 0.672f, my - 0.672f), Quaternion.identity) as GameObject;
                    obj.transform.parent = this.mapContainer.transform;
                }

                  

                // corner bottom left
                int indexCBottomL = i + this.sizeX - 1;
                if ((k == 0 || indexCBottomL > map.Count - 1 || map[indexCBottomL] == 0) && leftBorder && bottomBorder)
                {
                    obj = GameObject.Instantiate(this.corner, new Vector3(mx - 0.672f, my - 0.672f), Quaternion.identity) as GameObject;
                    obj.transform.parent = this.mapContainer.transform;
                }
              

                // corner top right
                int indexCTopR = i - this.sizeX + 1;
                if ( (k == this.sizeX - 1 || indexCTopR < 0 || map[indexCTopR] == 0) && rightBorder && topBorder)
                {
                    obj = GameObject.Instantiate(this.corner, new Vector3(mx + 0.672f, my + 0.672f), Quaternion.identity) as GameObject;
                    obj.transform.parent = this.mapContainer.transform;
                }

               

                // corner top left
                int indexCTopL = i - this.sizeX - 1;
                if ((k == 0 || indexCTopL < 0 || map[indexCTopL] == 0) && topBorder && leftBorder)
                {
                    obj = GameObject.Instantiate(this.corner, new Vector3(mx - 0.672f, my + 0.672f), Quaternion.identity) as GameObject;
                    obj.transform.parent = this.mapContainer.transform;
                }

                

                /* Unknown Shapes */
                if(mapValue == 2)
                {
                   GameObject.Instantiate(this.unknownShape, new Vector3(mx, my), Quaternion.identity);
                }


                /* Targets */
                if (mapValue == 3)
                {
                    GameObject.Instantiate(this.target1, new Vector3(mx, my), Quaternion.identity);
                }

                if (mapValue == 4)
                {
                    GameObject.Instantiate(this.target2, new Vector3(mx, my), Quaternion.identity);
                }


            }

            k++;
        }

    }
  
}
