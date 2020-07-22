using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       MotionDefinition
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProCommon.Communal
 * File      Name：       MotionDefinition
 * Creating  Time：       4/21/2020 1:18:35 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProCommon.Communal
{
    #region 运动控制

    /// <summary>
    /// 移动方向
    /// </summary>
    public enum MoveDirection : int
    {
        BackWard = -1,
        StandBy = 0,
        ForWard = 1
    }

    /// <summary>
    /// 元器件逻辑电平
    /// </summary>
    public enum ElectricalLevel : int
    {
        Low = 0,
        High = 1
    }

    /// <summary>
    /// 元器件有效信号
    /// [注:元器件逻辑电平或逻辑电平变化边沿产生有效的触发信号]
    /// </summary>
    public enum EffectiveSignal : int
    {
        LogicFalse = 0,
        LogicTrue = 1,
        FallEdge = 2,
        RaiseEdge = 3
    }

    /// <summary>
    /// 轴动限位模式
    /// </summary>
    public enum LimitMode : int
    {
        None = 0,
        SoftLimit = 1,
        HardLimit = 2,
        BothLimit = 3
    }

    #region 输入控制变量对象

    [Serializable]
    public class InVarObj : System.ComponentModel.INotifyPropertyChanged
    {
        public virtual event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// 构造函数
        /// [注:备用初始化内部变量]
        /// </summary>
        private InVarObj() { }

        public InVarObj(string invarID) : this()
        {
            ID = invarID;
        }

        public string ID
        {
           set; get;
        }

        public string Name
        {
            set;
            get;
        }

        /// <summary>
        /// 站号
        /// [Plc时会用]
        /// </summary>
        public string StationNumber { get; set; }

        public string VarType
        {
            get { return "IN"; }
        }

        /// <summary>
        /// 属性:控制变量的地址
        /// </summary>
        public int Address
        {
            set;
            get;
        }

        /// <summary>
        /// 有效逻辑电平
        /// [注:元器件的有效逻辑电平
        /// 一般NPN类型元器件,有效(即导通)电平为低电平;
        /// PNP类型元器件,有效(即断开)电平为高电平]
        /// </summary>
        public ProCommon.Communal.ElectricalLevel EffectiveLevel
        {
            set; get;
        }

        /// <summary>
        /// 触发信号
        /// </summary>
        public ProCommon.Communal.EffectiveSignal TriggerLogic
        {
            set; get;
        }

        public string Description
        {
            set;
            get;
        }

        private object _newValue;
        [System.ComponentModel.Bindable(true)]
        public object NewValue
        {
            get { return _newValue; }
            set
            {
                if (value != null)//避免与控件绑定时的空异常
                {
                    if (value.GetType() == typeof(bool))
                    {
                        //不能直接赋值，否则一直触发属性值事件(虽然未改变)
                        if (_newValue == null || (bool)_newValue != (bool)value)
                        {
                            LastValue = _newValue == null ? false : _newValue;
                            _newValue = value;
                            NotifyPropertyChanged("InNewValue");  //最新值更新
                        }
                    }
                    else if (value.GetType() == typeof(float))
                    {
                        //不能直接赋值，否则一直触发属性值事件(虽然未改变)
                        if (_newValue == null || (float)_newValue != (float)value)
                        {
                            LastValue = _newValue == null ? (float)0 : _newValue;
                            _newValue = value;
                            NotifyPropertyChanged("InNewValue");  //最新值更新
                        }
                    }
                    else
                    {
                        //不能直接赋值，否则一直触发属性值事件(虽然未改变)
                        if (_newValue == null || _newValue != value)
                        {
                            LastValue = _newValue;
                            _newValue = value;
                            NotifyPropertyChanged("InNewValue");   //最新值更新
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 属性:上次值
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public object LastValue
        {
            get;
            set;
        }

        [System.Xml.Serialization.XmlIgnore]
        private object _EditValue;

        /// <summary>
        /// 属性:编辑值(控制变量对象绑定控件的编辑值)
        /// </summary>
        public object EditValue
        {
            get
            {
                return _EditValue;
            }
            set
            {
                _EditValue = value;
                NotifyPropertyChanged("InEditValue"); //编辑值更新
            }
        }

        /// <summary>
        /// 控制变量对象复制
        /// </summary>
        /// <returns></returns>
        public InVarObj Clone()
        {
            InVarObj ctrlVarObj = new InVarObj(this.ID);
            return ctrlVarObj;
        }
    }

    [Serializable]
    public class InVarObjList : System.Collections.ICollection
    {
        private System.Collections.SortedList _list;

        public InVarObjList() { _list = new System.Collections.SortedList(); }

        public void Add(InVarObj inVarObj)
        {
            if (!_list.ContainsKey(inVarObj.ID))
            {
                _list.Add(inVarObj.ID, inVarObj);
            }
        }
        public void Delete(InVarObj inVarObj)
        {
            if (!_list.ContainsKey(inVarObj.ID))
            {
                _list.Remove(inVarObj.ID);
            }
        }
        public void Clear()
        {
            if (_list != null)
                _list.Clear();
        }

        public InVarObj this[int indx]
        {
            get
            {
                InVarObj ctrlVarObj = null;
                if (_list.Count > 0 && indx < _list.Count)
                {
                    ctrlVarObj = (InVarObj)_list.GetByIndex(indx);
                }
                return ctrlVarObj;
            }
        }

        public InVarObj this[string invarID]
        {
            get
            {
                InVarObj ctrlVarObj = null;
                if (_list.ContainsKey(invarID))
                {
                    ctrlVarObj = (InVarObj)_list[invarID];
                }
                return ctrlVarObj;
            }
        }

        public void CopyTo(Array arr, int indx)
        {
            _list.CopyTo(arr, indx);
        }

        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public object SyncRoot
        {
            get { return this; }

        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
    #endregion

    #region 输出变量对象
    [Serializable]
    public class OutVarObj : System.ComponentModel.INotifyPropertyChanged
    {
        public virtual event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// 构造函数
        /// [注:备用初始化内部变量]
        /// </summary>
        private OutVarObj() { }

        public OutVarObj(string outvarID) : this()
        {
            ID = outvarID;
        }

        public string ID
        {
            set; get;
        }

        public string Name
        {
            set;
            get;
        }

        public string VarType
        {
            get { return "OUT"; }
        }

        public int Address { get; set; }

        /// <summary>
        /// 有效逻辑电平
        /// [注:元器件的有效逻辑电平
        /// 一般NPN类型元器件,有效电平为低电平(即导通ON);
        /// PNP类型元器件,有效电平为高电平(即导通ON)]
        /// </summary>
        public ProCommon.Communal.ElectricalLevel EffectiveLevel
        {
            set; get;
        }

        /// <summary>
        /// 触发信号
        /// </summary>
        public ProCommon.Communal.EffectiveSignal TriggerLogic { set; get; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            set;
            get;
        }

        private object _newValue;
        /// <summary>
        /// 属性:更新值(控制变量所在实体本身值)
        /// </summary>
        [System.ComponentModel.Bindable(true)]
        public object NewValue
        {
            get { return _newValue; }
            set
            {
                if (value != null) //避免与控件绑定时的空异常
                {
                    if (value.GetType() == typeof(bool))
                    {
                        //不能直接赋值，否则一直触发属性值事件(虽然未改变)
                        if (this._newValue == null || (bool)this._newValue != (bool)value)
                        {
                            LastValue = _newValue == null ? false : this._newValue;
                            _newValue = value;
                            NotifyPropertyChanged("OutNewValue");  //最新值更新
                        }
                    }
                    else if (value.GetType() == typeof(float))
                    {
                        //不能直接赋值，否则一直触发属性值事件(虽然未改变)
                        if (_newValue == null || (float)_newValue != (float)value)
                        {
                            LastValue = _newValue == null ? (float)0 : _newValue;
                            _newValue = value;
                            NotifyPropertyChanged("OutNewValue");  //最新值更新
                        }
                    }
                    else
                    {
                        //不能直接赋值，否则一直触发属性值事件(虽然未改变)
                        if (_newValue == null || _newValue != value)
                        {
                            LastValue = _newValue;
                            _newValue = value;
                            NotifyPropertyChanged("OutNewValue");   //最新值更新
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 属性:上次值
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public object LastValue
        {
            get;
            set;
        }

        [System.Xml.Serialization.XmlIgnore]
        private object _EditValue;

        public object EditValue
        {
            get
            {
                return _EditValue;
            }
            set
            {
                _EditValue = value;
                NotifyPropertyChanged("OutEditValue"); //编辑值更新
            }
        }

        public OutVarObj Clone()
        {
            OutVarObj ctrlVarObj = new OutVarObj(this.ID);
            return ctrlVarObj;
        }
    }

    [Serializable]
    public class OutVarObjList : System.Collections.ICollection
    {
        private System.Collections.SortedList _list;

        public OutVarObjList() { _list = new System.Collections.SortedList(); }

        /// <summary>
        /// 方法：增加控制变量实体
        /// </summary>
        /// <param name="outVarObj"></param>
        public void Add(OutVarObj outVarObj)
        {
            if (!_list.ContainsKey(outVarObj.ID))
            {
                _list.Add(outVarObj.ID, outVarObj);
            }
        }

        /// <summary>
        /// 方法：删除指定控制变量实体
        /// </summary>
        /// <param name="outVarObj"></param>
        public void Delete(OutVarObj outVarObj)
        {
            if (!_list.ContainsKey(outVarObj.ID))
            {
                _list.Remove(outVarObj.ID);
            }
        }

        public void Clear()
        {
            if (_list != null)
                _list.Clear();
        }

        /// <summary>
        /// 索引器：返回控制变量对象列表中的实体
        /// </summary>
        /// <param name="indx"></param>
        /// <returns></returns>
        public OutVarObj this[int indx]
        {
            get
            {
                OutVarObj ctrlVarObj = null;
                if (_list.Count > 0 && indx < _list.Count)
                {
                    ctrlVarObj = (OutVarObj)_list.GetByIndex(indx);
                }
                return ctrlVarObj;
            }
        }

        public OutVarObj this[string outvarID]
        {
            get
            {
                OutVarObj ctrlVarObj = null;
                if (_list.ContainsKey(outvarID))
                {
                    ctrlVarObj = (OutVarObj)_list[outvarID];
                }
                return ctrlVarObj;
            }
        }

        public void CopyTo(Array arr, int indx)
        {
            _list.CopyTo(arr, indx);
        }

        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public object SyncRoot
        {
            get { return this; }

        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
    #endregion

    #region 轴对象
    [Serializable]
    public class Axis
    {
        public virtual event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(name));
            }
        }

        /// <summary>
        /// 构造函数
        /// [注:备用初始化内部变量]
        /// </summary>
        private Axis()
        {
        }

        /// <summary>
        /// 创建轴对象
        /// </summary>
        /// <param name="axisID"></param>
        public Axis(string axisID) : this()
        {
            ID = axisID;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ID
        {
           set; get;
        }

        public int Number { set; get; }

        public string Name { set; get; }

        /// <summary>
        /// 轴类型
        /// </summary>
        public int AxisType { set; get; }

        public int PulseOutMode { set; get; }

        /// <summary>
        /// 轴伺服使能对应控制器输出端口号
        /// </summary>
        public int ServoOnOutBitNumber { set; get; }

        /// <summary>
        /// 轴伺服使能对应控制器输出端口有效电平
        /// </summary>
        /// 
        public ProCommon.Communal.ElectricalLevel ServoOnLevel { set; get; }


        /// <summary>
        /// 轴报警对应控制器输入端口号
        /// </summary>
        public int AlarmInBitNumber { set; get; }

        /// <summary>
        /// 轴报警对应输入端口有效电平
        /// </summary>
        public ProCommon.Communal.ElectricalLevel AlarmInLevel { set; get; }

        /// <summary>
        /// 报警清除对应控制器输出端口号
        /// </summary>
        public int AlarmClearOutBitNumber { set; get; }

        /// <summary>
        /// 报警清除对应控制器输出端口有效电平
        /// </summary>
        public ProCommon.Communal.ElectricalLevel AlarmClearOutLevel { set; get; }

        /// <summary>
        /// 轴报警状态
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public bool StatusALM { set; get; }

        /// <summary>
        /// 限位模式
        /// </summary>
        public ProCommon.Communal.LimitMode AxisLimitMode { set; get; }

        /// <summary>
        /// 轴负限位对应控制器输入端口号
        /// </summary>
        public int HardReverseInBitNumber { set; get; }

        /// <summary>
        /// 轴负限位对应控制器输入端口有效电平
        /// </summary>
        public ProCommon.Communal.ElectricalLevel HardReverseInLevel { set; get; }

        /// <summary>
        /// 轴负限位状态
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public bool StatusOfHardReverse { set; get; }

        /// <summary>
        /// 轴原点输入端口号
        /// </summary>
        public int DatumInBitNumber { set; get; }

        /// <summary>
        /// 轴原点输入端口有效电平
        /// </summary>
        public ProCommon.Communal.ElectricalLevel DatumInLevel { set; get; }

        /// <summary>
        /// 轴原点限位状态
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public bool StatusOfHardDatum { set; get; }

        /// <summary>
        /// 轴正限位输入端口号
        /// </summary>
        public int HardForwarInBitNumber { set; get; }

        /// <summary>
        /// 轴正限位输入端口有效电平
        /// </summary>
        public ProCommon.Communal.ElectricalLevel HardForwardInLevel { set; get; }

        /// <summary>
        /// 轴正限位状态
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public bool StatusOfHardForward { set; get; }

        /// <summary>
        /// 导程
        /// [注:螺距]
        /// </summary>
        public float HelicalPitch { set; get; }

        /// <summary>
        /// 每转脉冲数
        /// </summary>
        public float PulsePerRound { set; get; }

        /// <summary>
        /// 属性：脉冲当量
        /// [注:一般定义:每转脉冲数/导程,即每转一个用户单位所需的脉冲数]
        /// </summary>
        public float PulseUnit { set; get; }

        /// <summary>
        /// 回零方向
        /// </summary>
        private ProCommon.Communal.MoveDirection _datumDir;
        public string DatumDirection
        {
            set
            {
                if (value == "负向"
                    || value == "NEGATIVE")
                    this._datumDir = ProCommon.Communal.MoveDirection.BackWard;
                else if (value == "正向"
                    || value == "POSITIVE")
                    this._datumDir = ProCommon.Communal.MoveDirection.ForWard;

            }
            get
            {
                if (this._datumDir == ProCommon.Communal.MoveDirection.BackWard)
                    return "负向";
                else
                    return "正向";
            }
        }

        /// <summary>
        /// 属性：负向软限位
        /// </summary>
        public float SoftReverseLimit { set; get; }

        /// <summary>
        /// 属性：正向软限位
        /// </summary>
        public float SoftForwardLimit { set; get; }

        /// <summary>
        /// 属性：初始速度
        /// </summary>
        public float StartSpeed { set; get; }

        /// <summary>
        /// 属性：运行速度
        /// </summary>
        public float RunSpeed { set; get; }

        /// <summary>
        /// 属性:末速度
        /// </summary>
        public float EndSpeed { set; get; }

        /// <summary>
        /// 属性：加速度
        /// </summary>
        public float Accel { set; get; }

        /// <summary>
        /// 属性：减速度
        /// </summary>
        public float Decel { set; get; }

        /// <summary>
        /// 属性：S曲线速度
        /// </summary>
        public float Sramp { set; get; }

        /// <summary>
        /// 属性：回零爬行速度
        /// [注:二次回零时的反向速度]
        /// </summary>
        public float DatumCreepSpeed { set; get; }

        /// <summary>
        /// 属性:回零速度
        /// </summary>
        public float DatumSpeed { set; get; }

        /// <summary>
        /// 属性:回零偏移
        /// </summary>
        public float DatumOffset { set; get; }

        /// <summary>
        /// 属性：回零模式
        /// </summary>
        public int DatumMode { set; get; }

        /// <summary>
        /// 属性：轴位置1
        /// [注:用于设置轴的待机位1或安全位1]
        /// </summary>
        public float FirstPos { set; get; }

        /// <summary>
        /// 属性：位置2
        /// [注:用于设置轴的待机位2或安全位2]
        /// </summary>
        public float SecondPos { set; get; }

        /// <summary>
        /// 属性：位置3
        /// [注:用于设置轴的待机位3或安全位3]
        /// </summary>
        public float ThirdPos { set; get; }

        private float _currentPos;
        /// <summary>
        /// 属性：轴当前位置
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public float CurrentPos
        {
            set
            {
                //不能直接赋值，否则一直触发属性值事件(虽然未改变)
                if (value != _currentPos)
                {
                    _currentPos = value;
                    NotifyPropertyChanged("AxisCurrentPos");  //最新值更新
                }
            }
            get
            {
                return _currentPos;
            }
        }

        public ProCommon.Communal.Axis Clone()
        {
            ProCommon.Communal.Axis destAxis = new ProCommon.Communal.Axis(this.ID);
            destAxis.Accel = this.Accel;
            destAxis.AlarmClearOutBitNumber = this.AlarmClearOutBitNumber;
            destAxis.AlarmClearOutLevel = this.AlarmClearOutLevel;
            destAxis.AlarmInBitNumber = this.AlarmInBitNumber;
            destAxis.AlarmInLevel = this.AlarmInLevel;
            destAxis.DatumCreepSpeed = this.DatumCreepSpeed;
            destAxis.CurrentPos = this.CurrentPos;
            destAxis.DatumDirection = this.DatumDirection;
            destAxis.DatumInBitNumber = this.DatumInBitNumber;
            destAxis.DatumInLevel = this.DatumInLevel;
            destAxis.DatumMode = this.DatumMode;
            destAxis.Decel = this.Decel;
            destAxis.FirstPos = this.FirstPos;
            destAxis.HardForwarInBitNumber = this.HardForwarInBitNumber;
            destAxis.HardForwardInLevel = this.HardForwardInLevel;
            destAxis.HardReverseInBitNumber = this.HardReverseInBitNumber;
            destAxis.HardReverseInLevel = this.HardReverseInLevel;
            destAxis.SoftForwardLimit = this.SoftForwardLimit;
            destAxis.SoftReverseLimit = this.SoftReverseLimit;
            destAxis.StartSpeed = this.StartSpeed;
            destAxis.Name = this.Name;
            destAxis.Number = this.Number;
            destAxis.PulseOutMode = this.PulseOutMode;
            destAxis.PulseUnit = this.PulseUnit;
            destAxis.SecondPos = this.SecondPos;
            destAxis.ServoOnLevel = this.ServoOnLevel;
            destAxis.ServoOnOutBitNumber = this.ServoOnOutBitNumber;
            destAxis.RunSpeed = this.RunSpeed;
            destAxis.Sramp = this.Sramp;
            destAxis.StatusALM = this.StatusALM;
            destAxis.StatusOfHardDatum = this.StatusOfHardDatum;
            destAxis.StatusOfHardForward = this.StatusOfHardForward;
            destAxis.StatusOfHardReverse = this.StatusOfHardReverse;
            destAxis.ThirdPos = this.ThirdPos;
            destAxis.AxisType = this.AxisType;
            destAxis.HelicalPitch = this.HelicalPitch;
            destAxis.PulsePerRound = this.PulsePerRound;
            destAxis.DatumSpeed = this.DatumSpeed;
            destAxis.DatumOffset = this.DatumOffset;
            destAxis.AxisLimitMode = this.AxisLimitMode;
            return destAxis;
        }
    }

    [Serializable]
    public class AxisList : System.Collections.ICollection
    {
        private System.Collections.SortedList _list;

        public AxisList() { _list = new System.Collections.SortedList(); }

        public void Add(Axis axis)
        {
            if (!_list.ContainsKey(axis.ID))
            {
                _list.Add(axis.ID, axis);
            }
        }

        public void Delete(Axis axis)
        {
            if (_list.ContainsKey(axis.ID))
            {
                _list.Remove(axis.ID);
            }
        }

        public void Clear()
        {
            if (_list != null)
                _list.Clear();
        }

        public Axis this[int index]
        {
            get
            {
                Axis axis = null;
                if (_list.Count > 0 && index < _list.Count)
                {
                    axis = (Axis)_list.GetByIndex(index);
                }
                return axis;
            }
        }

        public Axis this[string axisID]
        {
            get
            {
                Axis axis = null;
                if (_list.ContainsKey(axisID))
                {
                    axis = (Axis)_list[axisID];
                }
                return axis;
            }
        }

        public void CopyTo(Array arr, int indx)
        {
            _list.CopyTo(arr, indx);
        }

        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public object SyncRoot
        {
            get { return this; }

        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }

    #endregion

    #endregion
}
