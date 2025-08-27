using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public void TowerHeightChanged(Component sender, object obj)
    {
        float newHeight = (float)(obj as float?);
        StartCoroutine(ChangeCamHeight(newHeight));
    }

    private IEnumerator ChangeCamHeight(float newHeight)
    {
        float elapsed = 0f;
        float duration = 1f;
        Vector3 startPos = transform.position;
        Vector3 targetPos = Vector3.up * (newHeight + 2.24f);
        targetPos.z = -10f;

        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }
        transform.position = targetPos;
    }

    public void FollowBiscuit(Component sender, object obj)
    {
        float height = (float)(obj as float?);
        Vector3 newPos = transform.position;
        newPos.y = height + 2.24f;
        transform.position = newPos;
    }
}
