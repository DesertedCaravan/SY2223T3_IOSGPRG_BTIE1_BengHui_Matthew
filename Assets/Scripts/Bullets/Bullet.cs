using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    PlayerPistolBullet,
    PlayerRapidFire,
    PlayerShotgunSpread,
    PlayerGrenade,
    EnemyPistolBullet,
    EnemyRapidFire,
    EnemyShotgunSpread,
    EnemyGrenade
}

public class Bullet : MonoBehaviour
{
    // Image Reference: https://www.bing.com/images/search?view=detailV2&ccid=PDjjFaT4&id=402FB36A53C77E3D235550FF0290F49A3500C1E2&thid=OIP.PDjjFaT4mfOP9VN-5XMZLgHaHa&mediaurl=https%3A%2F%2Fcdn0.iconfinder.com%2Fdata%2Ficons%2Fweapon-5%2F100%2F11-512.png&exph=512&expw=512&q=blast+gun+white+icon&simid=607989828591054779&form=IRPRST&ck=7A3848819180BADB4B1AA552242C3E8A&selectedindex=82&ajaxhist=0&ajaxserp=0&vt=0&sim=11

    [SerializeField] private BulletType _bulletType;
    [SerializeField] public GameObject _playerExplosion;
    [SerializeField] public GameObject _enemyExplosion;

    public float _speed;
    public float _scatter;
    public float _rate;

    public float _activeTime;

    public void Start()
    {
        _speed = 30f;
        _scatter = 0;

        if (_bulletType == BulletType.PlayerPistolBullet || _bulletType == BulletType.EnemyPistolBullet)
        {
            _scatter = Random.Range(-0.6f, 0.6f);
        }
        else if (_bulletType == BulletType.PlayerRapidFire || _bulletType == BulletType.EnemyRapidFire)
        {
            _scatter = Random.Range(-0.55f, 0.55f);
        }
        else if (_bulletType == BulletType.PlayerShotgunSpread || _bulletType == BulletType.EnemyShotgunSpread)
        {
            _speed = 50f;
            _scatter = Random.Range(-5, 6);
        }
        else if (_bulletType == BulletType.PlayerGrenade || _bulletType == BulletType.EnemyGrenade)
        {
            _scatter = Random.Range(-0.5f, 0.5f);
        }
    }

    public void Update()
    {
        transform.position += transform.right * _scatter * Time.deltaTime;
        transform.position += transform.up * _speed * Time.deltaTime;

        _activeTime += 1f * Time.deltaTime;

        if (_activeTime >= 4.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Unit unit = collision.gameObject.GetComponent<Unit>();
        BarrelSpawner barrelSpawner = collision.gameObject.GetComponent<BarrelSpawner>();
        Obstacle obstacle = collision.gameObject.GetComponent<Obstacle>();
        GrenadeExplosion grenadeExplosion = collision.gameObject.GetComponent<GrenadeExplosion>();

        if (unit != null)
        {
            Destroy(this.gameObject);
        }

        if (barrelSpawner != null)
        {
            Sound.Instance.BarrelBreak(this.transform);

            if ((_bulletType == BulletType.PlayerPistolBullet || _bulletType == BulletType.PlayerShotgunSpread) || (_bulletType == BulletType.EnemyPistolBullet || _bulletType == BulletType.EnemyShotgunSpread))
            {
                barrelSpawner.TakeDamage(10);
            }
            else if (_bulletType == BulletType.PlayerRapidFire || _bulletType == BulletType.EnemyRapidFire)
            {
                barrelSpawner.TakeDamage(15);
            }
            else if (grenadeExplosion != null)
            {
                barrelSpawner.TakeDamage(100);
            }

            barrelSpawner.TakeDamage(10);
            Destroy(this.gameObject);
        }

        if (obstacle != null && obstacle.GetObstacleType() == ObstacleType.Wood)
        {
            Sound.Instance.ObstacleBulletHit(this.transform, 1);
            Destroy(this.gameObject);
        }
        else if (obstacle != null && obstacle.GetObstacleType() == ObstacleType.Stone)
        {
            Sound.Instance.ObstacleBulletHit(this.transform, 2);
            Destroy(this.gameObject);
        }
        else if (obstacle != null && obstacle.GetObstacleType() == ObstacleType.Metal)
        {
            Sound.Instance.ObstacleBulletHit(this.transform, 3);
            Destroy(this.gameObject);
        }
        else if (obstacle != null && obstacle.GetObstacleType() == ObstacleType.Plastic)
        {
            Sound.Instance.ObstacleBulletHit(this.transform, 4);
            Destroy(this.gameObject);
        }
        else if (obstacle != null && obstacle.GetObstacleType() == ObstacleType.Null)
        {
            Destroy(this.gameObject);
        }

        if (unit != null || barrelSpawner != null || obstacle != null)
        {
            if (_bulletType == BulletType.PlayerGrenade)
            {
                GameObject unitGO = Instantiate(_playerExplosion, this.transform.position, Quaternion.identity);
                Sound.Instance.GrenadeLauncherExplosion(this.transform);
                Destroy(this.gameObject);
            }
            else if (_bulletType == BulletType.EnemyGrenade)
            {
                GameObject unitGO = Instantiate(_enemyExplosion, this.transform.position, Quaternion.identity);
                Sound.Instance.GrenadeLauncherExplosion(this.transform);
                Destroy(this.gameObject);
            }
        }
    }

    public BulletType GetBulletType()
    {
        return _bulletType;
    }
}
