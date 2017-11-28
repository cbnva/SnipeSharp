﻿using SnipeSharp.Common;
using SnipeSharp.Endpoints.Models;
using SnipeSharp.Endpoints.SearchFilters;

namespace SnipeSharp.Endpoints
{
    public interface IEndpointManager
    {
        IResponseCollection GetAll();
        IResponseCollection FindAll(ISearchFilter filter);
        ICommonEndpointModel FindOne(ISearchFilter filter);
        ICommonEndpointModel Get(int id);
        ICommonEndpointModel Get(string name);
        IRequestResponse Create(ICommonEndpointModel toCreate);
        IRequestResponse Update(ICommonEndpointModel toUpdate);
        IRequestResponse Delete(int id);
    }
}
