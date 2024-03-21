using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraAnims : MonoBehaviour
{
    //dotween kamera animasyonlari
    public void Shake(float duration, float vibrato)
    {
        transform.DOShakePosition(duration, vibrato);
    }
    public void Size(float targetSize, float duration)
    {
        Camera.main.DOOrthoSize(targetSize, duration);
    }
    public void Rotate(float targetRotation, float duration)
    {
        Vector3 rotate = new Vector3(0, 0, targetRotation);
        transform.DORotate(rotate, duration);
    }
    public void Move(Vector3 vector3, float duration)
    {
        transform.DOMove(vector3, duration);
    }
}
