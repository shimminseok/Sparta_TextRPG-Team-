using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static Camp_FourthWeek_Basic_C__.StringUtil;

namespace Camp_FourthWeek_Basic_C__
{
    public static class StringUtil
    {
        public static string PadRightWithKorean(string _str, int _totalWidth)
        {
            int width = _str.Sum(c => IsKorean(c) ? 2 : 1);

            int padding = _totalWidth - width;
            StringBuilder sb = new StringBuilder();
            sb.Append(_str);
            if (padding > 0)
            {
                sb.Append(new string(' ', padding));
                return sb.ToString();
            }

            return sb.ToString();
        }

        private static bool IsKorean(char _c)
        {
            return (_c >= 0xAC00 && _c <= 0xD7A3);
        }
        public static int GetCharWidth(char c)
        {
            return IsKorean(c) ? 2 : 1;
        }


        public static string FormatText(string template, Dictionary<int, string>? dict, int maxFormat=0)
        {
            for(int i = 0; i < maxFormat; i++)
            {
                if(dict.ContainsKey(i) == false)
                {
                    string placeholder = "{" + i + "}";
                    template = template.Replace(placeholder, new string(' ', placeholder.Length));
                }
            }

            foreach (var pair in dict)
            {
                string placeholder = "{" + pair.Key + "}";
                int index = template.IndexOf(placeholder);
                if (index < 0) continue;

                string replacement = pair.Value;

                int deleteCount = -placeholder.Length;
                foreach (char c in replacement)
                {
                    deleteCount += IsKorean(c) ? 2 : 1;
                }

                // 치환
                template = template.Replace(placeholder, replacement);

                // replacement 끝 위치
                int afterIndex = index + replacement.Length;

                if (deleteCount > 0)
                {
                    // 남는 폭만큼 뒤 공백 제거
                    int removed = 0;
                    while (afterIndex < template.Length && removed < deleteCount && template[afterIndex] == ' ')
                    {
                        template = template.Remove(afterIndex, 1);
                        removed++;
                    }
                }
                else if (deleteCount < 0)
                {
                    // 부족한 폭만큼 뒤에 공백 추가
                    int padCount = -deleteCount;
                    template = template.Insert(afterIndex, new string(' ', padCount));
                }
            }

            return template;
        }

        public static (int,Dictionary<int, string>) MergeFormatDic((int, Dictionary<int, string>) A, (int, Dictionary<int, string>)? B)
        {
            foreach (var value in B.Value.Item2)
            {
                A.Item2[value.Key] = value.Value;
            }
            return (A.Item1+ B.Value.Item1, A.Item2);
        }

        public static string GetBar(int cur, int Max)
        {
            cur = Math.Max(0, Math.Min(cur, Max));

            double ratio = (double)cur / Max;
            int filled = (int)Math.Ceiling(ratio * 10);

            filled = Math.Max(0, Math.Min(10, filled));

            // 채워진 부분 @, 빈 부분 _
            const char filledChar = '@';
            const char emptyChar = '_';

            return $"(( {new string(filledChar, filled)}{new string(emptyChar, 10 - filled)} ))"; ;
        }
    }
    


    internal static class UiManager
    {
        private static int width = 172;
        private static int height = 59;

