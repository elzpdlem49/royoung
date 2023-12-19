/* 1. 플레이어 클래스만 사용
* 2. 플레이어와 몬스터 전투 구현
* 3. 몬스터를 쓰러뜨리면 아이템을 획득한다.
* 아이템을 사용하는 함수 구현
* 4. 몬스터와 전투에서 활용 가능 하도록 만든다.
* 5. 아이템의 구매와 판매를 구현한다 -> NPC가 필요하다.
* -> NPC의 상점목록을 구현하기위해 인벤토리 필요.
* -> 인벤토리 아이템들을 여러개 저장하는 곳 -> 배열, 리스트
* -> NPC -> Player 추가 해야한다.
* - 상점 목록에서 구매하면 인벤토리에 아이템이 삭제되지 않는다.
* - 거래기능은 아이템이 삭제되고 구매한 대상에게 아이템을 준다.
* 6. 렙업요소 구현하기.
* 7. 장비를 구현하고싶다.
* 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG;

namespace ResRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TextRPG();
            //TestStore();
        }

        /*static void TestStore()
        {
            Player player = new Player("player", 100, 20, 10, 0, 100);
            Player npc = new Player("store", 100, 20, 10, 0, 100);

            npc.SetInventoryItem(new Item("힐링포션(소)", 10, 0, 0, 0, 10));
            npc.SetInventoryItem(new Item("힐링포션(중)", 50, 0 ,0 , 0, 50));
            npc.SetInventoryItem(new Item("힐링포션(대)", 100, 0, 0, 0,100));

            npc.DisplayInventory("의 상점 목록(선택할 아이템의 번호를 입력해주세요");

            string strInputText = Console.ReadLine();
            int nSelectIdx = int.Parse(strInputText);

            player.StoreBuy(npc, nSelectIdx);

            player.DisplayInventory("의 인벤토리");
            npc.DisplayInventory("의 인벤토리");

            player.Sell(npc, 0);
            player.DisplayInventory("의 인벤토리");
            npc.DisplayInventory("의 인벤토리");

        }*/

        enum E_TEM { HPPOSTION_S, HPPOSITION_M, HPPOSTION_L }
        static void TextRPG()
        {
            Player player = new Player("player", 100, 20, 10, 0);
            Player monster = new Player("slime", 100, 20, 10, 0);

            List<Item> m_ListItemManager = new List<Item>();

            m_ListItemManager.Add(new Item(Item.E_ITEM_CATEGORY.CONSUMABLE, "힐링포션(소)", 10, 0, 0, 0, 10));
            m_ListItemManager.Add(new Item(Item.E_ITEM_CATEGORY.CONSUMABLE,"힐링포션(중)", 50, 0, 0, 0, 50));
            m_ListItemManager.Add(new Item(Item.E_ITEM_CATEGORY.CONSUMABLE,"힐링포션(대)", 100, 0, 0, 0, 100));

            Player npc = new Player("store", 100, 20, 10, 20, 0);

            foreach (var item in m_ListItemManager)
                npc.SetInventoryItem(item);

            player.SetItemSlot(m_ListItemManager[(int)E_TEM.HPPOSTION_S]);
            monster.SetItemSlot(m_ListItemManager[(int)E_TEM.HPPOSTION_S]);

            string strSelectFiled = "";

            while (true)
            {
                Console.WriteLine("장소이름을 입력하세요.(상점, 장비함, 필드)");
                strSelectFiled = Console.ReadLine();

                Console.WriteLine("{0}에 들어갔습니다.", strSelectFiled);
                switch (strSelectFiled)
                {
                    case "상점":
                        Store(player, npc);
                        break;
                    case "장비함":
                        Inventory(player);
                        break;
                    case "필드":
                        Battle(player, monster);
                        break;
                }
            }
        }

        static void Inventory(Player player)
        {
            player.DisplayInventory("의 인벤토리");
            string strInputText = Console.ReadLine();
            int nSelectIdx = int.Parse(strInputText);
            Item selectItem = player.GetInventoryItemIdx(nSelectIdx);
            if (selectItem != null) selectItem.Use(player);
            player.Display("의 장비함");
        }
        static void Store(Player player, Player npc)
        {
            npc.DisplayInventory("의 상점 목록(선택할 아이템의 번호를 입력하세요)");
            string strInputText = Console.ReadLine();
            int nSelectIdx = int.Parse(strInputText);

            player.StoreBuy(npc, nSelectIdx);
            player.DisplayInventory("의 인벤토리");
        }

        static void Battle(Player player, Player monster)
        {

            while (true)
            {
                string strInput;
                Console.WriteLine("행동 선택");
                strInput = Console.ReadLine();
                if (strInput == "A")
                {
                    player.Display("가 공격했다!");
                    player.Attack(monster);
                }
                else
                {
                    player.UseItemSlot();
                    player.Display();
                }

                if (monster.Death())
                {
                    player.Display("가 승리!");
                    Item item = (monster.ReleaseItem());
                    player.SetItemSlot(item);
                    Console.WriteLine("{0} 가 아이템을 획득", player.m_strName);
                    break;
                }
                monster.Display("가 공격했다!");
                monster.Attack(player);
                if (player.Death())
                {
                    player.Display("가 패배");
                    break;
                }
            }
        }
    }
}
