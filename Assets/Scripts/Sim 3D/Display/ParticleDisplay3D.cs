using UnityEngine;

public class ParticleDisplay3D : MonoBehaviour
{

    public Shader shader;
    public float scale;
    Mesh mesh;
    public Color col;
    Material mat;

    ComputeBuffer argsBuffer;
    Bounds bounds;

    public Gradient colourMap;
    public int gradientResolution;
    public float velocityDisplayMax;
    Texture2D gradientTexture;
    bool needsUpdate;

    public int meshResolution;
    public int debug_MeshTriCount;

    public void Init(Simulation3D sim)
    {
        mat = new Material(shader);
        mat.SetBuffer("Positions", sim.positionBuffer);
        mat.SetBuffer("Velocities", sim.velocityBuffer);

        mesh = SebStuff.SphereGenerator.GenerateSphereMesh(gradientResolution);
        debug_MeshTriCount = mesh.triangles.Length / 3;
        argsBuffer = ComputeHelper.CreateArgsBuffer(mesh, sim.positionBuffer.count);
        bounds = new Bounds(Vector3.zero, Vector3.one * 10000);

        // Vector3[] positions = new Vector3[sim.positionBuffer.count];
        // sim.positionBuffer.GetData(positions);

        // Instantiate particle game objects at positions
        // for (int i = 0; i < sim.positionBuffer.count; i++)
        // {
        //     Instantiate(particlePrefab, positions[i], Quaternion.identity, transform);
        // }
    }

    void LateUpdate()
    {

        UpdateSettings();
        Graphics.DrawMeshInstancedIndirect(mesh, 0, mat, bounds, argsBuffer);
    }

    void UpdateSettings()
    {
        if (needsUpdate)
        {
            needsUpdate = false;
            ParticleDisplay2D.TextureFromGradient(ref gradientTexture, gradientResolution, colourMap);
            mat.SetTexture("ColourMap", gradientTexture);
        }
        mat.SetFloat("scale", scale);
        mat.SetColor("colour", col);
        mat.SetFloat("velocityMax", velocityDisplayMax);

        Vector3 s = transform.localScale;
        transform.localScale = Vector3.one;
        var localToWorld = transform.localToWorldMatrix;
        transform.localScale = s;

        mat.SetMatrix("localToWorld", localToWorld);
    }

    void OnValidate()
    {
        needsUpdate = true;
    }

    void OnDestroy()
    {
        ComputeHelper.Release(argsBuffer);
    }
}

