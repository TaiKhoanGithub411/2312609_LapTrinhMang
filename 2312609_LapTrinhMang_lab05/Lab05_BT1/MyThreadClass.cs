using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Lab05_BT1
{
    public class MyThreadClass
    {
        //class giúp các thread chạy song song với nhau nhờ hàm runMythread
        private const int RANDOM_SLEEP_MAX = 1000;//thời gian ngủ của thread, từ 0 đến 1000 milliseconds (1 giây)  
        private const int LOOP_COUNT = 10;//số lần lặp lại hành động của thread
        private String greeting;//lưu chuỗi thông báo của thread
        public MyThreadClass(String greeting)
        {
            this.greeting = greeting;
        }
        public void runMyThread()
        {
            Random rand = new Random();
            for(int x=0;x<LOOP_COUNT; x++)
            {
                //In chuỗi thông báo và id của thread
                Console.WriteLine(greeting+"(Thread ID: "+Thread.CurrentThread.GetHashCode()+")");
                try
                {
                    //thread dừng với thời gian ngẫu nhiên --> mô phỏng tác vụ tốn thời gian chờ
                    Thread.Sleep(rand.Next(0, RANDOM_SLEEP_MAX));
                }
                catch (ThreadInterruptedException) { }
            }    
        }
    }
}
