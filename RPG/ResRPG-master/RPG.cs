using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    class Status
    {
        public int nHP;
        public int nMP;
        public int nStr;
        public int nDef;

        public Status(int nHP = 0, int nMP = 0, int nStr = 0, int nDef = 0)
        {
            this.nHP = nHP;
            this.nMP = nMP;
            this.nStr = nStr;
            this.nDef = nDef;
        }

        public Status(Status status)
        {
            nHP = status.nHP;
            nMP = status.nMP;
            nStr = status.nStr;
            nDef = status.nDef;
        }

        public Status Add(Status b)
        {
            Status temp = new Status();
            temp.nHP = this.nHP + b.nHP;
            temp.nMP = this.nMP + b.nMP;
            temp.nStr = this.nStr + b.nStr;
            temp.nDef = this.nDef; 
            return temp;
        }

        public static Status operator +(Status a, Status b)
        {
            Status temp = new Status();
            temp.nHP = a.nHP + b.nHP;
            temp.nMP = a.nMP + b.nMP;
            temp.nStr = a.nStr + b.nStr;
            temp.nDef = a.nDef + b.nDef;
            return temp;
        }

        public static Status operator -(Status a, Status b)
        {
            Status temp = new Status();
            temp.nHP = a.nHP - b.nHP;
            temp.nMP = b.nMP - a.nMP;
            temp.nStr = b.nStr - a.nStr;
            temp.nDef = b.nDef - a.nDef;
            return temp;
        }

        public void Display()
        {
            Console.WriteLine("HP{0}", nHP);
            Console.WriteLine("MP{0}", nMP);
            Console.WriteLine("Str{0}", nStr);
            Console.WriteLine("Def{0}", nDef);
        }
    }
    class Item
    {
        public enum E_ITEM_CATEGORY { CONSUMABLE, EQUMENT_WEAPON, EQUMENT_ARMOR, EQUMENT_ACC, ACTIVE }
        public E_ITEM_CATEGORY m_eCategory;
        public string m_strName;
        public Status m_sStatus;
        public int m_nPrice;
        public Item(E_ITEM_CATEGORY eCategory, string name, Status status, int price)
        {
            m_eCategory = eCategory;
            m_sStatus = new Status(status);
            m_strName = name;
            m_nPrice = price;
        }

        public  Item(E_ITEM_CATEGORY eCategory, string name, int hp, int mp, int str, int def, int price)
        {
            m_eCategory = eCategory;
            m_sStatus = new Status(hp, mp, str, def);
            m_strName = name;
            m_nPrice = price;
        }

        public void Use(Player target)
        {
            switch(m_eCategory)
            {
                case E_ITEM_CATEGORY.CONSUMABLE:
                    if (target.m_listEqument[(int)Player.E_EQUMENT_TYPE.WEAPON] == null)
                    {
                        target.m_listEqument[(int)Player.E_EQUMENT_TYPE.WEAPON] = this;
                        target.m_sStatus += this.m_sStatus;
                    }
                    else
                    {
                        target.SetInventoryItem(target.m_listEqument[(int)Player.E_EQUMENT_TYPE.WEAPON]);
                        target.m_sStatus -= this.m_sStatus;
                        target.m_listEqument[(int)Player.E_EQUMENT_TYPE.WEAPON] = null;
                    }
                    break;
                case E_ITEM_CATEGORY.EQUMENT_ARMOR:
                    if (target.m_listEqument[(int)Player.E_EQUMENT_TYPE.ARMOR] == null)
                    {
                        target.m_listEqument[(int)Player.E_EQUMENT_TYPE.ARMOR] = this;
                        target.m_sStatus += this.m_sStatus;
                    }
                    else
                    {
                        target.SetInventoryItem(target.m_listEqument[(int)Player.E_EQUMENT_TYPE.ARMOR]);
                        target.m_sStatus -= this.m_sStatus;
                        target.m_listEqument[(int)Player.E_EQUMENT_TYPE.ARMOR] = null;
                    }
                    break;
                case E_ITEM_CATEGORY.EQUMENT_ACC:
                    if (target.m_listEqument[(int)Player.E_EQUMENT_TYPE.ACC] == null)
                    {
                        target.m_listEqument[(int)Player.E_EQUMENT_TYPE.ACC] = this;
                        target.m_sStatus += this.m_sStatus;
                    }
                    else
                    {
                        target.SetInventoryItem(target.m_listEqument[(int)Player.E_EQUMENT_TYPE.ACC]);
                        target.m_sStatus -= this.m_sStatus;
                        target.m_listEqument[(int)Player.E_EQUMENT_TYPE.ACC] = null;
                    }
                    break;
                case E_ITEM_CATEGORY.ACTIVE:
                    break;
            }
        }
    }

    class Player
    {
        //변수(속성): 변경될수있는 값.
        public string m_strName;
        public Status m_sStatus;
        public int m_nHp;
        public int m_nMp;
        
        public int m_nGold;

        public enum E_EQUMENT_TYPE { WEAPON, ARMOR, ACC, MAX }
        public List<Item> m_listEqument = new List<Item>((int)E_EQUMENT_TYPE.MAX);

        public void Consumable(Status status)
        {
            m_nHp += status.nHP;
            m_nMp += status.nMP;
        }

        public void SetEqument(Status status)
        {
            m_sStatus += status;
        }

        public void ReleaseEqu(Player target)
        {
            target.m_sStatus -= m_sStatus;
        }

        public List<Item> m_ListInventory = new List<Item>();

        public void SetInventoryItem(Item item)
        {
            m_ListInventory.Add(item);
        }

        public Item GetInventoryItemIdx(int idx)
        {
            return m_ListInventory[idx];
        }
        public void RemoveInventoryItem(Item item)
        {
            m_ListInventory.Remove(item);
        }

        public void UseInventoryItem(int idx)
        {
            Item item = m_ListInventory[idx];
            if (item != null)
                item.Use(this);
            m_ListInventory.Remove(item);
        }

        public bool StoreBuy(Player store, int selectidx)
        {
            Item item = store.GetInventoryItemIdx(selectidx);
            if (m_nGold >= item.m_nPrice)
            {
                this.SetInventoryItem(item);
                m_nGold -= item.m_nPrice;
                Console.WriteLine("{0}을 거래 하여 {1}을 소모했습니다!", item.m_strName, item.m_nPrice);
                return true;
            }
            Console.WriteLine("소지금이 부족합니다!");
            return false;
        }
        public bool Sell(Player target, int selectidx)
        {
            Item item = this.GetInventoryItemIdx(selectidx);
            if(target.m_nGold >= item.m_nPrice)
            {
                target.SetInventoryItem(item);
                target.m_nGold -= item.m_nPrice;
                this.RemoveInventoryItem(item);
                this.m_nGold += item.m_nPrice;
                Console.WriteLine("{0}을 거래 하여 {1}을 얻었습니다!",item.m_strName,item.m_nPrice);
                return true;
            }
            Console.WriteLine("{0}의 소지금이 부족합니다!", target.m_strName);
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
                Console.WriteLine("{0} 사용 ", m_cItemSlot.m_strName);
                m_sStatus += m_cItemSlot.m_sStatus;
                m_cItemSlot = null;
            }
            else
            {
                Console.Write("아이템이 없습니다.");
                Console.WriteLine();
            }
        }

        public Player(string name,  int hp, int mp, int str, int def, int gold = 9999)
        {
            m_sStatus = new Status(hp, mp, str, def);
            m_nHp = hp;
            m_nMp = mp;
            m_strName = name;
            m_nGold = gold;
        }

        public void Damaged(int damage)
        {
            this.m_sStatus.nHP -= damage;
        }

        //함수(동작): 객체가 하는 행동의 알고리즘을 함수화 한것.
        public void Attack(Player target)
        {
            Random cRandom = new Random(); //? 
            int nRandom = 0;// cRandom.Next(0, 3); //1. 1// 2// 3//
            float fDamage = m_sStatus.nStr;
            //Console.WriteLine("Random:{0}", nRandom);
            if (nRandom == 1) //2. 1 == 1:T //2 == 1 : F //3 == 1 : F
            {
                fDamage = fDamage * 1.5f;
                Console.WriteLine("Ciritcal Attcka!");
            }
            else //3.
                target.Damaged((int)fDamage);
        }
        //인터페이스(접근방식): 인간은 값을 일일히 확인하여 사고하는데 익숙하지않다. 이를 함수화하여 제공하면 이를 인터페이스라고 부른다. 
        //죽었다는것은 행동으로 보기 어려우나, 인간의 사고과정에 맞춰서 생각하 쉽게 만든다.
        public bool Death()
        {
            if (this.m_sStatus.nHP > 0)
                return false;
            else
                return true;
        }

        public void Display(string msg = "")
        {
            Console.WriteLine("{0} {1}", m_strName, msg);
            m_sStatus.Display();
            if (m_cItemSlot != null)
            {
                Console.WriteLine("아이템: {0}", m_cItemSlot.m_strName);
            }
            else
            {
                Console.WriteLine("아이템 : 비었음");
            }
            Console.WriteLine("# 장비함 ", m_strName, msg);
            for (E_EQUMENT_TYPE e = E_EQUMENT_TYPE.WEAPON; e < E_EQUMENT_TYPE.MAX; e++)
            {
                if (m_listEqument[(int)e] != null)
                    Console.WriteLine("{0}{1}", e.ToString(), m_listEqument[(int)e].m_strName);
                else
                    Console.WriteLine("{0} 비었음", e.ToString());
            }
            foreach (var c in m_strName) Console.Write("#");
            foreach (var c in msg) Console.Write("#");
            Console.WriteLine();

        }
        public void DisplayInventory(string msg = "")
        {
            Console.WriteLine("# {0} {1} #", m_strName, msg);
            for (int i = 0; i < m_ListInventory.Count; i++)
            {
                Console.WriteLine("[{0}]:{1}", i, m_ListInventory[i].m_strName);
            }
            Console.WriteLine("Gold :{0}",m_nGold);
        }


        /*internal class RPG
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
        }*/
    }
}