        private static char[,] uiPanel = new char[height, width];
        private static Dictionary<int, List<int>> uiDic = new Dictionary<int, List<int>>();
        private static Tuple<int, int>? cusorPosition;
        public static void TextShowUI()
        {
            /*
            //인트로
            if (IntroRoop() == 1)
             {
                // 이름 정하기
                 UIUpdater(UIName.Intro_TextBox);
                //스타팅 포켓몬
                     UIUpdater(UIName.Intro_SetStarting, new Dictionary<int, Tuple<int, int>?>
                 {                
                     {0, new Tuple<int, int>(0,10) },
                     {1, new Tuple<int, int>(20,10) },
                     {2, new Tuple<int, int>(70,10) },
                     {3, new Tuple<int, int>(120,10) },

                 });
               // 메인
                  UIUpdater(UIName.Main);
                // 스테이터스창
                  UIUpdater(UIName.Status, new Dictionary<int, Tuple<int, int>?>
                 {
                     {0, new Tuple<int, int>(0,0) },
                     {1, new Tuple<int, int>(10,21)},
                 },
                 (9,new Dictionary<int, string>
                 {
                     {13, "한지우"},
                     {14, "1500G"},
                     {15, "피카츄"},
                     {16, "1"},
                     {17, "10"},
                     {18, "5"},
                     {19, "100"},
                     {20, "200"},
                 }));
             }
           
            //전투
            UIUpdater(UIName.Battle, new Dictionary<int, Tuple<int, int>?>
                 {
                     {0, new Tuple<int, int>(0,0) },
                     {1, new Tuple<int, int>(7,28)},
                     {2, new Tuple<int, int>(5,6) },
                     {3, new Tuple<int, int>(60,6) },
                 },
                 (18,new Dictionary<int, string>
                 {   
                     // 플레이어 정보
                     {0, 5.ToString()},
                     {1, MonsterTable.MonsterDataDic[MonsterType.Charizard].Name},
                     {2, $"{100.ToString()} / {200.ToString()}"},
                     {3, GetBar(100,200)},
                     {4, $"{200.ToString()} / {300.ToString()}"},
                     {5, GetBar(200,300)},
                     // 적 정보 1
                     {6, $"L   V  : {10.ToString()}" },
                     {7, $"이  름 : {MonsterTable.MonsterDataDic[MonsterType.Squirtle].Name}" },
                     {8, $"H   P  : {70.ToString()} / {50.ToString()}" },
                     {9, $"    {GetBar(50,100)}" },
                     // 적 정보 2
                     {10, $"L   V  : {7.ToString()}" },
                     {11, $"이  름 : {MonsterTable.MonsterDataDic[MonsterType.Bulbasaur].Name}" },
                     {12, $"H   P  : {70.ToString()} / {100.ToString()}" },
                     {13, $"    {GetBar(70,100)}" },
    
                 }),
                 new List<int>() { 201, 102, 103 }
                 );
             */

            //UIUpdater(UIName.Inventory);
           // UIUpdater(UIName.SetPokectmon);
            UIUpdater(UIName.Equipment);
        }
        public static void PrintPanel()
        {
            Console.Clear(); // 추가!
            StringBuilder sb = new StringBuilder(); // 줄바꿈 고려

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                     sb.Append(uiPanel[y, x]);
                }
                sb.AppendLine(); // 줄 바꿈
            }

