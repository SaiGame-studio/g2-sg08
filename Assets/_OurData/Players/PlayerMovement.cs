using Assets.HeroEditor.Common.CharacterScripts;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Character character;
    public CharacterController charCtrl;
    public Vector3 mouseToChar = Vector3.zero;
    private Vector3 _speed = Vector3.zero;

    //public void Start()
    //{
    //    this.character.Animator.SetBool("Ready", true);
    //}

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
        Vector3 vec3 = new Vector3(mouse.x, mouse.y, this.character.transform.position.y);
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(vec3);
        this.mouseToChar = mouseWorld - this.character.transform.position;
        this.character.transform.localScale = new Vector3(Mathf.Sign(this.mouseToChar.x), 1, 1);
    }



    public void Move(Vector2 direction)
    {
        if (charCtrl.isGrounded)
        {
            _speed = new Vector3(5 * direction.x, 10 * direction.y);

            if (direction != Vector2.zero)
            {
                //Turn(_speed.x);
            }
        }

        if (charCtrl.isGrounded)
        {
            if (direction != Vector2.zero)
            {
                character.SetState(CharacterState.Run);
            }
            else if (character.GetState() < CharacterState.DeathB)
            {
                character.SetState(CharacterState.Idle);
            }
        }
        else
        {
            character.SetState(CharacterState.Jump);
        }

        _speed.y -= 25 * Time.deltaTime; // Depends on project physics settings
        charCtrl.Move(_speed * Time.deltaTime);
    }

    public void Turn(float direction)
    {
        character.transform.localScale = new Vector3(Mathf.Sign(direction), 1, 1);
    }
}