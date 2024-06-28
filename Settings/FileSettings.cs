namespace movies_ecommerce.Settings
{
    public class FileSettings
    {
        public const string ImagePath ="/assets/Images";
        public const string ImagePathActor = $"{ImagePath}/Actors";
        public const string ImagePathCinema = $"{ImagePath}/Cinemas";
        public const string ImagePathProducer = $"{ImagePath}/Producers";
        public const string ImagePathMovies = $"{ImagePath}/Movies";
        public const string AllowedExtentions = ".png,.jpg,.jpeg";
        public const int MaxSizeInMB = 3;
        public const int MaxSizeinByte = MaxSizeInMB * 1024 * 1024;
    }
}
