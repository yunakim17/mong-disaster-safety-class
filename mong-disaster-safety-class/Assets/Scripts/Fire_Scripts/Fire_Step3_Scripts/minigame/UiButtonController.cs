using UnityEngine;

public class UIButtonController : MonoBehaviour
{
    public PlayerMover playerMover;

    public void OnUpButtonDown() => playerMover.MoveUp();
    public void OnDownButtonDown() => playerMover.MoveDown();
    public void OnLeftButtonDown() => playerMover.MoveLeft();
    public void OnRightButtonDown() => playerMover.MoveRight();
    public void OnButtonUp() => playerMover.Stop();  // ¹öÆ° ¶ÃÀ» ¶§ ¸ØÃã
}
