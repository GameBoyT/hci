namespace Model
{
    public class Medicine
    {
        public int Id { get; set; }
        public string Name { get; set; }



        public Medicine(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        
        
      
    
    }
}