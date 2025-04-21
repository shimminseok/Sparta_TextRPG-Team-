namespace Camp_FourthWeek_Basic_C__;

public class SelectedJobAction : ActionBase
{
    public SelectedJobAction(string _name)
    {
        foreach (var job in JobTable.JobDataDic.Values)
            SubActionMap.Add((int)job.Type, new SelectJobAction(job, _name));
    }

    public override string Name => "직업 선택";

    public override void OnExcute()
    {
        Console.WriteLine("플레이 하실 직업을 선택해주세요.");
        Console.WriteLine();
        foreach (var job in JobTable.JobDataDic.Values) Console.Write($"\t{job.Name}");
        Console.WriteLine();
        SelectAndRunAction(SubActionMap);
    }
}