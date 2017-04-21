using System;
using System.Collections.Generic;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
           var val=  DBNull.Value;
            int result = 0;
            var v = int.TryParse(val.ToString(), out   result);
            List<object> intList = new List<object>()
            {
                1,val,2,val,null
            };
            for (int i = 0; i < intList.Count; i++)
            {
               bool re= int.TryParse((intList[i] ?? 0).ToString(), out result);
                Console.WriteLine(result);
                //string bojlist = (intList[i] ?? 0).ToString();
                //Console.WriteLine(Convert.ToInt32(bojlist));
                // Console.WriteLine(list[i] ?? "暂无");
            }
            List<object> list = new List<object>()
            {
                "张三",null,"王五",val
            };
            string str = "好";
            str.JsonStr();
           
            for (int i = 0; i < list.Count; i++)
            {
                string bojlist = (list[i] ?? "为null").ToString(); ;
                Console.WriteLine(bojlist);
                // Console.WriteLine(list[i] ?? "暂无");
            }
           
            Console.WriteLine(1);
            Console.ReadLine();
        }
    }
    public static class Helper
    {
        public static void JsonStr(this object obj)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(obj));
        }
    }
}
