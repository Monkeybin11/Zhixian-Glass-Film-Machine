using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       Board
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProLaminator.Device
 * File      Name：       Board
 * Creating  Time：       5/20/2020 4:03:27 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProLaminator.Device
{
    public class Board
    {
        public Board(ProCommon.Communal.BoardProperty boardPro)
        {
            this.Property = boardPro;
            this.API = new ProDriver.API.BoardAPI(Property);
        }

        public ProCommon.Communal.BoardProperty Property { private set; get; }

        public ProDriver.API.BoardAPI API { private set; get; }
    }
}
