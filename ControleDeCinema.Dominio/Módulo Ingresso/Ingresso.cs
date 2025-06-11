using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControleDeCinema.Domínio;

public class Ingresso : EntidadeBase
{
    public int Id { get; set; }
    public Assento Assento { get; set; }

}