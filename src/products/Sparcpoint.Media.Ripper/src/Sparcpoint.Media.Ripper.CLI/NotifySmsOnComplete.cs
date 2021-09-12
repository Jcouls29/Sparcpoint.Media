using Sparcpoint.Notification;
using System;
using System.Threading.Tasks;

namespace Sparcpoint.Media.Ripper.CLI
{
    public class NotifySmsOnComplete
    {
        private readonly ISmsNotification _Notification;
        private readonly PhoneNumber _Phone;
        private readonly SmsProvider _Provider;

        public NotifySmsOnComplete(ISmsNotification notification, PhoneNumber phone, SmsProvider provider)
        {
            _Notification = notification ?? throw new ArgumentNullException(nameof(notification));
            _Phone = phone;
            _Provider = provider;
        }

        public async Task PerformAsync(DiscTitleRecord record)
        {
            await _Notification.SendAsync(_Phone, _Provider, $"Title Ripped! {record}");
        }
    }
}
