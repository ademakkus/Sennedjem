﻿using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.UserClaims.Queries
{
  public class GetUserClaimQuery : IRequest<IDataResult<UserClaim>>
  {
    public int Id { get; set; }
    public class GetUserClaimQueryHandler : IRequestHandler<GetUserClaimQuery, IDataResult<UserClaim>>
    {
      private readonly IUserClaimDal _userClaimDal;

      public GetUserClaimQueryHandler(IUserClaimDal userClaimDal)
      {
        _userClaimDal = userClaimDal;
      }

      public async Task<IDataResult<UserClaim>> Handle(GetUserClaimQuery request, CancellationToken cancellationToken)
      {
        return new SuccessDataResult<UserClaim>(await _userClaimDal.GetAsync(x => x.Id == request.Id));
      }
    }
  }
}
