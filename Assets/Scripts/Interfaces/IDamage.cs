public interface IDamage
{
    public float CurrenHealth { get; set; }
    void Hit(float amount);
    void Die();
}