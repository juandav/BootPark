# ToolBarad

## ¿Como usar ToolBarad C#?

```cs
using System;
using TolBarad;

namespace Test
{
    public class Test
    {
      TolBarad _TB = new TolBarad("192.168.1.250","27011");

      public void RFID () {
        string data = _TB.RunRFID();

        string[] etiqueta = data.Split(',');

        string tag = etiqueta[0];
        string antena = etiqueta[1];
      }

    }
}
```
