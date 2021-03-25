namespace SocialNetworkSample.Services.Models
{
    public class Client
    {
        /// <remarks>Когда в каком-нибудь кейсе понадобится айдишка - раскоментруем</remarks>
        //public Guid Id { get; set; }

        public string Name { get; set; }

        public int SubscribersCount { get; set; }

        public override string ToString()
        {
            return Name ?? base.ToString();
        }
    }
}