using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private bool _isPlayer = false;

    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        if (!_isPlayer)
        {
            Destroy(gameObject);
        }
    }
}
