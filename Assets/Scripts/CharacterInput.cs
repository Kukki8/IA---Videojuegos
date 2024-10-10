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
        Vector3 moveDirection = new Vector3(Input.GetAxisRaw("Vertical"), 0f, -Input.GetAxisRaw("Horizontal"));
        character.Velocity = moveDirection * Speed;
    }

}
