using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MeshFilter))]
public class TerrainGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Defines the coordinates of the sample terrain
        Vector2[] coordinates = new Vector2[5];
        coordinates[0] = new Vector2(0, 3);
        coordinates[1] = new Vector2(2, 1);
        coordinates[2] = new Vector2(4, 2);
        coordinates[3] = new Vector2(6, 3);
        coordinates[4] = new Vector2(8, 1);


        GetComponent<MeshFilter>().mesh = GenerateMesh(coordinates);
    }

    Mesh GenerateMesh(Vector2[] coordinates)
    {
        Mesh mesh = new Mesh();

        // Sets up required things for mesh
        Vector3[] vertices = new Vector3[40];
        Vector2[] uv = new Vector2[40];
        int[] triangles = new int[36];

        // Allows for keeping track of array indecies
        int coordinateIndex = 0;
        int vertexIndex = 0;
        int uvIndex = 0;
        int triangleIndex = 0;

        // Loops through all of the coordinates and generates the vertices
        foreach (Vector2 coordinate in coordinates)
        {   
            // Sets the coordinate as one of the vertices and uv points
            vertices[vertexIndex] = new Vector3(coordinate.x, coordinate.y);
            vertexIndex += 1;
            uv[uvIndex] = coordinate;
            uvIndex += 1;

            // Sets the base as a vertex and uv point
            vertices[vertexIndex] = new Vector3(coordinate.x, 0);
            vertexIndex += 1;
            uv[uvIndex] = new Vector2(coordinate.x, 0);
            uvIndex += 1;

            // If it is higher than the one before it create a vertex in line with it
            if (coordinateIndex != 0)
            {
                if (coordinate.y > coordinates[coordinateIndex - 1].y)
                {
                    vertices[vertexIndex] = new Vector3(coordinate.x, coordinates[coordinateIndex - 1].y);
                    vertexIndex += 1;
                    uv[uvIndex] = new Vector2(coordinate.x, coordinates[coordinateIndex - 1].y);
                    uvIndex += 1;
                }
            }

            // If it is higher than the one after it create a vertex in line with it
            if (coordinateIndex < coordinates.Length-1)
            {
                if (coordinate.y > coordinates[coordinateIndex + 1].y)
                {
                    vertices[vertexIndex] = new Vector3(coordinate.x, coordinates[coordinateIndex + 1].y);
                    vertexIndex += 1;
                    uv[uvIndex] = new Vector2(coordinate.x, coordinates[coordinateIndex + 1].y);
                    uvIndex += 1;
                }
            }

            coordinateIndex += 1;
        }

        vertices = vertices.Distinct().ToArray();

        coordinateIndex = 0;

        while (coordinateIndex < coordinates.Length - 1)
        {
            // Adds current coordinate to triangles array
            triangles[triangleIndex] = Array.IndexOf(vertices, coordinates[coordinateIndex]);
            triangleIndex += 1;
            // Adds next coordinate to triangles array
            triangles[triangleIndex] = Array.IndexOf(vertices, coordinates[coordinateIndex + 1]);
            triangleIndex += 1;
            // Adds third connecting point to triangles array
            // as well as the square underneath the triangle
            if (coordinates[coordinateIndex].y < coordinates[coordinateIndex + 1].y)
            {
                triangles[triangleIndex] = Array.IndexOf(vertices, new Vector2(coordinates[coordinateIndex + 1].x, coordinates[coordinateIndex].y));
                triangleIndex += 1;

                triangles[triangleIndex] = Array.IndexOf(vertices, coordinates[coordinateIndex]);
                triangleIndex += 1;
                triangles[triangleIndex] = Array.IndexOf(vertices, new Vector2(coordinates[coordinateIndex + 1].x, coordinates[coordinateIndex].y));
                triangleIndex += 1;
                triangles[triangleIndex] = Array.IndexOf(vertices, new Vector2(coordinates[coordinateIndex].x, 0));
                triangleIndex += 1;

                triangles[triangleIndex] = Array.IndexOf(vertices, new Vector2(coordinates[coordinateIndex + 1].x, coordinates[coordinateIndex].y));
                triangleIndex += 1;
                triangles[triangleIndex] = Array.IndexOf(vertices, new Vector2(coordinates[coordinateIndex + 1].x, 0));
                triangleIndex += 1;
                triangles[triangleIndex] = Array.IndexOf(vertices, new Vector2(coordinates[coordinateIndex].x, 0));
                triangleIndex += 1;
            }
            else if (coordinates[coordinateIndex].y > coordinates[coordinateIndex + 1].y)
            {
                triangles[triangleIndex] = Array.IndexOf(vertices, new Vector2(coordinates[coordinateIndex].x, coordinates[coordinateIndex + 1].y));
                triangleIndex += 1;

                triangles[triangleIndex] = Array.IndexOf(vertices, new Vector2(coordinates[coordinateIndex].x, coordinates[coordinateIndex + 1].y));
                triangleIndex += 1;
                triangles[triangleIndex] = Array.IndexOf(vertices, coordinates[coordinateIndex + 1]);
                triangleIndex += 1;
                triangles[triangleIndex] = Array.IndexOf(vertices, new Vector2(coordinates[coordinateIndex].x, 0));
                triangleIndex += 1;

                triangles[triangleIndex] = Array.IndexOf(vertices, coordinates[coordinateIndex + 1]);
                triangleIndex += 1;
                triangles[triangleIndex] = Array.IndexOf(vertices, new Vector2(coordinates[coordinateIndex + 1].x, 0));
                triangleIndex += 1;
                triangles[triangleIndex] = Array.IndexOf(vertices, new Vector2(coordinates[coordinateIndex].x, 0));
                triangleIndex += 1;
            }

            coordinateIndex += 1;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        //mesh.uv = uv;

        return mesh;
    }
}
