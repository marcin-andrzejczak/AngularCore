﻿using MassTransit;
using AngularCore.Microservices.Services.Events;
using System.Linq;
using System.Threading.Tasks;
using SearchService.Data;
using Microsoft.EntityFrameworkCore;

namespace SearchService.Messaging
{
    public class UserUpdatedEventConsumer : IConsumer<UserUpdatedEvent>
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<User> _users;

        public UserUpdatedEventConsumer(ApplicationContext context)
        {
            _users = context.Set<User>();
            _context = context;
        }

        public async Task Consume(ConsumeContext<UserUpdatedEvent> eventContext)
        {
            var user = _users.Where(u => u.Id == eventContext.Message.UserId).FirstOrDefault();
            if (user != null)
            {
                user.Name = eventContext.Message.FirstName;
                user.Surname = eventContext.Message.LastName;

                _users.Update(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
