using System.Threading.Tasks;

namespace Resource.Builder
{
    class Client
    {
        async Task run()
        {
            var builder = new Builder(null);
            var director = new Director(builder);

            var result = await director.Make();
        }
    }

}