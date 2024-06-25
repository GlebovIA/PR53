using System.Windows.Shapes;

namespace PR53.Classes
{
    public class PointInfo
    {
        public double value { get; set; }
        public Line line { get; set; }
        public PointInfo(double value)
        {
            this.value = value;
        }
    }
}
