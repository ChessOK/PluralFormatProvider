namespace SampleApp
{
    using System;
    using System.Diagnostics;
    using System.Globalization;

    using Common.I18n;

    class Program
    {
        static void Main(string[] args)
        {
            var s = new Stopwatch();
            var culture = CultureInfo.GetCultureInfo("en-US");
            s.Start();

            for (int i = 0; i < 1000000; i++)
            {
                Plural.Format(culture, "{0:#cat;cats}", i);
                //str = String.Format("{0}", i);
            }

            s.Stop();

            Console.WriteLine("Elapsed: " + s.Elapsed);
            Console.ReadKey();
        }
    }
}
