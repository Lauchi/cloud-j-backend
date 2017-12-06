namespace cloud_j_backend.Controllers.Volume
{
    public class VolumeDto
    {
        public VolumeDto(double volumeValue)
        {
            VolumeValue = volumeValue;
        }
        
        public double VolumeValue { get; set; }
    }
}