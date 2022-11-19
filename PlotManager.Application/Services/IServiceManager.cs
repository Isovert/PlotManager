namespace PlotManager.Application.Services
{
    public interface IServiceManager
    {
        IFeatureService FeatureService { get; }
        IPlotComplexService PlotComplexService { get; }
        IPlotService PlotService { get; }
    }
}
