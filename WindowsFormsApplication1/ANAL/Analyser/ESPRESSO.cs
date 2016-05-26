using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace CRS.Core
{
    public static class ESPRESSO
    {
        private static bool espressoExeExist = false;

        static ESPRESSO()
        {
            if (Directory.Exists(Application.StartupPath + "\\ANAL\\ESPRESSO"))
            {
                if (File.Exists(Application.StartupPath + "\\ANAL\\ESPRESSO\\espresso.exe")) espressoExeExist = true;
                else MessageBox.Show(@"Не найден \ANAL\ESPRESSO\spresso.exe");
            }
            else MessageBox.Show(@"Не найдена папка \ESPRESSO\");
        }

        private static string EspressoParams = "-Dexact";
        public static void SetDopEspressoParams(string Params)
        {
            if(Params != null) EspressoParams = Params;
        }

        /// <summary>
        /// Выполняет упрощение на основании переданной таблицы, возвращает упрощенную таблицу для восстановления функции
        /// </summary>
        /// <param name="varBooleanTable">Столбцы значений переменных</param>
        /// <param name="nodeFunctionBT">Столбец значений функции</param>
        /// <returns>Список строк формата -1001</returns>
        public static List<List<int>> DoEspresso(List<List<bool>> varBooleanTable, List<bool> nodeFunctionBT)
        {
            if (!espressoExeExist)
            {
                MessageBox.Show(@"Невозможно выполнить анализ, не найден espresso.exe");
                return null;
            }

            //формируем выходной файл на основании таблицы истинности
            string outFile = "";
            //.i 2
            //.o 1
            //00  1
            //01  1
            //10  1
            //11  0
            //.e
            outFile += string.Format(".i {0}\n", varBooleanTable.Count);

            outFile += string.Format(".o {0}\n", 1);

            //пишем таблицу истинности
            //i - строка в столбцах значений
            for (int i = 0; i < nodeFunctionBT.Count; i++)
            {
                //j - столбец переменной
                for (int j = 0; j < varBooleanTable.Count; j++)
                    outFile += varBooleanTable[j][i] ? "1" : "0";

                outFile += " " + (nodeFunctionBT[i] ? "1" : "0") + "\n";
            }

            outFile += ".e";

            String esp = @"""" + Application.StartupPath + @"\\ANAL\\ESPRESSO\\espresso.exe"" " + EspressoParams;
            String input = Application.StartupPath + "\\ANAL\\ESPRESSO\\toEspresso.logic";
            String output = Application.StartupPath + "\\ANAL\\ESPRESSO\\fromEspresso.logic";

            StreamWriter file = File.CreateText(input);
            file.Write(outFile);
            file.Close();

            String ParamStartEspresso = String.Format(@" /c "" {0} ""{1}"" > ""{2}"" "" ", esp, input, output);

            //запускаем ESPRESSO и анализируем выходной файл
            try
            {
                String command = "cmd";

                ProcessStartInfo startInfo = new ProcessStartInfo(command, ParamStartEspresso);
                startInfo.WindowStyle = ProcessWindowStyle.Hidden; // невидимое окно
                Process runCmd = new Process();
                runCmd.StartInfo = startInfo;
                runCmd.Start();
                runCmd.WaitForExit();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Программа espresso.exe не запускается!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            List<List<int>> simpleBT = new List<List<int>>();
            StreamReader fileSimpledBT = File.OpenText(output);
            
            //выходной формат
            //.i 2
            //.o 1
            //.p 2
            //-0 1
            //1- 1
            //.e
            fileSimpledBT.ReadLine();
            fileSimpledBT.ReadLine();
            fileSimpledBT.ReadLine();

            string lineTxt = fileSimpledBT.ReadLine();
            while( lineTxt != null && lineTxt[0] != '.' )
            {
                simpleBT.Add(new List<int>());
                int i = simpleBT.Count - 1;
                for (int j = 0; j < varBooleanTable.Count; j++)
                {
                    if (lineTxt[j] == '0') simpleBT[i].Add(0);
                    if (lineTxt[j] == '1') simpleBT[i].Add(1);
                    if (lineTxt[j] == '-') simpleBT[i].Add(-1);
                }

                if (lineTxt[varBooleanTable.Count + 1] == '0') simpleBT[i].Add(0);
                if (lineTxt[varBooleanTable.Count + 1] == '1') simpleBT[i].Add(1);
                if (lineTxt[varBooleanTable.Count + 1] == '-') simpleBT[i].Add(-1);

                lineTxt = fileSimpledBT.ReadLine();
            }
            fileSimpledBT.Close();

            return simpleBT;
        }

    }
}
