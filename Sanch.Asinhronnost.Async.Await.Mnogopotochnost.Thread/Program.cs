using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sanch.Asinhronnost.Async.Await.Mnogopotochnost.ThreadMy
{
    class Program
    {//конкуренция
     //параллельность
     //многопоточность
     //ассинхронность

        public static object locker = new object(); //блокировка

        public static int i1 = 100; //проблема с потоками которые зависят друг от друга
    
        static void M1()
        {
            for (int i = 0; i<=i1; i++)
            {

            }
            
        }

        static void M2()
        {
            for (int i = i1; i >= i1; i--)
            {

            }

        }
//////////////////////////////////////////////////////////////////////////////////

        static void Main(string[] args)
        {
            //многопоточность - работает меднно и долго и беспорядочно
            #region thread
            /*Thread thread = new Thread(new ThreadStart(DoWork)); // если DoWork с параметрами то ParameterizedThreadStart
            thread.Start(); // запуск

            Thread thread2 = new Thread(new ParameterizedThreadStart(DoWork2)); 
            thread2.Start(10); // запуск

           
            for (int i = 0; i < int.MaxValue; i++)
            {
                
                    Console.WriteLine("Main");
               
            }
            Console.ReadKey(); // три потока работают одновременно ассинхронно параллельно
            */
            #endregion

            #region async
            /*Console.WriteLine("Begin main");
            DoWorkAsync(100);//c параметром но можно и без
            Console.WriteLine("Continue main");

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Main"); // это будет выполняться паралельно с ассинхронным методом     
            }
            Console.WriteLine("End main");
            Console.ReadKey();
            */
            #endregion

            // задача на асинх
            //var result = SaveFile("my2.txt");
            //Console.WriteLine(result);
            //Console.ReadKey();

            var result2 = SaveFileAsync("my3.txt");
            var input = Console.ReadLine();
            Console.WriteLine(result2.Result);
            Console.ReadKey(); ////////////крутая штука когда нужно действовать одновременно с программой пока она считает и выдает результат



        }

        static bool SaveFile(string path)
        {
            lock (locker) // заблокировали участок кода
            {
                var rnd = new Random();
                var text = "";
                for (int i = 0; i < 50000; i++)
                {
                    text += rnd.Next();
                }
            }

            using (var sw = new StreamWriter(path, false, Encoding.UTF8))
            {
                sw.WriteLine();
            }




            return true;

        }


        //задача на асинх
        static async Task<bool> SaveFileAsync(string path)
        {
            var result = await Task<bool>.Run(() => SaveFile(path));
            return result;
        }




        //асинхронность
        ///////////////////////////////////////////////////////////
        static async Task DoWorkAsync(int max) // task - оболочка для асинхронности
        {
            Console.WriteLine("Begin Async");
            await Task.Run(() => DoWork(max)); //await - создает новый поток(await-ждет). Пока это команда не закончится, консоль ниже не выйдет
            Console.WriteLine("End Async");
        }

        /*static async Task DoWorkAsync() // task - оболочка для асинхронности
        {
            Console.WriteLine("Begin Async");
            await Task.Run(() => DoWork()); //await - создает новый поток(await-ждет). Пока это команда не закончится, консоль ниже не выйдет
            Console.WriteLine("End Async");
        }
         */

        static void DoWork(int max)
        {

            for (int i = 0; i < max; i++)


            {
                Console.WriteLine("DoWork");

            }

        }

        //Многопоточность
        ///////////////////////////////////////////////////////////

        /* static void DoWork()
         {

             for (int i = 0; i < 10; i++)


             {
                 Console.WriteLine("DoWork");

             }

         }*/

        static void DoWork2(object max)// передает только тип object
        {

            for (int i = 0; i < (int)max; i++)
            {

                Console.WriteLine("DoWork2");

            }
        }

    }
}
