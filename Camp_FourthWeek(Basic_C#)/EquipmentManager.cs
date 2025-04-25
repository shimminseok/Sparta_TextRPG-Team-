using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camp_FourthWeek_Basic_C__
{
    internal static class EquipmentManager
    {
        // public static Dictionary<ItemType, Item> EquipmentItems = new Dictionary<ItemType, Item>();
        private static PlayerInfo playerInfo = GameManager.Instance.PlayerInfo;

        public static void EquipmentItem(Item _equipItem)
        {
            //아이템의 장착은 현재 착용한 몬스터에만 가능하고
            //만약 해당 아이템을 장착한 몬스터가 있으면 빼주고
            if (_equipItem == null) return;

            var equippedMonster = GetEquippedMonster(_equipItem);
            if (equippedMonster != null)
            {
                UnequipItem(equippedMonster);
            }

            for (int i = 0; i < _equipItem.Stats.Count; i++)
            {
                Stat stat = _equipItem.Stats[i];
                playerInfo?.Monster.Stats[stat.Type].ModifyEquipmentValue(stat.FinalValue);
            }

            playerInfo.Monster.EquipItem(_equipItem);
        }

        public static void UnequipItem(Monster _targetMonster)
        {
            Item targetItem = _targetMonster.Item;
            if (targetItem != null)
            {
                for (int i = 0; i < targetItem.Stats.Count; i++)
                {
                    _targetMonster.Stats[targetItem.Stats[i].Type]
                        .ModifyEquipmentValue(-targetItem.Stats[i].FinalValue);
                }

                _targetMonster.UnEquipItem();
            }
        }

        /// <summary>
        /// 현재 MonsterBox에 있는 몬스터중에 해당 아이템을 장착한 몬스터가 있는지
        /// </summary>
        /// <param name="_item"></param>
        /// <returns></returns>
        public static bool IsEquipped(int _uniqueNumber)
        {
            Monster equipMonster =
                InventoryManager.Instance.MonsterBox.Find(monster => monster.Item?.UniqueNumber == _uniqueNumber);
            return equipMonster != null;
        }

        /// <summary>
        /// 해당 아이템을 장착한 몬스터를 리턴해주는 함수
        /// </summary>
        /// <param name="_targetItem"></param>
        /// <returns></returns>
        public static Monster GetEquippedMonster(Item _targetItem)
        {
            return InventoryManager.Instance.MonsterBox.Find(monster => monster.Item == _targetItem);
        }
    }
}