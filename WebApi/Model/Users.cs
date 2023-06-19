using System;

namespace WebApi.Model
{
    public class Users
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public byte Type { get; set; }
        public DateTime Date{ get; set; }

    }
}
