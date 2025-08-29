using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Coroutine _moveCam;
    public void TowerHeightChanged(Component sender, object obj)
    {
        float newHeight = (float)obj;
        if (_moveCam != null) StopCoroutine(_moveCam);
        _moveCam = StartCoroutine(ChangeCamHeight(newHeight));
    }

    private IEnumerator ChangeCamHeight(float newHeight)
    {
        Vector3 targetPos = Vector3.up * (newHeight + 2.24f);
        targetPos.z = -10f;

        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            // Move smoothly with damping, not time-based t
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.05f); // 0.15f is the smoothing factor
            yield return null;
        }
        transform.position = targetPos; // Snap exactly
    }

    public void FollowBiscuit(Component sender, object obj)
    {
        float height = (float)(obj as float?);
        Vector3 newPos = transform.position;
        newPos.y = height + 2.24f;
        transform.position = newPos;
    }
}
