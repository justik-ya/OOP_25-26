using System;
using System.Threading;

namespace Task3
{
    /*
     * В данной работе вы должны самостоятельно изучить работу примитивов синхронизации
     * ManualResetEvent и AutoResetEvent и с их помощью реализовать синхронизацию
     * потоков так, чтобы треды выполнились по порядку. Т.е. чтобы на консоли
     * появились три строчки в порядке:
     *   Thread 1
     *   Thread 2
     *   Thread 3
     * Изменять можно только содержимое трех функций Thread1Main, Thread2Main и Thread3Main
     */
    public class Task5
    {
        // В этой части класса можно резвиться как хотите :)

        private AutoResetEvent evt1 = new AutoResetEvent(false);
        private AutoResetEvent evt2  = new AutoResetEvent(false);

        private void Thread1Main()
        {
            PrintThreadNumber( 1 );
            evt1.Set();
        }

        private void Thread2Main()
        {
            evt1.WaitOne();
            PrintThreadNumber( 2 );
            evt2.Set();
        }

        private void Thread3Main()
        {
            evt2.WaitOne();
            PrintThreadNumber( 3 );
        }

        #region Не изменяйте код внутри этого региона, это часть тестов

        private readonly Object s_consoleLock = new();

        private void PrintThreadNumber( Int32 number )
        {
            lock( s_consoleLock )
                Console.WriteLine( $"Thread {number}" );
        }

        public void Start( Int32[] threadStartOrder, Int32 msBeforeNextStart )
        {
            Thread[] threads =
            {
                new(Thread1Main),
                new(Thread2Main),
                new(Thread3Main)
            };

            for( Int32 i = 0; i < 3; ++i )
            {
                threads[threadStartOrder[i]].Start();
                if( msBeforeNextStart > 0 )
                    Thread.Sleep( msBeforeNextStart );
            }

            for( Int32 i = 0; i < 3; ++i )
                threads[i].Join();
        }

        #endregion
    }
}
