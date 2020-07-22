using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLaminator.Data
{
    /// <summary>
    /// 日志类
    /// </summary>
    public class Log
    {
        public Log(string serialNo,string item,string description)
        {
            this.SerialNo = serialNo;
            this.Item = item;
            this.Description = description;
            this.DoTime = System.DateTime.Now;
        }

        public virtual int LogID { set; get; }

        public virtual string StationName { set; get; }

        public virtual System.DateTime DoTime { set; get; }

        public virtual string SerialNo { set; get; }

        public virtual string Item { set; get; }

        public virtual string Description { set; get; }
    }
}
