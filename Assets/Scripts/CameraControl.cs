using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float minX = -45f;
    [SerializeField] private float maxX = 45f;
    [SerializeField] private Transform playerTransform;

    private void Start()
    {
        playerTransform = (playerTransform == null) ? GameObject.Find("Player").transform : playerTransform;
    }

    private void Update()
    {
        FollowPlayer();
        RotateCamera();
    }

    private void RotateCamera()
    {
        float horizontalInput = Input.GetAxis("HorizontalR");
        float verticalInput = Input.GetAxis("VerticalR");

        float rotationX = rotationSpeed * horizontalInput * Time.deltaTime;
        float rotationY = rotationSpeed * verticalInput * Time.deltaTime;

        float newRotationX = (transform.eulerAngles.x > 180) ? transform.eulerAngles.x - 360 : transform.eulerAngles.x;
        newRotationX = Mathf.Clamp(newRotationX + rotationX,minX,maxX);

        transform.rotation = Quaternion.Euler(newRotationX, transform.rotation.eulerAngles.y + rotationY, 0);
    }

    private void FollowPlayer()
    {
        transform.position = playerTransform.position;
    }
}
