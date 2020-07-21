namespace AcceptanceTests.TestAPI.Contract.Responses
{
    /// <summary>
    /// 
    /// </summary>
    public class HealthCheckResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public HealthCheckResponse()
        {
            Version = new ApplicationVersion();
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Successful { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ApplicationVersion Version { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public class ApplicationVersion
        {
            /// <summary>
            /// 
            /// </summary>
            public string Version { get; set; }
        }
    }
}