            Console.Write(sb.ToString());
        }

        public static void UIUpdater(UIName name, Dictionary<int, Tuple<int, int>?>? fixPivotDic = null, (int, Dictionary<int, string>)? addFormat = null, List<int>? addUIList =null)
        {
            Console.Clear();
            ResetUIPanel();

            List<int> printUI = UITable.UITableDic[name];
            if(addUIList != null)
            {
                foreach(var value in addUIList)
                {
                    printUI.Add(value);
                }
            }
            
            int dicCount = 0;

            if(fixPivotDic != null)
            {
                foreach (int value in printUI)
                {
                    if (printUI[dicCount] < 100)
                    {
                        ChangeUiPanel(name, UITable.UIDic[value], null, addFormat);
                        dicCount++;
                    }
                    else
                    {
                        ChangeUiPanel(name, UITable.UIDic[value], fixPivotDic[dicCount], addFormat);
                        dicCount++;
                    }
                }
            }
            else
            {
                foreach (var value in printUI)
                {
                    ChangeUiPanel(name, UITable.UIDic[value]);
                }
            }
            // 출력
            PrintPanel();

            // 커서 위치 변경
            if(cusorPosition!=null)
                ReadLineAt(cusorPosition.Item1, cusorPosition.Item2);
        }
  
        public static void ChangeUiPanel(UIName name, UI ui, Tuple<int,int>? fixedPivot = null, (int, Dictionary<int, string>)? addFormat = null)
        {
            int pivotX = 0;
            int pivotY = 0;
            if (fixedPivot != null)
            {
               ( pivotX,  pivotY) = fixedPivot;
                pivotY -= 1;
            }
            else
            {
                ( pivotX,  pivotY) = ui.Pivot;
                pivotY -= 1;
            }
            if(ui.CusorPivot.Item1 != 0 && ui.CusorPivot.Item2 != 0)
            {
                cusorPosition = ui.CusorPivot;
            }
            string inputString = ui.UiString;
            string changeString;
            if (UITable.textDic.ContainsKey(name))
            {
                if(addFormat == null)
                {
                    changeString = FormatText(inputString, UITable.textDic[name].Item2, UITable.textDic[name].Item1);
                }
                else
                {
                    (int, Dictionary<int, string>) mergeDic = MergeFormatDic(UITable.textDic[name], addFormat);
                    changeString = FormatText(inputString, mergeDic.Item2, mergeDic.Item1);
                }

             
            }
            else
            {
                changeString = inputString;

            }
            string[] splitStrings = changeString.Split('\n');

            for (int y = 1; y < splitStrings.Length; y++)
            {
                string line = splitStrings[y];

                for (int x = 0; x < line.Length; x++)
                {
                    int panelY = pivotY + y;
                    int panelX = pivotX + x;

                    // 유효 범위 확인
                    if (panelY < 0 || panelY >= height || panelX < 0 || panelX >= width)
                    {
                        continue;
                    }
                    if(line[x] == '$')
                    {
                        continue;
                    }

                    uiPanel[panelY, panelX] = line[x];
                }
            }
        }
        public static void ReadLineAt(int left, int top)
        {
            string inputBuffer = "";
            int displayWidth = 0;

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

                if (keyInfo.Key == ConsoleKey.Enter)
                    break;

                if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (inputBuffer.Length > 0)
                    {
                        char lastChar = inputBuffer[^1];
                        int charWidth = GetCharWidth(lastChar);
                        displayWidth -= charWidth;
                        inputBuffer = inputBuffer.Substring(0, inputBuffer.Length - 1);

                        for (int i = 0; i < charWidth; i++)
                            uiPanel[height - top, left + displayWidth + i] = ' ';
                    }
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    char c = keyInfo.KeyChar;
                    int charWidth = GetCharWidth(c);

                    if (left + displayWidth + charWidth <= width) // overflow 방지
                    {
                        inputBuffer += c;
                        uiPanel[height - top, left + displayWidth] = c;

                        if (charWidth == 2)
                            uiPanel[height - top, left + displayWidth + 1] = ' ';

                        displayWidth += charWidth;
                    }
                }

                PrintPanel();
                Console.SetCursorPosition(left + displayWidth, height - top);
            }
        }
        public static int IntroRoop()
        {
            while (true)
            {
                UIUpdater(UIName.Intro_GameStart_1);
                Thread.Sleep(200);
                var result = CheckIntroInput(); if (result.HasValue) return result.Value;

                UIUpdater(UIName.Intro_GameStart_2);
                Thread.Sleep(200);
                result = CheckIntroInput(); if (result.HasValue) return result.Value;

                UIUpdater(UIName.Intro_GameStart_3);
                Thread.Sleep(200);
                result = CheckIntroInput(); if (result.HasValue) return result.Value;

                UIUpdater(UIName.Intro_GameStart_2);
                Thread.Sleep(200);
                result = CheckIntroInput(); if (result.HasValue) return result.Value;

                UIUpdater(UIName.Intro_GameStart_1);
                Thread.Sleep(200);
                result = CheckIntroInput(); if (result.HasValue) return result.Value;

                UIUpdater(UIName.Intro_GameStart_4);
                Thread.Sleep(200);
                result = CheckIntroInput(); if (result.HasValue) return result.Value;
            }
        }

        public static int? CheckIntroInput()
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.D1) return 1;
                if (key == ConsoleKey.D0) return 0;
            }

            return null;
        }
        public static void ResetUIPanel()
        {
            uiPanel = new char[height, width];
        }


        public static StringBuilder ItemPrinter(Item _item, int _index = -1, bool _isShowDescript = true)
        {
            var sb = new StringBuilder();

            if (_index < 0) return sb;
            sb.Append($"{PadRightWithKorean($"- {_index + 1}", 5)}"); //아이템 이름


            sb.Append($"{PadRightWithKorean(_item.Name, 18)} | ");
            var statBuilder = new StringBuilder();
            foreach (var stat in _item.Stats)
            {
                statBuilder.Append($"{PadRightWithKorean($"{stat.GetStatName()}", 10)}");
                statBuilder.Append($"{PadRightWithKorean($"+{stat.FinalValue}", 5)}");
            }

            sb.Append($"{PadRightWithKorean(statBuilder.ToString(), 35)}");
            if (_isShowDescript)
                sb.Append($" | {PadRightWithKorean(_item.Description, 50)}");
            return sb;
        }

        public static void PrintStat(Stat _stat)
        {
            Console.WriteLine($"{StringUtil.PadRightWithKorean(_stat.GetStatName(), 15)} : {_stat.FinalValue}");
        }

        public static void PrintHeager(string _title)
        {
            Console.WriteLine("\n===============================");
            Console.WriteLine(_title);
            Console.WriteLine("===============================");
        }

        public static void PrintError(string _message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[오류] {_message}");
            Console.ResetColor();
        }

        public static void PrintSuccess(string _message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[성공] {_message}");
            Console.ResetColor();
        }
    }
}