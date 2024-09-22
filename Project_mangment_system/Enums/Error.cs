namespace Project_management_system.Enums
{
	public record Error(string Description, int? StatusCode)
	{
		public static readonly Error None = new(string.Empty, null);
	}
}
