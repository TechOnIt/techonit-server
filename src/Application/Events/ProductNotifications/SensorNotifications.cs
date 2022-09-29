﻿using iot.Infrastructure.Common.Notifications.SmtpClientEmail;

namespace iot.Application.Events.ProductNotifications
{
    public class SensorNotifications : INotification
    {
    }

    public class SensorSmsNotifications : INotificationHandler<SensorNotifications>
    {
        public async Task Handle(SensorNotifications notification, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }

    public class SensorEmailNotifications : INotificationHandler<SensorNotifications>
    {
        #region constructor
        private readonly ISmtpEmailService emailService;
        public SensorEmailNotifications(ISmtpEmailService emailService)
        {
            this.emailService = emailService;
        }

        #endregion
        public async Task Handle(SensorNotifications notification, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }
}
