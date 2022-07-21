using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TileModel : MonoBehaviour
{

    public async Task FlipModel()
    {
        //TODO Flip tile model and take waiting time out
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.AngleAxis(180, transform.right + transform.forward) * currentRotation;

        while (transform.rotation != targetRotation)
        {
            Debug.Log("Rotating");
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 180);
            await Task.Delay(1);
        }
        Debug.Log("Flip tile model");

    }

    public async Task InitializeModel(int owner)
    {
        //TODO Initialize tile model and take waiting time out
        await Task.Yield();
        Debug.Log("Initialize tile model");
    }
}