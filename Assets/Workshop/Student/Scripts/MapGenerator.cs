    using System;
using UnityEngine;

namespace Workshop.Student
{
    public class MapGenerator : MonoBehaviour
    {
        public int columns = 10;
        public int rows = 10;

        public GameObject[] floorTiles;
        public GameObject[] wallTiles;
        public GameObject[] foodTiles;

        public GameObject[] playerGO;
        public GameObject obstacleGO;
        public GameObject exitGO;

        public string[,] saveItemMap = new string[3, 3] {
            { " ", "Soda", " "},
            { " ", " ", " "},
            { " ", " ", "Food"},
        };

        // 1. declare Players variable

        // 7. declare Exit variable 


        public void Start()
        {
            // 1. random player at the position <0, 0> map
                
            int player_r = UnityEngine.Random.Range(0, playerGO.Length);
            GameObject player = Instantiate(playerGO[player_r], new Vector2(0, 0), Quaternion.identity);

            // 2. create obstacles // center tile vetical hight half of the map

            for (int i = 0; i < rows / 2; i++) 
            {
                GameObject obstac = Instantiate(obstacleGO, new Vector2(columns/2, i), Quaternion.identity);
                obstac.GetComponent<SpriteRenderer>().sortingOrder = 1;

                obstac.name = $"obstacle {i}";
            }

            // 3. create floor
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    int floor_r = UnityEngine.Random.Range(0, floorTiles.Length);
                    GameObject tile = Instantiate(floorTiles[floor_r], new Vector2(x, y), Quaternion.identity);
                    tile.name = $"Floor {y}_{x}";
                }
            }
            // 4. create walls

            for (int y = -1; y <= rows ; y++)
            {
                for (int x = -1 ; x <= columns ; x++)
                {
                    if (x == -1 || x == rows  || y == -1 || y == columns) 
                    {
                        int wall_r = UnityEngine.Random.Range(0, wallTiles.Length);
                        GameObject tile = Instantiate(wallTiles[wall_r], new Vector2(x, y), Quaternion.identity);
                        tile.name = $"wall {y}_{x}";
                    }
                }
            }

            // 5. random foods
            
            int numberFood = UnityEngine.Random.Range(1, 3);

            for (int f = 0; f < numberFood; f++)
            {
                int food_x = UnityEngine.Random.Range(0, columns);
                int food_y = UnityEngine.Random.Range(0, rows);

                int food_r = UnityEngine.Random.Range(0, foodTiles.Length);
                GameObject food = Instantiate(foodTiles[food_r], new Vector2(food_x, food_y), Quaternion.identity);
            }

            // 6. generate item along with the saveItemMap
            for(int y = 0; y < saveItemMap.GetLength(0); y++) 
            {
                for (int x = 0; x < saveItemMap.GetLength(1); x++) 
                {
                    if (!(string.IsNullOrEmpty(saveItemMap[y, x])))
                    {
                        switch (saveItemMap[y, x])
                        {
                            case "Soda":
                                GameObject soda = Instantiate(foodTiles[1], new Vector2(x, y), Quaternion.identity);
                                break;
                            case "Food":
                                GameObject food = Instantiate(foodTiles[0], new Vector2(x, y), Quaternion.identity);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }


            // 7. place exit // exit on the top right
            GameObject exit = Instantiate(exitGO, new Vector2(columns - 1, rows - 1), Quaternion.identity);
        }
    }

}