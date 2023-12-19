using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    class Item
    {
        public string m_strName;
        public int m_nRecovery;
        public int m_nPrice;

        public Item(string name, int recovery, int price)
        {
            m_nRecovery = recovery;
            m_strName = name;
            m_nPrice = price;
        }
    }

    class Player
    {
        //변수(속성): 변경될수있는 값.
        public string m_strName;
        public int m_nAtk;
        public int m_nHp;

        public int m_nGold;

        public List<Item> m_listInventory = new List<Item>();

        public Item GetInventoryItemIdx(int idx)
        {
            return m_listInventory[idx];
        }
        public void SetInventory(Item item)
        {
            m_listInventory.Add(item);
        }

        public void RemoveInventoryItem(Item item)
        {
            m_listInventory.Remove(item);
        }
        public void UseInventory(int idx)
        {
            Item item = m_listInventory[idx];
            if (item != null)
                m_nHp += item.m_nRecovery;
            m_listInventory.Remove(item);
        }

        public bool StoreBuy(Player store, int selectIdx)
        {
            Item item = GetInventoryItemIdx(selectIdx);
            if (m_nGold >= item.m_nPrice)
            {
                this.SetInventory(item);
                m_nGold -= item.m_nPrice;
                Console.WriteLine("{0}을 구매하여 {1}을 소모했습니다.", item.m_strName, item.m_nPrice);
                return true;
            }
            Console.WriteLine("소지금이 부족합니다!");
            return false;
        }

        public bool Sell(Player target, int selectIdx)
        {
            Item item = this.GetInventoryItemIdx(selectIdx);
            if (target.m_nGold >= item.m_nPrice)
            {
                target.SetInventory(item);
                target.m_nGold -= item.m_nPrice;
                this.RemoveInventoryItem(item);
                this.m_nGold += item.m_nPrice;
                Console.WriteLine("{0}을 거래하여 {1}을 얻었습니다.", item.m_strName, item.m_nPrice);
                return true;
            }
            Console.WriteLine("{0}의 소지금이 부족합니다!", target.m_nGold);
            return false;
        }

        public Item m_cItemSlot;

        public void SetItemSlot(Item item)
        {
            m_cItemSlot = item;
        }

        public Item ReleaseItem()
        {
            Item temp = m_cItemSlot;
            m_cItemSlot = null;
            return temp;
        }

        public void UseItemSlot()
        {
            if (m_cItemSlot != null)
            {
                Console.WriteLine("{0} 사용", m_cItemSlot);
                m_nHp += m_cItemSlot.m_nRecovery;
                m_cItemSlot = null;
            }
            else
            {
                Console.WriteLine("아이템이 없습니다.");
            }
        }

        public Player(string name, int atk, int hp, int gold = 99999)
        {
            m_nAtk = atk;
            m_nHp = hp;
            m_strName = name;
            m_nGold = gold;
        }

        //함수(동작): 객체가 하는 행동의 알고리즘을 함수화 한것.
        public void Attack(Player target)
        {
            Random cRandom = new Random(); //? 
            int nRandom = 0;// cRandom.Next(0, 3); //1. 1// 2// 3//
            Console.WriteLine("Random:{0}", nRandom);
            if (nRandom == 1) //2. 1 == 1:T //2 == 1 : F //3 == 1 : F
            {
                target.m_nHp = target.m_nHp - (this.m_nAtk + 10); // 100 - 10 = 90 //3. //3.
                Console.WriteLine("Ciritcal Attcka!");
            }
            else //3.
                target.m_nHp = target.m_nHp - this.m_nAtk; // 100 - 10 = 90
        }
        //인터페이스(접근방식): 인간은 값을 일일히 확인하여 사고하는데 익숙하지않다. 이를 함수화하여 제공하면 이를 인터페이스라고 부른다. 
        //죽었다는것은 행동으로 보기 어려우나, 인간의 사고과정에 맞춰서 생각하 쉽게 만든다.
        public bool Death()
        {
            if (m_nHp > 0)
                return false;
            else
                return true;
        }

        public void Display(string msg = "")
        {
            Console.WriteLine("# {0} {1} #", m_strName, msg);
            Console.WriteLine("공격력: {0}", m_nAtk);
            Console.WriteLine("체력: {0}", m_nHp);
            if (m_cItemSlot != null)
                Console.WriteLine("아이템슬롯:", m_cItemSlot.m_strName);
            else
                Console.WriteLine("아이템슬롯: 비었음");
            foreach (var c in m_strName) Console.Write("#");
            foreach (var c in msg) Console.Write("#");
            Console.WriteLine();
        }
        public void DisplayInventory(string msg = "")
        {
            Console.WriteLine("# {0} {1} #", m_strName, msg);
            for (int i = 0; i < m_listInventory.Count; i++)
            {
                Console.WriteLine("[{0}]:{1}", i, m_listInventory[i].m_strName);
            }
        }
    }

    internal class RPG
    {
        public static void BattleMain()
        {
            Player player = new Player("player", 10, 100);
            Player monster = new Player("monster", 10, 100);

            while (true)
            {
                if (player.Death() == false) //플레이어가 살아있다면,
                {
                    Console.WriteLine("##### " + player.m_strName + "의 공격 #######");

                    Console.WriteLine("" + player.m_strName + "의 공격력:{0}", player.m_nAtk);
                    Console.WriteLine("" + monster.m_strName + "의 체력:{0}", monster.m_nHp);
                    if (monster.Death() == false) //if (monster.m_nHp <= 0)  break;
                        player.Attack(monster);
                    Console.WriteLine("남은 " + monster.m_strName + "의 체력:{0}", monster.m_nHp);
                }
                else
                {
                    Console.WriteLine("##### " + monster.m_strName + " 승리! #####");
                    break;
                }
                if (!monster.Death()) //몬스터가 살아있다면,
                {
                    Console.WriteLine("##### " + monster.m_strName + "의 반격 #######");
                    Console.WriteLine("" + player.m_strName + "의 공격력:{0}", monster.m_nAtk);
                    Console.WriteLine("" + monster.m_strName + "의 체력:{0}", player.m_nHp);
                    if (!player.Death())
                        monster.Attack(player);
                    Console.WriteLine("남은 " + player.m_strName + "의 체력:{0}", player.m_nHp);
                }
                else
                {
                    Console.WriteLine("##### " + player.m_strName + " 승리! #####");
                    break;
                }
            }
        }

        public static void Battle(Player player, Player monster)
        {
            while (true)
            {
                if (player.Death() == false) //플레이어가 살아있다면,
                {
                    Console.WriteLine("##### {0}의 공격 #######", player.m_strName);

                    Console.WriteLine("" + player.m_strName + "의 공격력:{0}", player.m_nAtk);
                    Console.WriteLine("" + monster.m_strName + "의 체력:{0}", monster.m_nHp);
                    if (monster.Death() == false) //if (monster.m_nHp <= 0)  break;
                        player.Attack(monster);
                    else
                    {

                    }
                    Console.WriteLine("남은 " + monster.m_strName + "의 체력:{0}", monster.m_nHp);
                }
                else
                {
                    Console.WriteLine("#####" + monster.m_strName + " 승리! #####");
                    break;
                }
                if (!monster.Death()) //monster.m_strName가 살아있다면,
                {
                    Console.WriteLine("##### " + monster.m_strName + "의 반격 #######");
                    Console.WriteLine("" + player.m_strName + "의 공격력:{0}", monster.m_nAtk);
                    Console.WriteLine("" + monster.m_strName + "의 체력:{0}", player.m_nHp);
                    if (!player.Death())
                        monster.Attack(player);
                    Console.WriteLine("남은 " + player.m_strName + "의 체력:{0}", player.m_nHp);
                }
                else
                {
                    Console.WriteLine("##### " + player.m_strName + " 승리! #####");
                    Item item = monster.ReleaseItem();
                    player.SetItemSlot(item);
                    Console.WriteLine("{0}가 {1}을 쓰러뜨리고 {2}를 획득했다.", player.m_strName, monster.m_strName, item.m_strName);
                    break;
                }
            }
        }
    }
}