namespace Inzynierka_API.Services
{
    public class AuthMessageSenderOptions
    {
        private string sendGridKey = "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";

        public string SendGridKey { get => sendGridKey; set => sendGridKey = value; }
    }
}
