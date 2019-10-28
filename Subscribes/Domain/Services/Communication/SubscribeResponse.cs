using Subscribes.Domain.Models;

namespace Subscribes.Domain.Services.Communication
{
    public class SubscribeResponse : BaseResponse
    {
        public Subscribe Subscribe { get; private set; }

        private SubscribeResponse(bool success, string message, Subscribe subscribe) : base(success, message)
        {
            Subscribe = subscribe;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="subscribe">Saved subscribe.</param>
        /// <returns>Response.</returns>
        public SubscribeResponse(Subscribe subscribe) : this(true, string.Empty, subscribe)
        {
            
        }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public SubscribeResponse(string message) : this(false, message, null)
        {
            
        }
    }
}