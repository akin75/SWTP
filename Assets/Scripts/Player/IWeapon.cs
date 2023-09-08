using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{

    public void SetDamage(int value);
    public void SetTimeBetweenShots(float value);
    public int GetLevel();
    public int GetAmmo();
    public void AddLevel(int level);
    public  void Shoot();
    public IEnumerator Reload();

    public int GetDamage();

}
