using Assets.HeroEditor.Common.CharacterScripts;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Character Character;
    public CharacterController Controller; // https://docs.unity3d.com/ScriptReference/CharacterController.html
    public Vector3 mouseToChar = Vector3.zero;
    private Vector3 _speed = Vector3.zero;

    public void Start()
    {
        if (Controller == null)
        {
            Controller = Character.gameObject.AddComponent<CharacterController>();
            Controller.center = new Vector3(0, 1.125f);
            Controller.height = 2.5f;
            Controller.radius = 0.75f;
            Controller.minMoveDistance = 0;
        }

        Character.Animator.SetBool("Ready", true);
    }

    public void Update()
    {
        var direction = Vector2.zero;

        if (Input.GetKey(KeyCode.LeftArrow)) direction.x = -1;
        if (Input.GetKey(KeyCode.RightArrow)) direction.x = 1;
        if (Input.GetKey(KeyCode.UpArrow)) direction.y = 1;

        this.Move(direction);
        this.Turning();

        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    Character.SetState(CharacterState.DeathB);
        //}
    }

    protected virtual void Turning()
    {
        Vector3 mouse = Input.mousePosition;

        Vector3 vec3 = new Vector3(mouse.x, mouse.y, this.Character.transform.position.y);

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(vec3);
        this.mouseToChar = mouseWorld - this.Character.transform.position;
        this.Character.transform.localScale = new Vector3(Mathf.Sign(this.mouseToChar.x), 1, 1);
    }



    public void Move(Vector2 direction)
    {
        if (Controller.isGrounded)
        {
            _speed = new Vector3(5 * direction.x, 10 * direction.y);

            if (direction != Vector2.zero)
            {
                //Turn(_speed.x);
            }
        }

        if (Controller.isGrounded)
        {
            if (direction != Vector2.zero)
            {
                Character.SetState(CharacterState.Run);
            }
            else if (Character.GetState() < CharacterState.DeathB)
            {
                Character.SetState(CharacterState.Idle);
            }
        }
        else
        {
            Character.SetState(CharacterState.Jump);
        }

        _speed.y -= 25 * Time.deltaTime; // Depends on project physics settings
        Controller.Move(_speed * Time.deltaTime);
    }

    public void Turn(float direction)
    {
        Character.transform.localScale = new Vector3(Mathf.Sign(direction), 1, 1);
    }
}