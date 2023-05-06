using API.DTOs.UserDtos;
using Azure;
using Domain.Entity;
using ErrorOr;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API;

public static class ErrorOrExtension
{
    public static IResult MatchToHttpResponse<TDao>(this ErrorOr<TDao> errorOrResponse, 
        Func<TDao, IResult> onResponse,
        Func<Error, IResult> onError)
    {
        if (!errorOrResponse.IsError)
            return onResponse(errorOrResponse.Value);

        return errorOrResponse.Errors.Count > 1 ? 
            Results.BadRequest(errorOrResponse.Errors) : 
            onError(errorOrResponse.FirstError);
    }
}