using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Boot_Park.Model.BootPark;

namespace Boot_Park.Controller.BootPark
{
    public class ParametrizacionCOD
    {
        ParticularOAD particular = new ParticularOAD();

        public DataTable consultarParticulares() {
            return particular.consultarParticulares();
        }

        public bool registrarParticulares() {
            return particular.registrarParticulares();
        }

        public bool registrarParticular() {
            return particular.registrarParticular();
        }
    }
}