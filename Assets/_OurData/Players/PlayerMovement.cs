using Assets.HeroEditor.Common.CharacterScripts;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Character character;
    public CharacterController charCtrl;
    [SerializeField] protected float walkingSpeed = 5;
    [SerializeField] protected float jumpSpeed = 10;
    [SerializeField] protected float fallingSpeed = 25;
    [SerializeField] protected Vector3 speed = Vector3.zero;
    [SerializeField] protected Vector2 direction;
    [SerializeField] protected Vector3 mouseToChar = Vector3.zero;

    public void Update()
    {
        this.InputToDirection();
        this.CharacterStateUpdate();
        this.Move();
        this.Turning();
    }

    protected virtual Vector2 InputToDirection()
    {
        Vector2 direction = Vector2.zero;

        if (Input.GetKey(KeyCode.LeftArrow)) direction.x = -1;
        if (Input.GetKey(KeyCode.RightArrow)) direction.x = 1;
        if (Input.GetKey(KeyCode.UpArrow)) direction.y = 1;
        this.direction = direction;
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

    protected virtual void CharacterStateUpdate()
    {
        if (this.direction != Vector2.zero) this.character.SetState(CharacterState.Run);
        else if (this.character.GetState() < CharacterState.DeathB) this.character.SetState(CharacterState.Idle);
        if (!this.charCtrl.isGrounded) this.character.SetState(CharacterState.Jump);
    }

    public void Move()
    {
        this.SpeedCalculate();
        this.speed.y -= this.fallingSpeed * Time.deltaTime;
        this.charCtrl.Move(this.speed * Time.deltaTime);
    }

    protected virtual void SpeedCalculate()
    {
        if (!this.charCtrl.isGrounded) return;
        this.speed = new Vector3(this.walkingSpeed * this.direction.x, this.jumpSpeed * this.direction.y);
    }

    public void Turn(float direction)
    {
        this.character.transform.localScale = new Vector3(Mathf.Sign(direction), 1, 1);
    }
}