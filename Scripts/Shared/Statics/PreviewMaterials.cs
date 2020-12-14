using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Shared.Statics
{
    public static class PreviewMaterials
    {
        public static void EnableMaterials(bool enableMaterials, GameObject geo)
        {
            MeshRenderer meshRenderTopLevel = geo.GetComponent<MeshRenderer>();
            MeshRenderer[] renders = geo.GetComponentsInChildren<MeshRenderer>();
            List<MeshRenderer> meshRendersList = new List<MeshRenderer>();

            foreach (MeshRenderer item in renders)
            {
                meshRendersList.Add(item);

            }

            meshRendersList.Add(meshRenderTopLevel);


            for (int i = 0; i < meshRendersList.Count; i++)
            {
                if (enableMaterials == false)
                {
                    meshRendersList[i].enabled = false;
                }
                else
                {
                    meshRendersList[i].enabled = true;
                }
            }
        }


        public static void CreatePreviewMesh(BoxCollider collider, Material materialToUse)
        {
            Debug.Log(collider.size.x);

            Bounds meshBounds = collider.bounds;

            Vector3[] vertices = {

    new Vector3 (-meshBounds.size.x /2, -meshBounds.size.y /2 , -meshBounds.size.z / 2),
    new Vector3 (meshBounds.size.x /2,  -meshBounds.size.y /2, -meshBounds.size.z / 2),
    new Vector3 (meshBounds.size.x /2, meshBounds.size.y /2, -meshBounds.size.z / 2),
    new Vector3 (-meshBounds.size.x /2, meshBounds.size.y /2, -meshBounds.size.z / 2),
    new Vector3 (-meshBounds.size.x /2, meshBounds.size.y /2, meshBounds.size.z / 2),
    new Vector3 (meshBounds.size.x /2,  meshBounds.size.y /2, meshBounds.size.z / 2),
    new Vector3 (meshBounds.size.x /2   ,  -meshBounds.size.y /2, meshBounds.size.z / 2),
    new Vector3 (-meshBounds.size.x /2,  -meshBounds.size.y /2, meshBounds.size.z / 2),
};



            int[] triangles = {
    0, 2, 1, //face front
	0, 3, 2,
    2, 3, 4, //face top
	2, 4, 5,
    1, 2, 5, //face right
	1, 5, 6,
    0, 7, 4, //face left
	0, 4, 3,
    5, 4, 7, //face back
	5, 7, 6,
    0, 6, 7, //face bottom
	0, 1, 6
};


            MeshFilter meshFilter = collider.gameObject.GetComponent<MeshFilter>();
            MeshRenderer meshRenderer = collider.gameObject.GetComponent<MeshRenderer>();
            if (meshFilter == null)
            {
                meshFilter = collider.gameObject.AddComponent<MeshFilter>();
                meshRenderer = collider.gameObject.AddComponent<MeshRenderer>();

            }

            meshRenderer.material = materialToUse;

            if (meshFilter.sharedMesh != null)
            {
                meshFilter.sharedMesh.Clear();
            }
            else
            {
                Mesh mesh = new Mesh();
                meshFilter.sharedMesh = mesh;
            }
            meshFilter.sharedMesh.vertices = vertices;
            meshFilter.sharedMesh.triangles = triangles;

            meshFilter.sharedMesh.Optimize();
            meshFilter.sharedMesh.RecalculateNormals();

        }
    }
}