namespace TailCallTest
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            // structを使った末尾再帰が最適化されない問題？
            RecA(new AStruct(100000000)); // With RyuJIT, Stack Overflow Exception!

            // classなら普通に最適化効く
            RecB(new BClass(100000000));

            // App.config の <useLegacyJit enabled="0"/> を1に変えてRyuJITを無効にすると両方動く

            // コメントアウトしてためしてみてね
        }

        private static AStruct RecA(AStruct a)
        {
            //System.Console.WriteLine(new System.Diagnostics.StackTrace().FrameCount);
            return (a.Value <= 0) ? a : RecA(new AStruct(a.Value - 1));
        }

        private static BClass RecB(BClass b)
        {
            //System.Console.WriteLine(new System.Diagnostics.StackTrace().FrameCount);
            return (b.Value <= 0) ? b : RecB(new BClass(b.Value - 1));
        }
    }

    public struct AStruct
    {
        public int Value;

        public AStruct(int x)
        {
            this.Value = x;
        }
    }

    public class BClass
    {
        public int Value;

        public BClass(int x)
        {
            this.Value = x;
        }
    }
}
