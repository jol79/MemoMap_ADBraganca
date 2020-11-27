﻿using MemoMap.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MemoMap.Domain
{
    public interface IUnitOfWork
    {
        IGroupRepository GroupRepository { get; }
        ILocationRepository LocationRepository { get; }
        IMapRepository MapRepository { get; }
        IRouteRepository RouteRepository { get; }
        IUserRepository UserRepository { get; }
    }
}