namespace NightTech.Domain.Exceptions;

public class FieldRequiredException(string message) : Exception($"{message}");

