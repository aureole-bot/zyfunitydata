using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    //抽象类，作为基类，各种枪械继承该类
    [Header("基础武器属性")]
    [Tooltip("武器名称")] protected string weaponName = "武器";
    [Tooltip("武器伤害")] protected int damage = 10;
    [Tooltip("攻击速度")] protected float attackInterval = 0.5f;
    [Tooltip("武器射程")] protected float range = 100f;
    [Tooltip("射线发出位置")] protected Transform fireRate;
    [Tooltip("抛出弹壳的位置")] protected Transform bulletSpeed;
    [Tooltip("使用子弹类型")] public AmmoType ammoType;
    [Tooltip("换弹时间")] public float reloadTime = 2f;

    [Tooltip("最大弹药量")] public int MaxAmmo  = 30;
    [Tooltip("当前弹夹中的子弹数量")] protected int CurrMagazinesAmmo;
    [Tooltip("当前该种类弹药数量")] protected int CurrentAmmoInMagazine;
    [Tooltip("弹夹容量")] public int MagazinesAmmo = 10;
    [Tooltip("判断是否在换弹")] bool isReloading = false;
    protected float nextTimeToFire = 0f;//下一次能开枪的时间，通过Time.time和每个武器的攻击速度来定义 

    
    public virtual bool TryShoot()
    {
        if (isReloading) return false;
        
        if (Time.time < nextTimeToFire) return false;
        
        if (CurrMagazinesAmmo <= 0)//弹夹中没子弹，换子弹
        {
            StartReload();
            return false;
        }
        
        // 消耗弹药
        if (CurrMagazinesAmmo >= 1)
        {
            CurrMagazinesAmmo--;
            nextTimeToFire = Time.time + attackInterval;
            // 实际射击逻辑
            FireWeapon();
            return true;
        }
        
        return false;
    }
    
    protected abstract void FireWeapon();
    
    //换弹函数
    public virtual void StartReload()
    {
        //如果正在换弹当中，或者目前弹夹是满的
        if (isReloading || CurrMagazinesAmmo == MagazinesAmmo) return;
        
        isReloading = true;
        //延迟调用，等待reloadTime之后执行换弹
        Invoke("FinishReload", reloadTime);
        
        // 可以在这里播放重新装弹音效
    }
    //装弹的逻辑
    protected virtual void FinishReload()
    {
        int ammoNeeded = MagazinesAmmo - CurrMagazinesAmmo;
        CurrentAmmoInMagazine = AmmoInventory.Instance.GetAmmoCount(ammoType);
        
        int ammoToAdd = Mathf.Min(ammoNeeded, CurrentAmmoInMagazine);//取小，如果库存没有一整个弹夹就全部取出来
        
        if (ammoToAdd > 0)
        {
            AmmoInventory.Instance.AddOrRemoveAmmo(ammoType, ammoToAdd);
            CurrMagazinesAmmo += ammoToAdd;
        }
        
        isReloading = false;
    }
    
    //获取当前弹匣中的弹药数量
    public int GetCurrentAmmo()
    {
        return CurrMagazinesAmmo;
    }
    //获取库存中该类型弹药的总数量
    public int GetTotalAmmo()
    {
        return AmmoInventory.Instance.GetAmmoCount(ammoType);
    }
    //检查是否正在重新装弹
    public bool IsReloading()
    {
        return isReloading;
    }
}
