using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using ZilyanusLib.Audio;

public class Punch : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _punchForce;
    [SerializeField] private List<GameObject> _effect;
    [SerializeField] private Transform _spriteRenderer; 
    private Transform _targetT;
    private bool _canSpawn;
    public void Init(Transform targetT)
    {
        _canSpawn = true;
        _targetT = targetT;
    }
    void Update(){
        if(_targetT != null){
            transform.position = Vector2.MoveTowards(transform.position, _targetT.position, _speed * Time.deltaTime);
            _spriteRenderer.right = _targetT.position;
        }
    }
    void OnCollisionEnter2D(Collision2D collision){
        if(!_canSpawn) return;
        if (collision.transform.CompareTag("Player"))
        {
            foreach (GameObject effect in _effect)
            {
                GameObject spawnedEffects = Instantiate(effect, collision.gameObject.transform.position, Quaternion.identity);
                spawnedEffects.transform.localScale = Vector2.zero;
                spawnedEffects.transform.DOScale(Vector2.one, .2f);
                AudioClass.PlayAudio("Punch", Random.Range(.5f, 1f));
            }
            collision.rigidbody.AddForce(-(transform.position - _targetT.position).normalized * _punchForce);
            StartCoroutine(collision.transform.parent.transform.parent.GetComponent<Bekir>().Hit());
            _canSpawn = true;
            
            Destroy(gameObject);
        }
    }
}