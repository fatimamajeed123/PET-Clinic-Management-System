namespace PetManagementSystem.Config
{
    public class AppConfig
    {
        private static AppConfig _instance;
        private static readonly object _lock = new object();

        public string ApplicationName { get; set; }
        public int MaxPetsAllowed { get; set; }

        // Private constructor to prevent direct instantiation
        private AppConfig()
        {
            // Initialize default values
            ApplicationName = "Pet Management System";
            MaxPetsAllowed = 3;
        }

        // Public method to provide access to the instance
        public static AppConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new AppConfig();
                        }
                    }
                }
                return _instance;
            }
        }
    }
}
    