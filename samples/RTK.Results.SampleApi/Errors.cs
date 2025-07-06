

using RTK.Results.Core;

using System.Net;

namespace RTK.Results.SampleApi;

public static class Errors
{
    /// <summary>
    /// Value is below zero
    /// </summary>
    public static Error Error_0001 => Error.Create(
        nameof(Error_0001), "Value is below zero", type: typeof(Errors).Namespace, statusCode: (int)HttpStatusCode.BadRequest);


    /// <summary>
    /// Value is greater than 10
    /// </summary>
    public static Error Error_0002 => Error.Create(
        nameof(Error_0002), "Value is greater than 10", type: typeof(Errors).Namespace, statusCode: (int)HttpStatusCode.BadRequest);

    /// <summary>
    /// Value is greater than 10
    /// </summary>
    public static Error Error_0003 => Error.Create(
        nameof(Error_0003), "Handling result is below zero", type: typeof(Errors).Namespace, statusCode: (int)HttpStatusCode.Conflict);
}
