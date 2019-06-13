using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Procedure_TwoTuple : MonoBehaviour
{

    public GameObject Cube_Preset;
    public GameObject Boundary_Preset;
    public int x;
    public int y;
    public int z;
    public GameObject[,,] cells;

    public int[,,] neighbours;

    // Use this for initialization
    void Start()
    {

        GameObject boundary = Instantiate(Boundary_Preset, new Vector3((x - 1) / 2f, (y - 1) / 2f, (z - 1) / 2f), Quaternion.identity);
        boundary.GetComponent<Transform>().localScale = new Vector3(x, y, z);

        neighbours = new int[x, y, z];

        cells = new GameObject[x, y, z];
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                for (int k = 0; k < z; k++)
                {
                    cells[i, j, k] = Instantiate(Cube_Preset, new Vector3(i, j, k), Quaternion.identity);
                }
            }
        }

        // 2x2x1
        //cells[(int)(x / 2), (int)(y / 2), (int)(z / 2)].GetComponent<Cube_Script>().alive = true;
        //cells[(int)(x / 2) - 1, (int)(y / 2), (int)(z / 2)].GetComponent<Cube_Script>().alive = true;
        //cells[(int)(x / 2), (int)(y / 2) - 1, (int)(z / 2)].GetComponent<Cube_Script>().alive = true;
        //cells[(int)(x / 2) - 1, (int)(y / 2) - 1, (int)(z / 2)].GetComponent<Cube_Script>().alive = true;

        // Initializing neighbours
        for (int ci = 0; ci < x; ci++)
        {
            for (int cj = 0; cj < y; cj++)
            {
                for (int ck = 0; ck < z; ck++)
                {

                    int liveNeighbours = 0;

                    // Check Neighbours
                    for (int ni = -1; ni <= 1; ni++)
                    {
                        for (int nj = -1; nj <= 1; nj++)
                        {
                            for (int nk = -1; nk <= 1; nk++)
                            {

                                if ((ni == 0 && nj == 0 && nk == 0) || ci + ni < 0 || cj + nj < 0 || ck + nk < 0 || ci + ni >= x || cj + nj >= y || ck + nk >= z)
                                {
                                    continue;
                                }
                                else if (cells[ci + ni, cj + nj, ck + nk].GetComponent<Cube_Script>().alive)
                                {
                                    liveNeighbours++;
                                }
                            }
                        }
                    }

                    neighbours[ci, cj, ck] = liveNeighbours;
                }
            }
        }
    }

    void FixedUpdate()
    {

        // New method of updating
        int[,,] newNeighbours = new int[x, y, z];

        // Copy neighbors array into newNeighbours
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                for (int k = 0; k < z; k++)
                {
                    newNeighbours[i, j, k] = neighbours[i, j, k];
                }
            }
        }

        // Update cells
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                for (int k = 0; k < z; k++)
                {
                    // R = (9944)

                    int aliveNeighbours = neighbours[i, j, k];
                    Cube_Script script = cells[i, j, k].GetComponent<Cube_Script>();

                    if (script.alive == false && aliveNeighbours == 4)
                    {
                        script.next_Alive = true;


                        // MAKE THIS A FUNCTION
                        for (int ni = -1; ni <= 1; ni++)
                        {
                            for (int nj = -1; nj <= 1; nj++)
                            {
                                for (int nk = -1; nk <= 1; nk++)
                                {
                                    if ((ni == 0 && nj == 0 && nk == 0) || (i + ni < 0) || (j + nj < 0) || (k + nk < 0) || (i + ni >= x) || (j + nj >= y) || (k + nk >= z))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        newNeighbours[i + ni, j + nj, k + nk] += 1;
                                    }
                                }
                            }
                        }


                    }
                    else if (script.alive == false)
                    {
                        script.next_Alive = false;
                    }

                    if (script.alive == true && aliveNeighbours == 9)
                    {
                        script.next_Alive = true;
                    }
                    else if (script.alive == true)
                    {
                        script.next_Alive = false;

                        // MAKE THIS A FUNCTION
                        for (int ni = -1; ni <= 1; ni++)
                        {
                            for (int nj = -1; nj <= 1; nj++)
                            {
                                for (int nk = -1; nk <= 1; nk++)
                                {
                                    if ((ni == 0 && nj == 0 && nk == 0) || (i + ni < 0) || (j + nj < 0) || (k + nk < 0) || (i + ni >= x) || (j + nj >= y) || (k + nk >= z))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        newNeighbours[i + ni, j + nj, k + nk] -= 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        foreach (GameObject cell in cells)
        {
            cell.GetComponent<Cube_Script>().alive = cell.GetComponent<Cube_Script>().next_Alive;
        }

        // Copy newNeighbours array into neighbours
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                for (int k = 0; k < z; k++)
                {
                    neighbours[i, j, k] = newNeighbours[i, j, k];
                }
            }
        }
    }
}