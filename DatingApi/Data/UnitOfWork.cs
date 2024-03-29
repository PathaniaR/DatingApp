﻿using AutoMapper;
using DatingApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApi.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatingDataContext context;
        private readonly IMapper mapper;

        public UnitOfWork(DatingDataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public IUserRepository UserRepository => new UserRepository(context, mapper);

        public IMessageRepository MessageRepository => new MessageRepository(context, mapper);

        public ILikeRepository LikeRepository => new LikeRepository(context);

        public async Task<bool> Complete()
        {
            return await context.SaveChangesAsync() > 0;
        }
        public bool HasChanges()
        {
            return context.ChangeTracker.HasChanges();
        }
    }
}
