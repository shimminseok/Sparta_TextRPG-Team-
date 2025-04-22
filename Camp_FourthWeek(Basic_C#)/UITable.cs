namespace Camp_FourthWeek_Basic_C__;


public interface IUIBase
{
    Tuple<int, int> Pivot { get; }
    Tuple<int, int> CusorPivot { get; }
    string UiString { get; }
}



public enum UIName
{
    Intro_TextBox,

}

public static class UITable
{
        public static readonly Dictionary<UIName, int[]> UITableDic = new Dictionary<UIName, int[]>
    {
        {
            UIName.Intro_TextBox, new int[] { 2 ,1}
        },
 
        // 필요하면 계속 추가
    };

    public static readonly Dictionary<int, UI> UIDic = new Dictionary<int, UI>
    {
        {
            // 인트로 텍스트 박스
            1,
            new UI(
                new Tuple<int, int>(0, 41), new Tuple<int, int>(7,7),
                @"
  #@@:                                                                                                                                                               :@@@   /
 @@  @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@+ %@  /
 @#.@@                                                                                                                                                               @@= @  /
 @= @                                                                                                                                                                 @  @  /
 @- @                                                                                                                                                                 @= @  /
 @= @                                                                                                                                                                 @= @  /
 @= @                                                                                                                                                                 @= @  /
 @= @                                                                                                                                                                 @= @  /
 @- @                                                                                                                                                                 @= @  /
 @= @                                                                                                                                                                 @= @  /
 @*-@                                                                                                                                                                 @# @  /
 @- @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@  @  /
 @@@                                                                                                                                                                   @@@  /
    @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@     /"
            )
        },
        {
            // 인트로 오박사
            2,
            new UI(
           new Tuple<int, int>(0, 3), new Tuple<int, int>(0,0),
                @"
                                                                          @@@@@@@@@@@@=                                                                                      /
                                                                        .  @        . :@@@@                                                                                  /
                                                                        @@@    -+         =@=                                                                                /
                                                                          @: @@@@@*:@@@@@- @@                                                                                /
                                                                          @@@     @@     @:@@                                                                                /
                                                                           .@@@@      .@@@@-                                                                                 /
                                                                          @  .@@@@ .@@+@@  @@                                                                                /
                                                                          @=@   #@= -@   @@@@                                                                                /
                                                                            @@          -@@                                                                                  /
                                                                              @  @@@@@ @                                                                                     /
                                                                          @@@@@@@%  :@@@@@@@.                                                                                /
                                                                        #@     @*@@  -@@@ :@@@@                                                                              /
                                                                       :@  @@@@@  :@%.@@@@@@-  %@                                                                            /
                                                                  @%  %@*   -  @@%@@@@ @      .@@+                                                                           /
                                                             :@@@.=@@@   @ @:   @==.-:@@   @@%@  @*                                                                          /
                                                             @ @@@  @@@  @  @- .@:= * @@  @@ @@=  @                                                                          /
                                                               @@ #  *@@@@  -@  @@*@@@@@  @    @   @                                                                         /
                                                              @  :@@@ + @@   @  @@@@@@@@ +.   #@   @                                                                         /
                                                               @@-   -@. %    .@@@@@@@@@@     +@    @+                                                                       /
                                                                @@@ .   -       @@@@@@@@      =@    @@                                                                       /
                                                                 -@@@@@@@      .@@:.+@@@       @   @+                                                                        /
                                                                        @@@@@+ -@ @@@  @  @@@@@@@@@                                                                          /
                                                                        @   @- =@ *    @  @:   @                                                                             /
                                                                        @   @- =@@@ .:.@  @#  =@                                                                             /
                                                                        @   @= =@=@@ :.@  @*  .@                                                                             /
                                                                        @@  @* +@ :@ ..@  @#  @@                                                                             /
                                                                        @@      @ +@ :=@     :@@                                                                             /
                                                                          @@@@@@= *@ : =@@@@@@                                                                               /
                                                                           @.    .@@+.::    @@                                                                               /
                                                                          *@@@*  @= @- @@@ @@                                                                                /
                                                                         -@@  @@@@@ @@@@@@@@@                                                                                /
                                                                        *@@ .=*%@@@  @@    %@@                                                                               /
                                                                          +@@%+=      :#@@#*                                                                                 /"
            )
        }
        // 필요하면 계속 추가
    };
}


public class UI : IUIBase
{
    public Tuple<int, int> Pivot { get; }
    public Tuple<int, int> CusorPivot { get; }
    public string UiString { get; }


    public UI(Tuple<int, int> pivot, Tuple<int, int> cusorPivot, string uiString)
    {
        Pivot = pivot;
        CusorPivot = cusorPivot;
        UiString = uiString;
    }
}

