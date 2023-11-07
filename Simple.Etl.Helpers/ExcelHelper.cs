﻿using Ganss.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Etl.Helpers
{
    public static class ExcelHelper
    {

        public static List<T> FromExcel<T>(string fn)
        {
            return new ExcelMapper(fn).Fetch<T>(fn).ToList();
        }


    }
}

/*
    
    - MSH <SEGMENT HEADER>
    - ^~\& <DELIMITER DEFINITIONS>
    - GOOD HEALTH HOSPITAL <SENDING FAC>
    - GHH LAB, INC. <RECEVING>
    - GOOD HEALTH HOSPITAL <RECEVING>
    - 198808181126 <MsgID>
    

    MSH|^~\&|ADT1|GOOD HEALTH HOSPITAL|GHH LAB, INC.|GOOD HEALTH HOSPITAL|198808181126|SECURITY|ADT^A01^ADT_A01|MSG00001|P|2.8||
    EVN|A01|200708181123||
    PID|1||PATID1234^5^M11^ADT1^MR^GOOD HEALTH HOSPITAL~123456789^^^USSSA^SS||EVERYMAN^ADAM^A^III||19610615|M||C|2222 HOME STREET^^GREENSBORO^NC^27401-1020|GL|(555) 555-2004|(555)555-2004||S||PATID12345001^2^M10^ADT1^AN^A|444333333|987654^NC|
    NK1|1|NUCLEAR^NELDA^W|SPO^SPOUSE||||NK^NEXT OF KIN
    PV1|1|I|2000^2012^01||||004777^ATTEND^AARON^A|||SUR||||ADM|A0|

 */