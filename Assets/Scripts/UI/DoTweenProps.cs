using UnityEngine;

public class DoTweenProps : MonoBehaviour
{
    public static DoTweenProps Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this);
    }

    [Header("----UI DoTween Props----")]
    public Vector3 OnHoverEnterScale;
    public Vector3 ButtonPunch;
    public float ButtonEffectDuration;
    public Vector3 OnDownScale;


    [Header("----Item DoTween Props----")]
    public float ItemAimMovementSpeed;

    [Header("----Collectable DoTween Props----")]
    public float MoveSpeed;
    public Vector3 PunchScale;
    public float PunchDuration;
    public float PushForce;
    [Header("----Market DoTween Props----")]
    public Vector3 NotEnoughMoneyPunch;
    public float PurchasedPunchScale;
    public float PurchasedItemSpeed;
    public float NotEnoughMoneyDuration;



    public Vector3 WithRandom(Vector3 initialVector, float randomGap) => new Vector3(initialVector.x * Random.Range(-randomGap, randomGap), initialVector.y * Random.Range(-randomGap, randomGap), initialVector.z * Random.Range(-randomGap, randomGap));
}