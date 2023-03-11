using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AiController", menuName = "InputController/AiController")]
public class AiController : InputController
{
    public override float RetrieveMoveInput()
    {
        return 1f;
    }

    public override bool RetrieveJumpInput()
    {
        return true;
    }

    public override bool RetrieveJumpHoldInput()
    {
        return false;
    }
}
