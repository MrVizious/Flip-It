using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public Transform cameraPivot;
    private Camera mainCamera;
    [SerializeField]
    private float stress = 0f;
    [Range(0f, 1f)]
    public float stressDecay = 0.1f;
    public float maxOffset = 2f;
    public float maxRotation = 35f;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {

        if (stress > 0f)
        {
            stress -= stressDecay * Time.deltaTime;
            if (stress < 0f)
            {
                stress = 0f;
            }
        }
        Shake();
    }

    public void AddStress(float amount)
    {
        stress = Mathf.Clamp(stress + amount, 0f, 1f);
    }

    private void Shake()
    {
        if (stress > 0f)
        {
            float offsetX = (0.5f - Mathf.PerlinNoise(stress + 0f, Time.time)) * stress * stress * maxOffset;
            float offsetY = (0.5f - Mathf.PerlinNoise(stress + 1f, Time.time)) * stress * stress * maxOffset;
            mainCamera.transform.localPosition = new Vector3(offsetX, offsetY, 0f);

            float rotation = (0.5f - Mathf.PerlinNoise(stress + 0f, Time.time)) * stress * stress * maxRotation;
            mainCamera.transform.localRotation = Quaternion.Euler(0f, 0f, rotation);
        }
        else
        {
            mainCamera.transform.localPosition = new Vector3(0f, 0f, 0f);
            mainCamera.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

}
