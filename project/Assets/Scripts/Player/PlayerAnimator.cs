using UnityEngine;
using KinematicCharacterController;
using System.Collections.Generic;
using Services;
using KinematicCharacterController.Examples;
using UnityEngine.Windows;
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] ExampleCharacterController exampleCharacterController;
    public KinematicCharacterMotor Motor;
    [SerializeField]  InputService inputService;

    private Vector2 movementInput;
    void Start()
    {
        
    }
    void Update()
    {
        if (DeathPlayer.Instance.isDeathPlayer)
            return;
        movementInput = inputService.GetMovementAxisRaw();

        // Проверяем, двигается ли игрок
        bool isMoving = movementInput.magnitude > 0.1f;

        // Обновляем параметр "run" в Animator в зависимости от того, двигается ли игрок
        animator.SetBool("run", isMoving);

        // Также можно добавить другие анимации, например, для прыжка или атаки
        if (inputService.GetJumpButton() && Motor.GroundingStatus.IsStableOnGround)
        {
            animator.SetTrigger("jump");
            AudioPlayerController.Instance.PlayFxPlayer(0);
        }

        if (inputService.GetActionButton())
        {
            if (Time.timeScale <= 0) return;
            animator.SetTrigger("attack");
            GamePLay.instance.AddPower(5);
        }
    }

    public void Attack()
    {
        
        animator.SetTrigger("attack");
        GamePLay.instance.AddPower(5);
    }
}
