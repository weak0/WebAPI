namespace WebAPI
{
    public interface IWeatrherForcastSerivces
    {
        IEnumerable<WeatherForecast> Get(int a, int b, int c);
    }
}