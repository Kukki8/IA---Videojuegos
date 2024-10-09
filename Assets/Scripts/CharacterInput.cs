using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    public float Speed;
    private Agent character;

    private void Awake()
    {
        character = GetComponent<Agent>();
    }
    private void Update()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        character.Velocity = -moveDirection * Speed;
    }

}
