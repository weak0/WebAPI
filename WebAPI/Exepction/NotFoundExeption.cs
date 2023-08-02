namespace WebAPI.Exepction
{
    public class NotFoundExeption : Exception 
    {
        public NotFoundExeption(string massage ) : base(massage) 
        {
            
        }
    }
}
