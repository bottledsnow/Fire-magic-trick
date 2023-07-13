using UnityEngine;

public interface IFirePoint
{
    void DestroyChoosePoint();
    void PlayerChoosePoint();
    void PlayerNotChoosePoint();

    //Some FirePoint without particle ex. fire plant
    void AbsorbFire();
}
