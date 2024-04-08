using static System.Console;
using System.ComponentModel.DataAnnotations;

namespace mockOSApi.Models {
    interface IThread {
    int GetTid();
    int Run();
    
};

    public class Thread : OSObject, IThread {

        private static int _threadcount;


        private Stack<object> _stack;
        private int _tid;

        private Stack<object> Stack {
            get => _stack;
            set => _stack = value;
        }

        [Key]
       public int Tid {
            get => _tid;
            set => _tid = value;
        }

        public int threadCount {
            get => _threadcount;
            set => _threadcount = value;
        }
        public Thread() {
            Tid = ++threadCount;
            WriteLine("created thread");
            InitStack();

        }

        public int GetTid() {
            return 1;
        }
        public int Run() {

            return 1;
        }

        private int TerminateThread() {
            WriteLine("terminated thread {0}",this.Tid);
            return 1;
        }

        
        private void InitStack() {
            Stack = new Stack<object>();
            WriteLine("initiated stack");

        }

    }
}