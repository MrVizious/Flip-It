using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TileModelController : MonoBehaviour
{


    public bool isRotating { get; private set; }
    [SerializeField] private Mesh emptyTileMesh, filledTileMesh;
    [SerializeField] private Material emptyTileMaterial, filledTileMaterial;



    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;

    private Quaternion initialRotation;

    private void Awake()
    {
        initialRotation = transform.rotation;
        meshCollider = GetComponent<MeshCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
    }

    public async Task SetOwner(int owner)
    {
        if (owner < 0 || owner > 2)
        {
            Debug.LogError("Invalid owner");
            return;
        }

        Quaternion targetRotation;
        if (owner == 0 || owner == 1)
        {
            targetRotation = initialRotation * Quaternion.AngleAxis(360, Vector3.right + Vector3.forward);
        }
        else
        {
            targetRotation = initialRotation * Quaternion.AngleAxis(180, Vector3.right + Vector3.forward);
        }

        // Choice: Can be used simultaneously
        //Task[] tasks = new Task[2];
        //tasks[0] = SetUpModel(owner);
        //tasks[1] = RotateTo(targetRotation);
        //await Task.WhenAll(tasks);

        await SetModel(owner);
        await RotateTo(targetRotation);
    }

    private async Task RotateTo(Quaternion targetRotation)
    {
        isRotating = true;
        //TODO Flip tile model and take waiting time out
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            Quaternion newRotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 3);
            transform.rotation = newRotation;
            await Task.Yield();
        }
        // TODO: Delete debug
        Debug.Log("Rotated");
        isRotating = false;
        return;
    }

    public async Task SetModel(int owner)
    {
        if (owner < 0 || owner > 2)
        {
            Debug.LogError("Invalid owner");
            return;
        }
        if (owner == 0)
        {
            Debug.Log("Set empty model");
            Mesh newMesh = emptyTileMesh;
            meshFilter.mesh = newMesh;
            meshRenderer.material = emptyTileMaterial;
            meshCollider.sharedMesh = newMesh;
        }
        else
        {
            meshFilter.mesh = filledTileMesh;
            meshRenderer.material = filledTileMaterial;
            meshCollider.sharedMesh = filledTileMesh;
        }
    }
}