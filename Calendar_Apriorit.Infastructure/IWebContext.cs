namespace Calendar_Apriorit.Infastructure
{
    public interface IWebContext
    {
        IRootContext RootContext { get; set; }
        IServiceProviderFactory Factory { get; }
    }
}