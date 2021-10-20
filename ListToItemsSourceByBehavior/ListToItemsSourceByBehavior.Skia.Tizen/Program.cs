using Tizen.Applications;
using Uno.UI.Runtime.Skia;

namespace ListToItemsSourceByBehavior.Skia.Tizen
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new TizenHost(() => new ListToItemsSourceByBehavior.App(), args);
            host.Run();
        }
    }
}
