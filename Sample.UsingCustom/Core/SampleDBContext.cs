using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.UsingCustom
{
    public class SampleDBContext
    {
        public List<OriginEntity> OriginEntityList { get; private set; } = new List<OriginEntity>();

        public SampleDBContext()
        {
            OriginEntityList.Add(new OriginEntity
            {
                Id = 1,
                Origin = "http://localhost:1234",
                Enabled = true,
                Method = "GET"
            });

            OriginEntityList.Add(new OriginEntity
            {
                Id = 1,
                Origin = "http://localhost:1236",
                Enabled = false,
                Method = "GET"
            });
        }
    }

    public class OriginEntity
    {
        public int Id { get; set; }

        public string Origin { get; set; }

        public bool Enabled { get; set; }

        public string Method { get; set; }
    }
}
