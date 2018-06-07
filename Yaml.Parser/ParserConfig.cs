namespace Xamarin.Yaml.Parser
{
    public class ParserConfig
    {
        public string Separator { get; set; } = ".";
        
        /// <summary>
        /// Optional: Throw an exception when keys are not found (recommended only for debugging).
        /// </summary>
        public bool ThrowWhenKeyNotFound { get; set; }
    }
}