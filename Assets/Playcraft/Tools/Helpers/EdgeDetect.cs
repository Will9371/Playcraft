// Required ticks functionality not verified
namespace Playcraft.Optimized
{
    public class EdgeDetect
    {
        public bool changeHigh;
        public bool changeLow;
        
        public EdgeDetect(int ticksRequired = 1)
        {
            this.ticksRequired = ticksRequired;
        }
            
        bool prior;
        int ticks;
        int ticksRequired;

        public void Update(bool current)
        {
            ticks = current == prior ? 0 : ticks + 1;

            if (ticks >= ticksRequired)
            {
                changeHigh = current;
                changeLow = !current;
                prior = current;
            }
            else
            {
                changeHigh = false;
                changeLow = false;
            }
        }
    }        
}

