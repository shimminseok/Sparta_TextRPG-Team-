using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
    


    internal static class UiManager
    {
        private static int width = 172;
        private static int height = 55;

        private static char[,] uiPanel = new char[height, width];
        private static Dictionary<int, List<int>> uiDic = new Dictionary<int, List<int>>();
        private static Tuple<int, int> cusorPosition;
        public static void TextShowUI()
        {
              UIUpdater(UIName.Intro_TextBox);
            UIUpdater(UIName.Intro_TextBox);
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

        public static void UIUpdater(UIName name)
        {
            Console.Clear();

            var printUI = UITable.UITableDic[name];
            foreach (var value in printUI)
            {
                ChangeUiPanel(UITable.UIDic[value]);
            }

            // 출력
            PrintPanel();

            // 커서 위치 변경
            ReadLineAt(cusorPosition.Item1, cusorPosition.Item2);
        }
        public static void ReadLineAt(int left, int top)
        {
            string inputBuffer = "";

            ConsoleKeyInfo keyInfo;

            while (true)
            {
                keyInfo = Console.ReadKey(intercept: true);

                if (keyInfo.Key == ConsoleKey.Enter)
                    break;

                // 백스페이스 처리
                if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (inputBuffer.Length > 0)
                    {
                        inputBuffer = inputBuffer.Substring(0, inputBuffer.Length - 1);
                        uiPanel[height-top, left + inputBuffer.Length] = ' ';
                    }
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    if (left + inputBuffer.Length < width) // overflow 방지
                    {
                        inputBuffer += keyInfo.KeyChar;
                        uiPanel[height- top, left + inputBuffer.Length - 1] = keyInfo.KeyChar;
                    }
                }

                PrintPanel();
                Console.SetCursorPosition(left + inputBuffer.Length, height- top);
            }

            // 최종 입력 결과 inputBuffer에 저장됨
        }
        public static void ChangeUiPanel(UI ui)
        {
            (int pivotX, int pivotY) = ui.Pivot;
            pivotY -=1;

            if(ui.CusorPivot.Item1 != 0 && ui.CusorPivot.Item2 != 0)
            {
                cusorPosition = ui.CusorPivot;
            }
           
            string[] splitStrings = ui.UiString.Split('\n');

            for (int y = 0; y < splitStrings.Length; y++)
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

                    uiPanel[panelY, panelX] = line[x];
                }
            }
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