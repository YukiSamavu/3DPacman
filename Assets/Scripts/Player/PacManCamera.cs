using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PacManCamera : MonoBehaviour
{
    public Transform targetTransform;
    [Range(0, 20)] public float rate = 5.0f;
    public bool orientToTarget = true;

    public float pitch = 25f;
    public float yaw = 0f;
    public float distance = 3.25f;

    void FixedUpdate()
    {
        Quaternion rotationBase = (orientToTarget) ? targetTransform.rotation : Quaternion.identity;
        Quaternion rotation = rotationBase * Quaternion.AngleAxis(yaw, Vector3.up) * Quaternion.AngleAxis(pitch, Vector3.right);

        Vector3 targetPosition = targetTransform.position + (rotation * (Vector3.back * distance));

        Ray ray = new Ray(targetTransform.position, (targetPosition - targetTransform.position));
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            targetPosition = raycastHit.point;
            Vector3 v = targetPosition - targetTransform.position;
            targetPosition = targetTransform.position + Vector3.ClampMagnitude(v, 5);
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, rate * Time.deltaTime);

        Vector3 direction = targetTransform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
