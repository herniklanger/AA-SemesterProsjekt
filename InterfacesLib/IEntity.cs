namespace InterfacesLib
{
	public interface IEntity<TId>
	{
		public TId Id { get; set; }
	}
}