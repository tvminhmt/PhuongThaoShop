namespace App.Shared.Core.Entities
{
    public enum RedirectTypes : byte
    {
        StatusCode404 = 1,
        StatusCode301 = 2,
        StatusCode302 = 3,
        StatusCode303 = 4
    }
}
