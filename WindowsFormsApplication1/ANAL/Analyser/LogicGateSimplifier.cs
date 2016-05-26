using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRS.Core
{
    public class LogicGateSimplifier
    {
        /// <summary>
        /// Получение логического дерева из строки с упрощением/без
        /// </summary>
        /// <param name="mStr">строка функции</param>
        public static SLGate GetSLGTreeFromString(String mStr, bool doSimple)
        {
            //создаем корневую ячейку
            SLGate owner_node = new SLGate();
            
            owner_node.func = mStr;
            owner_node.child_count = -1;
            owner_node.parent_lgate = null;
            owner_node.lgate1 = null;
            owner_node.lgate2 = null;
            
            //список всех переменных
            List<variable> varlist = new List<variable>();

            //генерируем дерево, раскрывая функцию корневого узла
            GenerateTreeFromNodeFunc(owner_node, ref varlist);
            owner_node.VarList = varlist;

            //производим упрощение дерева
            if (doSimple)
            {
                //ESPRESSO упрощатель
                SimplGateESPRESSO(ref owner_node);
            }

            return owner_node;
        }

        public static void SimplGateESPRESSO(ref SLGate owner_node)
        {
            //столбцы значений переменных для построения таблицы истинности узла
            List<List<bool>> varBooleanTable = new List<List<bool>>();
            //столбец значений функции узла
            List<bool> nodeFunctionBT = BuildNodeBooleanTable(owner_node, varBooleanTable);

            List<List<int>> SimpledBT = ESPRESSO.DoEspresso(varBooleanTable, nodeFunctionBT);

            if (SimpledBT == null || SimpledBT.Count == 0 || SimpledBT[0].Count != varBooleanTable.Count + 1)
            {
                MessageBox.Show("Неверно сформирована упрощенная таблица истинности SimpledBT. Упрощение не выполнено.");
                return;
            }

            string SimpledBTFunc = "";
            //переводим из таблицы истинности в упрощенную функцию
            bool wasFirstVar = false;
            for (int i = 0; i < SimpledBT.Count; i++)
            {
                if (i > 0) SimpledBTFunc += "V";

                SimpledBTFunc += "(";

                for (int j = 0; j < SimpledBT[i].Count - 1; j++)
                {
                    if (SimpledBT[i][j] == 0)
                    {
                        if (j > 0 && wasFirstVar) SimpledBTFunc += "&";
                        SimpledBTFunc += "!" + owner_node.VarList[j].name;
                        wasFirstVar = true;
                    }
                    if (SimpledBT[i][j] == 1)
                    {
                        if (j > 0 && wasFirstVar) SimpledBTFunc += "&";
                        SimpledBTFunc += owner_node.VarList[j].name;
                        wasFirstVar = true;
                    }

                }
                wasFirstVar = false;

                SimpledBTFunc += ")";

            }

            //создаем новую корневую ячейку
            owner_node = new SLGate();
            
            owner_node.func = SimpledBTFunc;
            owner_node.child_count = -1;
            owner_node.parent_lgate = null;
            owner_node.lgate1 = null;
            owner_node.lgate2 = null;
            
            //список всех переменных
            List<variable> varlist = new List<variable>();

            //генерируем дерево, раскрывая функцию корневого узла
            GenerateTreeFromNodeFunc(owner_node, ref varlist);
            owner_node.VarList = varlist;
        }

        /// <summary>
        /// Развертывание дерева по функции узла
        /// </summary>
        /// <param name="owner_node">Развертываемая ячейка с функцией</param>
        /// <param name="varlist">Ообщий список переменных встретившихся в функции на момент развертывания</param>
        public static void GenerateTreeFromNodeFunc(SLGate owner_node, ref List<variable> varlist)
        {
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //разбираем функцию owner_node и заполняем поля owner_node по дочерним элементам, оператору и т.п.

            String func = Braket_remover(owner_node.func);      // Функция, убирающая лишние скобки
            owner_node.func = func;

            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            // ищем оператор минимального приоритета и создаём из него верхний узел
            int min_priority = func.Length * 3; // переменная минимального приоритета оператора, устанавливаем её в максимум
            int op_index = -1; // индекс оператора с наименьшим приоритетом

            // приоритет операторов (чем больше, тем выше приоритет)
            // ! (НЕ)  = 3
            // & (И)   = 2
            // V (ИЛИ) = 1

            int braket = 0;  // счётчик не закрытых скобок, приоритет оператора определяется как его собственный приоритет + количество не закрытых скобок * 3

            for (int i = 0; i <= func.Length - 1; i++) // бежим по строке и ищем оператор минимального приоритета
            {
                if (func[i] == '(') braket++; // если наткнулись на скобки
                if (func[i] == ')') braket--;

                if ((func[i] == 'V') || (func[i] == '&') || (func[i] == '!')) // если наткнулись на оператор
                {
                    int curr_prior = 0; // приоритет текущего оператора

                    // получаем приоритет текущего оператора
                    if (func[i] == 'V') // ИЛИ
                        curr_prior = braket * 3 + 1;
                    if (func[i] == '&') // И
                        curr_prior = braket * 3 + 2;
                    if (func[i] == '!') // НЕ
                        curr_prior = braket * 3 + 3;

                    if (curr_prior < min_priority)  // если приоритет текущего оператора ниже чем у найденного ранее, тогда сохраняем текущий оператор, как оператор с наименьшим приоритетом
                    {
                        min_priority = curr_prior;  // Величина приоритета
                        op_index = i;               // Место в строке, на котором находится оператор с наименьшим приоритетом
                    }
                }
            }
            if (braket != 0)
            {
                MessageBox.Show("Неправильно расставлены скобки.");
                return;
            }

            ////////////////////////////////////////////////////////////////////////////////////////////
            // получили индекс оператора с наименьщим приоритетом, если же его нет, то индекс равен -1.

            // если оператор найден, то создаём узел древа и записываем в него этот оператор, после чего для его потомков создаём строки и рекурсивно вызываем себя для потомков
            if ((op_index >= 0) && (op_index < func.Length))
            {

                if ((func[op_index] == 'V') || (func[op_index] == '&')) // в зависимости от оператора
                {	//бинарные операторы
                    SLGate left_node = new SLGate();   /// создаем новые узелы дерева
                    SLGate right_node = new SLGate();
                    left_node.parent_lgate = owner_node;
                    right_node.parent_lgate = owner_node;

                    owner_node.SetChildCount(2);       // количество дочерних узлов
                    owner_node.SetOp(func[op_index]);       // задаём оператор узла 
                    owner_node.Setvar_ID(0); //это промежуточный узел

                    //левый операнд
                    owner_node.lgate1 = left_node;
                    left_node.func = func.Substring(0, op_index); // переписываем левую строку для того, чтобы вытащить из неё левое поддерево (бежим от начала до оператора с наименьшим приоритетом)

                    //правый операнд
                    owner_node.lgate2 = right_node;
                    right_node.func = func.Substring(op_index + 1); // переписываем правую строку для того, чтобы вытащить из неё правое поддерево

                    //разбираем функции опперандов
                    GenerateTreeFromNodeFunc(left_node, ref varlist);
                    GenerateTreeFromNodeFunc(right_node, ref varlist);

                    //добавляем в список переменных ячейки дочерние переменные
                    owner_node.VarList.AddRange(left_node.VarList);
                    owner_node.VarList.AddRange(right_node.VarList);
                    owner_node.VarList = owner_node.VarList.Distinct().ToList();

                    return;
                }

                if (func[op_index] == '!') // в зависимости от оператора
                {	// унарный оператор
                    SLGate right_node = new SLGate();
                    right_node.parent_lgate = owner_node;

                    //правый операнд
                    owner_node.lgate1 = right_node;
                    right_node.func = func.Substring(op_index + 1); // переписываем правую строку для того, чтобы вытащить из неё правое поддерево

                    owner_node.SetOp(func[op_index]);        // задаём оператор узла 
                    owner_node.SetChildCount(1);             // количество дочерних узлов
                    owner_node.Setvar_ID(0);                 // вар айди - ноль для промежуточных узлов

                    GenerateTreeFromNodeFunc(right_node, ref varlist);

                    //добавляем в список переменных ячейки дочерние переменные
                    owner_node.VarList.AddRange(right_node.VarList);
                    owner_node.VarList = owner_node.VarList.Distinct().ToList();

                    return;
                }

            }
            else
            {// если оператор в строке не найден, то это листьевой элемент, вот его и создаём
                if (func.Length == 0)
                {
                    MessageBox.Show("Сбой создания листьевого узла.");
                    return;
                }

                if ((func == "1") || (func == "0")) // это число (булевое)
                {
                    if (func == "1") owner_node.SetValue(true);  // задаём значение переменной
                    else owner_node.SetValue(false);

                    owner_node.Setvar_ID(-1); // это число
                    owner_node.func = func;
                    owner_node.SetChildCount(0);

                }

                if ((func.Length >= 1) && (func != "1") && (func != "0"))   // это переменная
                {
                    // Если это листьевой элемент, не булевое число, размером больше либо равным 1, то это - название входного сигнала. 

                    //добавляем переменную в список всех переменных функции, если ее еще нет
                    variable varOfGate = null;
                    foreach (variable VAR in varlist)
                    {
                        if (VAR.name == func)
                        {
                            varOfGate = VAR;
                            break;
                        }
                    }

                    //если не нашли - создаем новую переменную для узла дерева
                    if (varOfGate == null)
                    {
                        varOfGate = new variable(varlist.Count + 1, func);
                        varlist.Add(varOfGate);
                    }

                    //инициализируем элемент как листьевой
                    owner_node.var_ID = varOfGate.var_id;
                    owner_node.VarList.Add(varOfGate);
                    owner_node.func = func;
                    owner_node.SetChildCount(0);
                }
            }

            return;
        }


        /// <summary>
        /// Построение таблицы истинности
        /// </summary>
        /// <param name="mSlgate">Ячейка для построения таблицы</param>
        /// <param name="mVarList">Список столбцов значений переменных</param>
        /// <returns></returns>
        public static List<bool> BuildNodeBooleanTable(SLGate mSlgate, List<List<bool>> varBT)
        {
            //сколько переменных в функции узла - столько и столбцов значений для переменных
            for (int i = 0; i < mSlgate.VarList.Count; i++) varBT.Add(new List<bool>());
            
            //выходной массив - столбец значений узла
            List<bool> mout = null;

            //устанавливаем перебором все варианты переменных узла
            if ( mSlgate.VarList.Count >= 1)
            {
                mout = new List<bool>((int)Math.Pow(mSlgate.VarList.Count, 2.0));
                SetBTVars(mout, mSlgate, 0, varBT);
            }
            else
            {//для одной переменной сразу вычисляем столбец значений функции узла
                mout = new List<bool>();
                mout.Add(calculate_node(mSlgate));
            }

            return mout;

        }

        /// <summary>
        /// Установка значений переменных узла перебором всех возможных двоичных значений 2^Nпеременных
        /// </summary>
        /// <param name="result">выходные значения</param>
        /// <param name="mVarList">список переменных</param>
        /// <param name="mSlgate">ячейка</param>
        /// <param name="mVar_pos">номер рассматриваемой переменной в списке переменных ячейки</param>
        /// <returns>тру/фолс</returns>
        public static bool SetBTVars(List<bool> result, SLGate mSlgate, int mVar_pos, List<List<bool>> varBT)
        {
            if (mSlgate == null) return false;
            if (mSlgate.VarList == null) return false;
            if (result == null) return false;

            if (mVar_pos < mSlgate.VarList.Count - 1)
            { 
                // если это не последняя переменная
                mSlgate.VarList[mVar_pos].value = false;
                SetBTVars(result, mSlgate, mVar_pos + 1, varBT);
                for (int i = 0; i < (Math.Pow(2, (mSlgate.VarList.Count - mVar_pos - 1))); i++)
                    varBT[mVar_pos].Add(mSlgate.VarList[mVar_pos].value);

                mSlgate.VarList[mVar_pos].value = true;
                SetBTVars(result, mSlgate, mVar_pos + 1, varBT);
                for (int i = 0; i < (Math.Pow(2, (mSlgate.VarList.Count - mVar_pos - 1))); i++)
                    varBT[mVar_pos].Add(mSlgate.VarList[mVar_pos].value);
            }
            else
            { 
                // если это последняя переменная => все переменные уже установлены и надо вычислять значение функции
                mSlgate.VarList[mVar_pos].value = false;
                result.Add(calculate_node(mSlgate));
                varBT[mVar_pos].Add(mSlgate.VarList[mVar_pos].value);

                mSlgate.VarList[mVar_pos].value = true;
                result.Add(calculate_node(mSlgate));
                varBT[mVar_pos].Add(mSlgate.VarList[mVar_pos].value);

            }

            return true;
        }

        /// <summary>
        /// Вычисление значения узла
        /// </summary>
        /// <param name="mSlgate">узел</param>
        /// <returns>значение узла</returns>
        public static bool calculate_node(SLGate mSlgate)
        {
            if (mSlgate == null) return false;

            if (mSlgate.child_count == 0) // если узел листьевой
            {
                if (mSlgate.var_ID == -1) return mSlgate.value; // если это константа
                else // если нет
                {
                    for (int i = 0; i < mSlgate.VarList.Count; i++)  // ищем переменную в списке переменных
                    {
                        if (mSlgate.VarList[i].var_id == mSlgate.var_ID) 
                            return mSlgate.VarList[i].value;
                    }
                }
            }
            else // если узел - не листьевой
            {
                if (mSlgate.child_count == 1) // если унарный оператор
                    if (mSlgate.op == "!" && mSlgate.lgate1 != null) return (!calculate_node(mSlgate.lgate1));
                if (mSlgate.child_count == 2)	// если бинарный оператор
                {
                    if (mSlgate.op == "V") return (calculate_node(mSlgate.lgate1) || (calculate_node(mSlgate.lgate2)));
                    if (mSlgate.op == "&") return (calculate_node(mSlgate.lgate1) && (calculate_node(mSlgate.lgate2)));
                }
            }

            return false;
        }

        /// <summary>
        /// Удаление скобок
        /// </summary>
        /// <param name="func">строка</param>
        /// <returns>строка без скобок</returns> 
        public static String Braket_remover(String func)
        {
            // убираем лишние скобки

            int str_len = func.Length;
            String OUT = "";

            bool[] del = new bool[str_len]; // массив, в котором ставится флаг удаления скобки с соответствующего места

            for (int i = 0; i <= str_len - 1; i++) del[i] = false;

            int lev = 0; // уровень символа

            for (int i = 0; i <= str_len - 1; i++) // бежим по строке, ищем открывающиеся скобки
            {
                if (func[i] == '(')
                {
                    //int ch_level = 1; // относительный уровень, на котором если не будет значимых символов, мы текущую пару скобок удаляем

                    int L = 1;
                    bool int_significant = false; // встретился ли значимый символ

                    int j = 0;

                    // есть ли что отделять сверху
                    for (j = i + 1; ((j <= str_len - 1) && (L != 0)); j++) // бежим пока стартовая скобка не закрылась
                    {
                        if (func[j] == '(') L++;
                        if (func[j] == ')') L--;

                        if ((func[j] != '(') && (func[j] != ')'))
                            if (L == 1)
                                if (func[j] == '&' || func[j] == 'V' ) // тогда выражения типа (IN1)V(IN2) вычистятся. Можно '!' добавить (Макс)
                                    int_significant = true;  // если есть значимый символ на первом относительном слое
                        if (L == 0) break;
                    }

                    L = 0;
                    bool nothing = true;
                    for (int m = 0; m <= str_len - 1; m++)
                    {
                        if (func[m] == '(') L++;
                        if (func[m] == ')') L--;

                        if ((func[m] != '(') && (func[m] != ')'))
                            if (L <= lev)
                                nothing = false;  // если всё находится выше пары скобок
                    }

                    if ((!int_significant) || (nothing)) // если значимый символ не найден на первом после скобки уровне, то эту пару скобок помечаем для удаления
                    {
                        del[i] = true;
                        del[j] = true;
                    }

                    lev++;
                }
                if (func[i] == ')') lev--;
            }

            // все скобки для удаления помечены

            for (int k = 0; k <= str_len - 1; k++)
                if (!(del[k]))
                {
                    OUT += func[k];
                }

            //OUT  += 0;

            return OUT;
        }
    }
}
