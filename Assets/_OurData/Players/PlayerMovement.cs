using Assets.HeroEditor.Common.CharacterScripts;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Character character;
    public CharacterController charCtrl;
    public Vector3 mouseToChar = Vector3.zero;
    [SerializeField] protected Vector3 speed = Vector3.zero;
    [SerializeField] protected Vector2 direction;

    public void Update()
    {
        this.direction = this.InputToDirection();
        this.Move(direction);
        this.Turning();
    }

    protected virtual Vector2 InputToDirection()
    {
        Vector2 direction = Vector2.zero;

        if (Input.GetKey(KeyCode.LeftArrow)) direction.x = -1;
        if (Input.GetKey(KeyCode.RightArrow)) direction.x = 1;
        if (Input.GetKey(KeyCode.UpArrow)) direction.y = 1;
        return direction;
    }

    protected virtual void Turning()
    {
        Vector3 mouse = Input.mousePosition;
        Vector3 vec3 = new Vector3(mouse.x, mouse.y, this.character.transform.position.y);
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(vec3);
        this.mouseToChar = mouseWorld - this.character.transform.position;
        this.character.transform.localScale = new Vector3(Mathf.Sign(this.mouseToChar.x), 1, 1);
    }

    public void Move(Vector2 direction)
    {
        if (charCtrl.isGrounded)
        {
            this.speed = new Vector3(5 * direction.x, 10 * direction.y);
            if (direction != Vector2.zero) this.character.SetState(CharacterState.Run);
            else if (this.character.GetState() < CharacterState.DeathB) this.character.SetState(CharacterState.Idle);
        }
        else this.character.SetState(CharacterState.Jump);

        this.speed.y -= 25 * Time.deltaTime; // Depends on project physics settings
        this.charCtrl.Move(this.speed * Time.deltaTime);
    }

    public void Turn(float direction)
    {
        this.character.transform.localScale = new Vector3(Mathf.Sign(direction), 1, 1);
    }
}