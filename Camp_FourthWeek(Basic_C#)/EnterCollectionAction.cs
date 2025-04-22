using System.Text;

namespace Camp_FourthWeek_Basic_C__;

public class EnterCollectionAction : ActionBase
{
    public override string Name => "포켓몬 도감";

    public EnterCollectionAction(IAction _prevAction)
    {
        PrevAction = _prevAction;
    }

    public override void OnExcute()
    {
        //1. 포켓몬 데이터Dic을 전부 가져온다.
        //2. 한번이라도 조우했으면 이름을 표시, 아니라면 ????
        //3. 포획하면 이름을 초록색으로
        SubActionMap.Clear();
        int index = 1;
        foreach (var monster in MonsterTable.MonsterDataDic.Keys)
        {
            bool isDiscovered = CollectionManager.Instnace.IsDiscovered(monster);
            bool isCaptured = CollectionManager.Instnace.IsCaptured(monster);
            string monsterName = (isCaptured || isDiscovered)
                ? MonsterTable.MonsterDataDic[monster].Name
                : "???";

            StringBuilder sb = new StringBuilder(monsterName);

            if (isCaptured)
            {
                sb.Append("\t [포획 완료]");
                Console.ForegroundColor = ConsoleColor.Green;
            }

            Console.WriteLine("테스트로 해당 번호 입력시 조우, 한번 더 입력하면 포획입니다.");
            Console.WriteLine($"{index}. {sb.ToString()}");
            Console.ResetColor();

            SubActionMap[index++] = new TestCollectionAction(monster, this);
        }

        SelectAndRunAction(SubActionMap);
    }
}

public class TestCollectionAction : ActionBase
{
    public override string Name => MonsterTable.GetMonsterByType(monsterType).Name;
    private MonsterType monsterType;

    public TestCollectionAction(MonsterType _type, IAction _prevAction)
    {
        PrevAction = _prevAction;
        monsterType = _type;
    }

    public override void OnExcute()
    {
        if (!CollectionManager.Instnace.IsDiscovered(monsterType))
        {
            CollectionManager.Instnace.OnDiscovered(monsterType);
        }
        else
        {
            CollectionManager.Instnace.OnCaptured(monsterType);
        }

        SelectAndRunAction(SubActionMap);
    }
}