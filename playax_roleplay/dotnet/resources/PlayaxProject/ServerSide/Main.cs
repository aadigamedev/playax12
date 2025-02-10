using GTANetworkAPI;

namespace ServerSide
{
    public class Main : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {
            NAPI.Util.ConsoleOutput("server started");

        }
     }

}

