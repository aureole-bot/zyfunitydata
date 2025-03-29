using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoInventory : MonoBehaviour
{
    //弹药库管理，当前所有类型的子弹数量，单例模式
    public static AmmoInventory Instance;
    //字典类型，用于存储当前各种类型子弹的数量
    private Dictionary<AmmoType, int> ammoStock = new Dictionary<AmmoType, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        // 初始化弹药库
        foreach (AmmoType type in System.Enum.GetValues(typeof(AmmoType)))
        {
            ammoStock[type] = 0;
        }
    }
    // 添加或减少弹药到库存,这边判断能否添加和能否添加和删除写在每个枪的逻辑里面(由于每把枪的最大子弹数量不同),这里的返回值是判断有没有添加成功
    public bool AddOrRemoveAmmo(AmmoType type, int amount)
    {
        if (ammoStock.ContainsKey(type))
        {
            ammoStock[type] += amount;
            return true;
            //Debug.Log($"Added {amount} {type} ammo. Total: {ammoStock[type]}");
        }
        return false;
    }
    
    // 获取特定类型弹药的剩余数量
    public int GetAmmoCount(AmmoType type)
    {
        return ammoStock.ContainsKey(type) ? ammoStock[type] : 0;
    }
}
