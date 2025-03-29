using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2000 : Gun
{
    private void Awake()
    {
        weaponName = "p2000";//武器名称
        damage = 10;//武器伤害
        attackInterval = 0.5f;//攻击速度
        range = 100f;//武器射程
        //fireRate = ;//射线发出位置
        //bulletSpeed = ;//抛出弹壳的位置
        ammoType = AmmoType.Pistol;//使用子弹类型
        reloadTime = 2f;//换弹时间
        MaxAmmo   = 30;//最大弹药量
        MagazinesAmmo  = 10;//弹夹容量
    }

    protected override void FireWeapon()
    {
        throw new System.NotImplementedException();
        if (Input.GetMouseButtonDown(0))
        {
            //animator.CrossFade("Fire",0);
        }
    }
}
