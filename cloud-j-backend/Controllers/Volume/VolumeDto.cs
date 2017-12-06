namespace cloud_j_backend.Controllers.Volume
{
    public class VolumeDto
    {
        public VolumeDto(float volumeValue)
        {
            VolumeValue = volumeValue;
        }

        public float VolumeValue { get; set; }
    }
}