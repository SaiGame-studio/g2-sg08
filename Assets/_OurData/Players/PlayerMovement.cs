using Assets.HeroEditor.Common.CharacterScripts;
using UnityEngine;

public class PlayerMovement : SaiBehaviour
{
    [Header("Components")]
    public Character character;
    public CharacterController charCtrl;
    [SerializeField] protected Transform myGround;

    [Header("Layers")]
    [SerializeField] protected int layerHero;
    [SerializeField] protected int layerGround;
    [SerializeField] protected int layerCeiling;

    [Header("Movement")]
    [SerializeField] protected float walkingSpeed = 7;
    [SerializeField] protected float jumpSpeed = 9;
    [SerializeField] protected float fallingSpeed = 25;
    [SerializeField] protected int jumbMax = 2;
    [SerializeField] protected int jumbCount = 0;
    [SerializeField] protected bool isGrounded = true;
    [SerializeField] protected bool canJumb = false;
    [SerializeField] protected bool jumbed = false;

    [Header("Input")]
    [SerializeField] protected float inputHorizontalRaw = 0f;
    [SerializeField] protected float inputVerticalRaw = 0f;
    [SerializeField] protected float inputJumbRaw = 0f;
    [SerializeField] protected bool pressJumb = false;

    [Header("Vectors")]
    [SerializeField] protected Vector3 speed = Vector3.zero;
    [SerializeField] protected Vector2 direction;
    [SerializeField] protected Vector3 mouseToChar = Vector3.zero;

    private void Awake()
    {
        this.jumbCount = this.jumbMax;
        Physics.IgnoreLayerCollision(this.layerHero, this.layerCeiling, true);
    }

    public void Update()
    {
        this.GroundFinding();
        this.InputToDirection();
        //this.IsGrounded();
        this.IsGoingDown();
        this.CharacterStateUpdate();
        this.Turning();
        this.Move();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.GetPlayers();
    }

    protected virtual void GetPlayers()
    {
        if (this.layerHero > 0 && this.layerGround > 0 && this.layerCeiling > 0) return;

        this.layerHero = LayerMask.NameToLayer("Hero");
        this.layerGround = LayerMask.NameToLayer("Ground");
        this.layerCeiling = LayerMask.NameToLayer("Ceiling");

        if (this.layerHero < 0) Debug.LogError("Layer Hero is mising");
        if (this.layerGround < 0) Debug.LogError("Layer Ground is mising");
        if (this.layerCeiling < 0) Debug.LogError("Layer Ceiling is mising");

        Debug.Log(transform.name + ": GetPlayers");
    }

    protected virtual void GroundFinding()
    {
        Vector3 direction = Vector3.down;
        Vector3 position = this.character.transform.position;
        Physics.Raycast(position, direction, out RaycastHit hit);
        this.DebugRaycast(position, hit, direction);
        if (hit.transform == null) return;

        Ground ground = hit.transform.GetComponent<Ground>();
        if (ground == null) return;
        if (this.myGround == hit.transform) return;

        ground.ChangeLayer(this.layerGround);
        this.myGround = hit.transform;
    }


    protected virtual Vector2 InputToDirection()
    {
        Vector2 direction = Vector2.zero;

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

        if (this.isGrounded) this.ResetJumb();
        return this.isGrounded;
    }

    protected virtual void ResetJumb()
    {
        this.jumbCount = this.jumbMax;
        this.canJumb = true;
        this.jumbed = false;
    }

    protected virtual bool IsGoingDown()
    {
        bool isGoingDown = this.direction.y < 0;
        if (isGoingDown) this.ResetMyGround();
        return isGoingDown;
    }

    public virtual void ResetMyGround()
    {
        if (this.myGround == null) return;
        this.myGround.GetComponent<Ground>().ChangeLayer(this.layerCeiling);
        this.myGround = null;
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
        if (this.jumbed && !this.pressJumb)
        {
            this.canJumb = true;
            this.jumbCount--;
            this.jumbed = false;
        }

        if (!this.pressJumb) return;
        if (!this.canJumb) return;
        if (this.jumbCount < 1) return;

        this.jumbed = true;
        this.canJumb = false;

        this.speed.y = this.jumpSpeed * this.direction.y;
    }
}