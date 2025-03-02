#if UNITY_EDITOR
using Core.EditorMisc;
using UnityEditor;
#endif
using System;
using UnityEngine;

namespace Core.Meshes
{
    [ExecuteInEditMode]
    public class Block : MonoBehaviour
    {
        [SerializeField] private float _width = 1f;
        [SerializeField] private float _height = 1f;
        [SerializeField] private float _depth = 1f;
        
        [Header("Save Settings")]
        [SerializeField] private string _saveFolder = "Resources/Models/Generated";
        [SerializeField] private Mesh _savedMesh;

        void OnValidate()
        {
            if (Application.isEditor)
            {
                GenerateMesh();
            }
        }

        void Awake()
        {
            GenerateMesh();
        }

        void GenerateMesh()
        {
            MeshFilter meshFilter = GetComponent<MeshFilter>();
            if (meshFilter == null) meshFilter = gameObject.AddComponent<MeshFilter>();
        
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            if (renderer == null) renderer = gameObject.AddComponent<MeshRenderer>();

            Mesh mesh = _savedMesh != null ? _savedMesh : new Mesh();

            Vector3[] vertices = new Vector3[]
            {
                new Vector3(0, 0, 0), new Vector3(_width, 0, 0), new Vector3(_width, _height, 0), new Vector3(0, _height, 0),
                new Vector3(_width, 0, _depth), new Vector3(0, 0, _depth), new Vector3(0, _height, _depth), new Vector3(_width, _height, _depth),
                new Vector3(0, 0, _depth), new Vector3(0, 0, 0), new Vector3(0, _height, 0), new Vector3(0, _height, _depth),
                new Vector3(_width, 0, 0), new Vector3(_width, 0, _depth), new Vector3(_width, _height, _depth), new Vector3(_width, _height, 0),
                new Vector3(0, 0, _depth), new Vector3(_width, 0, _depth), new Vector3(_width, 0, 0), new Vector3(0, 0, 0),
                new Vector3(0, _height, 0), new Vector3(_width, _height, 0), new Vector3(_width, _height, _depth), new Vector3(0, _height, _depth)
            };

            int[] triangles = {
                0, 2, 1,  0, 3, 2,        // Front
                4, 6, 5,  4, 7, 6,        // Back
                8, 10, 9,  8, 11, 10,     // Left
                12, 14, 13,  12, 15, 14,  // Right
                16, 18, 17,  16, 19, 18,  // Bottom
                20, 22, 21,  20, 23, 22   // Top
            };

            Vector2[] uvs = new Vector2[24]
            {
                new Vector2(0, 0), new Vector2(_width, 0), new Vector2(_width, _height), new Vector2(0, _height),
                new Vector2(0, 0), new Vector2(_width, 0), new Vector2(_width, _height), new Vector2(0, _height),
                new Vector2(0, 0), new Vector2(_depth, 0), new Vector2(_depth, _height), new Vector2(0, _height),
                new Vector2(0, 0), new Vector2(_depth, 0), new Vector2(_depth, _height), new Vector2(0, _height),
                new Vector2(0, 0), new Vector2(_width, 0), new Vector2(_width, _depth), new Vector2(0, _depth),
                new Vector2(0, 0), new Vector2(_width, 0), new Vector2(_width, _depth), new Vector2(0, _depth)
            };

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uvs;
            mesh.RecalculateNormals();

            meshFilter.sharedMesh = mesh;

            if (renderer.sharedMaterial == null)
                renderer.sharedMaterial = new Material(Shader.Find("Standard"));
        }

#if UNITY_EDITOR
        [Button]
        void SerializeMesh()
        {
            MeshFilter meshFilter = GetComponent<MeshFilter>();
            if (meshFilter == null || meshFilter.sharedMesh == null)
            {
                Debug.LogWarning("No mesh to serialize. Generate the mesh first.");
                return;
            }
            
            string objectName = string.IsNullOrEmpty(gameObject.name) ? "UnnamedBlock" : gameObject.name;
            string path = "Assets/" + _saveFolder + "/BlockMesh_" + objectName + ".asset";
            string uniquePath = AssetDatabase.GenerateUniqueAssetPath(path);

            if (string.IsNullOrEmpty(uniquePath))
            {
                Debug.LogError("Generated path is empty! Base path: " + path);
                return;
            }

            Mesh meshToSave = Instantiate(meshFilter.sharedMesh);
            AssetDatabase.CreateAsset(meshToSave, uniquePath);
            AssetDatabase.SaveAssets();
            _savedMesh = meshToSave;

            Debug.Log($"Mesh saved to: {uniquePath}");
        }
#endif

#if UNITY_EDITOR
        [MenuItem("GameObject/3D Object/Block", false, 10)]
        static void CreateCustomCube()
        {
            GameObject cube = new GameObject("Block");
            cube.AddComponent<Block>();
            Undo.RegisterCreatedObjectUndo(cube, "Create Block");
            Selection.activeObject = cube;
        }
#endif
    }
}