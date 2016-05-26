using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRS.Core
{
    public class SLGate
    {
        #region Public Properties
        /// <summary>
        /// функция ячейки
        /// </summary>
        public String func = "";
        /// <summary>
        /// код операции, в символьном формате. '&' - "И", 'V' - "ИЛИ", '!' - "НЕ"
        /// </summary>
        public String op;
        /// <summary>
        /// первая ячейка потомок
        /// </summary>
        public SLGate lgate1;
        /// <summary>
        /// вторая ячейка потомок
        /// </summary>
        public SLGate lgate2;
        /// <summary>
        /// указатель на родительский узел (тот, которому возвращаем полученное значение)
        /// </summary>
        public SLGate parent_lgate;
        /// <summary>
        /// количество потомков, 
        /// для бинарных операторов  =  2, 
        /// для унарных = 1 и используется только ссылка CHILD_1,
        /// для листьевых элементов = 0 
        /// Если child_count = -1, то это значит что узел вааще не принадлежит к нашему дереву (его тока создали)
        /// </summary>
        public int child_count = -1;
        /// <summary>
        /// Возможны три случая, если var_id = -1, то узел хранит константу(0 или 1)
        /// Если var_id = 0, то узел промежуточный(не листьевой)
        /// Если узел - листьевая переменная(входящая в varlist), то его var_id равен индексу его расположения в varlist'е + 1 (первая переменная имеет var_id = 1)
        /// </summary>
        public int var_ID = -1;
        /// <summary>
        /// значение узла при расчёте, которое он передаёт наверх
        /// </summary>
        public bool value;
        /// <summary>
        /// список переменных ячейки
        /// </summary>
        public List<variable> VarList = new List<variable>();
        /// <summary>
        /// используемые ячейки типа схемы
        /// </summary>
        //public List<SGate> usedSate = new List<SGate>();

        #endregion

        /// <summary>
        /// конструктор 1
        /// </summary>
        public SLGate()
        {
            this.parent_lgate = null;
            this.lgate1 = null;
            this.lgate2 = null;
            this.child_count = -1;
            this.op = "";
            //this.value = false; 
            this.func = "";
        }
        public void Setvar_ID(int var_ID)
        {
            this.var_ID = var_ID;
        }
        public void SetOp(char op)
        {
            this.op = op.ToString();
        }
        public void SetChildCount(int child_count)
        {
            this.child_count = child_count;
        }
        public void SetValue(bool value)
        {
            this.value = value;
        }
    }
    public class variable
    {
        /// <summary>
        /// ID переменной
        /// </summary>
        public int var_id { get; private set; }

        /// <summary>
        /// имя переменной
        /// </summary>
        public String name { get; private set; }

        /// <summary>
        /// значение переменной
        /// </summary>
        public bool value;

        /// <summary>
        /// Конструктор переменной
        /// </summary>
        /// <param name="ID">Идентификатор переменной</param>
        /// <param name="Name">Название переменной</param>
        public variable(int ID, string Name)
        {
            this.var_id = ID;
            this.name = Name;
        }
    }
}
