using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    public float Speed;
    public float RotationSpeed;
    private Agent character;

    Camera cam;

    private void Awake()
    {
        character = GetComponent<Agent>();
        cam = Camera.main;
    }
    
    private void Update()
    {
        Vector3 moveDirection = cam.transform.forward * Input.GetAxisRaw("Vertical");
        moveDirection += cam.transform.right * Input.GetAxisRaw("Horizontal");
        moveDirection.y = 0;

        Vector3 rotation = new Vector3(Input.GetAxis("Mouse X"), 0, 0);

        float angle = (character.transform.eulerAngles - rotation).y;

        character.KinematicData.Velocity = moveDirection * Speed;
        character.KinematicData.Rotation = Input.GetAxis("Mouse X") * RotationSpeed;
    }

}
