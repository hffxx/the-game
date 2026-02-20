using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : MonoBehaviour
{
    [SerializeField]
    private GameObject slashAnimPrefab;

    [SerializeField]
    private Transform slashAnimSpawnPoint;
    private PlayerControls playerControls;
    private Animator myAnimator;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;

    private GameObject slashAnimation;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        playerControls = new PlayerControls();
        myAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    void Start()
    {
        playerControls.Combat.Attack.started += _ => Attack();
    }

    private void Attack()
    {
        //fire sword animation
        myAnimator.SetTrigger("Attack");
        slashAnimation = Instantiate(
            slashAnimPrefab,
            slashAnimSpawnPoint.position,
            Quaternion.identity
        );
        slashAnimation.transform.parent = this.transform.parent;
    }

    public void SwingUpFlipAnimation()
    {
        slashAnimation.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);
        if (playerController.FacingLeft)
        {
            slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnimation()
    {
        slashAnimation.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (playerController.FacingLeft)
        {
            slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        float angle =
            Mathf.Atan2(
                mousePosition.y - playerController.transform.position.y,
                Mathf.Abs(mousePosition.x - playerController.transform.position.x)
            ) * Mathf.Rad2Deg;

        if (mousePosition.x < playerController.transform.position.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void Update()
    {
        MouseFollowWithOffset();
    }
}
