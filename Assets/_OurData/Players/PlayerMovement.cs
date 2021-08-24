using Assets.HeroEditor.Common.CharacterScripts;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    public Character character;
    public CharacterController charCtrl;

    [Header("Movement")]
    [SerializeField] protected float walkingSpeed = 7;
    [SerializeField] protected float jumpSpeed = 9;
    [SerializeField] protected float fallingSpeed = 25;
    [SerializeField] protected int jumbMax = 3;
    [SerializeField] protected int jumbCount = 3;
    [SerializeField] protected bool isGrounded = true;
    [SerializeField] protected bool canJumb = false;
    [SerializeField] protected bool jumbing = false;

    [Header("Input")]
    [SerializeField] protected float inputHorizontal = 0f;
    [SerializeField] protected float inputHorizontalRaw = 0f;
    [SerializeField] protected float inputVertical = 0f;
    [SerializeField] protected float inputVerticalRaw = 0f;
    [SerializeField] protected float inputJumbRaw = 0f;
    [SerializeField] protected bool pressJumb = false;

    [Header("Vectors")]
    [SerializeField] protected Vector3 speed = Vector3.zero;
    [SerializeField] protected Vector2 direction;
    [SerializeField] protected Vector3 mouseToChar = Vector3.zero;

    public void Update()
    {
        this.IsGrounded();
        this.InputToDirection();
        this.CharacterStateUpdate();
        this.Move();
        this.Turning();
    }

    protected virtual Vector2 InputToDirection()
    {
        Vector2 direction = Vector2.zero;

        this.inputHorizontal = Input.GetAxis("Horizontal");
        this.inputVertical = Input.GetAxis("Vertical");

        this.inputHorizontalRaw = Input.GetAxisRaw("Horizontal");
        this.inputVerticalRaw = Input.GetAxisRaw("Vertical");
        this.inputJumbRaw = Input.GetAxisRaw("Jump");

        direction.x = this.inputHorizontalRaw;
        direction.y = this.inputVerticalRaw;
        if (this.inputVerticalRaw == 0) direction.y = this.inputJumbRaw;

        if (direction.y > 0) this.pressJumb = true;
        else this.pressJumb = false;

        this.direction = direction;
        return direction;
    }

    protected virtual bool IsGrounded()
    {
        this.isGrounded = this.charCtrl.isGrounded;

        if (this.isGrounded)
        {
            this.jumbCount = this.jumbMax;
            this.canJumb = true;
            this.jumbing = false;
        }
        return this.isGrounded;
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
        if (!this.IsGrounded()) this.character.SetState(CharacterState.Jump);
    }

    public void Move()
    {
        this.Walking();
        this.Jumbing();

        this.speed.y -= this.fallingSpeed * Time.deltaTime;
        this.charCtrl.Move(this.speed * Time.deltaTime);
    }

    protected virtual void Walking()
    {
        if (!this.IsGrounded()) return;
        this.speed.x = this.walkingSpeed * this.direction.x;
    }

    protected virtual void Jumbing()
    {
        if (this.jumbing && !this.pressJumb)
        {
            this.canJumb = true;
            this.jumbCount--;
            this.jumbing = false;
        }

        if (!this.pressJumb) return;
        if (!this.canJumb) return;
        if (this.jumbCount < 1) return;

        this.jumbing = true;
        this.canJumb = false;

        Debug.Log("Jumbing");

        this.speed.y = this.jumpSpeed * this.direction.y;
    }

    public void Turn(float direction)
    {
        this.character.transform.localScale = new Vector3(Mathf.Sign(direction), 1, 1);
    }
}